using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AstronomicalProcessing
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

        private void BtnRandClick_Click(object sender, RoutedEventArgs e)
        {
            if(AstroDataLBX.Items.Count >= 24)
            {
                AstroDataLBX.Items.Clear();
            }
            if (!(AstroDataLBX.Items.Count >= 24))
            {
                for (int i = 0; i < 24; i++)
                {
                    Random random = new Random();
                    AstroDataLBX.Items.Add(random.Next(10, 91));
                }
                StatusMessage("Added Element");
            }
            else
            {
                MessageBox.Show("Already have 24 elements in the list", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void BtnSort_Click(object sender, RoutedEventArgs e)
        {

        }// end sort

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {

        }// end search

        private void BtnInput_Click(object sender, RoutedEventArgs e)
        {
            if(InputBox.Text.Length > 0 && AstroDataLBX.SelectedItem != null)
            {
                bool isDigit = true;
                foreach(char c in InputBox.Text)
                {
                    if (!Char.IsDigit(c))
                    {
                        isDigit = false;
                        break;
                    }
                }
                if (isDigit == true)
                {
                    int selectedIndex = AstroDataLBX.SelectedIndex;
                    int.TryParse(InputBox.Text, out int value);
                    AstroDataLBX.Items.RemoveAt(selectedIndex);
                    AstroDataLBX.Items[selectedIndex] = value;
                    StatusMessage("Replaced Element");
                }
                else
                {
                    MessageBox.Show("Input isnt a integer, Please enter a number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }// end input

        public void StatusMessage(string message)
        {
            StatusTxt.Text = $"Status: {message}";
        }
    }
}