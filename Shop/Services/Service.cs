using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Shop.Data;

namespace Shop.Services
{
    public class Service
    {
        private readonly ApplicationDbContext _context;
        public Service(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Categories>> GetCategoriesAsync() => await _context.Categories.ToListAsync();
        public async Task<List<Genders>> GetGendersAsync() => await _context.Genders.ToListAsync();
        public async Task<List<ProductSize>> GetProductSizeAsync() => await _context.ProductSize.ToListAsync();
        public async Task<List<Images>> GetImageAsync() => await _context.Images.ToListAsync();
        public async Task<List<Colors>> GetColorsAsync() => await _context.Colors.ToListAsync();
        public async Task<List<Sizes>> GetSizesAsync() => await _context.Sizes.ToListAsync();
        public async Task<List<Brands>> GetBrandsAsync() => await _context.Brands.ToListAsync();
        public async Task<List<Products>> GetProductsAsync() => await _context.Products.ToListAsync();
        public async Task<List<Images>> GetImagesAsync() => await _context.Images.ToListAsync();
        public void AddProduct(Products product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception();
            }
        }
        public async Task AddProductSize(ProductSize product)
        {
                _context.ProductSize.Add(product);
                await _context.SaveChangesAsync();
        }

    }
}
