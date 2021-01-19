
namespace SimpleTemplate_Shop.Models.ViewModels
{
    public class ProductEditViewModel : ProductCreateViewModel
    {
        public int ProductID { get; set; }
        public string ExistingPhotoPath { get; set; }
        public string ExistingPhotoPath2 { get; set; }
        public string ExistingPhotoPath3 { get; set; }
    }
}
