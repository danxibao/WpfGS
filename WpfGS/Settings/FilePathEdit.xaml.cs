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
using System.Windows.Forms;
using System.IO;


namespace WpfGS
{
    /// <summary>
    /// Interaction logic for FilePathEdit.xaml
    /// </summary>
    public partial class FilePathEdit : System.Windows.Controls.UserControl
    {
        public FilePathEdit()
        {
            InitializeComponent();

            Settings.ReadSettings();
            text1.Text = Settings.CalibrationPath;
            text2.Text = Settings.TransmissionPath;
            text3.Text = Settings.DataPath;
        }


        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                text1.Text = fbd.SelectedPath;
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                text2.Text = fbd.SelectedPath;
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                text3.Text = fbd.SelectedPath;
            }
        }


        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            saveData();
            

        }

        private void ButtonDefault_Click(object sender, RoutedEventArgs e)
        {
            Settings.ReadSettings();
            text1.Text = Settings.CalibrationPath;
            text2.Text = Settings.TransmissionPath;
            text3.Text = Settings.DataPath;
        }
        public void saveData()
        {
            if (Directory.Exists(text1.Text))
            {
                if (Directory.Exists(text2.Text))
                {
                    if (Directory.Exists(text3.Text))
                    {
                        Settings.CalibrationPath = text1.Text;
                        StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "\\Settings\\CalibrationPath.txt");
                        sw.WriteLine(Settings.CalibrationPath);
                        sw.Close();
                        Settings.TransmissionPath = text2.Text;
                        sw = new StreamWriter(Environment.CurrentDirectory + "\\Settings\\TransmissionPath.txt");
                        sw.WriteLine(Settings.TransmissionPath);
                        sw.Close();
                        Settings.DataPath = text3.Text;
                        sw = new StreamWriter(Environment.CurrentDirectory + "\\Settings\\DataPath.txt");
                        sw.WriteLine(Settings.DataPath);
                        sw.Close();

                        //this.Close();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show(
                        "重建路径错误",
                        "ExpenseIt Standalone",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show(
                                    "测量数据路径错误",
                                    "ExpenseIt Standalone",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                }

            }
            else
            {
                System.Windows.MessageBox.Show(
                "校核路径错误",
                "ExpenseIt Standalone",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
            }
        }
    }
}
