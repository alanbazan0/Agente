using System;
using AgenteApp.Models;
using AgenteApp.DataAccess;
using System.Threading.Tasks;

namespace AgenteApp.Presenters
{
    public class UserDataAccess
    {
        public UserDataAccess()
        {
        }

        public async Task<User> Find(string username, string password)
        {
            DataService<User> dataService = new DataService<User>(Constants.BASE_ADDRESS, "/api/users/find.php");

            dataService.AddParameter("username", username);
            dataService.AddParameter("password", password);
            User user = await dataService.ExecutePOST();
            return user;

        }
    }
}