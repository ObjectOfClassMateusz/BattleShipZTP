using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BattleshipZTP.Networking
{
    public interface INetworkingProxy 
    {
        Task<(string, string)> NetworkWriteAndReadStrings(string v1, string v2);
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
        public async Task<(string,string)> NetworkWriteAndReadStrings(string v1, string v2)
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
            return(v1,v2);
        }

    }

   
}
