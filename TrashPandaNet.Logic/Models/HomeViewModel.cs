namespace TrashPandaNet.Logic.Models
{
    public class HomeViewModel
    {
        public RegisterViewModel RegisterViewModel { get; set; }
        public LoginViewModel LoginViewModel { get; set; }

        public HomeViewModel() 
        {
            RegisterViewModel = new RegisterViewModel();
            LoginViewModel = new LoginViewModel();
        }
    }
}
