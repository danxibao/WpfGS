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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;


namespace WpfGS
{
    /// <summary>
    /// Interaction logic for CalibrationGraph.xaml
    /// </summary>
    public partial class CalibrationGraph : Window
    {
        

        public CalibrationGraph()
        {
            Settings.ReadSettings();

            InitializeComponent();

            DirectoryInfo theFolder = new DirectoryInfo(Settings.CalibrationPath);

            treeview.ItemsSource = Bind(theFolder);
            datagrid.ItemsSource = list;
        }

        List<Node> Bind(DirectoryInfo TheFolder)
        {
            List<Node> nodes = new List<Node>();
            

            //遍历文件夹
            foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
            {
                Node n=new Node { Name=NextFolder.Name, fpath = NextFolder.FullName , Kind="folder"};
                nodes.Add(n);
                n.Nodes = Bind(NextFolder);
            }
                
            //遍历文件
            foreach (FileInfo NextFile in TheFolder.GetFiles())
                nodes.Add(new Node { Name = NextFile.Name, fpath = NextFile.FullName });


            return nodes;
        }

        public List<ECPara> list = new List<ECPara>();
        List<double> Energy = new List<double>();
        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            var n=treeview.SelectedItem as Node;
            if(n.Kind!="folder")
            {
                StreamReader sr = new StreamReader(n.fpath);
                sr.ReadLine();
                string line=sr.ReadLine();
                string[] data = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                
                for(int i=4;i<data.Length;i++){
                    double tmp;
                    double.TryParse(data[i].Substring(1,data[i].Length-2),out tmp);
                    Energy.Add(tmp);

                    //System.Windows.MessageBox.Show(tmp.ToString(),"ExpenseIt Standalone",MessageBoxButton.OK,MessageBoxImage.Information);

                }
                
                sr.ReadLine();

                line=sr.ReadLine();
                while (line[0] != 'E')
                {
                    data = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    ECPara ecp = new ECPara();
                    
                    double.TryParse(data[0], out ecp.Density);
                    double.TryParse(data[1], out ecp.dY);
                    double.TryParse(data[2], out ecp.Height);
                    for (int i = 3; i < data.Length; i++)
                    {
                        double tmp;
                        double.TryParse(data[i], out tmp);
                        evsr er = new evsr(Energy[i-3],tmp);
                        
                        ecp.list.Add(er);

                        //System.Windows.MessageBox.Show(tmp.ToString(),"ExpenseIt Standalone",MessageBoxButton.OK,MessageBoxImage.Information);

                    }
                    list.Add(ecp);

                    line = sr.ReadLine();
                }
                
            }
                
        }



        
    }

    
}
