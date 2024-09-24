using DevExpress.Utils;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraWaitForm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace LocalizationStorage {
    internal static class Program {
        static List<string> assemblyResolving = new List<string>();
        [STAThread]
        static void Main(string[] args) {
            AppDomain.CurrentDomain.AssemblyResolve += (context, assembly) => {
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
                    $@"Packages\{IOHelper.GetShortAssembluName(assembly.Name)}.dll");
                bool fileExist = File.Exists(path);
                assemblyResolving.Add($"{path} - {fileExist}");
                if(fileExist)
                    return Assembly.LoadFrom(path);
                return null;
            };
            Application.ApplicationExit += (s, e) => IOHelper.SaveLog(assemblyResolving, "AssemblyResolving.txt");
            RunApp(args);
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        static void RunApp(string[] args) {
            SplashScreenManager.ShowForm(typeof(DemoWaitForm));
            if(args.Length > 0)
                Settings.RootPath = args[0];
            Settings.SetUser(args);
            //if(!LocalizationHelper.IsExistRoot)
            //    Settings.RootPath = @"D:\Work\v24.2\Localization";
            AppearanceObject.DefaultFont = new System.Drawing.Font("Calibry", 9);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if(LocalizationHelper.IsExistRoot) {
                //XtraMessageBox.Show("Localization path is not found.\r\nUse the first argument in the command line arguments.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Run(new MainForm());
            }
            else
                Application.Run(new ClientForm());
        }
    }
}
