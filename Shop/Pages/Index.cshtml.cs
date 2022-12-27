using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Services;
using System.Text.Json;

namespace Shop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Service _service;

        public IndexModel(Service service)
        {
            _service = service;
            //первое обращение к бд (скорость загрузки первой страницы снизится, зато следующая ускорится)
            List<Genders> genders = _service.GetGendersAsync().Result;
        }
        public async void OnGet()
        {
        }
    }
}