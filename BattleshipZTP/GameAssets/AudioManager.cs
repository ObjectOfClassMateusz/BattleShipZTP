using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.GameAssets
{
    public class AudioManager
    {
        public void Play(string fileName)
        {
            if (OperatingSystem.IsWindows())
            {
                SoundPlayer player = new SoundPlayer($"audio/{fileName}.wav");
                player.LoadAsync();
                player.Play();
            }
        }
    }
}
