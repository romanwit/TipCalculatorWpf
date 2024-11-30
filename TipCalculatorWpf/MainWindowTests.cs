using NUnit.Framework;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TipCalculatorWpf;

namespace TipCalculatorWpf.Tests
{
    [TestFixture]
    public class MainWindowUITests
    {
        private MainWindow _mainWindow;
        private ManualResetEvent _loadedEvent = new ManualResetEvent(false); 

        [SetUp]
        public void Setup()
        {
            if (Application.Current == null)
            {
                var application = new Application();
                application.ShutdownMode = ShutdownMode.OnExplicitShutdown;

                var resourceDictionary = new ResourceDictionary();
                resourceDictionary.MergedDictionaries.Add(
                    new ResourceDictionary
                    {
                        Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml")
                    });
                resourceDictionary.MergedDictionaries.Add(
                    new ResourceDictionary
                    {
                        Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesign2.Defaults.xaml")
                    });


                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            }

         

            Thread newWindowThread = new Thread(new ThreadStart(() =>
                {
                    _mainWindow = new MainWindow();
                    _mainWindow.InitializeComponent();

                    _mainWindow.Loaded += _mainWindow_Loaded;

                    _mainWindow.Show();


                    System.Windows.Threading.Dispatcher.Run();

                    _loadedEvent.Set();
                }));

                newWindowThread.SetApartmentState(ApartmentState.STA);

                newWindowThread.Start();

                _loadedEvent.WaitOne();
            

        }

        private void _mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _loadedEvent.Set();
        }

        /*[TearDown]
        public void Teardown()
        {
            _mainWindow.Close();
        }*/

        [Test]
        [STAThread]
        public async Task AddRecordButton_ExecutesCommand()
        {

            await _mainWindow.Dispatcher.InvokeAsync(() =>
            {
                var button = FindVisualChild<Button>(_mainWindow, "Add Record");
                var viewModel = _mainWindow.DataContext as MainViewModel;

                Assert.That(button != null);
                Assert.That(viewModel != null);

                button.Command.Execute(null);

                Assert.That(viewModel.Records.Count == 1);
                viewModel.Records.Clear();
                _mainWindow.Close();
                
            });
        }

        [Test]
        [STAThread]
        public async Task DataGrid_BindsToRecords()
        {
            await _mainWindow.Dispatcher.InvokeAsync(() =>
            {
                var dataGrid = FindVisualChild<DataGrid>(_mainWindow);
                var viewModel = _mainWindow.DataContext as MainViewModel;

                Assert.That(dataGrid != null);
                Assert.That(viewModel != null);

                // Act
                viewModel.Records.Add(new TipRecord { Amount = 100, Tip = 10 });

                // Assert
                Assert.That(dataGrid.Items.Count == 2);
                var firstRow = dataGrid.Items[0] as TipRecord;
                Assert.That(firstRow != null);
                Assert.That(firstRow.Amount == 100);
                Assert.That(firstRow.Tip == 10);
                viewModel.Records.Clear();
                _mainWindow.Close();
            });
        }

        [Test]
        [STAThread]
        public async Task TotalAmountTextBlock_UpdatesCorrectly()
        {
            await _mainWindow.Dispatcher.InvokeAsync(() =>
            {
                var card = VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(_mainWindow, 0), 0), 0), 0), 2);
                var viewModel = _mainWindow.DataContext as MainViewModel;

                Assert.That(card != null);
                Assert.That(viewModel != null);

                viewModel.Records.Add(new TipRecord { Amount = 200, Tip = 20 });

                Assert.That(card.ToString().Contains("240.00"));
            });
        }

        // Utility method to find visual child elements
        private T FindVisualChild<T>(DependencyObject parent, string content = null) where T : FrameworkElement
        {
            
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T typedChild)
                {
                    if (content == null || typedChild is ContentControl cc && cc.Content?.ToString() == content)
                        return typedChild;
                }

                var result = FindVisualChild<T>(child, content);
                if (result != null)
                    return result;
            }

            return null;
        }
    }
}

