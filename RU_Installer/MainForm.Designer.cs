namespace RU_Installer
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            MainPanel = new Panel();
            GroupBox_Admin = new GroupBox();
            button_Uninstall_AdminUser = new Button();
            button_Install_AdminUser = new Button();
            Label_InstallerExplanation = new Label();
            MainPanel.SuspendLayout();
            GroupBox_Admin.SuspendLayout();
            SuspendLayout();
            // 
            // MainPanel
            // 
            MainPanel.Controls.Add(GroupBox_Admin);
            MainPanel.Controls.Add(Label_InstallerExplanation);
            MainPanel.Dock = DockStyle.Fill;
            MainPanel.Location = new Point(0, 0);
            MainPanel.Name = "MainPanel";
            MainPanel.Size = new Size(384, 214);
            MainPanel.TabIndex = 0;
            // 
            // GroupBox_Admin
            // 
            GroupBox_Admin.Controls.Add(button_Uninstall_AdminUser);
            GroupBox_Admin.Controls.Add(button_Install_AdminUser);
            GroupBox_Admin.Dock = DockStyle.Top;
            GroupBox_Admin.Location = new Point(0, 140);
            GroupBox_Admin.Name = "GroupBox_Admin";
            GroupBox_Admin.Size = new Size(384, 74);
            GroupBox_Admin.TabIndex = 2;
            GroupBox_Admin.TabStop = false;
            GroupBox_Admin.Text = "All users";
            // 
            // button_Uninstall_AdminUser
            // 
            button_Uninstall_AdminUser.Location = new Point(197, 22);
            button_Uninstall_AdminUser.Name = "button_Uninstall_AdminUser";
            button_Uninstall_AdminUser.Size = new Size(175, 30);
            button_Uninstall_AdminUser.TabIndex = 1;
            button_Uninstall_AdminUser.Text = "Uninstall for all user";
            button_Uninstall_AdminUser.UseVisualStyleBackColor = true;
            button_Uninstall_AdminUser.Click += button_Uninstall_AdminUser_Click;
            // 
            // button_Install_AdminUser
            // 
            button_Install_AdminUser.Location = new Point(12, 22);
            button_Install_AdminUser.Name = "button_Install_AdminUser";
            button_Install_AdminUser.RightToLeft = RightToLeft.Yes;
            button_Install_AdminUser.Size = new Size(175, 30);
            button_Install_AdminUser.TabIndex = 0;
            button_Install_AdminUser.Text = "Install for all user";
            button_Install_AdminUser.UseVisualStyleBackColor = true;
            button_Install_AdminUser.Click += button_Install_AdminUser_Click;
            // 
            // Label_InstallerExplanation
            // 
            Label_InstallerExplanation.Dock = DockStyle.Top;
            Label_InstallerExplanation.Location = new Point(0, 0);
            Label_InstallerExplanation.Name = "Label_InstallerExplanation";
            Label_InstallerExplanation.Padding = new Padding(5);
            Label_InstallerExplanation.Size = new Size(384, 140);
            Label_InstallerExplanation.TabIndex = 0;
            Label_InstallerExplanation.Text = resources.GetString("Label_InstallerExplanation.Text");
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 214);
            Controls.Add(MainPanel);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "RPU_Installer";
            Load += MainForm_Load;
            MainPanel.ResumeLayout(false);
            GroupBox_Admin.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel MainPanel;
        private Label Label_InstallerExplanation;
        private GroupBox GroupBox_Admin;
        private Button button_Uninstall_AdminUser;
        private Button button_Install_AdminUser;
    }
}
