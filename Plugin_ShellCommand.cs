
using Newtonsoft.Json;
using Quokka.ListItems;
using Quokka.PluginArch;
using System.IO;

namespace Plugin_ShellCommand {

  /// <summary>
  /// The ShellCommand plugin
  /// </summary>
  public partial class ShellCommand : Plugin {

    private static PluginSettings pluginSettings = new();
    internal static PluginSettings PluginSettings { get => pluginSettings; set => pluginSettings = value; }

    /// <summary>
    /// Loads plugin settings
    /// </summary>
    public ShellCommand() {
      string fileName = Environment.CurrentDirectory + "\\PlugBoard\\Plugin_ShellCommand\\Plugin\\settings.json";
      PluginSettings = JsonConvert.DeserializeObject<PluginSettings>(File.ReadAllText(fileName))!;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override string PluggerName { get; set; } = "ShellCommand";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="query"><inheritdoc/></param>
    /// <returns>
    /// An empty list - you can only use this plugin via the command signifier
    /// </returns>
    public override List<ListItem> OnQueryChange(string query) { return new List<ListItem>(); }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns>
    /// The CommandSignifier from plugin settings
    /// </returns>
    public override List<string> CommandSignifiers() {
      return new List<string>() { PluginSettings.CommandSignifier };
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="command">The CommandSignifier for this plugin, followed by the command to run (and optionally the AdminFlag)</param>
    /// <returns>A list item that, when triggered, runs the command</returns>
    public override List<ListItem> OnSignifier(string command) {
      command = command.Substring(PluginSettings.CommandSignifier.Length);
      if (command.Contains(PluginSettings.AdminFlag)) {
        command = command.Replace(PluginSettings.AdminFlag, "");
        return new List<ListItem>() { new ShellCommandItem(command, true) };
      } else {
        return new List<ListItem>() { new ShellCommandItem(command, false) };
      }

    }
  }

}
