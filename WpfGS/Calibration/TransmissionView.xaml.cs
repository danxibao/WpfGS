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
using System.Windows.Forms;


namespace WpfGS
{
    
    /// <summary>
    /// Interaction logic for CalibrationView.xaml
    /// </summary>
    public partial class TransmissionView : System.Windows.Controls.UserControl
    {
        public List<ECPara> list = new List<ECPara>();
        List<double> Energy = new List<double>();
        List<KeyValuePair<double, double>> valueList = new List<KeyValuePair<double, double>>();
        Random rdm = new Random();

        DirectoryInfo theFolder;

        public TransmissionView()
        {
            Settings.ReadSettings();

            InitializeComponent();

            theFolder= new DirectoryInfo(Settings.TransmissionPath);
            treeview.ItemsSource = Bind(theFolder);

            //datagrid.ItemsSource = list;


            valueList.Add(new KeyValuePair<double, double>(rdm.Next(0, 100) / 100.0, rdm.Next(0, 100) / 100.0));
            valueList.Add(new KeyValuePair<double, double>(rdm.Next(0, 100) / 100.0, rdm.Next(0, 100) / 100.0));
            valueList.Add(new KeyValuePair<double, double>(rdm.Next(0, 100) / 100.0, rdm.Next(0, 100) / 100.0));
            valueList.Add(new KeyValuePair<double, double>(rdm.Next(0, 100) / 100.0, rdm.Next(0, 100) / 100.0));
            valueList.Add(new KeyValuePair<double, double>(rdm.Next(0, 100) / 100.0, rdm.Next(0, 100) / 100.0));
            valueList.Add(new KeyValuePair<double, double>(rdm.Next(0, 100) / 100.0, rdm.Next(0, 100) / 100.0));
            valueList.Add(new KeyValuePair<double, double>(rdm.Next(0, 100) / 100.0, rdm.Next(0, 100) / 100.0));
            valueList.Add(new KeyValuePair<double, double>(rdm.Next(0, 100) / 100.0, rdm.Next(0, 100) / 100.0));
            valueList.Add(new KeyValuePair<double, double>(rdm.Next(0, 100) / 100.0, rdm.Next(0, 100) / 100.0));
            valueList.Add(new KeyValuePair<double, double>(rdm.Next(0, 100) / 100.0, rdm.Next(0, 100) / 100.0));
            valueList.Add(new KeyValuePair<double, double>(rdm.Next(0, 100) / 100.0, rdm.Next(0, 100) / 100.0));

            lineChart.DataContext = valueList;


        }

        List<Node> Bind(DirectoryInfo TheFolder)
        {
            List<Node> nodes = new List<Node>();


            //遍历文件夹
            foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
            {
                Node n = new Node { Name = NextFolder.Name, father = nodes, fpath = TheFolder, Kind = "folder" };
                nodes.Add(n);
                n.Nodes = Bind(NextFolder);
            }

            //遍历文件
            foreach (FileInfo NextFile in TheFolder.GetFiles())
                nodes.Add(new Node { Name = NextFile.Name, father = nodes, fpath = TheFolder });


            return nodes;
        }

        
        

        private void datagrid_Selected(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedIndex != -1 )
            {

                    datagrid2.ItemsSource = ((ECPara)datagrid.SelectedItem).list;
                    
                    
                    valueList.Clear();
                    foreach (evsr er in datagrid2.ItemsSource)
                    {
                        valueList.Add(new KeyValuePair<double, double>(er.energy, er.rate));
                    }

                    lineChart.DataContext = null;
                    lineChart.DataContext = valueList;
                
            }

           
            
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedIndex != -1)
            {
                var i = datagrid.SelectedIndex;
                list.RemoveAt(i);

                var tmp = datagrid.ItemsSource;
                datagrid.ItemsSource = null;
                datagrid.ItemsSource = tmp;
            }
            

        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            list.Add(new ECPara());

            var tmp = datagrid.ItemsSource;
            datagrid.ItemsSource = null;
            datagrid.ItemsSource = tmp;

        }

        private void ButtonDelete2_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid2.SelectedIndex != -1)
            {
                var i = datagrid2.SelectedIndex;
                valueList.RemoveAt(i);

                var tmp = datagrid2.ItemsSource;
                datagrid2.ItemsSource = null;
                datagrid2.ItemsSource = tmp;
            }
            

        }

        private void ButtonAdd2_Click(object sender, RoutedEventArgs e)
        {
            ((ECPara)datagrid.SelectedItem).list.Add(new evsr(0,0));

            var tmp = datagrid2.ItemsSource;
            datagrid2.ItemsSource = null;
            datagrid2.ItemsSource = tmp;

        }

        private void ButtonTest_Click(object sender, RoutedEventArgs e)
        {
            CalibrationMeasure cm = new CalibrationMeasure();
            cm.Show();
        }

        private void treeOpen_Click(object sender, RoutedEventArgs e)
        {
            if (treeview.SelectedItem != null)
            {
                var n = treeview.SelectedItem as Node;
                if (n.Kind != "folder")
                {

                    standardFileLoad(n);

                    datagrid.ItemsSource = null;
                    datagrid.ItemsSource = list;

                }
            }
            

        }
        private void treeNew_Click(object sender, RoutedEventArgs e)
        {
                SaveFileDialog sfd = new SaveFileDialog();
                if (Settings.TransmissionPath[0] == '.') sfd.InitialDirectory = Environment.CurrentDirectory + Settings.TransmissionPath.Substring(1);
                else sfd.InitialDirectory = Settings.TransmissionPath;
                
                sfd.Title = "新建数据文件";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string localFilePath = sfd.FileName.ToString(); //获得文件路径 
                    string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径

                    FileStream fs = (FileStream)sfd.OpenFile();//输出文件

                }

                treeview.ItemsSource = Bind(theFolder);
        }



        private void AddFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (Settings.TransmissionPath[0] == '.') ofd.InitialDirectory = Environment.CurrentDirectory + Settings.TransmissionPath.Substring(1);
            else ofd.InitialDirectory = Settings.TransmissionPath;
            //点了保存按钮进入 
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = ofd.FileName.ToString(); //获得文件路径 
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径

                standardFileAdd(localFilePath);

                datagrid.ItemsSource = null;
                datagrid.ItemsSource = list;
            }
        }

        public void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Settings.TransmissionPath;
            sfd.Title = "新建数据文件";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = sfd.FileName; //获得文件路径 

                FileSave(localFilePath);
                
                
            }
        }
        void standardFileLoad(Node n)
        {
            try
            {
                list.Clear();


                StreamReader sr = new StreamReader(n.fpath.FullName + "//" + n.Name);
                sr.ReadLine();
                string line = sr.ReadLine();
                string[] data = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                Energy.Clear();
                for (int i = 4; i < data.Length; i++)
                {
                    double tmp;
                    double.TryParse(data[i].Substring(1, data[i].Length - 2), out tmp);
                    Energy.Add(tmp);

                    //System.Windows.MessageBox.Show(tmp.ToString(),"ExpenseIt Standalone",MessageBoxButton.OK,MessageBoxImage.Information);

                }

                sr.ReadLine();

                line = sr.ReadLine();
                while (line[0] != 'E')
                {
                    data = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    ECPara ecp = new ECPara();
                    double tmp;
                    double.TryParse(data[0], out tmp);
                    ecp.Density = tmp;
                    double.TryParse(data[1], out tmp);
                    ecp.dY = tmp;
                    double.TryParse(data[2], out tmp); ;
                    ecp.Height = tmp;
                    for (int i = 3; i < data.Length; i++)
                    {
                        double.TryParse(data[i], out tmp);
                        if (Math.Abs(tmp) < 1E-31) continue;//skip 0 figure
                        evsr er = new evsr(Energy[i - 3], tmp);

                        ecp.list.Add(er);

                        //System.Windows.MessageBox.Show(tmp.ToString(),"ExpenseIt Standalone",MessageBoxButton.OK,MessageBoxImage.Information);

                    }
                    list.Add(ecp);

                    line = sr.ReadLine();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("文件打开失败 " + ex.Message, "ExpenseIt Standalone", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            
        }

        void standardFileAdd(string path)
        {
            try
            {
                
                StreamReader sr = new StreamReader(path);
                sr.ReadLine();
                string line = sr.ReadLine();
                string[] data = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                List<double> Energy = new List<double>();
                for (int i = 4; i < data.Length; i++)
                {
                    double tmp;
                    double.TryParse(data[i].Substring(1, data[i].Length - 2), out tmp);
                    Energy.Add(tmp);

                    //System.Windows.MessageBox.Show(tmp.ToString(),"ExpenseIt Standalone",MessageBoxButton.OK,MessageBoxImage.Information);

                }

                sr.ReadLine();

                line = sr.ReadLine();
                while (line[0] != 'E')
                {
                    data = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    ECPara ecp = new ECPara();
                    double tmp;
                    double.TryParse(data[0], out tmp);
                    ecp.Density = tmp;
                    double.TryParse(data[1], out tmp);
                    ecp.dY = tmp;
                    double.TryParse(data[2], out tmp); ;
                    ecp.Height = tmp;

                    bool isFound = false;
                    foreach (ECPara e in list)
                    {

                        if (Math.Abs(e.Density - ecp.Density) < Settings.DIFF &&
                            Math.Abs(e.dY - ecp.dY) < Settings.DIFF &&
                            Math.Abs(e.Height - ecp.Height) < Settings.DIFF)
                        {
                            for (int i = 3; i < data.Length; i++)
                            {
                                double.TryParse(data[i], out tmp);
                                evsr er = new evsr(Energy[i - 3], tmp);

                                e.list.Add(er);

                                //System.Windows.MessageBox.Show(tmp.ToString(),"ExpenseIt Standalone",MessageBoxButton.OK,MessageBoxImage.Information);

                            }
                            isFound = true;
                            break;
                        }
                    }

                    if (!isFound)
                    {
                        for (int i = 3; i < data.Length; i++)
                        {
                            double.TryParse(data[i], out tmp);
                            evsr er = new evsr(Energy[i - 3], tmp);

                            ecp.list.Add(er);

                            //System.Windows.MessageBox.Show(tmp.ToString(),"ExpenseIt Standalone",MessageBoxButton.OK,MessageBoxImage.Information);

                        }
                        list.Add(ecp);
                    }
                    
                    


                    line = sr.ReadLine();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("文件打开失败 " + ex.Message, "ExpenseIt Standalone", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        void FileSave(string path)
        {
            string fileNameExt = path.Substring(path.LastIndexOf("\\") + 1); //获取文件名，不带路径
            //如果文件不存在，则创建；存在则覆盖 
            File.Create(path).Close();

            using (StreamWriter sw = new StreamWriter(path))
            {
                string str;
                sw.WriteLine("TITLE=\"" + fileNameExt + "\"");
                str = "VARIABLES= \"Density\" \"dY\" \"Height\" ";

                foreach (double d in Energy)
                {
                    str += "\"" + d + "\" ";
                }
                sw.WriteLine(str);

                sw.WriteLine("ZONE T=\"3D Data\" I=8 J=8 K=3 F=POINT");

                foreach (ECPara ecp in list)
                {
                    str = ecp.Density.ToString("E8") + " ";
                    str += ecp.dY.ToString("E8") + " ";
                    str += ecp.Height.ToString("E8") + " ";

                    ecp.list.Sort();
                    foreach (evsr er in ecp.list)
                    {
                        str += er.rate.ToString("E8") + " ";
                    }
                    sw.WriteLine(str);
                }

                sw.WriteLine("END");
            }

        }

    }
}
