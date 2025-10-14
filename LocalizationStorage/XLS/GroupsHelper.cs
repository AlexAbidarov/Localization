using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LocalizationStorage.XLS {
    public enum Team {
        None = 0,
        AI = 1,
        Web = 2,
        Win = 4,
        Wpf = 8,
        Xaf = 16,
        Office = 32,
        DataPresentation = 64,
        Service = 128
    }
    public static class GroupsHelper {
        public static Team GetTeamByName(string directoryName) {
            string name = LocalizationHelper.GetFirstChildDirectoryName(directoryName);
            if(!name.Contains(Settings.DX) || StartWith(name, ["DLL", "Key"]))
                return Team.Service; //Resources in these folders are not counted.
            if(StartWith(name, ["AI"]))
                return Team.AI;
            if(StartWith(name, ["Blazor", "Web"]))
                return Team.Web;
            if(StartWith(name, ["Xpf", "Diagram"]))
                return Team.Wpf;
            if(StartWith(name, ["ExpressApp", "EntityFramework", "Xpo"]))
                return Team.Xaf;
            if(StartWith(name, ["Dashboar", "DataAccess", "Drawing", "Report", "Printing"]))
                return Team.DataPresentation;
            if(StartWith(name, ["Docs", "Office", "Pdf", "RichEdit", "Snap", "Spreadsheet", "Presentation"]))
                return Team.Office;
            if(StartWith(name, [Settings.Xtra, "Data", "Utils", "Dialogs", "Map", "Mvvm", "Pivot", "Sparkline", "Tutorials", "Video", "Chart"]))
                return Team.Win;
            return Team.None;
        }
        static bool StartWith(string name, string[] names) {
            bool hasResult = false;
            for(int i = 0; i < names.Length; i++) {
                string line = names[i];
                if(string.IsNullOrEmpty(line)) continue;
                if(name.StartsWith($"{Settings.DX}.{line}", StringComparison.OrdinalIgnoreCase))
                    return true;
                if(line.StartsWith(Settings.Xtra))
                    hasResult = true;
                else
                    names[i] = $"{Settings.Xtra}{line}";
            }
            if(hasResult)
                return false;
            return StartWith(name, names);
        }
        internal static string GetProductByPath(string value, bool shortNames = false) {
            if(string.IsNullOrWhiteSpace(value))
                return string.Empty;

            foreach(var (pattern, prefix) in GetRegexProductList(shortNames)) {
                var match = Regex.Match(value, pattern);
                if(match.Success)
                    return $"{prefix}{match.Groups["Product"].Value}";
            }

            return string.Empty;
        }
        static List<(string pattern, string prefix)> GetRegexProductList(bool shortNames) {
            var result = new List<(string pattern, string prefix)>();
            if(shortNames) {
                result.Add(($@"^{Settings.DX}\.ExpressApp\\{Settings.DX}\.(?<Product>[^\\/]+)", string.Empty));
                result.Add(($@"^{Settings.DX}\.(?<Product>[^\\/]+)", string.Empty));
            }
            else {
                result.Add(($@"^{Settings.DX}\.ExpressApp\\(?<Product>[^\\/]+)", string.Empty));
                result.Add(($@"^(?<Product>[^\\/]+)", string.Empty));
            }
            return result;
        }
    }
    public class ImageCounter {
        readonly Dictionary<string, int> imageCountByExtension = [];
        public ImageCounter() {
        } //TODO add used images
        public string AddImage(string className) {
            className = CleanString(className);
            if(imageCountByExtension.TryGetValue(className, out int value))
                imageCountByExtension[className] = ++value;
            else
                imageCountByExtension[className] = 1;
            return $"{className}_{imageCountByExtension[className]:000}.png";
        }
        static string CleanString(string input) {
            string withoutParentheses = Regex.Replace(input, @"\s*\(.*?\)\s*", "");
            string cleaned = withoutParentheses.Replace(" ", "");

            return cleaned;
        }

    }
}
