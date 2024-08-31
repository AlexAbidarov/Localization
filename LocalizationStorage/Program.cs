using DevExpress.Utils;
using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
namespace LocalizationStorage {
    internal static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            if(args.Length > 0)
                Settings.RootPath = args[0];
            //if(!LocalizationHelper.IsExistRoot)
            //    Settings.RootPath = @"D:\Work\v24.2\Localization";
            AppearanceObject.DefaultFont = new System.Drawing.Font("Calibry", 9);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if(LocalizationHelper.IsExistRoot) {
                //XtraMessageBox.Show("Localization path is not found.\r\nUse the first argument in the command line arguments.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Run(new MainForm());
            }
            else {
                Application.Run(new ClientForm());
            }
        }
    }
}
