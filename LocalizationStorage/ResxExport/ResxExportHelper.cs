using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace LocalizationStorage.ResxExport {
    public class ResxExportHelper {
        static bool SetXValue(FileInfo fi, string key, string translation, bool checkValue = true) {
            if(fi.Exists && checkValue) {
                XDocument doc = XDocument.Load(fi.FullName);
                var elements = doc.Root.Elements("data").ToList<XElement>();
                bool addNewKey = true;
                foreach(var element in elements) {
                    if(element.Attribute("name").Value == key) {
                        addNewKey = false;
                        XElement e = element.Elements("value").First();
                        e.Value = translation;
                    }
                }
                if(addNewKey) {
                    XElement newElement = new XElement("data");
                    newElement.SetAttributeValue("name", key);
                    newElement.SetAttributeValue(XNamespace.Xml + "space", "preserve");
                    newElement.Add(new XElement("value", translation));
                    doc.Root.Add(newElement);
                }
                doc.Save(fi.FullName);
                return true;
            }
            return false;
        }
        internal static int ChangeXValue(string tPath, string key, string translation) {
            if(!string.IsNullOrEmpty(tPath)) {
                FileInfo fi = new FileInfo(tPath);
                if(SetXValue(fi, key, translation
                    //, fi.FullName.IndexOf(@"DevExpress.XtraEditors\Properties") >= 0 || fi.FullName.IndexOf(@"DXFontEditorControl") >= 0 //TODO check all values
                    ))
                    return 1;
            }
            return 0;
        }
        public static string CreateSatellite(string root, string path, string satteliteExt) {
            if(IsException(path)) return string.Empty;
            XDocument doc = new XDocument();
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
            XElement result = new XElement("resheader");
            result.SetAttributeValue("name", name);
            result.Add(new XElement("value", @value));
            return result;
        }
        static bool IsException(string path) {
            return path.IndexOf("~Localization", System.StringComparison.OrdinalIgnoreCase) > -1 ||
                path.IndexOf("_Localization", System.StringComparison.OrdinalIgnoreCase) > -1;
        }
    }
}
