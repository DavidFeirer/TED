using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QueueService.Model;
using QueueService.Services;

namespace QueueService.Controllers
{
    [Route("api/MessageController")]
    [ApiController]
    public class MessageController<T> : ControllerBase
    {
        private readonly MessageQueueService<T> messageQueueService;

        public MessageController(MessageQueueService<T> messageQueueService)
        {
            this.messageQueueService = messageQueueService;
        }

        [HttpPost]
        public IActionResult AddMessage(T message)
        {
            Message<T> newMessage = new Message<T>()
            {
                HttpMethode = HttpMethod.Post,
                Inhalt = message,
                Timestamp= DateTime.Now,
                Id = messageQueueService.GetNewLongId()
            };
            messageQueueService.PushMessage(newMessage);
            return Ok();
        }

        [HttpGet]
        public IActionResult RemoveMessage()
        {
            var message = messageQueueService.PopMessage();
            if (message != null)
            {
                return Ok(message);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("all")]
        public IActionResult GetAllMessages()
        {
            var messages = messageQueueService.GetAllMessages();
            return Ok(messages);
        }
    }
}
