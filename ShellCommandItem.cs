using Quokka;
using Quokka.ListItems;
using System.Windows.Media.Imaging;

namespace Plugin_ShellCommand {
  class ShellCommandItem : ListItem {

    bool admin;
    string command;

    public ShellCommandItem(string command, bool admin) {
      this.Name = $"Run `{command}`";
      if (admin) Name += " with Admin Privileges";
      this.Description = "Run the command via PowerShell";
      this.Icon = new BitmapImage(new Uri(
          Environment.CurrentDirectory + "\\PlugBoard\\Plugin_ShellCommand\\Plugin\\shell.png"));
      this.admin = admin;
      this.command = command;
    }

    public override void Execute() {
      var processInfo = new System.Diagnostics.ProcessStartInfo {
        Verb = admin ? "runas" : "",
        LoadUserProfile = true,
        FileName = "powershell.exe",
        Arguments = command,
        RedirectStandardOutput = false,
        UseShellExecute = true,
        CreateNoWindow = true
      };
      System.Diagnostics.Process.Start(processInfo);
      App.Current.MainWindow.Close();
    }
  }

}
