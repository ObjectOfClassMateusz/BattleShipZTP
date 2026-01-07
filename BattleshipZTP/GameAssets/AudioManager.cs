using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace BattleshipZTP.GameAssets
{
    public class AudioManager
    {
        private static AudioManager _instance = new AudioManager();
        private AudioManager() { }
        public static AudioManager Instance => _instance;

        private Dictionary<string,AudioFileReader> _audios = new Dictionary<string, AudioFileReader> ();

        public void Add(string fileName)
        {
            _audios[fileName] = new NAudio.Wave.AudioFileReader($"audio/{fileName}.wav");
        }

        public void Play(string fileName)
        {
            if (OperatingSystem.IsWindows())
            {
                var output = new NAudio.Wave.WaveOutEvent();
                output.Init(_audios[fileName]);
                output.Play();
            }
        }
        
        public void Stop(string fileName)
        {
            if (OperatingSystem.IsWindows())
            {
                // trzeba dodać obsługę zatrzymania dźwięku 
            }
        }
        public void ChangeVolume(string fileName , int v) 
        {
            float volume = v / 100.0f;
            _audios[fileName].Volume = volume;
        }
        /*
         * if (_audios.TryGetValue("name1", out var audio))
            {
                audio.Output.Stop();      // stops playback immediately
                audio.Output.Dispose();   // release audio device
                audio.Reader.Dispose();   // release file

                _audios.Remove("name1");
            }

        _audios["name1"].Position = 0;
        */
    }
}
