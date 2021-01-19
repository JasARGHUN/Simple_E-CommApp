namespace SimpleTemplate_Shop.Models.ViewModels
{
    public class InfoEditViewModel : InfoCreateViewModel
    {
        public int Id { get; set; }
        public string ExistingImagePath { get; set; }
        public string ExistingSocialImagePath { get; set; }
        public string ExistingAppHomeImage { get; set; }
        public string ExistingAppHomeImageFirst { get; set; }
        public string ExistingAppHomeImageSecond { get; set; }
    }
}
