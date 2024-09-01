using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace LocalizationStorage {
    public enum TranslationStatus {
        None = 0,
        Translated = 1,
        [Display(Name = "No Translation Needed")]
        NoTranslationNeeded = 2,
        [Display(Name = "Not Sure")]
        NotSure = 3,
        Problems = 4
    };
    public class ExpertDataTableDe : DataTable {
        DataColumn colEnglish = new DataColumn("English", typeof(string));
        DataColumn colPath = new DataColumn("Path", typeof(string));
        DataColumn colKey = new DataColumn("Key", typeof(string));
        DataColumn colTranslate = new DataColumn("NewGerman", typeof(string));
        DataColumn colGerman = new DataColumn("German", typeof(string));
        DataColumn colRussian = new DataColumn("Russian", typeof(string));
        DataColumn colStatus = new DataColumn("Status", typeof(int));
        DataColumn colNotes = new DataColumn("Notes", typeof(string));
        DataColumn colComment = new DataColumn("Comment", typeof(string));
        DataColumn colPicture = new DataColumn("Picture", typeof(string));
        public ExpertDataTableDe() {
            TableName = Settings.deTableName;
            this.Columns.Add(colPath);
            this.Columns.Add(colKey);
            this.Columns.Add(colEnglish);
            this.Columns.Add(colTranslate);
            this.Columns.Add(colGerman);
            this.Columns.Add(colRussian);
            this.Columns.Add(colStatus);
            this.Columns.Add(colNotes);
            this.Columns.Add(colComment);
            this.Columns.Add(colPicture);
        }
        public int AddTranslation(string key, string word, TranslationStatus status = TranslationStatus.Translated, string pKey = null, string path = null) {
            return ChangeRowValue(
                (row) => SetRowValue(row, word, status),
                key, pKey, path);
        }
        public int AddNoNeedTranslate(string key, string pKey = null, string path = null) {
            return ChangeRowValue(
                (row) => row[colStatus] = TranslationStatus.NoTranslationNeeded,
                key, pKey, path);
        }
        public int AddNotSure(string key, string pKey = null, string path = null) {
            return ChangeRowValue(
                (row) => row[colStatus] = TranslationStatus.NotSure,
                key, pKey, path);
        }
        public int AddClear(string key, string pKey = null, string path = null) {
            return ChangeRowValue(
                (row) => SetRowValue(row, string.Empty, TranslationStatus.None),
                key, pKey, path);
        }
        int ChangeRowValue(Action<DataRow> change, string key, string pKey = null, string path = null) {
            int result = 0;
            foreach(DataRow row in this.Rows) {
                bool keysAreEqual = $"{row[colEnglish]}".Trim() == key.Trim();
                if(keysAreEqual) {
                    if(pKey != null
                        && (pKey != $"{row[colKey]}" || path != $"{row[colPath]}")) continue;
                    change(row);
                    result++;
                }
            }
            return result;
        }
        void SetRowValue(DataRow row, string @value, TranslationStatus status) {
            row[colTranslate] = @value;
            row[colStatus] = status;
        }
        public string GetEnglishKeyByLink(BarItemLink link, GridView view) {
            var row = UIHelper.GetDataRowByLink(link, view);
            if(row == null) return null;
            return $"{row[colEnglish]}";
        }
        TranslationStatus GetStatus(int rowHandle, GridView view) {
            DataRow row = view.GetDataRow(rowHandle);
            var result = row[colStatus];
            if(result == null) 
                return TranslationStatus.None;
            return (TranslationStatus)result;
        }
        public TranslationStatus GetStatusByGroupRowValue(int rowHandle, GridView view) {
            TranslationStatus result;
            if(view.IsGroupRow(rowHandle)) {
                int iStartRow = view.GetChildRowHandle(rowHandle, 0);
                int iEndRow = view.GetChildRowHandle(rowHandle, view.GetChildRowCount(rowHandle));
                result = GetStatus(iStartRow, view);
                if(result == TranslationStatus.None) return result;
                for(int i = iStartRow + 1; i < iEndRow; i++)  
                    if(result != GetStatus(i, view)) return TranslationStatus.None;
                return result;
            }
            return TranslationStatus.None;
        }
    }
    public class LocalizationPath {
        public LocalizationPath(FileInfo file) {
            if(string.IsNullOrEmpty(Settings.RootPath))
                throw new ArgumentException("Settings.RootPath cannot be empty");
            if(!file.Exists) return;
            Info = file;
            Name = file.Name;
            ShortName = LocalizationHelper.GetShortName(file);
            Path = file.FullName.Replace($@"{Settings.RootPath}\", string.Empty);
            var match = Settings.satelliteFileNameRegex.Match(Name);
            IsSatellite = match.Success;
            if(!IsSatellite)
                Satellites = LocalizationHelper.GetSatellites(file);
        }
        public string Path { get; internal set; } = string.Empty;
        public string Name { get; internal set; } = string.Empty;
        internal FileInfo Info { get; set; } = null;
        internal string ShortName { get; set; }
        internal bool IsResource {
            get {
                if(string.IsNullOrEmpty(Path)) return false;
                if(Path.IndexOf(Settings.dX) == 0) return true;
                return false;
            }
        }
        internal bool IsSatellite { get; set; }
        public List<LocalizationSatellite> Satellites { get; set; }
        public List<LocalizationKey> Keys { get; set; }
        public int SatelliteCount { get { return Satellites.Count; } }
        public int KeyCount { get { return Keys.Count; } }
        public override string ToString() {
            return Path;
        }
        List<XElement> DeLocalization = null;
        List<XElement> RuLocalization = null;
        List<XElement> JaLocalization = null;
        internal string GetDeKey(string key, ref string xml) {
            return GetKey(key, ref DeLocalization, "de", ref xml);
        }
        internal string GetRuKey(string key, ref string xml) {
            return GetKey(key, ref RuLocalization, "ru", ref xml);
        }
        internal string GetJaKey(string key, ref string xml) {
            return GetKey(key, ref JaLocalization, "ja", ref xml);
        }

        string GetKey(string key, ref List<XElement> list, string satelliteKey, ref string xml) {
            if(list == null) {
                list = GetSatelliteValues(satelliteKey);
            }
            if(list.Count == 0) return Settings.fileNotFount;
            var elements = list.Find(element => element.Attribute("name").Value == key);
            if(elements == null) return Settings.keyNotFount;
            xml = $"{elements}";
            return elements.Value;
        }

        List<XElement> GetSatelliteValues(string satelliteKey) {
            var xdoc = Satellites.Where(si => {
                return si.Name == $"{ShortName}.{satelliteKey}.resx"; ;
            }).ToList<LocalizationSatellite>();
            if(xdoc.Count == 0) return new List<XElement>();
            return LocalizationHelper.GetAllElements(XDocument.Load(xdoc[0].FullName)).ToList<XElement>();
        }
    }
    public class LocalizationSatellite {
        public LocalizationSatellite(FileInfo fi) {
            Name = fi.Name;
            FullName = fi.FullName;
            Length = fi.Length;
            Extension = fi.Extension;
            if(Settings.LoadTranslations) {
                var dataElements = LocalizationHelper.GetAllElements(XDocument.Load(FullName));
                Translations = new List<LocalizationKey>();
                dataElements.ForEach(e => Translations.Add(new LocalizationKey(e)));
            }
        }
        public string Name { get; internal set; }
        internal string FullName { get; set; }
        internal string Extension { get; set; }
        public string Path { 
            get { 
                return FullName.Replace($@"{Settings.RootPath}\", string.Empty);
            }
        }
        public List<LocalizationKey> Translations { get; set; }
        public int KeyCount { get { return Translations == null ? -1 : Translations.Count; } }
        public string Language { 
            get {
                return LocalizationHelper.GetLangBySatellite(Name.Replace(Extension, string.Empty));
            } 
        }
        public long Length { get; internal set; }
    }
    public class LocalizationKey {
        public LocalizationKey(XElement element) { 
            Name = element.Attribute("name").Value;
            Type = element.Attribute("type")?.Value;
            Xml = $"{element}";
            Value = LocalizationHelper.PrepareValue(element.Value);
        }
        public string Name { get; internal set;}
        public string Value { get; internal set; }
        public string Xml { get; internal set; }
        public string Type { get; internal set; }
    }
    public class LocalizationTranslation {
        readonly string xmlde, xmlru, xmlja = string.Empty;
        public LocalizationTranslation(XElement dataElement, LocalizationPath path) {
            LocalizationPath = path;
            Key = dataElement.Attribute("name").Value;
            Xml = $"{dataElement}";
            IDValue = dataElement.Value;
            English = LocalizationHelper.PrepareValue(IDValue);
            Type = dataElement.Attribute("type")?.Value;
            German = LocalizationHelper.PrepareValue(path.GetDeKey(Key, ref xmlde));
            Russian = LocalizationHelper.PrepareValue(path.GetRuKey(Key, ref xmlru));
            Japan = LocalizationHelper.PrepareValue(path.GetJaKey(Key, ref xmlja));
        }
        internal bool IsLocalization { //TODO right criteria
            get {
                if(!string.IsNullOrEmpty(Type)) return false;
                if(Key.IndexOf(@">>") == 0) return false;
                //if(Key.IndexOf(@"stringid", StringComparison.OrdinalIgnoreCase) >= 0) return true; 
                //if(string.IsNullOrEmpty(Value)) return false;
                //if(Settings.DenyLocalization(Key, Value)) return false;
                return true;
            }
        }
        internal LocalizationPath LocalizationPath { get; set; }
        public string Path { get { return LocalizationPath.Path; } }
        public string Key { get; internal set; } = string.Empty;
        internal string Xml { get; set; } = string.Empty;
        internal string XmlDe { get { return xmlde; } }
        internal string IDValue { get; set; } = string.Empty;
        public string English { get; set; } = string.Empty;
        internal string Type { get; set; } = string.Empty;
        public string German { get; internal set; }
        internal string GermanValue { 
                get {
                return LocalizationHelper.PrepareTranslation(German, English);
            } 
        }
        public string Russian { get; internal set; }
        public string Japan { get; internal set; }
    }
    public class LocalizationRepDe {
        List<LocalizationTranslation> data;
        List<LocalizationTranslation> names = null;
        int count = -1;
        public LocalizationRepDe(string name, int count, List<LocalizationTranslation> translations) {
            Name = name;
            Count = count;
            data = translations;
        }
        public List<KeyValuePair<string, int>> Keys {
            get {
                var keyValuePairs = new Dictionary<string, int>();
                Names.ForEach(t => {
                    string key = t.German;
                    if(keyValuePairs.ContainsKey(key))
                        keyValuePairs[key]++;
                    else keyValuePairs.Add(key, 1);
                });
                return keyValuePairs.OrderByDescending(k => k.Value).ToList();
            }
        }
        public string Name { get; private set; }
        public int Count { get; private set; }
        public int TranslationCount {
            get {
                if(count == -1 )
                    count = Names.Select(t => t.GermanValue).Distinct().Count();
                return count;
            } 
        }
        public List<LocalizationTranslation> Names {
            get {
                if(names == null)
                    names = data.Where(t => t.English == Name 
                        && LocalizationHelper.ValueExist(t.German) 
                        /*&& t.German != t.English*/).ToList();
                return names;
            }
        }
    }
}
