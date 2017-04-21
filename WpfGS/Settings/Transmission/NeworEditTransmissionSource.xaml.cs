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
    /// Interaction logic for NeworEditContainer.xaml
    /// </summary>
    public partial class NeworEditTransmissionSource : Window
    {
        int index;
        TransmissionSource Ts;
        TransmissionSourcePara tsp = new TransmissionSourcePara();

        bool Opt;
        //Opt=true,New
        //Opt=false,Edit
        public NeworEditTransmissionSource(TransmissionSource ts,bool opt)
        {
            
            InitializeComponent();


            Ts = ts;
            Opt = opt;
            if (Opt)
            {
                DataContext = tsp;
            }
            else
            {
                index=ts.list1.SelectedIndex;
                tsp.Copy(Settings.listtsp[index]);
                DataContext = tsp;
            }
            


        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            bool isOK = true;
            foreach (TransmissionSourcePara exist in Settings.listtsp)
            {
                if (exist.Description == Description.Text)
                {
                    System.Windows.MessageBox.Show(
                    "标准源名称已存在",
                    "错误",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                    return;
                }
            }
            if ("" == Description.Text) isOK = false;

            if (isOK)
            {
                if (Opt)
                {
                    Settings.listtsp.Add(tsp);
                }
                else
                {
                    Settings.listtsp[index].Copy(tsp);
                }

                Ts.Refresh();
                this.Close();
            }
            else
            {
                System.Windows.MessageBox.Show(
                "参数有误，请修改",
                "错误",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedIndex != -1)
            {
                var i = datagrid.SelectedIndex;
                tsp.TSRows.RemoveAt(i);

                var tmp = datagrid.ItemsSource;
                datagrid.ItemsSource = null;
                datagrid.ItemsSource = tmp;
            }
            

        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            tsp.TSRows.Add(new TSRow());

            var tmp = datagrid.ItemsSource;
            datagrid.ItemsSource = null;
            datagrid.ItemsSource = tmp;

        }
    }
}
