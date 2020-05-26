namespace Timer.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            MainViewModel = new MainViewModel();
        }

        public MainViewModel MainViewModel { get; set; }
    }
}
