using TrashPandaNet.Data.Models;

namespace TrashPandaNet.Logic.Services
{
    public interface IUserGenerationService
    {
        public List<User> Generate(int userCount);
    }
}
