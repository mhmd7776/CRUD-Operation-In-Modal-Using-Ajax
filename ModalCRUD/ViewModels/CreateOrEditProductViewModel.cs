using System.ComponentModel.DataAnnotations;
using ModalCRUD.Models;

namespace ModalCRUD.ViewModels
{
    public class CreateOrEditProductViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "{0} is required")]
        public string Price { get; set; }

        [Display(Name = "Barcode")]
        public string Barcode { get; set; }

        [Display(Name = "Product Type")]
        public ProductType Type { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
