using BattleshipZTP.GameAssets;

namespace BattleshipZTP.Settings;

public class UserSettings
{
    private static UserSettings _instance;
    private UserSettings() { }

    public static UserSettings Instance => _instance ??= new UserSettings();

    public string Nickname { get; set; } = "PLAYER";
    public int MusicVolume { get; set; } = 50;
    public bool MusicEnabled { get; set; } = true;
    public bool SfxEnabled { get; set; } = true;

    public void UpdateSettings(List<string> options)
    {
        foreach (var opt in options)
        {
            if (opt.Contains("input-Nickname"))
            {
                Nickname = opt.Split("#:")[1];
            }
            if (opt.Contains("slider-Music volume"))
            {
                MusicVolume = int.Parse(opt.Split("#:")[1]);
            }
            if (opt.Contains("checkbox-Turn off Music"))
            {
                MusicEnabled = !bool.Parse(opt.Split("#:")[1]);
                AudioManager.Instance.Stop("2-02 - Dark Calculation");
            }
            if (opt.Contains("checkbox-Turn off SFX"))
            {
                SfxEnabled = !bool.Parse(opt.Split("#:")[1]);
            }
        }
    }
}