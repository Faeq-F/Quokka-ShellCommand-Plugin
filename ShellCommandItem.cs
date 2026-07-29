using Quokka.ListItems;
using Quokka.PluginArch;
using System.Diagnostics;
using System.Windows;

namespace PluginShellCommand
{
  internal sealed class ShellCommandItem : ListItem
  {

    private readonly bool admin;
    private readonly string command;

    public ShellCommandItem(string command, bool admin)
    {
      Name = $"Run `{command}`";
      if (admin)
      {
        Name += " with Admin Privileges";
      }
      Description = "Run the command via PowerShell";
      Icon = IconCache.GetOrAdd(
            Environment.CurrentDirectory + "\\PlugBoard\\PluginShellCommand\\Plugin\\shell.png"
      );
      this.admin = admin;
      this.command = command;
    }

    public override void Execute()
    {
      ProcessStartInfo processInfo = new()
      {
        Verb = admin ? "runas" : "",
        LoadUserProfile = true,
        FileName = "powershell.exe",
        Arguments = command,
        RedirectStandardOutput = false,
        UseShellExecute = true,
        CreateNoWindow = true
      };
      Process.Start(processInfo);
      Application.Current.MainWindow.Close();
    }
  }

}
