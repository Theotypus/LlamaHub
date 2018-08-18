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
using System.Windows.Shapes;

namespace Hub.Settings
{
    /// <summary>
    /// Logique d'interaction pour TabPickerDialog.xaml
    /// </summary>
    public partial class TabPickerDialog : Window  // Minimalistic Dialog to pick a new Tab
    {
        public List<Tab> Answer
        {
            get
            {
                return Selected;
            }
        }

        private List<Tab> Selected = new List<Tab>();

        public TabPickerDialog()
        {
            InitializeComponent();
            //ListBox.ItemsSource = TabManager.GetAllTabs();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Item_SelectionChanged(object sender, RoutedEventArgs e)
        {
            CheckBox check = sender as CheckBox;
            int index = ListBox.Items.IndexOf(check.DataContext);
            Tab selected = (Tab)ListBox.Items[index];
            if (check.IsChecked == true)
                Selected.Add(selected);
            else
                Selected.Remove(selected);
        }

        private void Item_Click(object sender, MouseButtonEventArgs e)
        {
            Grid clicked = sender as Grid;
            int index = ListBox.Items.IndexOf(clicked.DataContext);
            ListBoxItem item = ListBox.ItemContainerGenerator.ContainerFromIndex(index) as ListBoxItem;

            if (item == null)
                return;

            CheckBox box = FindDescendant<CheckBox>(item);
            if (box.IsChecked == true)
                box.IsChecked = false;
            else
                box.IsChecked = true;
        }

        public T FindDescendant<T>(DependencyObject obj) where T : DependencyObject
        {
            // Check if this object is the specified type
            if (obj is T)
                return obj as T;

            // Check for children
            int childrenCount = VisualTreeHelper.GetChildrenCount(obj);
            if (childrenCount < 1)
                return null;

            // First check all the children
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child is T)
                    return child as T;
            }

            // Then check the childrens children
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = FindDescendant<T>(VisualTreeHelper.GetChild(obj, i));
                if (child != null && child is T)
                    return child as T;
            }

            return null;
        }
    }
}
