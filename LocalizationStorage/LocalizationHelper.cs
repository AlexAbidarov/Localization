using DevExpress.Data;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.Utils.Extensions;
using DevExpress.Utils.Text;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LocalizationStorage {
    public class LocalizationHelper {
        public static List<LocalizationPath> GetLocalizationPathSource(string path) {
            List<LocalizationPath> result = [];
            AddResX(new DirectoryInfo(path), result);
            return result;
        }
        static void AddResX(DirectoryInfo di, List<LocalizationPath> result) {
            di.GetFiles("*.resx").ForEach(file => {
                LocalizationPath path = new(file);
                if(path.IsResource &&
                    !path.IsSatellite) //show all
                    result.Add(path);
            });
            di.GetDirectories().ForEach(directory => AddResX(directory, result));
        }
        internal static void CreateKeysData(List<LocalizationPath> paths) {
            var localizationKeys = new List<LocalizationTranslation>();
            paths.ForEach(path => {
                var dataElements = GetAllElements(XDocument.Load(path.Info.FullName));
                path.Keys = [];
                dataElements.ForEach(e => path.Keys.Add(new LocalizationKey(e)));
                dataElements.ForEach(element => {
                    LocalizationTranslation key = new(element, path);
                    if(key.IsLocalization)
                        localizationKeys.Add(key);
                });
            });
            Settings.Keys = localizationKeys;
        }
        internal static List<XElement> GetAllElements(XDocument document) {
            return [.. document.Root.Elements("data")];
        }
        internal static List<LocalizationSatellite> GetSatellites(FileInfo file) {
            List<LocalizationSatellite> result = [];
            file.Directory.GetFiles($"{GetShortName(file)}.*.resx")
                .Where((fi) => {
                    var _ = Settings.satelliteFileNameRegex.Match(fi.Name);
                    return _.Success;
                }).ForEach(fi =>
                result.Add(new LocalizationSatellite(fi)));
            return result;
        }
        internal static string GetLangBySatellite(string name) {
            int index = name.LastIndexOf(".");
            if(index > 0)
                return name.Substring(index + 1);
            return name;
        }
        internal static string GetShortName(FileInfo file) {
            return file.Name.Replace(file.Extension, string.Empty);
        }
        internal static string PrepareValue(string value) {
            if(value == null ||
                Settings.fileNotFount.Equals(value) ||
                Settings.keyNotFount.Equals(value)) return value;
            value = value.Trim();
            return value;
        }
        internal static bool IsExistRoot {
            get {
                return !string.IsNullOrEmpty(Settings.RootPath) &&
                    Directory.Exists(Settings.RootPath) &&
                    new DirectoryInfo(Settings.RootPath).GetDirectories().Length > 50 &&
                    Settings.RootPath.IndexOf("\\localization", StringComparison.OrdinalIgnoreCase) > 0;
            }
        }
        internal static bool ValueExist(string name) {
            return !string.IsNullOrEmpty(name) &&
                !Settings.fileNotFount.Equals(name) &&
                !Settings.keyNotFount.Equals(name);
        }
        internal static string PrepareTranslation(string value) {
            value = value.Replace("&", string.Empty);
            value = value.ToLower();
            return value;
        }
        internal static bool IsLastNewRow(string line) {
            return line.Length > 2 &&
                line.LastIndexOf("\r\n") == line.Length - 2;
        }
        internal static bool AreDifferentLineCount(string line1, string line2) {
            return LineCount(line1) != LineCount(line2);
        }
        static int LineCount(string text) {
            return text.Length - text.Replace("\n", string.Empty).Length;
        }
        static string SatelliteByName(string name, string satelliteExt) {
            int index = name.LastIndexOf(".");
            return $"{name.Substring(0, index)}.{satelliteExt}.resx";
        }
        internal static string GetSatellitePath(string root, string keyPath, string satelliteExt, bool force = false) {
            keyPath = SatelliteByName(keyPath, satelliteExt);
            string path = Path.Combine(root, keyPath);
            if(File.Exists(path) || force)
                return path;
            return string.Empty;
        }
        static string GetMainResourceFilePath(string satelliteFilePath) {
            if(Settings.satelliteFileNameRegex.Match(Path.GetFileName(satelliteFilePath)).Success) {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(satelliteFilePath);
                string[] parts = fileNameWithoutExtension.Split('.');
                if(parts.Length > 1 && parts[parts.Length - 1].Length >= 2) {
                    string mainFileName = string.Join(".", parts.Take(parts.Length - 1)) + ".resx";
                    return Path.Combine(Path.GetDirectoryName(satelliteFilePath), mainFileName);
                }
            }
            return string.Empty;
        }
        public static Dictionary<string, string> UpdateTranslatedValuesByMask(string satellitePath, Dictionary<string, string> values) {
            Dictionary<string, string> result = [];
            var docElements = GetMainResourceElements(satellitePath) ?? throw new Exception("Main resource file is not found");
            values.ForEach(row => {
                string value = $"{row.Value}";
                XElement e = GetElement(row.Key, docElements);
                if(e != null) {
                    value = GetValueWithSpaces(e.Element("value").Value, value);
                    result.Add(row.Key, value);
                }
            });
            return result;
        }
        static string GetValueWithSpaces(string mainValue, string value) {
            var spaces = new SpacesMask(mainValue);
            if(!spaces.IsEmpty)
                value = $"{spaces.LeadingString}{value}{spaces.TrailingString}";
            return value;
        }
        static List<XElement> GetMainResourceElements(string satelliteFilePath) {
            string mainFileName = GetMainResourceFilePath(satelliteFilePath);
            if(File.Exists(mainFileName))
                return GetAllElements(XDocument.Load(mainFileName));
            return null;
        }
        static bool IsElementExists(XElement element, List<XElement> elements) {
            foreach(var e in elements)
                if(e.Attribute("name").Value == element.Attribute("name").Value)
                    return true;
            return false;
        }
        static XElement GetElement(string element, List<XElement> elements) {
            foreach(var e in elements)
                if(e.Attribute("name").Value == element)
                    return e;
            return null;
        }
        internal static int RemoveOutdatedKeys(string path, XDocument doc) { //Remove outdated keys from the satellite file
            int removedItemsCount = 0;
            var mainElements = GetMainResourceElements(path);
            if(mainElements != null) {
                var docElements = GetAllElements(doc);
                for(int i = docElements.Count - 1; i >= 0; i--) {
                    if(!IsElementExists(docElements[i], mainElements)) {
                        docElements[i].Remove();
                        removedItemsCount++;
                    }
                }
            }
            return removedItemsCount;
        }
        internal static string GetFirstChildDirectoryName(string path) {
            if(path.Contains(Settings.RootPath))
                path = Settings.GetSimplePath(path);

            var firstSeparatorIndex = path.IndexOf('\\');
            return firstSeparatorIndex >= 0 ? path.Substring(0, firstSeparatorIndex) : path;
        }
    }
    public class Settings {
        public static void ClearData() {
            paths = null;
        }
        internal static string deDataSetName = "ExpertDataDe.xml";
        internal static string deTableName = "DeTable";
        internal static string DX { get; } = "DevExpress";
        internal static string Xtra { get; } = "Xtra";
        internal static string replyFile = "GermanReplays.txt";
        internal static string sessionChanged = "SessionChanged";
        internal static Regex satelliteFileNameRegex = new(@"^([\w\s-.])+\.([\w\s_-])+\.resx$");
        static List<LocalizationPath> paths = null;
        public static List<LocalizationPath> Paths {
            get {
                if(paths == null) {
                    paths = LocalizationHelper.GetLocalizationPathSource(RootPath);
                    LocalizationHelper.CreateKeysData(paths);
                }
                return paths;
            }
        }
        internal static void SetUser(string[] args) {
            args.ForEach(x => {
                if(x.IndexOf(userAttribute) == 0)
                    user = x.Replace(userAttribute, string.Empty);
            });
        }
        static readonly string userAttribute = "user:";
        static string user = Environment.UserName;
        public static string User {
            get { return user; }
            set {
                user = value;
            }
        }
        public static string GetSimplePath(string path) =>
            path.Replace(@$"{RootPath}\", string.Empty);
        public static string FormattedToday => GetDateString(DateOnly.FromDateTime(DateTime.Today));
        public static string GetDateString(DateOnly date) => string.Format("{0:yyyyMMdd}", date);
        public static bool IsAdmin => user.IndexOf("admin", StringComparison.OrdinalIgnoreCase) >= 0;
        public static string DataPath { get; } = $@"{Application.StartupPath}\Data";
        public static string LogsPath => Path.Combine(DataPath, @"..\Logs");
        public static string GermanDataSetPath { get; } = $@"{DataPath}\{deDataSetName}";
        public static string RootPath { get; set; }
        public static bool LoadTranslations { get; set; } = false;
        public static List<LocalizationTranslation> Keys { get; internal set; }
        public static List<LocalizationTranslation> ActualKeys => Keys?.Where<LocalizationTranslation>(x => !string.IsNullOrEmpty(x.English)).ToList();
        internal static string fileNotFount = "<< Satellite file is not found >>";
        internal static string keyNotFount = "<< Key is not found >>";
        readonly static string[] keyExceptionEnd = [".AccessibleName", ".AccessibleDescription"];
        readonly static string[] valueException = ["layoutcontrol"];
        internal static bool DenyLocalization(string key, string value) {
            foreach(string item in valueException)
                if(value.IndexOf(item, StringComparison.OrdinalIgnoreCase) >= 0) return true;
            foreach(string item in keyExceptionEnd)
                if(key.IndexOf(item, StringComparison.OrdinalIgnoreCase) == key.Length - item.Length) return true;
            return false;
        }
        internal static ExpertDataTableDe GeDataTable { get; } = new ExpertDataTableDe();
        static DataSet mainDataSet = null;
        public static DataSet MainDataSet {
            get {
                if(mainDataSet == null) {
                    mainDataSet = new DataSet();
                    mainDataSet.Tables.Add(GeDataTable);
                }
                return mainDataSet;
            }
        }
        public static ExpertDataTableDe MainTable => Settings.MainDataSet.Tables[Settings.deTableName] as ExpertDataTableDe;
        public static bool IsNameEmpty(string name) {
            return string.IsNullOrEmpty(name) || fileNotFount.Equals(name) || keyNotFount.Equals(name);
        }
        public static void ShowHelp() {
            bool ctrlHelp = (Control.ModifierKeys & System.Windows.Forms.Keys.Control) == System.Windows.Forms.Keys.Control;
            string docName = GetHelpDoc($@"{Application.StartupPath}\Docs", "Readme.pdf");
            if(!File.Exists(docName))
                XtraMessageBox.Show($"Document {docName} wasn't found.", "Documentation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else {
                if(!ctrlHelp)
                    Process.Start(docName);
                else
                    Process.Start("https://devexpress-my.sharepoint.com/:w:/p/denis_garavsky/EWfjNyEeBAZMrGFazWJejIoB9Zd0vbKjz5ADnKH7rtzExg");
            }
        }
        static string GetHelpDoc(string directory, string name) {
            string doc = Path.Combine(directory, name);
            if(File.Exists(doc)) return doc;
            if(Directory.Exists(directory)) {
                var files = Directory.GetFiles(directory, "*.doc*");
                if(files.Length > 0) return files[0];
            }
            return doc;
        }
    }
    public static class ElapsedTime {
        static readonly Stopwatch stopWatch = new();
        public static void Start() { stopWatch.Restart(); }
        public static void Stop() { stopWatch.Stop(); }
        public static string GetTime() {
            TimeSpan ts = stopWatch.Elapsed;
            return $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
        }
        public static string GetNuGetTime() {
            TimeSpan ts = stopWatch.Elapsed;
            return $"{ts.Seconds}.{ts.Milliseconds:000} sec.";
        }
    }
    public class ExpertDataHelper {
        internal static bool UpdateGermanData() {
            if(!Directory.Exists(Settings.DataPath) || !File.Exists(Settings.GermanDataSetPath))
                return false;
            Settings.MainDataSet.ReadXml(Settings.GermanDataSetPath);
            return true;
        }
    }
    public class UIHelper {
        readonly static Font rowFont = new("Calibry", 7.95F);
        public static void SetColumnAppearance(GridColumnCollection collection) {
            collection.ForEach(column => {
                if(column.FieldName == "Path" || column.FieldName == "Key")
                    column.AppearanceCell.Font = rowFont;
            });
        }
        public static void ShowTranslatioDetailForm(BarItemLink link) {
            ShowTranslatioDetailForm(((PopupMenu)link.LinkedObject).Tag as GridHitInfo);
        }
        public static void ShowTranslatioDetailForm(GridHitInfo info) {
            if(!info.InDataRow) return;
            if(info.View.GetRow(info.RowHandle) is LocalizationTranslation translation) {
                using(DetailForm form = new(info.View.GridControl.FindForm(), translation))
                    form.ShowDialog();
            }
        }
        internal static DataRow GetDataRow(int rowHandle, GridView view) {
            if(view.IsGroupRow(rowHandle))
                rowHandle = view.GetDataRowHandleByGroupRowHandle(rowHandle);
            return view.GetDataRow(rowHandle);
        }
        internal static DataRow GetDataRowByLink(BarItemLink link, GridView view) {
            GridHitInfo info = GetGridHitInfoByLink(link);
            if(info == null) return null;
            return GetDataRow(info.RowHandle, view);
        }
        internal static GridHitInfo GetGridHitInfoByLink(BarItemLink link) {
            if(link.LinkedObject is not PopupMenu) return null;
            return ((PopupMenu)link.LinkedObject).Tag as GridHitInfo;
        }
        static T GetValueByLink<T>(BarItemLink link) where T : class {
            GridHitInfo info = GetGridHitInfoByLink(link);
            if(info == null) return null;
            return info.View.GetRow(info.RowHandle) as T;
        }
        public static LocalizationKey GetKeyByLink(BarItemLink link) {
            return GetValueByLink<LocalizationKey>(link);
        }
        public static LocalizationTranslation GetTranslateByLink(BarItemLink link) {
            return GetValueByLink<LocalizationTranslation>(link);
        }
        internal static int ConvertData() {
            if(Settings.ActualKeys == null || Settings.ActualKeys.Count < 1) return 0;
            foreach(var item in Settings.ActualKeys) {
                Settings.GeDataTable.Rows.Add([item.Path, item.Key, item.English, string.Empty, item.German, item.Russian, 0]);
            }
            return Settings.ActualKeys.Count;
        }
        internal static void SortBySummary(GridView view, GridColumn column, ColumnSortOrder order) {
            List<GroupSummarySortInfo> items = [];
            items.Add(new GroupSummarySortInfo(view.GroupSummary[0], column, order));
            view.GroupSummarySortInfo.ClearAndAddRange([.. items]);
        }
        public static Point GetCenterPoint(Control owner, Size size) {
            return new Point(owner.Location.X + (owner.Width - size.Width) / 2,
                owner.Location.Y + (owner.Height - size.Height) / 2);
        }
        public static void SetGridReadOnly(GridView view, bool allowSort = true) {
            view.Columns.ForEach(col => col.OptionsColumn.AllowFocus = false);
            if(view.Columns.Count > 0) {
                GridColumn column = view.Columns[0];
                column.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                if(allowSort)
                    column.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            }
        }
        internal static void RemoveLinksFromMenu(PopupMenu menu, string name) {
            for(int i = menu.ItemLinks.Count - 1; i >= 0; i--)
                if(menu.ItemLinks[i].Item.Caption.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
                    menu.RemoveLink(menu.ItemLinks[i]);
        }
        public static void UpdateAppearance(AppearanceObject app, int status) {
            switch(status) {
                case 1://Translated
                    SetColor(app, DXSkinColors.FillColors.Success);
                    break;
                case 2://No Translation Needed
                    SetColor(app, DXSkinColors.FillColors.Primary);
                    break;
                case 3://Not Sure
                    SetColor(app, Color.Yellow);
                    break;
                case 4://Problems
                    SetColor(app, DXSkinColors.FillColors.Danger);
                    break;
                case 5://Automatic
                    SetColor(app, Color.LightSkyBlue);
                    break;
                case 6://Needs Verification
                    SetColor(app, Color.LightBlue);
                    break;
            }
        }
        static void SetColor(AppearanceObject app, Color color, object foreColor = null) {
            app.BackColor = color;
            app.ForeColor = foreColor == null ? ContrastColor.GetContrastForeColor(color) : (Color)foreColor;
        }
    }
    public class IOHelper {
        internal static string GetShortAssemblyName(string name) {
            return name.Substring(0, name.IndexOf(","));
        }
        public static void CreateBakFile(string name) {
            FileInfo fi = new(name);
            string bakFile = $"{fi.FullName}.bak";
            if(File.Exists(bakFile)) File.Delete(bakFile);
            fi.MoveTo(bakFile);
        }
        public static void SaveLog(List<string> lines, string name) {
            FileInfo fi = new($@"{Settings.LogsPath}\{name}");
            if(!fi.Directory.Exists) fi.Directory.Create();
            try {
                using(StreamWriter sw = new(fi.FullName)) {
                    int index = 0;
                    lines.ForEach((line) => sw.WriteLine($"{++index}. {line}"));
                }
            } catch(Exception e) {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static string GetLog(List<string> lines) {
            StringBuilder result = new();
            int index = 0;
            lines.ForEach((line) => result.AppendLine($"{++index}. {line}"));
            return $"{result}";
        }
        public static bool CopyAssemblies(string[] args) {
            if(args.Length > 0 && HasCopyArgs(args)) {
                string source = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
                DirectoryInfo diSource = new(Path.Combine(source, "Packages"));
                if(diSource.Exists) {
                    try {
                        foreach(FileInfo fi in diSource.GetFiles()) {
                            if(fi.Extension.IndexOf("dll", StringComparison.OrdinalIgnoreCase) >= 0) {
                                string newFileName = Path.Combine(source, fi.Name);
                                if(!File.Exists(newFileName))
                                    fi.CopyTo(newFileName, true);
                            }
                        }
                    } catch(Exception ex) {
                        MessageBox.Show($"{ex.Message}", "Copy Error");
                    }
                }
                return true;
            }
            return false;
        }
        static bool HasCopyArgs(string[] args) {
            foreach(string line in args)
                if(line.IndexOf("copylocal", StringComparison.OrdinalIgnoreCase) == 1) return true;
            return false;
        }
    }
    public class GridHelper {
        public static void ShowRowPopuMenu(GridView view, PopupMenu groupRowMenu, PopupMenu rowMenu) {
            int rowHandle = view.FocusedRowHandle;
            if(view.IsGroupRow(rowHandle))
                ShowPopupMenu(view, groupRowMenu, rowHandle);
            else if(rowHandle >= 0)
                ShowPopupMenu(view, rowMenu, rowHandle);
        }
        internal static void ShowPopupMenu(GridView view, PopupMenu menu, int rowHandle) {
            var rows = GetGridViewInfo(view).RowsInfo;
            GridRowInfo ri = GetRowInfo(rows, rowHandle);
            if(ri != null) {
                Point pi = new(ri.Bounds.X + ri.RowLineHeight / 2, ri.Bounds.Y + ri.RowLineHeight / 2);
                menu.Tag = view.CalcHitInfo(pi);
                menu.ShowPopup(view.GridControl.PointToScreen(pi));
            }
        }
        static GridViewInfo GetGridViewInfo(GridView view) {
            PropertyInfo pi = view.GetType().GetProperty("ViewInfo",
                BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.NonPublic, null, typeof(GridViewInfo), new Type[0], null);
            return pi.GetValue(view) as GridViewInfo;
        }
        static GridRowInfo GetRowInfo(GridRowInfoCollection list, int rowHandle) {
            foreach(var row in list)
                if(row.RowHandle == rowHandle) return row;
            return null;
        }
    }
    public class SpacesMask(string value) {
        public bool IsEmpty => LeadingSpaces == 0 &&
            TrailingSpaces == 0 &&
            LeadingNewLine == 0 &&
            TrailingNewLine == 0;
        int LeadingSpaces { get; } = value.Length - value.TrimStart(' ').Length;
        int TrailingSpaces { get; } = value.Length - value.TrimEnd(' ').Length;
        int LeadingNewLine { get; } = value.Length - value.TrimStart('\n').Length;
        int TrailingNewLine { get; } = value.Length - value.TrimEnd('\n').Length;
        public string LeadingString => $"{GetCharString(LeadingNewLine, '\n')}{GetCharString(LeadingSpaces)}";
        public string TrailingString => $"{GetCharString(TrailingSpaces)}{GetCharString(TrailingNewLine, '\n')}";
        static string GetCharString(int count, char symbol = ' ') {
            if(count <= 0) return string.Empty;
            return new string(symbol, count);
        }
    }
    public static class PromptHelper {
        static readonly HashSet<string> promptKeys = [
            "AIIntegrationStringId.AutoCompleteSystemPrompt",
            "AIIntegrationStringId.AutoCompleteDefaultRole",
            "AIIntegrationStringId.AbstractiveSummarySystemPrompt",
            "AIIntegrationStringId.ChangeToneSystemPrompt",
            "AIIntegrationStringId.ExpandSystemPrompt",
            "AIIntegrationStringId.ExplainSystemPrompt",
            "AIIntegrationStringId.ExtractiveSummarySystemPrompt",
            "AIIntegrationStringId.ProofreadSystemPrompt",
            "AIIntegrationStringId.ChangeStyleSystemPrompt",
            "AIIntegrationStringId.ShortenSystemPrompt",
            "AIIntegrationStringId.TranslateSystemPrompt",
            "AIIntegrationStringId.LocalizeSystemPrompt",
            "AIIntegrationStringId.GenerateDescriptionSystemPrompt",
            "AIIntegrationStringId.ExpressionGeneratorRegeneratePrompt",
            "AIIntegrationStringId.SmartPasteSystemPrompt",
            "AIIntegrationStringId.SmartPasteSchedulerPromptAugmentation",
            "AIIntegrationStringId.ContextSystemPrompt",
            "AIIntegrationStringId.SmartSearchSystemPrompt",
            "AIIntegrationStringId.SmartSearchUserPrompt",
            "AIIntegrationStringId.ExplainFormulaPrompt",
            "AIIntegrationStringId.CustomNoMarkdownSystemPrompt",
            "AIIntegrationStringId.SmartPasteBoolFormattingPrompt",
            "AIIntegrationStringId.SmartPasteIntegerFormattingPrompt",
            "AIIntegrationStringId.SmartPasteRealFormattingPrompt",
            "AIIntegrationStringId.SmartPasteDateTimeFormattingPrompt",
            "AIIntegrationStringId.SmartPasteDateOnlyFormattingPrompt",
            "AIIntegrationStringId.SmartPasteTimeOnlyFormattingPrompt",
            "AIIntegrationStringId.SmartPasteChoiceFormattingPrompt",
            "AIIntegrationStringId.SeparatorPromptAugmentationText",
            "AIIntegrationStringId.ToolsErrorToolCannotBeCalled",
            "AIIntegrationStringId.ToolsErrorTargetNotFound",
            "AIIntegrationStringId.ToolsTargetIdentifierParameterName",
            "AIIntegrationStringId.ToolsTargetIdentifierParameterDescription",
            "AIIntegrationStringId.ToolsGetTargetsToolName",
            "AIIntegrationStringId.ToolsGetTargetsToolDescription",
            "AIIntegrationStringId.TranslateRichText",
            "AIIntegrationStringId.FailedParseTranslateJSON",
            "AIIntegrationStringId.FailedDocumentClassificationExtensionRegistration",
            "AIIntegrationStringId.UnknownTranslationFailed",
            "AIIntegrationStringId.TranslationCanceled",
            "AIIntegrationStringId.UnexpectedErrorStatus",
            "AIIntegrationStringId.InvalidInputJsonChunkTextToChunkConverter",
            "AIIntegrationStringId.ProofreadRichText",
            "AIIntegrationStringId.PromptToExpressionSystemPrompt",
            "AIIntegrationStringId.PromptToExpressionRetryPrompt",
            "AIIntegrationStringId.PromptToFilterSystemPrompt",
            "AIIntegrationStringId.ProofreadDocumentSystemPrompt",
            "AIIntegrationStringId.TranslateDocumentSystemPrompt",
            "AIIntegrationStringId.AskAIDocumentRequestPrompt"];

        internal static bool IsPromptKey(string key) {
            foreach(var prompt in promptKeys) {
                if(key.Contains(prompt))
                    return true;
            }
            return false;
        }
    }
}
