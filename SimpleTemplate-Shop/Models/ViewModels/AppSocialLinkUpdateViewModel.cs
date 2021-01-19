namespace SimpleTemplate_Shop.Models.ViewModels
{
    public class AppSocialLinkUpdateViewModel : AppSocialLinkCreateViewModel
    {
        public int Id { get; set; }
        public string ExistingSocialImagePath { get; set; }
    }
}
