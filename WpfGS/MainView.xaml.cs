﻿using System;
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

using System.Windows.Media.Media3D;
using System.Net.Sockets;
using System.Runtime.InteropServices;

using System.Net;
using System.Threading;

using Modbus.Data;
using Modbus.Device;
using Modbus.Utility;


namespace WpfGS
{
    
    /// <summary>
    /// Interaction logic for CalibrationView.xaml
    /// </summary>
    public partial class MainView : System.Windows.Controls.UserControl
    {
        public List<ECPara> list = new List<ECPara>();
        List<double> Energy = new List<double>();

        DirectoryInfo theFolder;

        List<KeyValuePair<string, int>> valueList = new List<KeyValuePair<string, int>>();
        public MainView()
        {
            Settings.ReadSettings();

            InitializeComponent();

            Settings.listcpLoad();
            Settings.listdpLoad();
            drum.ItemsSource = Settings.listcp;
            drum.DisplayMemberPath = "Description";
            detector.ItemsSource = Settings.listdp;
            detector.DisplayMemberPath = "Description";


            theFolder= new DirectoryInfo(Settings.DataPath);
            treeview.ItemsSource = Bind(theFolder);

            

            // Setting data for pie chart
            valueList.Add(new KeyValuePair<string, int>("Developer", 60));
            valueList.Add(new KeyValuePair<string, int>("Misc", 20));
            valueList.Add(new KeyValuePair<string, int>("Tester", 50));
            valueList.Add(new KeyValuePair<string, int>("QA", 30));
            valueList.Add(new KeyValuePair<string, int>("Project Manager", 40));
            pieChart.DataContext = valueList;

        }

        List<Node> Bind(DirectoryInfo TheFolder)
        {
            List<Node> nodes = new List<Node>();


            //遍历文件夹
            foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
            {
                Node n = new Node { Name = NextFolder.Name, father = nodes, fpath = TheFolder, Kind = "folder" };
                //System.Windows.MessageBox.Show(TheFolder.ToString() + "\\_DetectionSetup.txt", "ExpenseIt Standalone", MessageBoxButton.OK, MessageBoxImage.Information);
                if (File.Exists(TheFolder + "\\" + NextFolder.Name + "\\_DrumInfo.txt")) n.Kind = "drum";
                nodes.Add(n);
                n.Nodes = Bind(NextFolder);
            }

            //遍历文件
            foreach (FileInfo NextFile in TheFolder.GetFiles())
                nodes.Add(new Node { Name = NextFile.Name, father = nodes, fpath = TheFolder });


            return nodes;
        }

        
        private void treeOpen_Click(object sender, RoutedEventArgs e)
        {
            if (treeview.SelectedItem != null)
            {
                var n = treeview.SelectedItem as Node;
                if (n.Kind == "drum")
                {
                    switch(tab.SelectedIndex)
                    {
                        case 0:
                            ItemLoad(n.fpath + "\\" + n.Name + "\\_DrumInfo.txt");
                            break;
                        case 1:

                            break;
                        case 2:
                            ReportLoad(n.fpath + "\\" + n.Name + "\\#DetectionReport.txt");
                            break;
                    }
                        
                    

                }
            }
            

        }
        private void ReportLoad(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {

                    doc.Text = sr.ReadToEnd();

                }
            }
            catch (Exception ex) { }
        }
        private void treeNew_Click(object sender, RoutedEventArgs e)
        {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = Settings.DataPath;
                sfd.Title = "新建数据文件";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string localFilePath = sfd.FileName.ToString(); //获得文件路径 
                    string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径

                    FileStream fs = (FileStream)sfd.OpenFile();//输出文件

                }

                treeview.ItemsSource = Bind(theFolder);
        }

        #region Settings
        string transmissionFilePath, calibrationFilePath;
        private void ButtonCalibrationFileSet_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Settings.CalibrationPath;
            
            //点了保存按钮进入 
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = ofd.FileName.ToString(); //获得文件路径 
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径

                calibrationFile.Text = fileNameExt;
                calibrationFilePath = localFilePath;
            }
        }
        private void ButtonTransmissionFileSet_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Settings.TransmissionPath;
            
            //点了保存按钮进入 
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = ofd.FileName.ToString(); //获得文件路径 
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径

                transmissionFile.Text = fileNameExt;
                transmissionFilePath = localFilePath;
            }
        }

        private void NewItem_Click(object sender, RoutedEventArgs e)
        {
            var node = treeview.SelectedItem as Node;
            string folderpath;
            if (node == null)
            {
                folderpath=Settings.DataPath + "\\" + folderName.Text;
                drumCreate(folderpath);  
            }
            else
            {
                folderpath=node.fpath + "\\" + folderName.Text;
                drumCreate(folderpath); 
            }

            treeview.ItemsSource = Bind(theFolder);
        }

        void drumCreate(string folderpath)
        {
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
                //System.Windows.MessageBox.Show(Settings.DataPath + "\\" + ID.Text, "ExpenseIt Standalone", MessageBoxButton.OK, MessageBoxImage.Information);
                string path=folderpath + "\\_DrumInfo.txt";
                ItemSave(path);
                path = folderpath + "\\_DetectionSetup.txt";
                DetectionSetupSave(path);

            }
            else
                System.Windows.MessageBox.Show(
                    "对象路径已存在", 
                    "错误", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
        }
        void ItemSave(string path)
        {
            File.Create(path).Close();
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("folderName:" + folderName.Text);
                sw.WriteLine("ID:" + ID.Text);
                sw.WriteLine("Description:" + Description.Text);
                sw.WriteLine("Time:" + date.SelectedDate.ToString());
                sw.WriteLine("Type:" + ((ComboBoxItem)type.SelectedItem).Content);
                sw.WriteLine("Container:" + ((ContainerPara)drum.SelectedItem).Description);
                sw.WriteLine("Detector:" + ((DetectorPara)detector.SelectedItem).Description);
                sw.WriteLine("CalibrationFilePath:" + calibrationFilePath);
                int TransmissionMeasure = (bool)isTM.IsChecked ? 1 : 0;
                sw.WriteLine("IsTransmission:" + TransmissionMeasure);
                sw.WriteLine("TransmissionFilePath:" + transmissionFilePath);

                int ScanType=0;
                if ((bool)r1.IsChecked)
                {
                    ScanType = 1;
                    sw.WriteLine("ScanType:" + ScanType);
                    int StartPos, Step, StopPos;
                    int.TryParse(startpos.Text, out StartPos);
                    int.TryParse(step.Text, out Step);
                    int.TryParse(stoppos.Text, out StopPos);
                    sw.WriteLine("StartPos:" + StartPos);
                    sw.WriteLine("Step:" + Step);
                    sw.WriteLine("StopPos:" + StopPos);
                }
                if ((bool)r2.IsChecked)
                {
                    ScanType = 2;
                    sw.WriteLine("ScanType:" + ScanType);
                    int Start, Stop;
                    int.TryParse(start.Text, out Start);
                    int.TryParse(distance.Text, out Stop);
                    sw.WriteLine("Start:" + Start);
                    sw.WriteLine("Stop:" + Stop);
                }

                if ((bool)r3.IsChecked)
                {
                    ScanType = 3;
                    sw.WriteLine("ScanType:" + ScanType);
                    int Height;
                    int.TryParse(pos.Text, out Height);
                    sw.WriteLine("Height:" + Height);

                }

                sw.WriteLine("END");
                

            }
        }
        void DetectionSetupSave(string path)
        {
            File.Create(path).Close();
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("==DetectionSetup==");

                sw.WriteLine("NoOfWD:" + ID.Text + ";");//废物桶编号
                sw.WriteLine("TypeOfWD:" + Description.Text + ";");
                sw.WriteLine("GWDH:" + ((ContainerPara)(drum.SelectedItem)).nums[0] + ";");//废物桶高度
                sw.WriteLine("GWDD:" + ((ContainerPara)(drum.SelectedItem)).nums[1] + ";");//废物桶直径

                sw.WriteLine("GHPGDX:" + 53 + ";");//探测器初始x坐标
                sw.WriteLine("GHPGDY:" + 0 + ";");//探测器初始y坐标
                sw.WriteLine("GHPGDZ:" + 5 + ";");//探测器初始z坐标

                int ScanType = type.SelectedIndex;
                int DSNumY = 1, DSNumZ = 1, DSNumA = 1;
                int startp, leap, endp;
                int.TryParse(startpos.Text, out startp);
                int.TryParse(step.Text, out leap);
                int.TryParse(stoppos.Text, out endp);

                switch (ScanType)
                {
                    case 0:
                        DSNumZ = (endp - startp) / leap + 1;
                        if (DSNumZ < 1)
                        {
                            System.Windows.MessageBox.Show("分层扫描参数错误", "ExpenseIt Standalone", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                        DSNumY = 1;

                        DSNumA = 1;
                        break;
                    case 1:
                        DSNumY = 1;
                        DSNumZ = 9;
                        DSNumA = 1;
                        break;
                    case 2:
                        DSNumY = 1;
                        DSNumZ = 9;
                        DSNumA = 1;
                        break;
                }


                sw.WriteLine("DSNumY:" + DSNumY + ";");//水平方向探测测数
                sw.WriteLine("DSNumZ:" + DSNumZ + ";");//高度方向探测次数
                sw.WriteLine("DSNumA:" + DSNumA + ";");//圆周方向探测次数，1表示旋转测量

                sw.WriteLine("DSLY:" + 0 + ";");//水平偏移步长
                sw.WriteLine("DSLZ:" + leap + ";");//高度方向步长
                sw.WriteLine("DSLA:" + 0 + ";"); ;//圆周方向步长

                sw.WriteLine("DSigleT:" + 10 + ";");//每次测量时间
                sw.WriteLine("VORotation:" + 10 + ";");//旋转速度

                int TransmissionMeasure = (bool)isTM.IsChecked ? 1 : 0;

                sw.WriteLine("IsTransmission:" + TransmissionMeasure + ";");//是否透射测量
                sw.WriteLine("WeightOfDrum:" + ((ContainerPara)(drum.SelectedItem)).nums[4] + ";");//废物桶重量

                sw.WriteLine("TypeOfIteration:" + 1 + ";");//迭代方法
                sw.WriteLine("MaxIteration:" + 500 + ";");//迭代步数

                sw.WriteLine("NuGridsX:" + 1 + ";");//半径方向的网格
                sw.WriteLine("NuGridsZ:" + DSNumZ + ";");//高度方向的网格
                sw.WriteLine("NuGridsY:" + 1 + ";");//圆周方向的网格
                sw.WriteLine("IsEqualVolumeOfGrids:" + 1 + ";");//是否等体积网格

                sw.WriteLine("END");
            }
            
        }
        private void CoverItem_Click(object sender, RoutedEventArgs e)
        {
            var node = treeview.SelectedItem as Node;
            if (node == null)
            {
                System.Windows.MessageBox.Show(
                    "未选择对象",
                    "错误",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else
            {
                if(System.Windows.MessageBox.Show(
                   "您真的要覆盖当前对象么？",
                   "警告",
                   MessageBoxButton.OKCancel,
                   MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    string path = node.fpath + "\\" + node.Name + "\\_DrumInfo.txt";
                    ItemSave(path);
                    path = node.fpath + "\\" + node.Name + "\\_DetectionSetup.txt";
                    DetectionSetupSave(path);
                }
                
            }

            treeview.ItemsSource = Bind(theFolder);
        }

        string GetStr(string str)//Get the content after ':'
        {
            for (int n = 0; n < str.Length; n++)
            {
                if (str[n] == ':')
                    return str.Substring(n + 1);
            }
            return "";
        }
        void ItemLoad(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    line = sr.ReadLine();
                    folderName.Text = GetStr(line);

                    line = sr.ReadLine();
                    string data = GetStr(line);
                    ID.Text = data;

                    line = sr.ReadLine();
                    data = GetStr(line);
                    Description.Text = data;

                    line = sr.ReadLine();
                    data = GetStr(line);
                    DateTime dt;
                    DateTime.TryParse(data, out dt);
                    date.SelectedDate = dt;

                    line = sr.ReadLine();
                    data = GetStr(line);
                    if (data == "SGS")
                    {
                        type.SelectedIndex = 0;
                    }
                    else if (data == "TSGS")
                    {
                        type.SelectedIndex = 1;
                    }
                    else if (data == "TGS")
                    {
                        type.SelectedIndex = 2;
                    }
                    else
                    {
                        System.Windows.MessageBox.Show(
                        "文件读取错误",
                        "错误",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                        return;
                    }

                    line = sr.ReadLine();
                    data = GetStr(line);
                    int index = 0;
                    foreach (ContainerPara cp in drum.Items)
                    {
                        if (cp.Description == data)
                        {
                            break;
                        }
                        index++;
                    }
                    drum.SelectedIndex = index;

                    line = sr.ReadLine();
                    data = data = GetStr(line);
                    index = 0;
                    foreach (DetectorPara dp in detector.Items)
                    {
                        if (dp.Description == data)
                        {
                            break;
                        }
                        index++;
                    }
                    detector.SelectedIndex = index;

                    line = sr.ReadLine();
                    data = data = GetStr(line);
                    calibrationFilePath = data;
                    calibrationFile.Text = calibrationFilePath.Substring(calibrationFilePath.LastIndexOf("\\") + 1);

                    line = sr.ReadLine();
                    data = data = GetStr(line);
                    int TransmissionMeasure;
                    int.TryParse(data, out TransmissionMeasure);
                    if (TransmissionMeasure == 1)
                        isTM.IsChecked = true;
                    else
                        isTM.IsChecked = false;

                    line = sr.ReadLine();
                    data = data = GetStr(line);
                    transmissionFilePath = data;
                    transmissionFile.Text = transmissionFilePath.Substring(transmissionFilePath.LastIndexOf("\\") + 1);

                    line = sr.ReadLine();
                    data = data = GetStr(line);
                    int ScanType;
                    int.TryParse(data, out ScanType);

                    if (ScanType == 1)
                    {
                        r1.IsChecked = true;

                        line = sr.ReadLine();
                        data = data = GetStr(line);
                        startpos.Text = data;

                        line = sr.ReadLine();
                        data = data = GetStr(line);
                        step.Text = data;

                        line = sr.ReadLine();
                        data = data = GetStr(line);
                        stoppos.Text = data;
                    }
                    else if (ScanType == 2)
                    {
                        r2.IsChecked = true;
                        line = sr.ReadLine();
                        data = data = GetStr(line);
                        start.Text = data;

                        line = sr.ReadLine();
                        data = data = GetStr(line);
                        distance.Text = data;
                    }

                    else if (ScanType == 3)
                    {
                        r3.IsChecked = true;
                        line = sr.ReadLine();
                        data = data = GetStr(line);
                        pos.Text = data;

                    }


                }
            }catch(Exception ex){}
        }
        #endregion

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            /*
            using (TcpClient client = new TcpClient("127.0.0.1", 502))
            {
                ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                ushort[]r={100,200,300,400};
                master.WriteMultipleRegisters(1, 0, r);
            }
            */
            using (TcpClient client = new TcpClient("192.168.31.100", 502))
            {

                ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

                //detection time,file name
                string OutputFileName = "firstTest";
                master.WriteMultipleRegisters(1, 0, StrToUshort(10, OutputFileName));

                //start detection or not,live or real model
                bool[] Coils={true,true};
                master.WriteMultipleCoils(1, 0, Coils);
                

                while (master.ReadCoils(1, 0, 1)[0])
                {
                    Thread.Sleep(1000);
                }
                System.Windows.MessageBox.Show("Finished");


            } 

            //ProgressBarProcessing();
        }
        ushort[] StrToUshort(ushort t, string str)
        {
            List<ushort> r = new List<ushort>(255);
            ushort len = (ushort)str.Length;
            r.Add(t);
            r.Add(len);
            for (int i = 0; i < len; i++)
                r.Add(str[i]);

            return r.ToArray();


        }
        //Create a Delegate that matches the Signature of the ProgressBar's SetValue method
        private delegate void UpdateProgressBarDelegate(System.Windows.DependencyProperty dp, Object value);
        private void ProgressBarProcessing()
        {
            //Configure the ProgressBar
            ProgressBar1.Minimum = 0;
            ProgressBar1.Maximum = short.MaxValue;
            ProgressBar1.Value = 0;

            //Stores the value of the ProgressBar
            int value = 0;

            int STEP=short.MaxValue/8,layer=1;
            //Create a new instance of our ProgressBar Delegate that points
            //  to the ProgressBar's SetValue method.
            UpdateProgressBarDelegate updatePbDelegate = new UpdateProgressBarDelegate(ProgressBar1.SetValue);

            Notice.Text="正在测量第1/9层";
            //Tight Loop:  Loop until the ProgressBar.Value reaches the max
            do
            {
                value += 1;

                /*Update the Value of the ProgressBar:
                  1)  Pass the "updatePbDelegate" delegate that points to the ProgressBar1.SetValue method
                  2)  Set the DispatcherPriority to "Background"
                  3)  Pass an Object() Array containing the property to update (ProgressBar.ValueProperty) and the new value */
                Dispatcher.Invoke(updatePbDelegate,
                    System.Windows.Threading.DispatcherPriority.Background,
                    new object[] { System.Windows.Controls.ProgressBar.ValueProperty, (double)value });

                if (value > STEP * layer )
                {
                    layer++;
                    Notice.Text = "正在测量第" + layer + "/9层";
                }

            }
            while (ProgressBar1.Value != ProgressBar1.Maximum);

        }

        


        
        
        
    }

    
    
}
