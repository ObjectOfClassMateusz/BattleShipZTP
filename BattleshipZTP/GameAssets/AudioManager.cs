using NAudio.Wave;

namespace BattleshipZTP.GameAssets
{
    /**
     * @brief singleton class that are responsible for playing audio
     */
    public class AudioManager
    {
        private static AudioManager _instance = new AudioManager();
        private AudioManager() { }
        public static AudioManager Instance => _instance;
        private Dictionary<string, AudioFileReader> _audios = new Dictionary<string, AudioFileReader>();
        private Dictionary<string, IWavePlayer> _activePlayers = new Dictionary<string, IWavePlayer>();
        /**
         *  @brief Loads and cache named audio file.
         *  The method searches for a file in the "audio" folder first in .wav format,
         *  and if it doesn't exist, it tries .mp3. The found file is loaded
         *  as an AudioFileReader and saved in the internal dictionary under the key
         *  that is the filename (without the extension).
         *
         * @param fileName file name without a extension
         * @throws FileNotFoundException Thrown when .wav or an .mp3 file with the given name doesn't exists in the "audio" folder.
         * @note If .wav file exist, takes precedence over mp3 file with the same name
         */
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
        /**
         * @brief variant with deeper directory path
         * @param path string that represents a path
         */
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
        /**
         * @brief plays an sound from cached dictonary
         * @note method working only on system windows
         */
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
        /**
         * @brief stoping an played sound
         */
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
        /**
         * @brief changes volume for sound
         */
        public void ChangeVolume(string fileName , int v) 
        {
            float volume = v / 100.0f;
            _audios[fileName].Volume = volume;
        }
    }
}
