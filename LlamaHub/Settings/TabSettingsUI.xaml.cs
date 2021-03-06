﻿using System;
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

namespace Hub.Settings
{
    /// <summary>
    /// Logique d'interaction pour TabSettingsGUI.xaml
    /// </summary>
    public partial class TabSettingsUI : UserControl
    {
        public TabSettingsUI()
        {
            InitializeComponent();
            TabControl.Items.Add(new TabItem()
            {
                Content = new TabManagerSettings(),
                Header = "TabManager"
            });

            TabControl.Items.Add(new TabItem()
            {
                Content = new ProfileSettings(),
                Header = "Profile"
            });
        }
    }
}
