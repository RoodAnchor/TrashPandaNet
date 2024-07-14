using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using TrashPandaNet.Data.Models;
using TrashPandaNet.Data.Repositories;
using TrashPandaNet.Data.UoW;
using TrashPandaNet.Logic.Models;
using TrashPandaNet.Logic.Services;

namespace TrashPandaNet.Presentation.Controllers
{
    [Route("[controller]")]
    public class AccountManagerController : Controller
    {
        private readonly ILogger<RegisterController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserGenerationService _userGenerationService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountManagerController(
            ILogger<RegisterController> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserGenerationService userGenerationService,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userGenerationService = userGenerationService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return RedirectToAction("Index", "Home");
        }

        [Route("Login")]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel() { ReturnUrl = returnUrl });
        }

        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(model);
                user.EmailConfirmed = true;
                var result = await _signInManager.PasswordSignInAsync(
                    user.Email,
                    model.Password,
                    model.RememberMe,
                    false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) &&
                        Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "User");
                    }
                }
                else
                {
                    ViewData["ValidationFor"] = "Login";
                    ModelState.AddModelError("", "Неправильные логин и (или) пароль");
                }
            }

            return View("~/Views/Home/Index.cshtml", new HomeViewModel() { LoginViewModel = model });
        }

        [Route("Logout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [Route("AddFriend/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFriend(string id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var friend = await _userManager.FindByIdAsync(id);
            var repository = _unitOfWork.GetRepository<Friend>() as FriendsRepository;
            
            await repository.AddFriend(currentUser, friend);
            await repository.AddFriend(friend, currentUser);

            return RedirectToAction("Index", "User");
        }

        [Route("DeleteFriend/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFriend(string id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var friend = await _userManager.FindByIdAsync(id);
            var repository = _unitOfWork.GetRepository<Friend>() as FriendsRepository;

            await repository.DeleteFriend(currentUser, friend);
            await repository.DeleteFriend(friend, currentUser);

            return RedirectToAction("Index", "User");
        }

        [Route("Generate/{userCount}")]
        [HttpGet]
        public async Task<IActionResult> Generate(int userCount = 10)
        {
            var users = _userGenerationService.Generate(userCount);

            foreach(var user in users)
            {
                var result = await _userManager.CreateAsync(user, "123456");

                if (!result.Succeeded)
                    continue;
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
