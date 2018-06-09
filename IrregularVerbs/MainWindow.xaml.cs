using System.Windows;

namespace IrregularVerbs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            new LearnView().Show();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new AddView().Show();
        }
    }
}
