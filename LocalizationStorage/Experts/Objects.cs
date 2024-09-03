using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace LocalizationStorage {
    public class TranslatinUserControl : XtraUserControl {
        public TranslatinUserControl() {
        }
        protected virtual void UpdateInfo(TranslationDe info) { }
        public virtual string Translation => string.Empty;
        public virtual string English => string.Empty;
        public virtual string Comment => string.Empty;
        public virtual string Key => null;
        public virtual string Path => null;
    }
    public class CommentForm : TranslationForm {
        public CommentForm(Form owner, TranslationDe info) : base(owner, info) {
        }
        protected override TranslatinUserControl CreateRowControl(TranslationDe info) {
            return new CommentControl(info);
        }
    }
}
