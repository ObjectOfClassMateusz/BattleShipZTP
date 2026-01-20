using System.Net.Sockets;
using System.Text.Json;

namespace BattleshipZTP.Networking
{
    public interface INetworkingProxy 
    {
        Task<(string, string)> NetworkWriteAndReadStrings(string v1, string v2="");
        Task<(T sent, T received)> NetworkWriteAndReadAsync<T>(T data);
    }

    public enum NetworkingTaskState
    {
        None,
        NameShipment,
        CountShips
    }

    public sealed class NetworkingProxy: INetworkingProxy
    {
        private TcpClient _client;
        private StreamReader _reader;
        private StreamWriter _writer;
        private string _role;
        public NetworkingProxy(TcpClient client , StreamReader reader , StreamWriter writer,string role)
        {
            _client = client;
            _reader = reader;
            _writer = writer;
            _role = role;
        }
        public async Task<(string,string)> NetworkWriteAndReadStrings(string v1, string v2="")
        {
            if (_role == "Server")
            {
                await _writer.WriteLineAsync(v1);
                v2 = await _reader.ReadLineAsync();
            }
            else
            {
                v2 = await _reader.ReadLineAsync();
                await _writer.WriteLineAsync(v1);
            }
            return (v1,v2);
        }

        public async Task<(T sent, T received)> NetworkWriteAndReadAsync<T>(T data)
        {
            if (_role == "Server")
            {
                // Send first
                string json = JsonSerializer.Serialize(data);
                await _writer.WriteLineAsync(json);
                // Receive
                string receivedJson = await _reader.ReadLineAsync();
                T received = JsonSerializer.Deserialize<T>(receivedJson);

                return (data, received);
            }
            else
            {
                // Receive first
                string receivedJson = await _reader.ReadLineAsync();
                T received = JsonSerializer.Deserialize<T>(receivedJson);
                // Send
                string json = JsonSerializer.Serialize(data);
                await _writer.WriteLineAsync(json);
                return (data, received);
            }
        }
    }
}
