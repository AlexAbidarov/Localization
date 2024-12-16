using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace LocalizationStorage {
    internal static class CSVHelper {
        static Type GetTypeByName(string name) {
            string[] dates = new string[] { "creation_date", "fixed_on", "closed_on" };
            if(dates.Contains(name)) return typeof(DateOnly);
            return typeof(string);
        }
        static bool IsId(string name) {
            string[] ids = new string[] { "ticket_scid" };
            return ids.Contains(name);
        }
        public static DataTable ConvertCSVtoDataTable(FileInfo fi) {
            DataTable dt = new DataTable();
            HashSet<string> id = new HashSet<string>();
            int idIndex = -1;
            using(StreamReader sr = new StreamReader(fi.FullName)) {
                string[] headers = SplitCSV(sr.ReadLine());
                for(int i = 0; i < headers.Length; i++) {
                    string header = headers[i];
                    if(IsId(header)) idIndex = i;
                    dt.Columns.Add(header, GetTypeByName(header));
                }
                while(!sr.EndOfStream) {
                    string[] rows = SplitCSV(sr.ReadLine());
                    DataRow dr = dt.NewRow();
                    for(int i = 0; i < headers.Length; i++) {
                        if(i >= rows.Length) continue;
                        string val = rows[i];
                        if(rows.Length > i) {
                            Type type = GetTypeByName(headers[i]);
                            if(type == typeof(DateOnly)) {
                                if(!string.IsNullOrEmpty(val))
                                    dr[i] = DateOnly.Parse(val);
                            }
                            else
                                dr[i] = val;
                        }
                    }
                    if(idIndex != -1 && !id.Contains($"{dr[idIndex]}")) {
                        dt.Rows.Add(dr);
                        id.Add($"{dr[idIndex]}");
                    }
                    else
                        if(idIndex == -1) dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        static string[] SplitCSV(string line) {
            List<string> result = new List<string>();
            line = line.Trim();
            StringBuilder res = new StringBuilder(string.Empty);
            int quote = 0;
            int index = 0;
            foreach(Char c in line) {
                if(c == '"') quote++;
                if(c == ',' && quote % 2 == 0) {
                    result.Add(GetResult(res));
                    res.Clear();
                }
                else
                    res.Append(c);
                if(++index == line.Length)
                    result.Add(GetResult(res));
            }
            return result.ToArray();
        }
        static string GetResult(StringBuilder builder) {
            string val = builder.ToString();
            val = val.Trim();
            val = val.Replace("English: ", string.Empty);
            if(string.IsNullOrEmpty(val))
                return string.Empty;
            if(val.Substring(0, 1) == "\"" &&
                val.Substring(val.Length - 1, 1) == "\"") {
                val = val.Substring(1, val.Length - 2);
            }
            val = val.Replace("\"\"", "\"");
            return val;
        }
    }
}
