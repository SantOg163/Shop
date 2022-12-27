using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using Shop.Services;

namespace Shop.Pages.ForShop
{
    public class ViewModel : PageModel
    {
        public ViewProduct productView { get; set; }
        private readonly Service _service;
        public ViewModel(Service service)
        {
            _service = service;
        }

        public async void OnGet(string name)
        {
            productView=await _service.GetProductView(name);
        }
    }
}
