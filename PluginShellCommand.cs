
using Newtonsoft.Json;
using Quokka.ListItems;
using Quokka.PluginArch;
using System.Collections.ObjectModel;
using System.IO;

namespace PluginShellCommand
{

  /// <summary>
  /// The ShellCommand plugin
  /// </summary>
  public partial class ShellCommand : Plugin
  {

    internal static PluginSettings PluginSettings { get; set; } = new();

    /// <summary>
    /// Loads plugin settings
    /// </summary>
    public ShellCommand()
    {
      string fileName = Environment.CurrentDirectory + "\\PlugBoard\\PluginShellCommand\\Plugin\\settings.json";
      PluginSettings = JsonConvert.DeserializeObject<PluginSettings>(File.ReadAllText(fileName))!;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override string PluginName { get; set; } = "ShellCommand";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="query"><inheritdoc/></param>
    /// <returns>
    /// An empty collection - you can only use this plugin via the command signifier
    /// </returns>
    public override Collection<ListItem> OnQueryChange(string query) { return new Collection<ListItem>(); }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns>
    /// The CommandSignifier from plugin settings
    /// </returns>
    public override Collection<string> CommandSignifiers()
    {
      return new Collection<string>() { PluginSettings.CommandSignifier };
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="command">The CommandSignifier for this plugin, followed by the command to run (and optionally the AdminFlag)</param>
    /// <returns>A list item that, when triggered, runs the command</returns>
    public override Collection<ListItem> OnSignifier(string command)
    {
      command ??= "";
      command = command.Substring(PluginSettings.CommandSignifier.Length);
      if (command.Contains(PluginSettings.AdminFlag, StringComparison.Ordinal))
      {
        command = command.Replace(PluginSettings.AdminFlag, "", StringComparison.Ordinal);
        return new Collection<ListItem>() { new ShellCommandItem(command, true) };
      }
      else
      {
        return new Collection<ListItem>() { new ShellCommandItem(command, false) };
      }

    }
  }

}
