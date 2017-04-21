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

namespace WpfGS
{
    /// <summary>
    /// Interaction logic for CalibrationMeasure.xaml
    /// </summary>
    public partial class CalibrationMeasure : Window
    {
        public CalibrationMeasure()
        {
            InitializeComponent();

            Settings.listcpLoad();
            Settings.listcspLoad(); 
            Settings.listdpLoad();
            drum.ItemsSource = Settings.listcp;
            drum.DisplayMemberPath = "Description";
            source.ItemsSource = Settings.listcsp;
            source.DisplayMemberPath = "Description";
            detector.ItemsSource = Settings.listdp;
            detector.DisplayMemberPath = "Description";
            
        }
    }
}
