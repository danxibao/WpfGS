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
    public partial class Container : UserControl
    {

        public Container()
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
                NeworEditContainer nec = new NeworEditContainer(this, false);
                nec.Show();
            }
            
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            NeworEditContainer nec = new NeworEditContainer(this, true);
            nec.Show();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (list1.SelectedIndex != -1)
            {
                int index = list1.SelectedIndex;
                list1.Items.RemoveAt(index);
                Settings.listcp.RemoveAt(index);

            }
        }

        void loadData()
        {
            Settings.listcpLoad();

            Refresh();
        }

        public void Refresh()
        {
            list1.Items.Clear();
            foreach (ContainerPara cp in Settings.listcp)
            {
                list1.Items.Add(cp.Description);
            }
        }

        public void saveData()
        {
            Settings.listcpSave();
        }
    }
}
