using System.Reflection;
using System.Windows;
using Ninject;

namespace Sample.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private IKernel kernel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureNinject();
            OpenMainWindow();
        }

        private void ConfigureNinject()
        {
            kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Load(Assembly.Load("Sample.DataSource"));
        }

        private void OpenMainWindow()
        {
            Current.MainWindow = kernel.Get<MainWindow>();
            Current.MainWindow.Show();
        }
    }
}