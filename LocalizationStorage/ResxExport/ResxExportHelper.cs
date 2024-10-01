using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace LocalizationStorage.ResxExport {
    public class ResxExportHelper {
        public static bool SetXValue(FileInfo fi, string key, string translation, bool checkValue = true) {
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
        public static void CreateSatellite(string root, string path, string tPath) { 
            //TODO
        }
    }
}
