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
    /// Interaction logic for Detector.xaml
    /// </summary>
    public partial class Detector : UserControl
    {

        public Detector()
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
                NeworEditDetector nec = new NeworEditDetector(this, false);
                nec.Show();
            }
            
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            NeworEditDetector nec = new NeworEditDetector(this, true);
            nec.Show();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (list1.SelectedIndex != -1)
            {
                int index = list1.SelectedIndex;
                list1.Items.RemoveAt(index);
                Settings.listdp.RemoveAt(index);

            }
        }

        void loadData()
        {
            Settings.listdpLoad();

                Refresh();
            
        }

        public void Refresh()
        {
            list1.Items.Clear();
            foreach (DetectorPara dp in Settings.listdp)
            {
                list1.Items.Add(dp.Description);
            }
        }
        public void saveData()
        {

            Settings.listdpSave();
        }
    }
}
