using PanCard.ViewModels;

namespace PanCard
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel vm;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = vm = new MainPageViewModel(this);
            vm.SetupControlCommand.Execute(null);
        }
    }
}
