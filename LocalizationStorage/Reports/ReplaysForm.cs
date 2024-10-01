using DevExpress.Data;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LocalizationStorage.Reports {
    public partial class ReplaysForm : XtraForm {
        List<LocalizationTranslation> data = null;
        List<LocalizationRepDe> replications = new List<LocalizationRepDe>();
        public ReplaysForm(Form owner) {
            InitializeComponent();
            Location = UIHelper.GetCenterPoint(owner, Size);
            data = Settings.Keys
                .Where(k => !string.IsNullOrEmpty(k.English) && k.English.Length > 1)
                .ToList();
        }

        private void ReplaysForm_Load(object sender, System.EventArgs e) {
            var keyValuePairs = new Dictionary<string, int>();
            data.ForEach(t => { 
                string key = t.English;
                if(keyValuePairs.ContainsKey(key))
                    keyValuePairs[key]++;
                else keyValuePairs.Add(key, 1);
            });
            var grouping = keyValuePairs
                .Where(k => k.Value > 1)
                .OrderByDescending(k => k.Value);
            
            grouping.ForEach(k => 
                replications.Add(new LocalizationRepDe(k.Key, k.Value, data))
            );
            gridControl1.DataSource = replications;
        }

        private void simpleButton1_Click(object sender, System.EventArgs e) {
            LoadFilterData();
        }
        void LoadFilterData() {
            ElapsedTime.Start();
            Cursor.Current = Cursors.WaitCursor;
            gridView1.BeginDataUpdate();
            try {
                int val = 0;
                replications.ForEach(r => val += r.TranslationCount);
                GridColumn tCol = gridView1.Columns["TranslationCount"];
                gridView1.ActiveFilter.Add(tCol, new ColumnFilterInfo($"[{tCol.FieldName}] > 1"));
                tCol.SortOrder = ColumnSortOrder.Descending;
                gridView1.FocusedRowHandle = 0;
            } finally {
                gridView1.EndDataUpdate();
                Cursor.Current = Cursors.Default;
                ElapsedTime.Stop();
            }
            simpleButton1.Text = $"Load/Filter Data({ElapsedTime.GetNuGetTime()})";
        }

        private void simpleButton2_Click(object sender, System.EventArgs e) {
            LoadFilterData();
            if(File.Exists(Settings.replyFile)) File.Delete(Settings.replyFile);
            using(StreamWriter file = new StreamWriter(Settings.replyFile)) {
                int num = 0;
                replications
                    .Where(k => k.TranslationCount > 1)
                    .OrderByDescending(k => k.TranslationCount)
                    .ForEach(r => {
                        file.WriteLine($"English: {r.Name}({r.Count}/{r.TranslationCount})");
                        num = 0;
                        r.Keys.ForEach(k => file.WriteLine($"    {++num}. {k.Key}({k.Value})")
                        );
                    });
            }
        }
    }
}
