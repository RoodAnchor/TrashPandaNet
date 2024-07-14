using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrashPandaNet.Data.Models;
using TrashPandaNet.Logic.Models;
using TrashPandaNet.Data.UoW;
using TrashPandaNet.Data.Repositories;

namespace TrashPandaNet.Presentation.Controllers
{
    [Route("[controller]")]
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public SearchController(
            ILogger<SearchController> logger,
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
            var users = await CreateSearchAsync(string.Empty);

            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string search = "")
        {
            var users = await CreateSearchAsync(search);

            return View(users);
        }

        private async Task<SearchViewModel> CreateSearchAsync(string search)
        {
            var result = await _userManager.GetUserAsync(User);
            var list = _userManager.Users.AsEnumerable().Where(x =>
                x.FullName.ToLower().Contains(search.ToLower()) && x.Id != result.Id).ToList();
            var withFriend = await GetAllFriends();
            var data = new List<UserWithFriendExt>();

            list.ForEach(x => 
            {
                var t = _mapper.Map<UserWithFriendExt>(x);

                t.IsFriendWithCurrent = withFriend.Where(y => y.Id == x.Id || x.Id == result.Id).Count() != 0;
                data.Add(t);
            });

            var model = new SearchViewModel(data);

            return model;
        }

        private async Task<List<User>> GetAllFriends()
        {
            var result = await _userManager.GetUserAsync(User);
            var repository = _unitOfWork.GetRepository<Friend>() as FriendsRepository;

            return repository.GetFriends(result);
        }
    }
}
