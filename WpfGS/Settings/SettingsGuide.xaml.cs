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
    /// Interaction logic for SettingsGuide.xaml
    /// </summary>
    public partial class SettingsGuide : Window
    {
        public SettingsGuide()
        {
            InitializeComponent();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            UCc.saveData();
            UCcs.saveData();
            UCd.saveData();
            UCf.saveData();
            UCts.saveData();
            this.Close();
        }

        private void ButtonSaveAll_Click(object sender, RoutedEventArgs e)
        {
            UCc.saveData();
            UCcs.saveData();
            UCd.saveData();
            UCf.saveData();
            UCts.saveData();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
