using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hub
{
    abstract public class Tab  // Superclass for all Tabs
    {
        public abstract string Header { get; protected set; }  // Title of the Tab
        public abstract UIElement RootElement { get; protected set; }  // Graphical root element that should be display in the Tab
        public abstract TabItem TabItem { get; set; }  // The TabItem this Tab is displayed in
        public abstract Color PrimaryColor { get; protected set; }  // Primary Color of the Tab, displayed in the header
        public abstract string PreviewImage { get; protected set; }
    }
}
