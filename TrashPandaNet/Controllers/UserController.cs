using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrashPandaNet.Data.Models;
using TrashPandaNet.Data.Repositories;
using TrashPandaNet.Data.UoW;
using TrashPandaNet.Logic.Exensions;
using TrashPandaNet.Logic.Models;

namespace TrashPandaNet.Presentation.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public UserController(
            ILogger<UserController> logger,
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
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = await GenerateModel(user);

            return View(model);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Index(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var model = await GenerateModel(user);

            return View(model);
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            var editUserViewModel = _mapper.Map<UserEditViewModel>(user);

            return View(editUserViewModel);
        }

        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await  _userManager.FindByIdAsync(model.Id);

                user.Convert(model);

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    return RedirectToAction("Edit");
                }
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return RedirectToAction("Edit", model);
            }
        }

        private async Task<List<User>> GetAllFriends(User user)
        {
            var repository = _unitOfWork.GetRepository<Friend>() as FriendsRepository;

            return repository.GetFriends(user);
        }

        private async Task<List<User>> GetAllFriends()
        {
            var user = await _userManager.GetUserAsync(User);
            var repository = _unitOfWork.GetRepository<Friend>() as FriendsRepository;

            return repository.GetFriends(user);
        }

        private async Task<UserViewModel> GenerateModel(User user)
        {
            var userViewModel = new UserViewModel(user);

            userViewModel.Friends = await GetAllFriends(user);

            return userViewModel;
        }
    }
}
