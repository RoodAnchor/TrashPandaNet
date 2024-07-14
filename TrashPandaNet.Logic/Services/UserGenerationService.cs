using TrashPandaNet.Data.Models;
using static System.Net.WebRequestMethods;

namespace TrashPandaNet.Logic.Services
{
    public class UserGenerationservice : IUserGenerationService
    {
        private readonly string[] _firstNames;
        private readonly string[] _lastNames;
        private readonly string[] _middleNames;

        public UserGenerationservice() 
        {
            _firstNames =
            [
                "Ярослав", "Александр", "Марк", "Арсений", 
                "Демид", "Владимир", "Михаил", "Константин", 
                "Павел", "Сергей"
            ];

            _lastNames = 
            [
                "Тихонов", "Скворцов", "Щеглов", "Степанов",
                "Григорьев", "Соколов", "Панов", "Филатов",
                "Сорокин", "Иванов"
            ];

            _middleNames =
            [
                "Мирославович", "Егорович", "Ильич", "Даниилович",
                "Янович", "Максимович", "Леонидович", "Иванович",
                "Макарович", "Дмитриевич"
            ];
        }

        public List<User> Generate(int userCount)
        {
            var users = new List<User>();
            var random = new Random();

            for(var i = 0; i < userCount; i++)
            {
                var user = new User();

                user.FirstName = _firstNames[random.Next(0, 9)];
                user.LastName = _lastNames[random.Next(0, 9)];
                user.MiddleName = _middleNames[random.Next(0, 9)];
                user.Email = $"user{i}@gmail.com";
                user.BirthDate = GetBirtDate();
                user.UserName = user.Email;
                user.Image = "https://random.imagecdn.app/500/500";

                users.Add(user);
            }

            return users;
        }

        private DateTime GetBirtDate()
        {
            var random = new Random();

            var year = random.Next(1970, 2008);
            var month = random.Next(1, 12);
            var day = random.Next(1, DateTime.DaysInMonth(year, month));

            return new DateTime(year, month, day);
        }
    }
}
