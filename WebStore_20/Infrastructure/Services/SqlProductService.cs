﻿using System.Collections.Generic;
using System.Linq;
using WebStore.Dal;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services
{
    public class SqlProductService : IProductService
    {
        private readonly WebStoreContext _context;

        public SqlProductService(WebStoreContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public IEnumerable<Brand> GetBrands()
        {
            return _context.Brands.ToList();
        }

        public IEnumerable<Product> GetProducts(ProductFilter filter)
        {
            var query = _context.Products.AsQueryable();
            if (filter.BrandId.HasValue)
                query = query.Where(c => c.BrandId.HasValue && c.BrandId.Value.Equals(filter.BrandId.Value));
            if (filter.CategoryId.HasValue)
                query = query.Where(c => c.CategoryId.Equals(filter.CategoryId.Value));

            return query.ToList();

        }
        public int GetCountProductsForBrand(int brandId)
        {
            return _context.Products.AsQueryable().Where(c => c.BrandId == brandId).Sum(c => c.AmountPiece);            
        }
    }
}