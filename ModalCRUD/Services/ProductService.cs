using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModalCRUD.DbContext;
using ModalCRUD.Models;
using ModalCRUD.ViewModels;

namespace ModalCRUD.Services
{
    public class ProductService : IProductService
    {
        #region Ctor

        private ModalCRUDDbContext _context;

        public ProductService(ModalCRUDDbContext context)
        {
            _context = context;
        }

        #endregion

        public bool CreateOrEditProduct(CreateOrEditProductViewModel createOrEdit)
        {
            if (createOrEdit.Id == 0)
            {
                // Create State
                var add = new Product()
                {
                    Barcode = createOrEdit.Barcode,
                    Description = createOrEdit.Description,
                    IsDelete = false,
                    Name = createOrEdit.Name,
                    Price = createOrEdit.Price,
                    Type = createOrEdit.Type
                };

                // Add Object To Database
                _context.Add(add);
                _context.SaveChanges();

                return true;
            }

            // Edit state
            var product = _context.Products.FirstOrDefault(s => s.Id == createOrEdit.Id && !s.IsDelete);

            if (product == null)
            {
                return false;
            }

            // Edit Product
            product.Name = createOrEdit.Name;
            product.Price = createOrEdit.Price;
            product.Type = createOrEdit.Type;
            product.Description = createOrEdit.Description;
            product.Barcode = createOrEdit.Barcode;

            // Apply To Database
            _context.Update(product);
            _context.SaveChanges();

            return true;
        }

        public CreateOrEditProductViewModel FillCreateOrEditProductViewModel(int productId)
        {
            if (productId == 0)
            {
                // Create Mode
                return new CreateOrEditProductViewModel
                {
                    Id = 0
                };
            }

            // Edit Mode
            var product = _context.Products.FirstOrDefault(s => s.Id == productId && !s.IsDelete);

            if (product == null)
            {
                return null;
            }

            return new CreateOrEditProductViewModel()
            {
                Name = product.Name,
                Barcode = product.Barcode,
                Type = product.Type,
                Description = product.Description,
                Price = product.Price,
                Id = product.Id
            };
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.Where(s => !s.IsDelete).ToList();
        }

        public bool DeleteProduct(int productId)
        {
            var product = _context.Products.FirstOrDefault(s => s.Id == productId && !s.IsDelete);

            if (product == null)
            {
                return false;
            }

            product.IsDelete = true;
            _context.Update(product);
            _context.SaveChanges();

            return true;
        }
    }
}
