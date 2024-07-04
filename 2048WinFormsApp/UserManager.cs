using Newtonsoft.Json;

namespace _2048WinFormsApp
{
    public class UserManager
    {
        private static string _path = "results.json";
        public static List<User> GetAll()
        {
            if (FileProvider.Exists(_path))
            {
                var jsonData = FileProvider.Get(_path);
                return JsonConvert.DeserializeObject<List<User>>(jsonData);
            }
            
            return new List<User>();
        }

        public static void Add(User newUser)
        {
            var users = GetAll();
            users.Add(newUser);

            var jsonData = JsonConvert.SerializeObject(users);
            FileProvider.Replace(_path, jsonData);
        }
    }
}
