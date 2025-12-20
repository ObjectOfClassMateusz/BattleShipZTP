using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipZTP.Networking
{
    public interface INetworkingProxy : IAsyncDisposable
    {
        bool IsConnected { get; }

        Task ConnectAsync(string host, int port);
        Task SendAsync(string message);
        Task DisconnectAsync();

        event Action<string> MessageReceived;
        event Action Disconnected;
    }

    public sealed class NetworkingProxy: INetworkingProxy
    {
        private TcpClient _client;
        private StreamReader _reader;
        private StreamWriter _writer;
        private CancellationTokenSource _cts;

        public bool IsConnected => _client?.Connected == true;

        public event Action<string> MessageReceived;
        public event Action Disconnected;

        public async Task ConnectAsync(string host, int port)
        {
            _client = new TcpClient();
            _cts = new CancellationTokenSource();

            await _client.ConnectAsync(host, port);

            var stream = _client.GetStream();
            _reader = new StreamReader(stream);
            _writer = new StreamWriter(stream) { AutoFlush = true };

            _ = Task.Run(ReadLoop);
        }
        public async Task SendAsync(string message)
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("Not connected");
            }
            await _writer.WriteLineAsync(message);
        }
        private async Task ReadLoop()
        {
            try
            {
                while (!_cts.IsCancellationRequested)
                {
                    var line = await _reader.ReadLineAsync();
                    if (line == null)
                    {
                        break;
                    }
                    MessageReceived?.Invoke(line);
                }
            }
            catch
            {
                // log
            }
            finally
            {
                Disconnected?.Invoke();
            }
        }
        public Task DisconnectAsync()
        {
            _cts?.Cancel();
            _client?.Close();
            return Task.CompletedTask;
        }
        public ValueTask DisposeAsync()
        {
            return new ValueTask(DisconnectAsync());
        }
    }

    public interface ITcpServerProxy
    {
        Task StartAsync(int port);
        Task StopAsync();

        event Action<INetworkingProxy> ClientConnected;
    }
}
