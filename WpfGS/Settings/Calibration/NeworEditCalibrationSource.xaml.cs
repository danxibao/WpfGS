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
    public partial class NeworEditCalibrationSource : Window
    {
        int index;
        CalibrationSource Cs;
        CalibrationSourcePara csp = new CalibrationSourcePara();

        
        bool Opt;
        //Opt=true,New
        //Opt=false,Edit
        public NeworEditCalibrationSource(CalibrationSource cs,bool opt)
        {
            
            InitializeComponent();


            Cs = cs;
            Opt = opt;
            if (Opt)
            {
                DataContext = csp;
            }
            else
            {
                index=cs.list1.SelectedIndex;
                csp.Copy(Settings.listcsp[index]);
                DataContext = csp;
            }
            


        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            bool isOK = true;
            foreach (CalibrationSourcePara exist in Settings.listcsp)
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
            int tmp;
            isOK &= int.TryParse(n1.Text, out tmp);

            if (isOK)
            {
                if (Opt)
                {
                    Settings.listcsp.Add(csp);
                }
                else
                {
                    Settings.listcsp[index].Copy(csp);
                }

                Cs.Refresh();
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
                csp.CSRows.RemoveAt(i);

                var tmp = datagrid.ItemsSource;
                datagrid.ItemsSource = null;
                datagrid.ItemsSource = tmp;
            }
            
            
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            csp.CSRows.Add(new CSRow());

            var tmp = datagrid.ItemsSource;
            datagrid.ItemsSource = null;
            datagrid.ItemsSource = tmp;

        }

    }
}
