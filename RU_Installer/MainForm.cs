using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Security.Principal;


namespace RU_Installer
{
    public partial class MainForm : Form
    {

        public static readonly string crtDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Text += " - V";//+ GetLEVersion();

            //DeleteOldFiles();
            //ReplaceDll();

            AddShieldToButton(button_Install_AdminUser);
            AddShieldToButton(button_Uninstall_AdminUser);

            button_Uninstall_AdminUser.Enabled = ShellExtensionManager.IsInstalled(true);
;
        }

        private void button_Install_AdminUser_Click(object sender, EventArgs e)
        {
            if (!IsAdministrator())
            {
                MessageBox.Show(this, "Please run this application as administrator and try again.",
                    "Restart Project Unreal Installer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                RunAsAdmin("--InstallAll");
                return;
            }

            DoRegister();

            NotifyShell();

            MessageBox.Show(this, "Install finished :)\r\n",
                "Restart Project Unreal Installer",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            button_Uninstall_AdminUser.Enabled = true;
        }

        private void button_Uninstall_AdminUser_Click(object sender, EventArgs e)
        {
            DoUnRegister();

            NotifyShell();

            MessageBox.Show(this, "Uninstall finished \r\n",
                "Restart Project Unreal Installer",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            button_Uninstall_AdminUser.Enabled = false;
        }

        static internal void AddShieldToButton(Button b)
        {
            const uint BCM_FIRST = 0x1600; //Normal button
            const uint BCM_SETSHIELD = (BCM_FIRST + 0x000C); //Elevated button

            b.FlatStyle = FlatStyle.System;
            SendMessage(b.Handle, BCM_SETSHIELD, 0, 0xFFFFFFFF);
        }

        private void RunAsAdmin(string Arguments = "")
        {
            var startup = new ProcessStartInfo();
            startup.WindowStyle = ProcessWindowStyle.Normal;
            startup.UseShellExecute = true;
            startup.WorkingDirectory = Environment.CurrentDirectory;
            startup.Arguments = Arguments;
            startup.FileName = Application.ExecutablePath;
            startup.Verb = "runas";

            try
            {
                using (var proc = Process.Start(startup))
                {
                    Environment.Exit(0);
                    return;
                }
            }
            catch (SystemException)
            {
                MessageBox.Show(this, "Error with Launching Application as administrator\r\n" +
                    "\r\n" +
                    "Please run this application as administrator and try again.",
                    "LE Context Menu Installer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
        }


        private void DoRegister()
        {
            try
            {

                ShellExtensionManager.RegisterShellExtContextMenuHandler();
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message + "\r\n\r\n" + e.StackTrace);
            }
        }

        private void DoUnRegister()
        {
            try
            {
                ShellExtensionManager.UnregisterShellExtContextMenuHandler();
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message + "\r\n\r\n" + e.StackTrace);
            }
        }

        private void NotifyShell()
        {
            const uint SHCNE_ASSOCCHANGED = 0x08000000;
            const ushort SHCNF_IDLIST = 0x0000;

            SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);
        }

        public static bool IsAdministrator()
        {
            var wp = new WindowsPrincipal(WindowsIdentity.GetCurrent());

            return wp.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private static bool Is64BitOS()
        {
            if (UIntPtr.Size == 8) // 64-bit programs run only on Win64
            {
                return true;
            }
            // Detect whether the current process is a 32-bit process 
            // running on a 64-bit system.
            bool flag;
            return DoesWin32MethodExist("kernel32.dll", "IsWow64Process") &&
                   IsWow64Process(GetCurrentProcess(), out flag) && flag;
        }

        private static bool DoesWin32MethodExist(string moduleName, string methodName)
        {
            var moduleHandle = GetModuleHandle(moduleName);
            if (moduleHandle == UIntPtr.Zero)
            {
                return false;
            }
            return GetProcAddress(moduleHandle, methodName) != UIntPtr.Zero;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int DeleteFile(string name);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern void SetLastError(int errorCode);

        [DllImport("shell32.dll", SetLastError = true)]
        private static extern void SHChangeNotify(uint wEventId, ushort uFlags, IntPtr dwItem1, IntPtr dwItem2);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern UIntPtr GetCurrentProcess();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern UIntPtr GetModuleHandle(string moduleName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern UIntPtr GetProcAddress(UIntPtr hModule, [MarshalAs(UnmanagedType.LPStr)] string procName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process(UIntPtr hProcess, out bool wow64Process);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        private static extern int RegOpenKeyEx(UIntPtr hKey, string subKey, int ulOptions, uint samDesired,
            out UIntPtr hkResult);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern int RegOverridePredefKey(UIntPtr hKey, UIntPtr hNewKey);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern int RegCloseKey(UIntPtr hKey);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern UInt32 SendMessage(IntPtr hWnd, UInt32 msg, UInt32 wParam, UInt32 lParam);

        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

       
    }
}
