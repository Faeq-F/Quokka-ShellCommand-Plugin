using Quokka;
using Quokka.ListItems;
using Quokka.PluginArch;

namespace PluginShellCommand
{
  class ShellCommandItem : ListItem
  {

    readonly bool admin;
    readonly string command;

    public ShellCommandItem(string command, bool admin)
    {
      Name = $"Run `{command}`";
      if (admin) Name += " with Admin Privileges";
      Description = "Run the command via PowerShell";
      Icon = IconCache.GetOrAdd(
            Environment.CurrentDirectory + "\\PlugBoard\\PluginShellCommand\\Plugin\\shell.png"
      );
      this.admin = admin;
      this.command = command;
    }

    public override void Execute()
    {
      var processInfo = new System.Diagnostics.ProcessStartInfo
      {
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
