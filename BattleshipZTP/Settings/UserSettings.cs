using BattleshipZTP.GameAssets;

namespace BattleshipZTP.Settings;

public class UserSettings
{
    private static UserSettings _instance;
    private UserSettings() { }

    public static UserSettings Instance => _instance ??= new UserSettings();

    public string Nickname { get; set; } = "PLAYER";
    public int MusicVolume { get; set; } = 1;
    public bool MusicEnabled { get; set; } = true;
    public bool SfxEnabled { get; set; } = true;

    public void UpdateSettings(List<string> options)
    {
        SfxEnabled = true;
        foreach (var opt in options)
        {
            if (opt.Contains("input-Nickname"))
            {
                Nickname = opt.Split("#:")[1];
            }
            else if (opt.Contains("slider-Music volume"))
            {
                MusicVolume = int.Parse(opt.Split("#:")[1]);
            }
            else if (opt.Contains("checkbox-Turn off Music"))
            {
                MusicEnabled = false;
                AudioManager.Instance.Stop("2-02 - Dark Calculation");
            }
            else if (opt.Contains("checkbox-Turn off SFX"))
            {
                SfxEnabled = false;
            }
        }
    }
}