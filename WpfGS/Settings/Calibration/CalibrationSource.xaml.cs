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
using System.IO;

namespace WpfGS
{
    /// <summary>
    /// Interaction logic for Container.xaml
    /// </summary>
    public partial class CalibrationSource : UserControl
    {
        
        public CalibrationSource()
        {
            InitializeComponent();

            loadData();
            
            
        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            saveData();

        }
        private void ButtonDefault_Click(object sender, RoutedEventArgs e)
        {
            loadData();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (list1.SelectedIndex != -1)
            {
                NeworEditCalibrationSource necs = new NeworEditCalibrationSource(this, false);
                necs.Show();
            }
            
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            NeworEditCalibrationSource necs = new NeworEditCalibrationSource(this, true);
            necs.Show();
        }

        
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (list1.SelectedIndex != -1)
            {
                int index = list1.SelectedIndex;
                list1.Items.RemoveAt(index);
                Settings.listcsp.RemoveAt(index);

            }
        }

        void loadData()
        {

            Settings.listcspLoad();

            Refresh();
            
        }
        public void Refresh()
        {
            list1.Items.Clear();
            foreach (CalibrationSourcePara csp in Settings.listcsp)
            {
                list1.Items.Add(csp.Description);
            }
        }

        public void saveData()
        {
            Settings.listcspSave();
        }
    }
}

