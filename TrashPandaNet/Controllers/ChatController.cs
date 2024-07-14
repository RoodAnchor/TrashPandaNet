using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Collections.Immutable;
using System.Runtime.InteropServices;
using TrashPandaNet.Data.Models;
using TrashPandaNet.Data.Repositories;
using TrashPandaNet.Data.UoW;
using TrashPandaNet.Logic.Models;
using TrashPandaNet.Presentation.Controllers;

namespace TrashPandaNet.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ChatController : Controller
    {
        private readonly ILogger<ChatController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public ChatController(
            ILogger<ChatController> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            UserManager<User> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Index(string id)
        {
            var model = await GenerateChat(id);

            return View(model);
        }

        [Route("NewMessage")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewMessage(string id, ChatViewModel chat)
        {
            var user = await _userManager.GetUserAsync(User);
            var friend = await _userManager.FindByIdAsync(id);
            var repository = _unitOfWork.GetRepository<Message>() as MessageRepository;
            var message = new Message()
            {
                Sender = user,
                Recipient = friend,
                Text = chat.NewMessage.Text,
            };

            await repository.Create(message);

            var messages = repository.GetMessages(user, friend);
            var model = new ChatViewModel()
            {
                Sender = user,
                Recipient = friend,
                Messages = messages.OrderBy(x => x.Id).ToList()
            };

            return RedirectToAction("Index", new { id = id });
        }

        private async Task<ChatViewModel> GenerateChat(string id)
        {
            var user = await _userManager.GetUserAsync(User);
            var friend = await _userManager.FindByIdAsync(id);
            var repository = _unitOfWork.GetRepository<Message>() as MessageRepository;
            var messages = repository.GetMessages(user, friend);
            var model = new ChatViewModel()
            {
                Sender = user,
                Recipient = friend,
                Messages = messages.OrderBy(x => x.Id).ToList()
            };

            return model;
        }
    }
}
