using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using OfficeIMO.Excel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LocalizationStorage.XLS {
    public static class XLSHelper {
        internal static void DoXLSImport(List<AdvTranslation> translation, string filePath) {
            var results = new List<string>();
            var imageCounter = new ImageCounter();
            var data = translation
                .Where(row => row.Status == TranslationStatus.Problems || row.Status == TranslationStatus.NotSure)
                .OrderBy(row => GroupsHelper.GetTeamByName(row.Path).ToString()).ToList();
            var list = data.Select(r => new string[] {
                $"{false}",
                GroupsHelper.GetTeamByName(r.Path).ToString(),
                GetCommentByStatus(r.Status, r.Comment),
                " ",
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                $"{false}",
                imageCounter.AddImage(GroupsHelper.GetProductByPath(r.Path, true)),
                r.English,
                r.Path,
                r.Key
            }).ToList();
            string[] headers = {
                "Done", "Team", "Request", "Solution",
                "Part of Speech", "Gender", "Number", "Case", "Degree",
                "Is the image uploaded to git?",
                "Image Name", "English", "Path", "Key"
            };
            XLSHelper.WriteXLS(filePath, headers, list);
        }
        static string GetCommentByStatus(TranslationStatus status, string comment) {
            if(!string.IsNullOrWhiteSpace(comment))
                return comment;
            return status switch {
                TranslationStatus.NotSure => "Context",
                _ => "???",
            };
        }
        public static void WriteXLS(string path, string[] headers, List<string[]> data, string sheetName = null) {
            using(var excelDocument = ExcelDocument.Create(path)) {
                var sheet = excelDocument.AddWorkSheet(sheetName ?? "Localization Data");

                for(int i = 0; i < headers.Length; i++) {
                    sheet.Cell(1, i + 1, headers[i]);
                    sheet.CellBold(1, i + 1, true);
                    sheet.CellBackground(1, i + 1, SixLabors.ImageSharp.Color.Yellow);
                }

                for(int row = 0; row < data.Count; row++) {
                    var dataRow = data[row];
                    for(int col = 0; col < dataRow.Length; col++)
                        sheet.Cell(row + 2, col + 1, GetFormatValue(EscapeField(dataRow[col])));
                }

                SetCustomValidationList(sheet, data.Count + 1);
                sheet.Freeze(1, 1);
                excelDocument.Save();
            }
        }
        static string EscapeField(string field) {
            if(string.IsNullOrEmpty(field))
                return field;
            return field.Replace("\r", "").Replace("\n", " ");
        }
        static object GetFormatValue(string value) {
            if(bool.TryParse(value, out bool b))
                return b;
            return value;
        }
        static void SetCustomValidationList(ExcelSheet sheet, int lastRow) {
            if(lastRow <= 1) return;

            string[] allowedE = ["Noun","Proper noun","Verb","Adjective","Adverb","Pronoun","Preposition",
                "Conjunction","Interjection","Article/Determiner","Numeral/Number","Abbreviation/Acronym"];
            string[] allowedF = ["Masculine", "Feminine", "Neuter", "Common", "None/Not applicable", "Unspecified"];
            string[] allowedG = ["Singular", "Plural", "Dual", "Uncountable/Mass", "Unspecified"];
            string[] allowedH = ["Nominative", "Accusative", "Dative", "Genitive"];
            string[] allowedI = ["Positive", "Comparative", "Superlative"];

            ApplyStrictListValidation(sheet, $"E2:E{lastRow}", allowedE, helperColumnLetters: "ZE", namedRange: "Allowed_E");
            ApplyStrictListValidation(sheet, $"F2:F{lastRow}", allowedF, helperColumnLetters: "ZF", namedRange: "Allowed_F");
            ApplyStrictListValidation(sheet, $"G2:G{lastRow}", allowedG, helperColumnLetters: "ZG", namedRange: "Allowed_G");
            ApplyStrictListValidation(sheet, $"H2:H{lastRow}", allowedH, helperColumnLetters: "ZH", namedRange: "Allowed_H");
            ApplyStrictListValidation(sheet, $"I2:I{lastRow}", allowedI, helperColumnLetters: "ZI", namedRange: "Allowed_I");

            RestrictBooleanColumn(sheet, 1, 2, lastRow); //A
            RestrictBooleanColumn(sheet, 10, 2, lastRow); //J

            int[] columnWidths = [10, 20, 30, 40, 15, 15, 15, 15, 15, -1, -1, 20, 40, 30];
            for(int i = 0; i < columnWidths.Length; i++) {
                if(columnWidths[i] > 0)
                    sheet.SetColumnWidth(i + 1, columnWidths[i]);
                else
                    sheet.AutoFitColumn(i + 1);
            }
            sheet.AddAutoFilter($"B1:B{lastRow}");
            var readOnlyColumns = new string[] { "B", "C", "K", "L", "M", "N" };
            foreach(var col in readOnlyColumns)
                ForbidColumnEdit(sheet, col, 2, lastRow);
        }
        static void ForbidColumnEdit(ExcelSheet sheet, string columnLetter, int fromRow, int toRow,
            string errorTitle = "Locked", string errorMessage = "Editing is not allowed") {

            string range = $"{columnLetter}{fromRow}:{columnLetter}{toRow}";
            sheet.ValidationCustomFormula(
                range,
                "=FALSE",
                allowBlank: true,
                errorTitle: errorTitle,
                errorMessage: errorMessage
            );
        }
        static void ApplyStrictListValidation(ExcelSheet sheet, string targetRangeA1, string[] allowedValues, string helperColumnLetters, string namedRange) {
            if(allowedValues == null || allowedValues.Length == 0) return;

            int helperCol = ColumnIndex(helperColumnLetters);
            for(int i = 0; i < allowedValues.Length; i++) {
                sheet.Cell(i + 1, helperCol, allowedValues[i]);
            }
            sheet.SetColumnHidden(helperCol, true);

            string helperA1 = $"{helperColumnLetters}1:{helperColumnLetters}{allowedValues.Length}";
            sheet.SetNamedRange(namedRange, helperA1, save: true, hidden: true);

            var wsPartProp = typeof(ExcelSheet).GetProperty("WorksheetPart", BindingFlags.Instance | BindingFlags.NonPublic);
            var wsPart = (WorksheetPart)wsPartProp.GetValue(sheet);
            var ws = wsPart.Worksheet;

            var dataValidations = ws.GetFirstChild<DataValidations>();
            if(dataValidations == null) {
                dataValidations = new DataValidations();
                ws.Append(dataValidations);
            }

            var dv = new DataValidation {
                Type = DataValidationValues.List,
                AllowBlank = false,
                ShowErrorMessage = true,
                ErrorStyle = DataValidationErrorStyleValues.Stop,
                ErrorTitle = "Invalid value",
                Error = "Select a value from the list.",
                SequenceOfReferences = new ListValue<StringValue> { InnerText = targetRangeA1 }
            };
            dv.Append(new Formula1($"={namedRange}"));

            dataValidations.Append(dv);
            ws.Save();
        }
        static int ColumnIndex(string letters) {
            int col = 0;
            foreach(char c in letters.ToUpperInvariant()) {
                if(c < 'A' || c > 'Z') continue;
                col = col * 26 + (c - 'A' + 1);
            }
            return col;
        }
        static void RestrictBooleanColumn(ExcelSheet sheet, int columnIndex, int fromRow, int toRow) {
            if(toRow < fromRow) return;
            string col = ColumnName(columnIndex);
            string range = $"{col}{fromRow}:{col}{toRow}";
            sheet.ValidationCustomFormula(
                range,
                $"=OR({col}{fromRow}=TRUE,{col}{fromRow}=FALSE)",
                allowBlank: false,
                errorTitle: "Invalid boolean",
                errorMessage: "Only TRUE or FALSE is allowed."
            );
            for(int r = fromRow; r <= toRow; r++)
                sheet.CellAlign(r, columnIndex, HorizontalAlignmentValues.Center);
        }

        static string ColumnName(int col) {
            var sb = new StringBuilder();
            while(col > 0) {
                int m = (col - 1) % 26;
                sb.Insert(0, (char)('A' + m));
                col = (col - 1) / 26;
            }
            return sb.ToString();
        }
    }
}
