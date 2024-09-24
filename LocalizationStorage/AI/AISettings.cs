using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace LocalizationStorage.AI {
    public partial class AISettingsForm : XtraForm {
        public AISettingsForm() {
            InitializeComponent();
            textEdit1.Text = AISettings.Default.AzureOpenAIEndpoint;
            textEdit2.Text = AISettings.Default.AzureOpenAIKey;
            memoEdit1.Text = AISettings.Default.AIRequest;
        }
        protected override bool ProcessDialogKey(Keys keyData) {
            Close();
            return false;
        }
    }
}
