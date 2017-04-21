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
    public partial class TransmissionSource : UserControl
    {
        
        public TransmissionSource()
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
                NeworEditTransmissionSource necs = new NeworEditTransmissionSource(this, false);
                necs.Show();
            }
            
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            NeworEditTransmissionSource necs = new NeworEditTransmissionSource(this, true);
            necs.Show();
        }

        
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (list1.SelectedIndex != -1)
            {
                int index = list1.SelectedIndex;
                list1.Items.RemoveAt(index);
                Settings.listtsp.RemoveAt(index);

            }
        }

        void loadData()
        {
            Settings.listtspLoad();

             Refresh();
            
        }
        public void Refresh()
        {
            list1.Items.Clear();
            foreach (TransmissionSourcePara tsp in Settings.listtsp)
            {
                list1.Items.Add(tsp.Description);
            }
        }

        public void saveData()
        {
            Settings.listtspSave();
        }
    }
}

