using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PanCard.Views.ContentViews;
using System.Collections.ObjectModel;

namespace PanCard.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        Page Page;
        [ObservableProperty]
        private ObservableCollection<View> viewDV = new ObservableCollection<View>();
        [ObservableProperty]
        private List<ImageButton> imgTabButtons = new List<ImageButton>();
        private int selectedViewModelIndex;
        public int SelectedViewModelIndex
        {
            get { return selectedViewModelIndex; }
            set
            {
                if (SelectedViewModelIndex != value)
                {
                    selectedViewModelIndex = value;
                    if (selectedViewModelIndex != -1)
                    {
                        SelectTabButton(selectedViewModelIndex, "svmi");
                    }

                }
                OnPropertyChanged();
            }
        }
        public MainPageViewModel(Page page)
        {
            try
            {
                Page = page;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        [RelayCommand]
        private async Task SetupControl()
        {

            try
            {
                await ExecuteSetupControlCommand();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading {ex}");
            }
        }
        private void AddTabMenu()
        {
            ImageButton imageButton = new ImageButton() { Source = "button" };
            imageButton.Clicked += Tb_Clicked;
            ImgTabButtons.Add(imageButton);
        }
        private void Tb_Clicked(object sender, EventArgs e)
        {

            try
            {
                var index = -1;
                if (sender.GetType() == typeof(ImageButton))
                {
                    var imgCtrl = (ImageButton)sender;

                    index = ImgTabButtons.FindIndex(x => x == imgCtrl);

                    SelectedViewModelIndex = index;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {

            }
        }
        private async Task ExecuteSetupControlCommand()
        {
            try
            {
                var cvPage = Page.FindByName<PanCardView.CarouselView>("cvPage");

                if (cvPage != null)
                {
                    SelectedViewModelIndex = -1;
                    ViewDV.Add(new cv1());
                    ViewDV.Add(new cv2());
                    ViewDV.Add(new cv1());
                    ViewDV.Add(new cv2());
                }
                    AddTabMenu();
                    AddTabMenu();
                    AddTabMenu();
                    AddTabMenu();

                    SetupTabMenuIcon();

                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private async void SetupTabMenuIcon()
        {
            try
            {
                var gridMenuIcon = Page.FindByName<Grid>("gridMenuIcon");

                // Clear the children only if necessary, or ensure they are fully disposed
                if (gridMenuIcon != null)
                {
                    gridMenuIcon.Children.Clear();
                    gridMenuIcon.ColumnDefinitions.Clear();

                    for (int i = 0; i < ImgTabButtons.Count; i++)
                    {
                        gridMenuIcon.ColumnDefinitions.Add(new ColumnDefinition());
                        if (i == 0)
                        {
                            ImgTabButtons[i].BackgroundColor = Colors.Gold;

                        }
                        gridMenuIcon.Add(ImgTabButtons[i], i);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private async Task SelectTabButton(int value, string fromWhere)
        {
            try
            {
                // Ensure UI operations are done on the main thread
                await MainThread.InvokeOnMainThreadAsync(() =>
                {

                    var gridMenuIcon = Page.FindByName<Grid>("gridMenuIcon");
                    for (int x = 0; x < gridMenuIcon.Children.Count; x++)
                    {
                        if (x == value)
                        {
                            ((ImageButton)gridMenuIcon.Children[x]).BackgroundColor = Colors.Gold;
                        }
                        else
                        {
                            ((ImageButton)gridMenuIcon.Children[x]).BackgroundColor = Colors.Transparent;

                        }
                    }

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

    }
}
