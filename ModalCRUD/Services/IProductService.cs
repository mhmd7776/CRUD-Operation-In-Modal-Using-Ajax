using ModalCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModalCRUD.ViewModels;

namespace ModalCRUD.Services
{
    public interface IProductService
    {
        bool CreateOrEditProduct(CreateOrEditProductViewModel createOrEdit);

        CreateOrEditProductViewModel FillCreateOrEditProductViewModel(int productId);

        List<Product> GetAllProducts();

        bool DeleteProduct(int productId);
    }
}
