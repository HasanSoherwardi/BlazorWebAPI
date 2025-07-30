using BlazorWebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAPI.Services
{
    public class LoginService : ILoginService
    {

        private string _baseUrl = "http://localhost:5199";

        public async Task<Response> DeleteUser(int id)
        {
            Response response = new Response();
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"{_baseUrl}/api/MAUILogin?id={id}";
                    var apiResponse = await client.DeleteAsync(url);

                    string apiErrorMessage = await apiResponse.Content.ReadAsStringAsync();

                    var errorObject = JsonConvert.DeserializeObject<Response>(apiErrorMessage);
                    response.GetMessage = errorObject.GetMessage;
                    response.Status = errorObject.Status;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                response.Status = 0;
                response.GetMessage = "Error occurred while deleting user: " + msg;
            }
            return response;
        }

        public async Task<Response> Get()
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAllUsers()
        {
            List<User> fileData = new List<User>();
            Response response = new Response();
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"{_baseUrl}/api/MAUILogin/GetAllUsers";
                    var apiResponse = await client.GetAsync(url);

                    if (apiResponse.IsSuccessStatusCode)
                    {

                        var userList = await apiResponse.Content.ReadAsStringAsync();
                        fileData = JsonConvert.DeserializeObject<List<User>>(userList);
                        return fileData;

                    }
                    else
                    {
                        response.GetMessage = "No records found.";
                        response.Status = 0;

                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                response.Status = 0; // Indicate failure due to an exception
                response.GetMessage = msg;
            }
            return fileData;
        }

        public async Task<(List<User> Users, Response response)> GetUser(string UserId, string Password)
        {
            List<User> fileData = new List<User>();
            Response response = new Response();
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"{_baseUrl}/api/MAUILogin/GetUser?UserId={UserId}&Password={Password}";
                    var apiResponse = await client.GetAsync(url);

                    if (apiResponse.IsSuccessStatusCode)
                    {

                        var userList = await apiResponse.Content.ReadAsStringAsync();
                        fileData = JsonConvert.DeserializeObject<List<User>>(userList);

                        string apiErrorMessage = await apiResponse.Content.ReadAsStringAsync();

                        var errorObject = JsonConvert.DeserializeObject<Response>(apiErrorMessage);
                        response.GetMessage = errorObject.GetMessage;
                        response.Status = errorObject.Status;

                    }
                    return (fileData, response);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                response.Status = 0; // Indicate failure due to an exception
                response.GetMessage = msg;
            }
            return (fileData, response);
        }

        
        public async Task<Response> SaveUser(User userdata)
        {
            Response response = new Response();
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"{_baseUrl}/api/MAUILogin";

                    var serializeContent = JsonConvert.SerializeObject(userdata);

                    var apiResponse = await client.PostAsync(url, new StringContent(serializeContent, Encoding.UTF8, "application/json"));

                    string apiErrorMessage = await apiResponse.Content.ReadAsStringAsync();

                    var errorObject = JsonConvert.DeserializeObject<Response>(apiErrorMessage);
                    response.GetMessage = errorObject.GetMessage;
                    response.Status = errorObject.Status;
                }
            }
            catch (Exception ex)
            {
                // This catch block handles network errors, timeout errors, or issues before/during API communication.
                string msg = ex.Message;
                response.Status = 0; // Indicate failure due to an exception
                response.GetMessage = "An error occurred while communicating with the server: " + msg;
            }
            return response;
        }

        public async Task<Response> UpdateUser(User userdata)
        {
            Response response = new Response();
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"{_baseUrl}/api/MAUILogin";

                    var serializeContent = JsonConvert.SerializeObject(userdata);

                    var apiResponse = await client.PutAsync(url, new StringContent(serializeContent, Encoding.UTF8, "application/json"));

                    string apiErrorMessage = await apiResponse.Content.ReadAsStringAsync();

                    var errorObject = JsonConvert.DeserializeObject<Response>(apiErrorMessage);
                    response.GetMessage = errorObject.GetMessage;
                    response.Status = errorObject.Status;
                   
                }
            }
            catch (Exception ex)
            {
                // This catch block handles network errors, timeout errors, or issues before/during API communication.
                string msg = ex.Message;
                response.Status = 0; // Indicate failure due to an exception
                response.GetMessage = "An error occurred while communicating with the server: " + msg;
            }
            return response;
        }
    }
}
