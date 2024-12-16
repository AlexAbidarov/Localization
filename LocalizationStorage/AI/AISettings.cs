using DevExpress.XtraEditors;
using System.ComponentModel;
using System.Windows.Forms;

namespace LocalizationStorage.AI {
    public partial class AISettingsForm : XtraForm {
        public AISettingsForm() {
            InitializeComponent();
            textEdit1.Text = AISettings.Default.AzureOpenAIEndpoint;
            textEdit2.Text = AISettings.Default.AzureOpenAIKey;
            memoEdit1.Text = AISettings.Default.AIRequest;
            ceGerman.DataBindings.Add("Checked", AISettings.Default, "UseExistingGerman");
            ceRussian.DataBindings.Add("Checked", AISettings.Default, "UseExistingRussian");
            seTemperature.DataBindings.Add("EditValue", AISettings.Default, "Temperature");
        }
        protected override bool ProcessDialogKey(Keys keyData) {
            if(keyData == Keys.Escape)
                Close();
            return false;
        }
        protected override void OnClosing(CancelEventArgs e) {
            ceGerman.DoValidate();
            ceRussian.DoValidate();
            seTemperature.DoValidate();
            base.OnClosing(e);
        }
    }
}
