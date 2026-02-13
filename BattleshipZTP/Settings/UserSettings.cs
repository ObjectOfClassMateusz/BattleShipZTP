using BattleshipZTP.GameAssets;
using BattleshipZTP.Utilities;

namespace BattleshipZTP.Settings;

public class UserSettings
{
    private static UserSettings _instance;
    private UserSettings() 
    {
        if (!File.Exists($"data/settings/s.sttgs"))
        {
            //File.Create($"data/settings/s.sttgs");
            StreamWriter writer = new StreamWriter($"data/settings/s.sttgs");
            writer.WriteLine("PLAYER");
            writer.WriteLine("50");
            writer.WriteLine("True");
            writer.WriteLine("True");
            writer.Close();
        }
        else
        {
            StreamReader reader = new StreamReader($"data/settings/s.sttgs");
            try
            {
                string name       = reader.ReadLine() ?? "Player";
                this.Nickname     = name;
                int volume        = Convert.ToInt32(reader.ReadLine());
                this.MusicVolume  = volume;
                string musicEnable  = reader.ReadLine();
                this.MusicEnabled = (musicEnable == "True") ? true : false;
                string sfxEnable    = reader.ReadLine();
                this.SfxEnabled   = (sfxEnable == "True") ? true : false;
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Failed to read appliation settings.");
            }
            reader.Close();
        }
    }

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
        StreamWriter writer = new StreamWriter($"data/settings/s.sttgs");
        writer.WriteLine(Nickname);
        writer.WriteLine(MusicVolume);
        writer.WriteLine(MusicEnabled);
        writer.WriteLine(SfxEnabled);
        writer.Close();
    }
}