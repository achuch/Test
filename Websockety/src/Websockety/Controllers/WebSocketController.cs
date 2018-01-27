using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Websockety.Helper;

namespace Websockety.Controllers
{
    [Produces("application/json")]
    [Route("api/WebSocket")]
    public class WebSocketController : Controller
    {
        private ChatMessageHandler _handler;

        public WebSocketController(ChatMessageHandler handler)
        {
            _handler = handler;
        }

        [HttpGet("{SN}")]
        public async Task<IActionResult> Send(string SN)
        {
            var x = _handler;
            var websocketMenager = _handler.WebSocketConnectionManager;
            var socketId = websocketMenager.GetIdBySN(SN);
            await _handler.SendMessageAsync(socketId, "Czeœæ tu api kontroller");
            return Ok();
        }
    }
}