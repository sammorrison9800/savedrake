using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using updater;

class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // List of required DLLs
        string[] requiredDlls = {
                "Newtonsoft.Json.dll",       
            };

        foreach (var dll in requiredDlls)
        {
            if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dll)))
            {
                MessageBox.Show($"The required DLL '{dll}' is missing.", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }

        // Add the event handler for resolving missing assemblies
        AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

        Application.Run(new UpdaterForm());
    }

    private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
    {
        string[] requiredDlls = {
                 "Newtonsoft.Json.dll",
            };
        // Check if the assembly name matches any of the required DLLs
        foreach (var dll in requiredDlls)
        {
            if (args.Name.Contains(dll))
            {
                MessageBox.Show($"The required DLL '{dll}' could not be loaded.", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }
        return null;
    }


}