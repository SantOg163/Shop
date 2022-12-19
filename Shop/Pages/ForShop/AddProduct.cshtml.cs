using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Shop.Data;
using Shop.Services;

namespace Shop.Pages.ForShop
{
    public class AddProductModel : PageModel
    {
        [BindProperty]
        public Input input { get; set; } = new Input();
        public List<Categories> Categories { get; set; }
        public List<Brands> Brands { get; set; }
        public List<Genders> Genders { get; set; }
        public List<Colors> Colors { get; set; }
        public List<Sizes> Sizes { get; set; }
        private readonly Service _service;
        private readonly ImageService _imageService;
        public AddProductModel(Service service, ImageService imageService)
        {
            _service = service;
            _imageService = imageService;
            Categories = _service.GetCategoriesAsync().Result;
            Genders = _service.GetGendersAsync().Result;
            Colors = _service.GetColorsAsync().Result;
            Sizes = _service.GetSizesAsync().Result;
            Brands = _service.GetBrandsAsync().Result;

        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
           
            if (ModelState.IsValid)
            {
                int maxSizeId = Sizes.Where(s => s.Name == input.maxSize).Select(s => s.SizeId).FirstOrDefault();
                int minSizeId = Sizes.Where(s => s.Name == input.minSize).Select(s => s.SizeId).FirstOrDefault();
                int GenderId = Genders.Where(g => g.Name == input.Gender).Select(g => g.GenderId).FirstOrDefault();
                int ColorId = Colors.Where(c => c.Name == input.Color).Select(c => c.ColorId).FirstOrDefault();
                int CategoryId = Categories.Where(c => c.Name == input.Category).Select(c => c.CategoryId).FirstOrDefault();
                int BrandId = Brands.Where(b => b.Name == input.Brand).Select(s => s.BrandId).FirstOrDefault();
                int ProductId;
                try
                {
                    ProductId = _service.GetProductsAsync().Result.Last().ProductId + 1;
                }
                catch { ProductId = 1; }
                int ProductSizeId;
                try
                {
                    ProductSizeId = _service.GetProductSizeAsync().Result.Last().ProductSizeId + 1;
                }
                catch { ProductSizeId = 1; }
                int ImageId;
                try
                {
                    ImageId = _service.GetImageAsync().Result.Last().ImageId + 1;
                }
                catch { ImageId = 1; }

                Products newProduct = new Products { ProductId = ProductId, BrandId = BrandId, CategoryId = CategoryId, ColorId = ColorId, GenderId = GenderId, Name = input.Name, Price = input.Price };
                _service.AddProduct(newProduct);
                for (int i = 0; i < input.Images.Count; i++)
                {
                    input.Images[i].ImageId = ImageId + i;
                    input.Images[i].ProductId = ProductId;
                    await _imageService.AddImage(input.Images[i]);
                }
                List<ProductSize> products = new List<ProductSize>();
                int id = 0;
                for (int i = minSizeId; i <= maxSizeId; i++)
                {
                    for (int j = 0; j < input.countSize; j++)
                    {
                        ProductSize product = new ProductSize { ProductSizeId = ProductSizeId + id, ProductId = newProduct.ProductId, SizeId = i };
                        id++;
                        await _service.AddProductSize(product);
                    }
                }
                return RedirectToPage("/Index");
            }
            else
            {
                for (int i = input.Images.Count; i < 5; i++)
                    input.Images.Add(new Images());
                return Page();
            }
        }
        public class Input
        {
            [Required(ErrorMessage = "Обязательное поле")]
            public string Brand { get; set; }
            [Required(ErrorMessage = "Обязательное поле")]
            public string minSize { get; set; }
            [Required(ErrorMessage = "Обязательное поле")]
            public string maxSize { get; set; }
            [Required(ErrorMessage = "Обязательное поле")]
            public int countSize { get; set; }
            [Required(ErrorMessage = "Обязательное поле")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Обязательное поле")]
            public int Price { get; set; }
            [Required(ErrorMessage = "Обязательное поле")]
            public string Gender { get; set; }
            [Required(ErrorMessage = "Обязательное поле")]
            public string Category { get; set; }
            [Required(ErrorMessage = "Обязательное поле")]
            public string Color { get; set; }
            public List<Images> Images { get; set; } = new List<Images>() 
            { 
                new Images(){ ImageId=0, Name="", ProductId=0 },
                new Images(){ ImageId=0, Name="", ProductId=0 },
                new Images(){ ImageId=0, Name="", ProductId=0 },
                new Images(){ ImageId=0, Name="", ProductId=0 },
                new Images(){ ImageId=0, Name="", ProductId=0 }
            };
            public string Description { get; set; } = "";

        }
    }
}