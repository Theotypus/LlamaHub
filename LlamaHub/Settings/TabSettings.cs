using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hub.Settings
{
    class TabSettings : Tab  // Controls the TabSettings
    {
        public override string Header { get; protected set; } = "Settings";
        public override UIElement RootElement { get; protected set; } = new TabSettingsUI();
        public override TabItem TabItem { get; set; }
        public override Color PrimaryColor { get; protected set; } = Colors.Gray;
        public override string PreviewImage { get; protected set; } = null;
    }
}
