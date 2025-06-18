using Microsoft.Extensions.Logging;
using TaskPulse.ViewModel;
using CommunityToolkit.Maui;
using TaskPulse.Interfaces;
using TaskPulse.Repositories;

namespace TaskPulse;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        }).UseMauiCommunityToolkit();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddTransient<TaskDetailPage>();
        builder.Services.AddTransient<TaskDetailViewModel>();

        builder.Services.AddScoped<ITaskRepository, TaskRepository>();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}