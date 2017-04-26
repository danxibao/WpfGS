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
    /// Interaction logic for NeworEditDetector.xaml
    /// </summary>
    public partial class NeworEditDetector : Window
    {
        int index;
        DetectorPara dp;
        Detector D;
        bool Opt;
        //Opt=true,New
        //Opt=false,Edit
        public NeworEditDetector(Detector cont,bool opt)
        {
            
            InitializeComponent();

            D = cont;
            Opt = opt;
            if (Opt)
            {
                //Nothing
            }
            else
            {
                index = D.list1.SelectedIndex;
                dp = Settings.listdp[index];

                Description.Text = dp.Description;
                Information.Text = dp.Information;
                DetectorIP.Text = dp.DetectorIP;
                MotorIP.Text = dp.MotorIP;

            }
            


        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            bool isOK=true;
            DetectorPara tmp = new DetectorPara();

            if(Opt)
            foreach (DetectorPara exist in Settings.listdp)
            {
                if (exist.Description == Description.Text)
                {
                    System.Windows.MessageBox.Show(
                    "探测器名称已存在",
                    "错误",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                    return;
                }
            }
            if ("" == (tmp.Description = Description.Text)) isOK = false;
            if ("" == (tmp.Information = Information.Text)) isOK = false;
            if ("" == (tmp.DetectorIP = DetectorIP.Text)) isOK = false;
            if ("" == (tmp.MotorIP = MotorIP.Text)) isOK = false;

            if (isOK)
            {
                if (Opt)
                {
                    Settings.listdp.Add(tmp);
                }
                else
                    dp.Copy(tmp);

                D.Refresh();
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
