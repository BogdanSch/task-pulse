using TaskPulse.Model;

namespace TaskPulse;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        InitializeDatabase();
        MainPage = new AppShell();
    }
    private void InitializeDatabase()
    {
        using (AppDatabaseContext database = new AppDatabaseContext())
        {
            database.Database.EnsureCreated();
        }
    }
}
