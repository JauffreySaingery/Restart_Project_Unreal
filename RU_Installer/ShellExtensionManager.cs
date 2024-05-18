using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RU_Installer
{
    internal class ShellExtensionManager
    {

        private static string UnrealLocaction = $@"\Software\Classes\Unreal.ProjectFile\shell\rungenproj";

        //private static string clsid = "{D93E9D52-1FE9-4E95-B144-98D8D34EDCE9}";
        private static string friendlyName = "Restart Project Unreal";
        private static string keyName = $@"Software\Classes\Unreal.ProjectFile\shell\projrestart";

        //Unreal.ProjectFile\shell

        public static void RegisterShellExtContextMenuHandler()
        {
            var rootName = Registry.LocalMachine;
            var IconLocation = Registry.GetValue(rootName + UnrealLocaction, "Icon","DefaultValueXXX");
            //var CommandLocation = Registry.GetValue(rootName + UnrealLocaction, "Icon","DefaultValueXXX");
            Console.WriteLine(IconLocation);
            using (var key = rootName.CreateSubKey(keyName))
            {
                key?.SetValue(null, friendlyName);
                key?.SetValue("Icon", IconLocation);
                using (var command = key?.CreateSubKey("command"))
                {
                    command?.SetValue(null, "\""+MainForm.crtDir+"\\Restart_Project_Unreal.exe\" /restart \"%1\"");
                }
            }
        }

        public static void UnregisterShellExtContextMenuHandler()
        {
            var rootName = Registry.LocalMachine;

            rootName.DeleteSubKeyTree(keyName);
        }

        public static bool IsInstalled(bool allUsers)
        {
            var rootName = allUsers ? Registry.LocalMachine : Registry.CurrentUser;

            return rootName.OpenSubKey(keyName, false) != null;
        }
    }
}
