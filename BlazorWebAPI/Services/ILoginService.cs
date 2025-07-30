using BlazorWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAPI.Services
{
    public interface ILoginService
    {
        Task<Response> Get();

        Task<List<User>> GetAllUsers();

        Task<(List<User> Users, Response response)> GetUser(string UserId, string Password);

        //FileContentResult DownloadFile(byte[] bytes, string fileName);

        Task<Response> SaveUser(User userdata);

        Task<Response> UpdateUser(User userdata);

        Task<Response> DeleteUser(int id);

    }
}
