using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        }
        public IActionResult OnGet()
        {
            return RedirectToPage("ForShoP/AddProduct");
        }
    }
}