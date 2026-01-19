using BattleshipZTP.GameAssets;
using BattleshipZTP.Utilities;

namespace BattleshipZTP.Settings;

public class UserSettings
{
    private static UserSettings _instance;
    private UserSettings() { }

    public static UserSettings Instance => _instance ??= new UserSettings();

    public string Nickname { get; set; } = "PLAYER";
    public int MusicVolume { get; set; } = 40;
    public bool MusicEnabled { get; set; } = true;
    public bool SfxEnabled { get; set; } = true;

    public void UpdateSettings(List<string> options)
    {
        //SfxEnabled = true;
        //MusicEnabled = true;
        foreach (var opt in options)
        {
            if (opt.Contains("input-Nickname"))
            {
                Nickname = opt.Split("#:")[1];//ok
            }
            if (opt.Contains("slider-Music volume"))
            {
                MusicVolume = int.Parse(opt.Split("#:")[1]);//ok
            }
            if (opt.Contains("checkbox-Turn off Music"))
            {
                MusicEnabled = !bool.Parse(opt.Split("#:")[1]);//ok
                AudioManager.Instance.Stop("2-02 - Dark Calculation");
            }
            if (opt.Contains("checkbox-Turn off SFX"))
            {
                SfxEnabled = bool.Parse(opt.Split("#:")[1]);//ok
            }
        }
    }
}