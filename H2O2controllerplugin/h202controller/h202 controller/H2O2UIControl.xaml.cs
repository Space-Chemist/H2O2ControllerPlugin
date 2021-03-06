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
using System.Windows.Navigation;
using System.Windows.Shapes;
using NLog;

namespace h202_controller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class H2O2UIControl : UserControl
    {
        public static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public H2O2UIControl()
        {
            InitializeComponent();
        }


        public H2O2UIControl(H202Core plugin) : this() {
            Plugin = plugin;
            DataContext = plugin.Config;
        }

        private h202_controller.H202Core Plugin { get; }
    }
}
