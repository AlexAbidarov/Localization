using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using LocalizationStorage.DataImport;
using LocalizationStorage.DataMerging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LocalizationStorage {
    public enum TranslationStatus {
        None = 0,
        Translated = 1,
        [Display(Name = "No Translation Needed")]
        NoTranslationNeeded = 2,
        [Display(Name = "Not Sure")]
        NotSure = 3,
        Problems = 4,
        [Display(Name = "Accepted Automatically")]
        IsAcceptedAutomatically = 5,
        [Display(Name = "Accepted (Needs Verification)")]
        IsAcceptedAutomatically2 = 6
    };
    public class UserTranslation(string name) {
        public string Name { get; set; } = name;
        public int Count { get; private set; } = 1;
        public void AddCount() {
            Count++;
        }
    }
    public class TranslationDe {
        readonly List<UserTranslation> userTranslation = [];
        public List<UserTranslation> UserTranslation => userTranslation;
        public TranslationDe() {
            IsGroup = false;
        }
        public TranslationDe(string translation, string english, string german, string path, string key, string russian, string comment) : this() {
            Translation = translation;
            English = english;
            German = german;
            Path = path;
            Key = key;
            Russian = russian;
            Comment = comment;
        }
        public TranslationDe(string translation, string english) {
            Translation = translation;
            English = english;
            IsGroup = true;
        }
        public bool IsGroup { get; set; }
        public string Translation { get; set; }
        public string English { get; set; }
        public string German { get; set; }
        public string Russian { get; set; }
        public string Path { get; set; }
        public string Key { get; set; }
        public string Comment { get; set; }
        internal string PureGerman {
            get {
                if(LocalizationHelper.ValueExist(Translation)) return Translation;
                if(LocalizationHelper.ValueExist(German)) return German;
                return string.Empty;
            }
        }
        internal string PureRussian {
            get {
                if(LocalizationHelper.ValueExist(Russian)) return Russian;
                return string.Empty;
            }
        }
        public void AddUserTranslation(string item) {
            if(Settings.IsNameEmpty(item)) return;
            var values = userTranslation.Where(x => x.Name == item).ToList();
            if(values.Count > 0)
                values[0].AddCount();
            else
                userTranslation.Add(new UserTranslation(item));
        }
    }
    public class ExpertDataTableDe : DataTable {
        protected DataColumn colEnglish = new("English", typeof(string));
        protected DataColumn colPath = new("Path", typeof(string));
        protected DataColumn colKey = new("Key", typeof(string));
        protected DataColumn colTranslate = new("NewGerman", typeof(string));
        protected DataColumn colGerman = new("German", typeof(string));
        protected DataColumn colStatus = new("Status", typeof(int));
        protected DataColumn colComment = new("Comment", typeof(string));
        protected DataColumn colUser = new("User", typeof(string));
        protected DataColumn colRussian = new("Russian", typeof(string));
        protected DataColumn colInternalInfo = new("InternalInfo", typeof(string));
        readonly DataColumn colSessionChanged = new(Settings.sessionChanged, typeof(bool));
        readonly DataColumn colNotes = new("Notes", typeof(string));
        readonly DataColumn colPicture = new("Picture", typeof(string));
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
            this.Columns.Add(colUser);
            this.Columns.Add(colInternalInfo);
            this.Columns.Add(colSessionChanged); //Last column!
        }
        public int AddTranslation(string key, string word, TranslationStatus status = TranslationStatus.Translated, string pKey = null, string path = null) {
            return ChangeRowValue(
                (row) => SetRowValue(row, word, status),
                key, pKey, path);
        }
        bool IsStatusExist(object val, TranslationStatus[] list) {
            foreach(var status in list)
                if((int)status == (int)val) return true;
            return false;
        }
        public int AddNoNeedTranslate(string key, string pKey = null, string path = null) {
            return ChangeRowValue(
                (row) => {
                    if(!IsStatusExist(row[colStatus], [TranslationStatus.Problems])) {
                        row[colStatus] = TranslationStatus.NoTranslationNeeded;
                        row[colTranslate] = string.Empty;
                        return true;
                    }
                    return false;
                },
                key, pKey, path);
        }
        public int Verify(string key, string pKey = null, string path = null) {
            return ChangeRowValue(
                (row) => {
                    if(string.IsNullOrEmpty($"{row[colTranslate]}")) return false;
                    row[colStatus] = TranslationStatus.Translated;
                    return true;
                },
                key, pKey, path);
        }
        internal int TryToAddAutomaticTranslate(string key, int rowHandle, GridView view) {
            if(view.IsGroupRow(rowHandle)) {
                int iStartRow = view.GetChildRowHandle(rowHandle, 0);
                int iEndRow = view.GetChildRowHandle(rowHandle, view.GetChildRowCount(rowHandle) - 1);
                HashSet<string> strings = [];
                int count = 0;
                for(int i = iStartRow; i <= iEndRow; i++) {
                    string word = $"{view.GetDataRow(i)[colGerman]}";
                    if(LocalizationHelper.ValueExist(word)) {
                        if(!strings.Contains(word))
                            strings.Add(word);
                        count++;
                    }
                }
                if(strings.Count == 1) {
                    if(count == 1) return AddAutomaticTranslate(key, TranslationStatus.IsAcceptedAutomatically2, strings.ElementAt(0));
                    if(count > 1) return AddAutomaticTranslate(key, TranslationStatus.IsAcceptedAutomatically, strings.ElementAt(0));
                }
            }
            return 0;
        }
        internal int AddAutomaticTranslate(string key, TranslationStatus status, string word) {
            return ChangeRowValue(
                (row) => {
                    if(!IsStatusExist(row[colStatus], [
                        //TranslationStatus.IsAcceptedAutomatically, TranslationStatus.IsAcceptedAutomatically2,
                        TranslationStatus.Problems, TranslationStatus.Translated, TranslationStatus.NotSure, TranslationStatus.NoTranslationNeeded ])) {
                        row[colTranslate] = word;
                        row[colStatus] = status;
                        return true;
                    }
                    return false;
                },
                key, null, null);
        }
        public int AddNotSure(string key, string pKey = null, string path = null) {
            return ChangeRowValue(
                (row) => {
                    if(!IsStatusExist(row[colStatus], [TranslationStatus.Problems])) {
                        row[colStatus] = TranslationStatus.NotSure;
                        return true;
                    }
                    return false;
                },
                key, pKey, path);
        }
        public int AddClear(string key, string pKey = null, string path = null) {
            return ChangeRowValue(
                (row) => SetRowValue(row, string.Empty, TranslationStatus.None),
                key, pKey, path);
        }
        public int AddComment(string key, string comment, string pKey, string path, bool auto = true) {
            return ChangeRowValue(
                (row) => SetRowComment(row, comment, string.IsNullOrEmpty(comment) ? TranslationStatus.None : TranslationStatus.Problems, auto),
                key, pKey, path);
        }
        int ChangeRowValue(Func<DataRow, bool> change, string key, string pKey = null, string path = null) {
            int result = 0;
            foreach(DataRow row in this.Rows) {
                bool keysAreEqual = $"{row[colEnglish]}".Trim() == key.Trim();
                if(keysAreEqual) {
                    if(!string.IsNullOrEmpty(pKey)
                        && (pKey != $"{row[colKey]}" || path != $"{row[colPath]}")) continue;
                    if(change(row)) {
                        row[colSessionChanged] = true;
                        row[colUser] = Settings.User; //TODO
                        result++;
                    }
                }
            }
            return result;
        }
        bool SetRowValue(DataRow row, string @value, TranslationStatus status) {
            row[colTranslate] = @value;
            row[colStatus] = status;
            return true;
        }
        bool SetRowComment(DataRow row, string @value, TranslationStatus status, bool auto = true) {
            row[colComment] = @value;
            if(auto && ((status == TranslationStatus.None && IsStatusExist(row[colStatus],
                [TranslationStatus.Problems])) || status == TranslationStatus.Problems))
                row[colStatus] = status;
            return true;
        }
        bool IsGroupRow(BarItemLink link) {
            var info = UIHelper.GetGridHitInfoByLink(link);
            return info.InGroupRow;
        }
        public string GetKeyByLink(BarItemLink link, GridView view) {
            if(IsGroupRow(link))
                return null;
            var row = UIHelper.GetDataRowByLink(link, view);
            if(row == null) return null;
            return $"{row[colKey]}";
        }
        public string GetPathByLink(BarItemLink link, GridView view) {
            if(IsGroupRow(link))
                return null;
            var row = UIHelper.GetDataRowByLink(link, view);
            if(row == null) return null;
            return $"{row[colPath]}";
        }
        public string GetEnglishKeyByLink(BarItemLink link, GridView view) {
            var row = UIHelper.GetDataRowByLink(link, view);
            if(row == null) return null;
            return $"{row[colEnglish]}";
        }
        public string GetEnglishKey(int rowHandle, GridView view) {
            var row = UIHelper.GetDataRow(rowHandle, view);
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
                int iEndRow = view.GetChildRowHandle(rowHandle, view.GetChildRowCount(rowHandle) - 1);
                result = GetStatus(iStartRow, view);
                if(result == TranslationStatus.None) return result;
                for(int i = iStartRow + 1; i <= iEndRow; i++)
                    if(result != GetStatus(i, view)) return TranslationStatus.None;
                return result;
            }
            return TranslationStatus.None;
        }
        public TranslationDe GetTranslationObjectByRow(int rowHandle, GridView view) {
            var row = UIHelper.GetDataRow(rowHandle, view);
            if(row == null) return new TranslationDe();
            if(!view.IsGroupRow(rowHandle)) {
                return new TranslationDe($"{row[colTranslate]}", $"{row[colEnglish]}", $"{row[colGerman]}",
                    $"{row[colPath]}", $"{row[colKey]}", $"{row[colRussian]}", $"{row[colComment]}");
            }
            else {
                TranslationDe result = new(string.Empty, $"{row[colEnglish]}");
                int iStartRow = view.GetChildRowHandle(rowHandle, 0);
                int iEndRow = view.GetChildRowHandle(rowHandle, view.GetChildRowCount(rowHandle) - 1);
                string word = $"{view.GetDataRow(iStartRow)[colTranslate]}";
                for(int i = iStartRow; i <= iEndRow; i++) {
                    row = view.GetDataRow(i);
                    if(!string.IsNullOrEmpty(word) && !word.Equals($"{row[colTranslate]}"))
                        word = string.Empty;
                    result.AddUserTranslation($"{row[colGerman]}");
                }
                result.Translation = word;
                return result;
            }
        }
        public TranslationDe GetTranslationObjectByRow(BarItemLink link, GridView view) {
            GridHitInfo info = UIHelper.GetGridHitInfoByLink(link);
            return GetTranslationObjectByRow(info.RowHandle, view);
        }
        internal void BeginDataSave(GridView view) {
            view.GridControl.FindForm().Cursor = Cursors.WaitCursor;
            view.BeginDataUpdate();
            this.Columns.Remove(colSessionChanged);
        }
        internal void EndDataSave(GridView view) {
            this.Columns.Add(colSessionChanged);
            view.EndDataUpdate();
            view.GridControl.FindForm().Cursor = Cursors.Default;
        }
        bool IsEmptyStatus(TranslationStatus status) {
            return status == TranslationStatus.None ||
                status == TranslationStatus.NoTranslationNeeded ||
                status == TranslationStatus.NotSure ||
                status == TranslationStatus.Problems;
        }
        public List<SimpleTranslation> GetTranslationData(bool onlyDifferentValues = false) { //Main source for resx
            var result = new List<SimpleTranslation>();
            foreach(DataRow row in this.Rows) {
                //if(IsEmptyStatus((TranslationStatus)row[colStatus])) continue;
                //Or
                if(((TranslationStatus)row[colStatus]) != TranslationStatus.Translated) continue;
                if(string.IsNullOrEmpty($"{row[colUser]}")) continue;
                //TODO test if($"{row[colUser]}".IndexOf("John") == -1) continue;
                string translation = $"{row[colTranslate]}";
                if(!LocalizationHelper.ValueExist(translation)) continue;
                if(onlyDifferentValues && $"{row[colGerman]}" == translation) continue;
                result.Add(new SimpleTranslation(
                    $"{row[colPath]}",
                    $"{row[colKey]}",
                    $"{row[colEnglish]}",
                    translation,
                    $"{row[colUser]}"
                    ));
            }
            return [.. result.OrderBy(x => x.Path)];
        }
        readonly List<string> merge = [];
        public int MergeData(List<SimpleTranslationDe> fromData, string logName) {
            merge.Clear();
            int count = 0;
            foreach(DataRow row in Rows) {
                SimpleTranslationDe data = fromData.Where(x => x.Path.Equals($"{row[colPath]}") && x.Key.Equals($"{row[colKey]}")).FirstOrDefault();
                if(data != null)
                    count += MergeRow(row, data) ? 1 : 0;
            }
            IOHelper.SaveLog(merge, logName);
            return count;
        }
        public int ImportData(List<NewTranslation> fromData, string logName) {
            merge.Clear();
            fromData.ForEach(row => {
                DataRow newRow = Rows.Add([
                    row.Path, row.Key, row.English, string.Empty, row.German, row.Russian, 0
                ]);
                newRow[colInternalInfo] = row.User;
                ImportRow(row);
            });
            if(merge.Count > 0)
                IOHelper.SaveLog(merge, logName);
            return merge.Count;
        }
        public int RemoveExtraData(List<SimpleTranslationDe> fromData, string logName) {
            merge.Clear();
            fromData.ForEach(row => {
                DataRow dRow = GetDataRow(row.Key, row.Path, row.English);
                if(dRow != null) {
                    if(!(row.Status == TranslationStatus.Translated && row.IsUserExists)) {
                        Rows.Remove(dRow);
                        RemoveRow(row);
                    }
                    else {
                        if(GetTranslatedDataRowCount(row.English) < 2 &&
                            LocalizationTranslation.IsLegalKey(row.Key)) {
                            if(string.IsNullOrEmpty($"{dRow[colInternalInfo]}"))
                                dRow[colInternalInfo] = $"Del[{Settings.FormattedToday}]";
                        }
                        else {
                            Rows.Remove(dRow);
                            RemoveRow(row);
                        }
                    }
                }
            });
            if(merge.Count > 0)
                IOHelper.SaveLog(merge, logName);
            return merge.Count;
        }
        DataRow GetDataRow(string key, string path, string english) {
            foreach(DataRow row in Rows)
                if(key.Equals(row[colKey]) &&
                    path.Equals(row[colPath]) &&
                    english.Equals(row[colEnglish]))
                    return row;
            return null;
        }
        int GetTranslatedDataRowCount(string english) {
            int count = 0;
            foreach(DataRow row in Rows)
                if(english.Equals(row[colEnglish]) &&
                    ((int)TranslationStatus.Translated).Equals(row[colStatus]) &&
                    !string.IsNullOrEmpty($"{row[colUser]}"))
                    count ++;
            return count;
        }
        bool MergeRow(DataRow row, SimpleTranslationDe data) {
            TranslationStatus status = (TranslationStatus)row[colStatus];
            string english = $"{row[colEnglish]}";
            string translation = $"{row[colTranslate]}";
            string comment = $"{row[colComment]}";
            string user = $"{row[colUser]}";
            if(status == data.Status && translation == data.Translation && comment == data.Comment) {
                row[colUser] = data.User;
                merge.Add($"Verify Translation: {status} '{english}' - nothing changed");
                return true; //change user only
            }
            if(data.Status == TranslationStatus.None) {
                merge.Add($"Error: {status}→{data.Status} '{english}'");
                return false;
            }
            row[colStatus] = data.Status;
            if(!string.IsNullOrEmpty(data.Translation))
                row[colTranslate] = data.Translation;
            if(!string.IsNullOrEmpty(data.Comment))
                row[colComment] = data.Comment;
            row[colUser] = data.User;
            row[colSessionChanged] = true;
            merge.Add($"{GetWarning(user)}{status}→{data.Status} '{english}' {GetLine(translation)}→{GetLine(data.Translation)}");
            return true;
        }
        void ImportRow(NewTranslation row) {
            merge.Add($"Added: {row.English} - {row.Key} - {row.Path}");
        }
        void RemoveRow(SimpleTranslationDe row) {
            merge.Add($"Remove: {row.English} - {row.Key} - {row.Path}");
        }
        string GetWarning(string user) {
            if(string.IsNullOrEmpty(user))
                return string.Empty;
            return $"Warning ({user} changed): ";
        }
        string GetLine(string line) {
            if(string.IsNullOrEmpty(line))
                return $"empty";
            return $"'{line}'";
        }
    }
    public class BaseTranslation(string path, string key, string english, string user) {
        public string Path { get; set; } = path;
        public string Key { get; set; } = key;
        public string English { get; set; } = english;
        public string User { get; set; } = user;
        internal bool IsUserExists => !string.IsNullOrEmpty(User);
    }
    public class SimpleTranslation(string path, string key, string english, string translation, string user) :
        BaseTranslation(path, key, english, user) {
        public string DePath { get; set; } = string.Empty;
        public string Translation { get; set; } = translation;
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
                if(IsException(Path)) return false;
                if(Path.Contains(@"DevExpress.DLL")) return false;
                if(Path.IndexOf(Settings.dX) == 0) return true;
                return false;
            }
        }
        internal static bool IsException(string path) {
            return path.IndexOf("~Localization", System.StringComparison.OrdinalIgnoreCase) > -1 ||
                path.IndexOf("_Localization", System.StringComparison.OrdinalIgnoreCase) > -1;
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
            list ??= GetSatelliteValues(satelliteKey);
            if(list.Count == 0) return Settings.fileNotFount;
            var elements = list.Find(element => element.Attribute("name").Value == key);
            if(elements == null) return Settings.keyNotFount;
            xml = $"{elements}";
            return elements.Value;
        }

        List<XElement> GetSatelliteValues(string satelliteKey) {
            var xdoc = Satellites.Where(si => {
                return si.Name == $"{ShortName}.{satelliteKey}.resx";
            }).ToList<LocalizationSatellite>();
            if(xdoc.Count == 0) return [];
            return [.. LocalizationHelper.GetAllElements(XDocument.Load(xdoc[0].FullName))];
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
                Translations = [];
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
        public long Length { get; }
    }
    public class LocalizationKey(XElement element) {
        public string Name { get; } = element.Attribute("name").Value;
        public string Value { get; } = LocalizationHelper.PrepareValue(element.Value);
        public string Xml { get; } = $"{element}";
        public string Type { get; } = element.Attribute("type")?.Value;
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
                if(IsProhibitedValues(German)) return false;
                if(Key.IndexOf(@">>") == 0) return false;
                //if(Key.IndexOf(@"stringid", StringComparison.OrdinalIgnoreCase) >= 0) return true; 
                //if(string.IsNullOrEmpty(Value)) return false;
                if(!IsLegalKey(Key)) return false;
                return true;
            }
        }
        internal static bool IsLegalKey(string key) { 
            //Some resources do not require translation, for example, spreadsheet functions and attributes must have the same translation as in Excel
            if(key.Contains("XtraSpreadsheetFunctionNameStringId")) return false;
            if(key.Contains("XtraSpreadsheetFunctionArgumentNameStringId")) return false;
            return true;
        }
        static readonly HashSet<string> prohibitedValues = ["This string has not yet been translated"];
        internal static bool IsProhibitedValues(string value) {
            foreach(var item in prohibitedValues)
                if(value.Contains(item)) return true;
            return false;
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
        internal string GermanValue => 
            LocalizationHelper.PrepareTranslation(German);
        public string Russian { get; internal set; }
        public string Japan { get; internal set; }
    }
    public class LocalizationRepDe(string name, int count, List<LocalizationTranslation> translations) {
        readonly List<LocalizationTranslation> data = translations;
        List<LocalizationTranslation> names = null;
        int count = -1;

        public List<KeyValuePair<string, int>> Keys {
            get {
                var keyValuePairs = new Dictionary<string, int>();
                Names.ForEach(t => {
                    string key = t.German;
                    if(keyValuePairs.ContainsKey(key))
                        keyValuePairs[key]++;
                    else keyValuePairs.Add(key, 1);
                });
                return [.. keyValuePairs.OrderByDescending(k => k.Value)];
            }
        }
        public string Name { get; } = name;
        public int Count { get; } = count;
        public int TranslationCount {
            get {
                if(count == -1)
                    count = Names.Select(t => t.GermanValue).Distinct().Count();
                return count;
            }
        }
        public List<LocalizationTranslation> Names {
            get {
                names ??= [.. data.Where(t => t.English == Name
                        && LocalizationHelper.ValueExist(t.German)
                        /*&& t.German != t.English*/)];
                return names;
            }
        }
    }
}
