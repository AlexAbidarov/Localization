using DevExpress.XtraEditors;

namespace LocalizationStorage {
    public class TranslatinUserControl : XtraUserControl {
        public TranslatinUserControl() {
        }
        protected virtual void UpdateInfo(TranslationDe info) { }
        public virtual string Translation => string.Empty;
        public virtual string English => string.Empty;
        public virtual string Key => null;
        public virtual string Path => null;
    }
}
