using TrashPandaNet.Data.Models;
using TrashPandaNet.Logic.Models;

namespace TrashPandaNet.Logic.Exensions
{
    public static class UserFromModel
    {
        public static User Convert(this User user, UserEditViewModel model)
        {
            user.Image = model.Image;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.MiddleName = model.MiddleName;
            user.Email = model.Email;
            user.BirthDate = model.BirthDate;
            user.UserName = model.Email;
            user.Status = model.Status;
            user.About = model.About;

            return user;
        }
    }
}
