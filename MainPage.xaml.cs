using TaskPulse.Model;
using TaskPulse.ViewModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace TaskPulse;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel mainViewModel)
    {
        InitializeComponent();
        BindingContext = mainViewModel;
    }
}

