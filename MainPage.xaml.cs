using TaskPulse.Model;
using TaskPulse.ViewModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace TaskPulse;

public partial class MainPage : ContentPage
{
    private MainViewModel _mainViewModel;
    public MainPage(MainViewModel mainViewModel)
    {
        InitializeComponent();
        _mainViewModel = mainViewModel;
        BindingContext = mainViewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _mainViewModel.LoadTasks();
    }
}