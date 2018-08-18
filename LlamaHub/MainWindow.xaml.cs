using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System;
using Hub.Settings;
using System.Windows.Input;

namespace Hub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml : FUCK INTERACTION LOGIC
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Tab> Tabs;

        public MainWindow()
        {
            InitializeComponent();
            PreviewKeyDown += new KeyEventHandler(HandleEsc);  // Close when escape pressed

            // Find all subclasses of 'Tab' and create a new instance for each
            Tabs = typeof(Tab)
                    .Assembly.GetTypes()
                    .Where(t => t.IsSubclassOf(typeof(Tab)) && !t.IsAbstract && t != typeof(TabSettings))
                    .OrderBy(t => t.Name)
                    .Select(t => (Tab)Activator.CreateInstance(t))
                    .ToList();

            TabControl.Items.Add(new TabItem()  // Creates a hidden tab to fill the gap between the left and right-aligned Tabs
            {
                Visibility = Visibility.Hidden
            });

            Tab settings = new TabSettings();  // Creates the Settings Tab
            TabItem TabItem = new TabItem()
            {
                Content = settings.RootElement,
                Header = settings.Header,
                Background = new SolidColorBrush(settings.PrimaryColor)
            };
            DockPanel.SetDock(TabItem, Dock.Right);  // Align to the right
            TabItem.HorizontalAlignment = HorizontalAlignment.Right;
            settings.TabItem = TabItem;
            TabControl.Items.Add(TabItem);

            foreach (Tab tab in Tabs)  // Creates a new TabItem for each Tab and display it in the TabControl
                AddTab(tab);

            TabControl.SelectionChanged += TabControl_SelectionChanged;  // Listen to Tab selection changes to change headers color
        }

        private void AddTab(Tab Tab)  // Add a new TabItem to the TabControl
        {
            TabItem tabItem = new TabItem()
            {
                Header = Tab.Header,
                Background = new SolidColorBrush(Tab.PrimaryColor),
                Content = Tab.RootElement
            };
            Tab.TabItem = tabItem;
            TabControl.Items.Insert(TabControl.Items.Count - 2, tabItem);
        }

        private void TabControl_SelectionChanged(object sender, RoutedEventArgs e)  // Triggered when the Tab selection changes
        {
            if (e.Source is TabControl)  // Change the tab's header color
            {
                TabItem selected = TabControl.SelectedValue as TabItem;

                foreach (TabItem tabItem in TabControl.Items)
                {
                    Tab tab = GetTab(tabItem);
                    if (tab == null)
                        return;
                    if (tabItem == selected)
                    {
                        tabItem.Background = new SolidColorBrush(Colors.White);
                        tabItem.Foreground = new SolidColorBrush(tab.PrimaryColor);
                    }
                    else
                    {
                        tabItem.Background = new SolidColorBrush(tab.PrimaryColor);
                        tabItem.Foreground = new SolidColorBrush(Colors.White);
                    }
                }
            }
        }

        private Tab GetTab(TabItem item)  // Return the Tab object linked to a TabItem
        {
            foreach (Tab tab in Tabs)
                if (tab.TabItem == item)
                    return tab;
            return null;
        }


        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
