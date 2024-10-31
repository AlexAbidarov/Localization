using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LocalizationStorage.DataMerging {
    public static class DataMergingHelper {
        static DataSet dataSet = new DataSet();
        public static string GetDateString(DateOnly date) => string.Format("{0:yyyyMMdd}", date);
        public static ExpertDataTableDeMerge DataTable => 
            dataSet.Tables.Count > 0 ? 
            dataSet.Tables[Settings.deTableName] as ExpertDataTableDeMerge : null;
        public static DataSet GetDataSet(string file) {
            dataSet.Tables.Clear();
            try {
                if(dataSet.Tables.Count == 0) 
                    dataSet.Tables.Add(new ExpertDataTableDeMerge());
                dataSet.ReadXml(file);
            } catch {
            }
            return dataSet;
        }
    }
    public class ExpertDataTableDeMerge : ExpertDataTableDe {
        public HashSet<string> GetUserList() {
            var result = new HashSet<string>();
            foreach(DataRow row in this.Rows) {
                string @value = $"{row[colUser]}";
                if(!string.IsNullOrEmpty(@value))
                    result.Add(@value);
            }
            return result;
        }
        public List<SimpleTranslationDe> GetUserData(string user, string userDate) {
            var result = new List<SimpleTranslationDe>();
            if(string.IsNullOrEmpty(user)) return result;
            foreach(DataRow row in this.Rows) {
                if(!user.Equals($"{row[colUser]}")) continue;
                result.Add(new SimpleTranslationDe(
                    $"{row[colPath]}",
                    $"{row[colKey]}",
                    $"{row[colEnglish]}",
                    $"{row[colTranslate]}",
                    $"{row[colGerman]}",
                    $"{row[colUser]}[{userDate}]",
                    (int)row[colStatus],
                    $"{row[colComment]}"
                    ));
            }
            return result;
        }
    }
    public class SimpleTranslationDe : SimpleTranslation {
        public SimpleTranslationDe(
            string path, 
            string key, 
            string english, 
            string translation, 
            string deTranslation, 
            string user, 
            int status, 
            string comment) :
            base(path, key, english, translation) {
            DeTranslation = deTranslation;
            User = user;
            Comment = comment;
            Status = (TranslationStatus)status;
        }
        public TranslationStatus Status { get; set; }
        public string User { get; set; }
        public string Comment { get; set; }
        public string DeTranslation { get; set; }
    }
}
