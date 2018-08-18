using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hub.Settings
{
    /// <summary>
    /// Logique d'interaction pour TabsSettings.xaml
    /// </summary>
    public partial class TabManagerSettings : UserControl
    {
        public TabManagerSettings()
        {
            InitializeComponent();
            //ListBox.ItemsSource = TabManager.Tabs;
        }

        private void Item_SelectionChanged(object sender, RoutedEventArgs e)  // Called when a checkbox is check/unchecked
        {
            ToggleButton check = sender as ToggleButton;
            int index = ListBox.Items.IndexOf(check.DataContext);
            Tab selected = (Tab)ListBox.Items[index];
            //TabManager.Enable(selected, check.IsChecked ?? false);  // Enable/Disable the selected Tab
        }

        private void AddTabButton_Click(object sender, RoutedEventArgs e)  // Show a popup dialog to pick a Tab
        {
            TabPickerDialog inputDialog = new TabPickerDialog();
            if (inputDialog.ShowDialog() == true)
                foreach (Tab tab in inputDialog.Answer) { }
            //TabManager.AddTab(tab);
        }

        private void DeleteTab_Click(object sender, RoutedEventArgs e)  // Called when the 'X' button is pressed
        {
            Button button = sender as Button;
            int index = ListBox.Items.IndexOf(button.DataContext);
            Tab selected = (Tab)ListBox.Items[index];
            //TabManager.DeleteTab(selected);  // Remove the selected Tab
        }
    }

    class PreviewIconPathConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = value as string;
            return "../" + path;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
