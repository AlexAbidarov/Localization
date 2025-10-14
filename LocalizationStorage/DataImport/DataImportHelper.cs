using LocalizationStorage.DataMerging;
using System.Collections.Generic;
using System.Data;

namespace LocalizationStorage.DataImport {
    public class DataImportHelper {
        static readonly DataSet dataSet = new();
        public static DataTableDeImport DataTable =>
            dataSet.Tables.Count > 0 ?
            dataSet.Tables[Settings.deTableName] as DataTableDeImport : null;
        public static DataSet GetDataSet(string file) {
            dataSet.Tables.Clear();
            try {
                if(dataSet.Tables.Count == 0)
                    dataSet.Tables.Add(new DataTableDeImport());
                dataSet.ReadXml(file);
            } catch {
            }
            return dataSet;
        }
    }
    public class DataTableDeImport : ExpertDataTableDe {
        public List<NewTranslation> GetNewData(ExpertDataTableDe data) {
            HashSet<string> keys = [];
            foreach(DataRow row in data.Rows)
                keys.Add(GetKey(row[colPath.ColumnName], row[colKey.ColumnName]));
            var result = new List<NewTranslation>();
            foreach(DataRow row in this.Rows) {
                if(keys.Contains(GetKey(row[colPath], row[colKey]))) continue;
                result.Add(new NewTranslation(
                    $"{row[colPath]}",
                    $"{row[colKey]}",
                    $"{row[colEnglish]}",
                    $"{row[colGerman]}",
                    $"{row[colRussian]}"
                    ));
            }
            return result;
        }
        public List<SimpleTranslationDe> GetExtraData(ExpertDataTableDe data) {
            HashSet<string> keys = [];
            foreach(DataRow row in this.Rows)
                keys.Add(GetKey(row[colPath], row[colKey]));
            var result = new List<SimpleTranslationDe>();
            foreach(DataRow row in data.Rows) {
                if(keys.Contains(GetKey(row[colPath.ColumnName], row[colKey.ColumnName]))) continue;
                result.Add(new SimpleTranslationDe(
                    $"{row[colPath.ColumnName]}",
                    $"{row[colKey.ColumnName]}",
                    $"{row[colEnglish.ColumnName]}",
                    $"{row[colTranslate.ColumnName]}",
                    $"{row[colGerman.ColumnName]}",
                    $"{row[colUser.ColumnName]}",
                    (int)row[colStatus.ColumnName],
                    $"{row[colComment.ColumnName]}"
                    ));
            }
            return result;
        }
        string GetKey(object path, object key) {
            return $"~{path}~{key}~";
        }
    }
    public class NewTranslation(
        string path,
        string key,
        string english,
        string german,
        string russian) : BaseTranslation(path, key, english, $"New[{Settings.FormattedToday}]") {
        public string German { get; set; } = german;
        public string Russian { get; set; } = russian;
    }
}
