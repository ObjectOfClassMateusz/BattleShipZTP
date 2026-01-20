using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using BattleshipZTP.UI;
using NAudio.Wave;

namespace BattleshipZTP.GameAssets
{
    public class AudioManager
    {
        private static AudioManager _instance = new AudioManager();
        private AudioManager() { }
        public static AudioManager Instance => _instance;
        private Dictionary<string, AudioFileReader> _audios = new Dictionary<string, AudioFileReader>();
        private Dictionary<string, IWavePlayer> _activePlayers = new Dictionary<string, IWavePlayer>();
        public void Add(string fileName)
        {
            var wavPath = Path.Combine("audio", $"{fileName}.wav");
            var mp3Path = Path.Combine("audio", $"{fileName}.mp3");
            if (File.Exists(wavPath)){
                _audios[fileName] = new AudioFileReader(wavPath);
            }
            else if (File.Exists(mp3Path)){
                _audios[fileName] = new AudioFileReader(mp3Path);
            }
            else{
                throw new FileNotFoundException(
                    $"Audio file not found. Expected '{wavPath}' or '{mp3Path}'.");
            }
        }
        public void Add(string fileName, string path)
        {
            var basePath = Path.Combine("audio", path);
            var wavPath = Path.Combine(basePath, $"{fileName}.wav");
            var mp3Path = Path.Combine(basePath, $"{fileName}.mp3");
            if (File.Exists(wavPath)){
                _audios[fileName] = new AudioFileReader(wavPath);
            }
            else if (File.Exists(mp3Path)){
                _audios[fileName] = new AudioFileReader(mp3Path);
            }
            else{
                throw new FileNotFoundException(
                    $"Audio file not found. Expected '{wavPath}' or '{mp3Path}'.");
            }
        }
        public void Play(string fileName, bool isLooping = false)
        {
            if (OperatingSystem.IsWindows())
            {
                if (_activePlayers.ContainsKey(fileName)) return;
                if (!_audios.TryGetValue(fileName, out var audioFile)) return;
                var outputDevice = new WaveOutEvent();
                audioFile.Position = 0;
                outputDevice.Init(audioFile);
                outputDevice.PlaybackStopped += (s, e) =>
                {
                    if (isLooping && _activePlayers.ContainsKey(fileName))
                    {
                        audioFile.Position = 0;
                        outputDevice.Play();
                    }
                    else
                    {
                        _activePlayers.Remove(fileName);
                    }
                };
                _activePlayers[fileName] = outputDevice;
                outputDevice.Play();
            }
        }
        public void Stop(string fileName)
        {
            if (_activePlayers.TryGetValue(fileName, out var player))
            {
                _activePlayers.Remove(fileName);
                if (player != null)
                {
                    player.Stop();
                    player.Dispose();
                }
            }
        }
        public void ChangeVolume(string fileName , int v) 
        {
            float volume = v / 100.0f;
            _audios[fileName].Volume = volume;
        }
    }
}
