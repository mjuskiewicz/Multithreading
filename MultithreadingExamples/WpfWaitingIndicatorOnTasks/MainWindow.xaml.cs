using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfWaitingIndicatorOnTasks.Services;
using WpfWaitingIndicatorOnTasks.ViewModels;

namespace WpfWaitingIndicatorOnTasks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var dataService = new ExampleService();

            AggregatorService aggregatorService = new AggregatorService();
            aggregatorService.RegisterTask("Allegro service ", () => dataService.LongRunningMethod(1000));
            aggregatorService.RegisterTask("Youtube service", () => dataService.LongRunningMethod(2000));
            aggregatorService.RegisterTask("eBay service", () => dataService.LongRunningMethod(3000));
            aggregatorService.RegisterTask("Google service", () => dataService.LongRunningMethod(4000));
            aggregatorService.RegisterTask("Binq service", () => dataService.LongRunningMethod(2000));
            aggregatorService.RegisterTask("Yahoo service", () => dataService.LongRunningMethod(5000));
            aggregatorService.Build();

            DataContext = new SplashScreenViewModel(aggregatorService);
        }
    }
}
