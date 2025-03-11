using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LocalizationStorage.ResxExport {
    public static class ResxExportHelper {
        static bool UpdateValue(XDocument doc, string key, string translation) {
            var elements = LocalizationHelper.GetAllElements(doc);
            bool addNewKey = true;
            bool update = false;
            foreach(var element in elements) {
                if(element.Attribute("name").Value == key) {
                    addNewKey = false;
                    XElement e = element.Elements("value").First();
                    if(!string.Equals(GetCorrectValue(e.Value), GetCorrectValue(translation))) {//TODO Correction of values ​​containing spaces at the beginning and end of the translation
                        e.Value = translation;
                        update = true;
                    }
                }
            }
            if(addNewKey) {
                XElement newElement = new("data");
                newElement.SetAttributeValue("name", key);
                newElement.SetAttributeValue(XNamespace.Xml + "space", "preserve");
                newElement.Add(new XElement("value", translation));
                doc.Root.Add(newElement);
            }
            return addNewKey || update;
        }
        static string GetCorrectValue(string line) {
            line = line.Trim();
            line = line.Replace("\n", "");
            line = line.Replace("\r", "");
            return line;
        }
        static int SetXValue(FileInfo fi, string key, string translation) {
            XDocument doc = XDocument.Load(fi.FullName);
            bool update = UpdateValue(doc, key, translation);
            doc.Save(fi.FullName);
            return update ? 1 : 0;
        }
        static int SetXValues(XDocument doc, Dictionary<string, string> values) {
            int updateCount = 0;
            foreach(var item in values)
                if(UpdateValue(doc, item.Key, item.Value))
                    updateCount++;
            return updateCount;
        }
        internal static int ChangeXValue(string tPath, string key, string translation) {
            if(!string.IsNullOrEmpty(tPath)) {
                FileInfo fi = new(tPath);
                if(fi.Exists)
                    return SetXValue(fi, key, translation);
            }
            return 0;
        }
        internal static int ChangeXValues(string tPath, Dictionary<string, string> values) {
            if(!string.IsNullOrEmpty(tPath) && values.Count > 0) {
                FileInfo fi = new(tPath);
                if(fi.Exists) {
                    XDocument doc = XDocument.Load(fi.FullName);
                    int count = SetXValues(doc, values);
                    if(count > 0)
                        count -= LocalizationHelper.RemoveOutdatedKeys(fi.FullName, doc);
                    doc.Save(fi.FullName);
                    return count;
                }
            }
            return 0;
        }
        public static string CreateSatellite(string root, string path, string satteliteExt) {
            if(LocalizationPath.IsException(path)) return string.Empty;
            string keyPath = Path.Combine(root, path);
            if(!File.Exists(keyPath)) return string.Empty;
            XDocument doc = new();
            doc.Add(new XElement("root", string.Empty));
            doc.Root.Add(CreateResheaderElement("resmimetype", "text/microsoft-resx"));
            doc.Root.Add(CreateResheaderElement("version", "2.0"));
            doc.Root.Add(CreateResheaderElement("reader", "System.Resources.ResXResourceReader, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
            doc.Root.Add(CreateResheaderElement("writer", "System.Resources.ResXResourceWriter, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
            string satelliteName = LocalizationHelper.GetSatellitePath(root, path, satteliteExt, true);
            doc.Save(satelliteName);
            return satelliteName;
        }
        static XElement CreateResheaderElement(string name, string @value) {
            XElement result = new("resheader");
            result.SetAttributeValue("name", name);
            result.Add(new XElement("value", @value));
            return result;
        }
        internal static bool IsTestValue(string path) => true; //To internal use only
        //path.IndexOf(@"DevExpress.XtraEditors") >= 0 || path.IndexOf(@"DXFontEditor") >= 0; //TODO check all values
        internal static string GetFolder(string name, IWin32Window owner) {
            DirectoryInfo di = new(name);
            var openDialog = new FolderBrowserDialog() {
                SelectedPath = di.Exists ? di.FullName : AppDomain.CurrentDomain.BaseDirectory
            };
            if(openDialog.ShowDialog(owner) == DialogResult.OK)
                return openDialog.SelectedPath;
            return null;
        }
    }
}
