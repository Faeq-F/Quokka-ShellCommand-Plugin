/// <summary>
/// All plugin specific settings
/// </summary>
public class PluginSettings {

  /// <summary>
  /// The command signifier used for this plugin (defaults to "> ")<br />
  /// This is the only way to use the plugin, as to not clutter search results
  /// </summary>
  public string CommandSignifier { get; set; } = "> ";

  /// <summary>
  /// If this text is included in the query, the command will be ran with administrator privileges. Defaults to " --Admin"
  /// </summary>
  public string AdminFlag { get; set; } = " --Admin";

}