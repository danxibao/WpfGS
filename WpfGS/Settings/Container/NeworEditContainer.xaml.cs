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
    public partial class NeworEditContainer : Window
    {
        int index;
        ContainerPara cp;
        Container C;
        bool Opt;
        //Opt=true,New
        //Opt=false,Edit
        public NeworEditContainer(Container cont,bool opt)
        {
            
            InitializeComponent();

            C = cont;
            Opt = opt;
            if (Opt)
            {
                //Nothing
            }
            else
            {
                index = C.list1.SelectedIndex;
                cp = Settings.listcp[index];

                Description.Text = cp.Description;
                n1.Text = cp.nums[0].ToString();
                n2.Text = cp.nums[1].ToString();
                n3.Text = cp.nums[2].ToString();
                n4.Text = cp.nums[3].ToString();
                n5.Text = cp.nums[4].ToString();
                n6.Text = cp.nums[5].ToString();
            }
            


        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            bool isOK=true;
            ContainerPara tmp = new ContainerPara();
            foreach (ContainerPara exist in Settings.listcp)
            {
                if (exist.Description == Description.Text)
                {
                    System.Windows.MessageBox.Show(
                    "容器名称已存在",
                    "错误",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                    return;
                }
            }
            if ("" == (tmp.Description = Description.Text)) isOK = false;
            isOK &= double.TryParse(n1.Text,out tmp.nums[0]);
            isOK &= double.TryParse(n2.Text, out tmp.nums[1]);
            isOK &= double.TryParse(n3.Text, out tmp.nums[2]);
            isOK &= double.TryParse(n4.Text, out tmp.nums[3]);
            isOK &= double.TryParse(n5.Text, out tmp.nums[4]);
            isOK &= double.TryParse(n6.Text, out tmp.nums[5]);

            if (isOK)
            {
                if (Opt)
                {
                    Settings.listcp.Add(tmp);
                }
                else
                 cp.Copy(tmp);

                C.Refresh();
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
    }
}
