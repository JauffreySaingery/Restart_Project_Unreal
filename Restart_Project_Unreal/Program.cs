// See https://aka.ms/new-console-template for more information
using Microsoft.Win32;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;

namespace Restart_Project_Unreal
{
    internal static class Program
    {

        private static string UnrealLocaction = $@"\Software\Classes\Unreal.ProjectFile\shell\rungenproj";


        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            if (args.Length >= 1)
            {
                if (args[0] == "/restart")
                {
                    RestartPorject(args[1]);
                }
            }
            else
                Console.WriteLine("Try rigth clicking on a .uproject or pass a path as an argument with the option /restart");
            Console.ReadLine();
        }


        static void RestartPorject(string ProjectPath)
        {
            var rootName = Registry.LocalMachine;
            var unrealSelectionLoaction = Registry.GetValue(rootName + UnrealLocaction, "Icon", "egg");

            List<string> temp = new List<string>(ProjectPath.Split('\\'));
            string projectName = temp[temp.Count - 1];
            temp.RemoveAt(temp.Count - 1);
            string ProjectDirectory = String.Join("\\", temp.ToArray());

            projectName = projectName.Split(".")[0];

            string UnrealEnginePath = "";

            try
            {
                //Deleteting all the files that are anoying
                if (Directory.Exists(ProjectDirectory + "\\.vs"))
                    Directory.Delete(ProjectDirectory + "\\.vs",true);

                if (Directory.Exists(ProjectDirectory + "\\Binaries"))
                    Directory.Delete(ProjectDirectory + "\\Binaries", true);

                if (Directory.Exists(ProjectDirectory + "\\Build"))
                    Directory.Delete(ProjectDirectory + "\\Build", true);

                if (Directory.Exists(ProjectDirectory + "\\DerivedDataCache"))
                    Directory.Delete(ProjectDirectory + "\\DerivedDataCache", true);

                if (Directory.Exists(ProjectDirectory + "\\Intermediate"))
                    Directory.Delete(ProjectDirectory + "\\Intermediate", true);
                
                if(File.Exists(ProjectDirectory + "\\" + projectName + ".sln"))
                {
                    foreach (string line in File.ReadLines(ProjectDirectory + "\\" + projectName + ".sln"))
                    {
                        if (line.Contains("Engine\\Source\\Programs\\"))
                        {
                            UnrealEnginePath = line;
                            UnrealEnginePath = UnrealEnginePath.Split(",")[1];
                            UnrealEnginePath = UnrealEnginePath.Split("\\Source")[0];
                            UnrealEnginePath = UnrealEnginePath.Replace(" ", "");
                            break;
                        }
                    }
                    File.Delete(ProjectDirectory + "\\" + projectName + ".sln");
                }


                string ArgLocation = "\"" + ProjectPath + "\"";
                //Regenerate the compelete project
                string GenerateVSprojectfiles = "\""+ unrealSelectionLoaction + "" + " /projectfiles " + ArgLocation+"\"";
                string strCmdGenerateVS = "/C " + GenerateVSprojectfiles;

                string BuildProjectfiles = "\"" + UnrealEnginePath + "\\Binaries\\DotNET\\UnrealBuildTool\\UnrealBuildTool.exe\"" + " Development Win64 -Project=" + ArgLocation + " -TargetType=Editor -Progress -NoHotReloadFromIDE \"";
                string strCmdBuildPF = "/C " + BuildProjectfiles;


                string strCmdLanchUnreal = "/C " + ArgLocation;

                
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                cmd.StartInfo.Arguments = strCmdGenerateVS;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.Start();
                cmd.WaitForExit();


                cmd.StartInfo.Arguments = strCmdLanchUnreal;
                cmd.Start();

                
                //System.Diagnostics.Process.Start("cmd.exe", strCmdGenerateVS).WaitForExit();
                //System.Diagnostics.Process.Start("cmd.exe", strCmdLanchUnreal);
                Environment.Exit(0);
                //System.Diagnostics.Process.Start("cmd.exe", strCmdBuildPF).WaitForExit();


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Some files seems to be opened by an other application, Please close the aplication and type anything to retry.");
                Console.ReadLine();
                RestartPorject(ProjectPath);
            }

        }
    }
}