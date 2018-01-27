using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Websockety.Helper
{
    public class WebSocketConnectionManager
    {
        private ConcurrentDictionary<string, WebSocketSerialNumber> _sockets = new ConcurrentDictionary<string, WebSocketSerialNumber>();

        public WebSocket GetSocketById(string id)
        {
            return _sockets.FirstOrDefault(p => p.Key == id).Value.WebSocket;
        }

        public ConcurrentDictionary<string, WebSocketSerialNumber> GetAll()
        {
            return _sockets;
        }

        public string GetId(WebSocket socket)
        {
            return _sockets.FirstOrDefault(p => p.Value.WebSocket == socket).Key;
        }
        public void AddSocket(WebSocket socket)
        {
            _sockets.TryAdd(CreateConnectionId(), new WebSocketSerialNumber()
            {
                WebSocket = socket
            });
        }

        public async Task RemoveSocket(string id)
        {
            WebSocketSerialNumber socket;
            _sockets.TryRemove(id, out socket);

            await socket.WebSocket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                                    statusDescription: "Closed by the WebSocketManager",
                                    cancellationToken: CancellationToken.None);
        }

        public bool ChangeSNForWebSocket(string id, string SN)
        {
            var oldValue = _sockets.FirstOrDefault(m => m.Key == id).Value;
            var newValue = new WebSocketSerialNumber()
            {
                SN = SN,
                WebSocket = oldValue.WebSocket
            };
            return _sockets.TryUpdate(id, newValue, oldValue);

        }

        public string GetIdBySN(string SN)
        {
            return _sockets.FirstOrDefault(m => m.Value.SN == SN).Key;
        }

        private string CreateConnectionId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
