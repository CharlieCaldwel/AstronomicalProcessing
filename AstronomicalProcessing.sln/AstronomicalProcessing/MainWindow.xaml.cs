// Charlie Caldwell, CharliesTeam, Sprint 1
// 11/09/2025
// Version: 1
// Astronomical Processing
// Program allows you to enter 24 random integers and sort them / modify them / search for a integer
// To sort it uses a bubble sort, to search it uses a binary search method



using System.Text;
using System.Text.RegularExpressions;
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
        private readonly Regex _int = new(@"^-?\d+$");
        bool Sorted = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnRandClick_Click(object sender, RoutedEventArgs e)
        {
            if(AstroDataLBX.Items.Count >= 23) // clears it so the ListBox cant go over 24 integers
            {
                AstroDataLBX.Items.Clear();
            }
            if (!(AstroDataLBX.Items.Count >= 24)) // fills the list with random integers
            {
                for (int i = 0; i < 24; i++)
                {
                    Random random = new Random();
                    AstroDataLBX.Items.Add(random.Next(10, 91));
                }
                Sorted = false;
            }
            else
            {
                MessageBox.Show("Already have 24 elements in the list", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSort_Click(object sender, RoutedEventArgs e)
        {
            sort();
        }// end sort

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if(SearchBox.Text.Length > 0 && Sorted == true && _int.IsMatch(SearchBox.Text)){ 
            int min = 0;
            int max = AstroDataLBX.Items.Count;
            int mid = (min + max) / 2;

                while (min != max || int.Parse(AstroDataLBX.Items[mid].ToString()) != int.Parse(SearchBox.Text)) // keeps in loop whilst it finds the number
                {
                    if (int.Parse(SearchBox.Text) > int.Parse(AstroDataLBX.Items[mid].ToString())) // If number is greater then mid min = mid
                    {
                        min = mid;
                        mid = (min + max) / 2;
                    }

                    else if (int.Parse(SearchBox.Text) < int.Parse(AstroDataLBX.Items[mid].ToString())) // if number is less then mid max = mid
                    {
                        max = mid;
                        mid = (min + max) / 2;
                    }

                    if (int.Parse(AstroDataLBX.Items[mid].ToString()) == int.Parse(SearchBox.Text))
                    {
                        AstroDataLBX.SelectedIndex = mid;
                        MessageBox.Show($"Item found at index: {AstroDataLBX.SelectedIndex + 1}", "Found", MessageBoxButton.OK, MessageBoxImage.Information);
                        AstroDataLBX.ScrollIntoView(AstroDataLBX.SelectedIndex);
                        break;
                    }
                    else if (min == max && int.Parse(AstroDataLBX.Items[mid].ToString()) != int.Parse(SearchBox.Text))
                    {
                        MessageBox.Show("Item not found", "Not found", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }

                    if((min + max) / 2 == min)
                    {
                        max = min;
                    }
                }
            }
            else if (AstroDataLBX.Items.Count <= 0) // fail safes to prevent errors
            {
                MessageBox.Show("Please input data into the list before searching", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if(Sorted == false)
            {
                MessageBox.Show("Please sort the list before searching", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if(!_int.IsMatch(SearchBox.Text))
            {
                MessageBox.Show("Please enter a valid integer to search for", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }// end search

        private void BtnInput_Click(object sender, RoutedEventArgs e)
        {
            if(InputBox.Text.Length > 0 && AstroDataLBX.SelectedItem != null)
            {
                bool isDigit = true;
                foreach(char c in InputBox.Text) // Checks to see if the input is a digit before inputing it 
                {
                    if (!Char.IsDigit(c))
                    {
                        isDigit = false;
                        break;
                    }
                }
                if (isDigit == true) // if it is a digit it allows you to input data
                {
                    int selectedIndex = AstroDataLBX.SelectedIndex;
                    int.TryParse(InputBox.Text, out int value);
                    AstroDataLBX.Items.RemoveAt(selectedIndex);
                    AstroDataLBX.Items.Add(value);
                    sort();
                }
                else
                {
                    MessageBox.Show("Input isnt a integer, Please enter a number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                Sorted = false;
            }
            else if(AstroDataLBX.Items.Count == 0) // fail safes to prevent errors
            {
                MessageBox.Show("Please input data into the list before modifying", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (InputBox.Text.Length > 0)
            {
                MessageBox.Show("Please input a integer to modify", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (AstroDataLBX.SelectedItem == null)
            {
                MessageBox.Show("Please select a element to modify", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }// end input

        private void TextGotFocus(object sender, KeyboardFocusChangedEventArgs e) // fades text out search box out when in focus
        {
            SearchFieldtxt.Text = "";
        }

        private void TextNotFocus(object sender, KeyboardFocusChangedEventArgs e) // fades text back in search box out when out of focus
        {
            if (SearchBox.Text.Length == 0)
            {
                SearchFieldtxt.Text = "Enter search data";
            }
        }

        private void InputGotFocus(object sender, KeyboardFocusChangedEventArgs e) // fades text out input box out when in focus
        {
            InputFieldtxt.Text = "";
        }

        private void InputNotFocus(object sender, KeyboardFocusChangedEventArgs e) // fades text back in input box out when out of focus
        {
            if (InputBox.Text.Length == 0)
            {
                InputFieldtxt.Text = "Enter data to input";
            }
        }

        public void sort()
        {
            if (AstroDataLBX.Items.Count != 0)
            {
                for (int i = 0; i < AstroDataLBX.Items.Count; i++)
                {
                    for (int j = 0; j < AstroDataLBX.Items.Count; j++)
                    {
                        int num1 = int.Parse(AstroDataLBX.Items[i].ToString());
                        int num2 = int.Parse(AstroDataLBX.Items[j].ToString());

                        if (num1 < num2) // swaps the numbers around if number 1 is less then number 2 
                        {
                            AstroDataLBX.Items[i] = num2;
                            AstroDataLBX.Items[j] = num1;
                        }
                    }
                }
                Sorted = true;
            }
        }
    }
}