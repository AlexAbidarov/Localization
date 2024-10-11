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
        static Program() {
            try {
                string packagesDir = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "Packages");
                if(!Directory.Exists(packagesDir))
                    assemblyResolving.Add($"{packagesDir} not found.");
                AppDomain.CurrentDomain.AssemblyResolve += (context, assembly) => {
                    string path = Path.Combine(packagesDir,
                        $@"{IOHelper.GetShortAssembluName(assembly.Name)}.dll");
                    bool fileExist = File.Exists(path);
                    assemblyResolving.Add($"{path} - {fileExist}");
                    if(fileExist)
                        return Assembly.LoadFrom(path);
                    return null;
                };
            } catch(Exception ex) {
                assemblyResolving.Add($"{ex.Message}");
            }
        }
        [STAThread]
        static void Main(string[] args) {
            try {
                Application.ApplicationExit += (s, e) => SaveLog();
                RunApp(args);
            } catch {
                MessageBox.Show(IOHelper.GetLog(assemblyResolving), "Error");
                SaveLog();
            }
        }
        static void SaveLog() {
            IOHelper.SaveLog(assemblyResolving, "AssemblyResolving.txt");
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
