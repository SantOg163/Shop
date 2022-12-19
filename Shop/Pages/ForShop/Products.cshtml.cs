using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Data;
using Shop.Services;
using System;

namespace Shop.Pages.ForShop
{
    public class ProductsModel : PageModel
    {
        public string Info { get; set; }
        public List<Categories> Categories { get; set; }
        public List<Colors> Colors { get; set; } 
        public List<Sizes> Sizes { get; set; }
        public List<Brands> Brands { get; set; }
        public List<Images> Images { get; set; }
        public List<Card> Cards { get; set; }
        private readonly Service _service;
        public ProductsModel(Service service)
        {
            _service=service;
        }
        public async void OnGet(string gender)
        {
            if (gender == "men")
                Info = "Мужская одежда и обувь";
            if (gender == "women")
                Info = "Женская одежда и обувь";
            //Ошибка CS1061  "List<string>" не содержит определения "GetAwaiter",
            //и не удалось найти доступный метод расширения "GetAwaiter",
            //принимающий тип "List<string>" в качестве первого аргумента(возможно, пропущена директива using или ссылка на сборку).
            //Categories = await _service.GetCategoriesAsync().Result;
            Categories = await  _service.GetCategoriesAsync();
            Colors = await _service.GetColorsAsync();
            Sizes = await _service.GetSizesAsync();
            Brands = await _service.GetBrandsAsync();
            Images= await _service.GetImagesAsync();
            foreach (var item in _service.GetProductsAsync().Result)
            {
                Cards.Add(new Card()
                {
                    Id = item.ProductId,
                    Name = item.Name,
                    Brand = Brands.Where(b => b.BrandId == item.BrandId).Select(b => b.Name).FirstOrDefault(""),
                    Category = Categories.Where(c => c.CategoryId == item.CategoryId).Select(b => b.Name).FirstOrDefault(""),
                    imgSrc = Images.Where(c => c.ProductId == item.ProductId).Select(b => b.Name).FirstOrDefault("")
                });
            }

        }
        public class Card
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public string Brand { get; set; }
            public string imgSrc { get; set; }
        }
    }
}
