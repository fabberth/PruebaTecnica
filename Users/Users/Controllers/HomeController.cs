using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using Users.Entities;
using Users.Models;

namespace Users.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetElementsAsync(string id, string name, string email, string city)
        {
            var usersList = new List<User>();
            try
            {
                HttpClient httpClient = new HttpClient();
                var users = await httpClient.GetFromJsonAsync<List<User>>("https://jsonplaceholder.typicode.com/users");
                if(users != null && users.Any())
                {
                    usersList = users.ToList();
                }
            }
            catch (Exception)
            {
            }
            
            if(int.TryParse(id, out int rid))
            {
                usersList = usersList.Where(x => x.Id == rid).ToList();
            }

            if (!string.IsNullOrEmpty(name))
            {
                usersList = usersList.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(email))
            {
                usersList = usersList.Where(x => x.Email.ToLower().Contains(email.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(city))
            {
                usersList = usersList.Where(x => x.Address != null && x.Address.City != null && 
                                            x.Address.City.ToLower().Contains(city.ToLower()
                )).ToList();
            }

            return Ok(usersList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
