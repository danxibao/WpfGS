using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
namespace WpfGS
{
     public static class Settings
     {
         public const double DIFF = 0.0005;
         //public static string SettingsDirectory = Environment.CurrentDirectory + "\\Settings";
         public static string SettingsDirectory = ".\\Settings";
         #region FilePath
        

         public static string CalibrationPath;
         public static string TransmissionPath;
         public static string DataPath;

         static void ReadFiles()
        {
            bool PathOK = true;
            string FileName;
            StreamReader sr;
            StreamWriter sw;
            //Read CalibrationPath
            FileName = ".\\Settings\\CalibrationPath.txt";
            if (File.Exists(FileName))
            {
                sr = new StreamReader(FileName);
                CalibrationPath = sr.ReadLine();
                sr.Close();
                if (!Directory.Exists(CalibrationPath))
                {
                    PathOK = false;
                }

            }
            else
            {
                //File.Create(FileName);//创建该文件
                CalibrationPath = ".\\CalibrationFiles";
                sw = new StreamWriter(FileName);
                sw.WriteLine(CalibrationPath);
                sw.Close();
                if (!Directory.Exists(CalibrationPath))
                {
                    Directory.CreateDirectory(CalibrationPath);
                }
            }
            //Read TransmissionPath
            FileName = ".\\Settings\\TransmissionPath.txt";
            if (File.Exists(FileName))
            {
                sr = new StreamReader(FileName);
                TransmissionPath = sr.ReadLine();
                sr.Close();
                if (!Directory.Exists(TransmissionPath))
                {
                    PathOK = false;
                }

            }
            else
            {
                //File.Create(FileName);//创建该文件
                TransmissionPath = ".\\TransmissionFiles";
                sw = new StreamWriter(FileName);
                sw.WriteLine(TransmissionPath);
                sw.Close();
                if (!Directory.Exists(TransmissionPath))
                {
                    Directory.CreateDirectory(TransmissionPath);
                }
            }

            //Read DataPath
            FileName = ".\\Settings\\DataPath.txt";
            if (File.Exists(FileName))
            {
                sr = new StreamReader(FileName);
                DataPath = sr.ReadLine();
                sr.Close();
                if (!Directory.Exists(DataPath))
                {
                    PathOK = false;
                }

            }
            else
            {
                //File.Create(FileName);//创建该文件
                DataPath = ".\\DataFiles";
                sw = new StreamWriter(FileName);
                sw.WriteLine(DataPath);
                sw.Close();
                if (!Directory.Exists(DataPath))
                {
                    Directory.CreateDirectory(DataPath);
                }
            }

            if (!PathOK)
            {
                System.Windows.MessageBox.Show(
                    "文件路径错误，请重新设置",
                    "ExpenseIt Standalone",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            
        }
         public static void ReadSettings(){
            
            if (Directory.Exists(SettingsDirectory))//判断是否存在
            {
                ReadFiles();

            }
            else
            {
                Directory.CreateDirectory(SettingsDirectory);//创建新路径
                ReadFiles();
            }

        }
         #endregion

         #region ContainerPara
         public static List<ContainerPara> listcp = new List<ContainerPara>();
         public static string ContainerPath = Settings.SettingsDirectory + "\\Container.txt";
         public static void listcpLoad()
         {
             Settings.listcp.Clear();

             if (!File.Exists(ContainerPath))
             {
                 File.Create(ContainerPath).Close();
             }
             else
             {
                 StreamReader sr = new StreamReader(ContainerPath);
                 String line;
                 while ((line = sr.ReadLine()) != null)
                 {
                     ContainerPara cp = new ContainerPara();
                     cp.Description = line;
                     for (int i = 0; i < 6; i++)
                     {
                         line = sr.ReadLine();
                         double.TryParse(line, out cp.nums[i]);//exception dealing
                     }
                     Settings.listcp.Add(cp);
                 }
                 sr.Close();
             }
         }
         public static void listcpSave()
         {
             StreamWriter sw = new StreamWriter(ContainerPath);
             foreach (ContainerPara cp in Settings.listcp)
             {
                 sw.WriteLine(cp.Description);
                 for (int i = 0; i < 6; i++)
                 {
                     sw.WriteLine(cp.nums[i]);
                 }
             }
             sw.Close();
         }
         #endregion

         #region CalibrationSource
         public static List<CalibrationSourcePara> listcsp = new List<CalibrationSourcePara>();
         public static string CalibrationSourcePath = Settings.SettingsDirectory + "\\CalibrationSource.txt";
         public static void listcspLoad()
         {
             Settings.listcsp.Clear();

             if (!File.Exists(CalibrationSourcePath))
             {
                 File.Create(CalibrationSourcePath).Close();
             }
             else
             {
                 StreamReader sr = new StreamReader(CalibrationSourcePath);
                 String line;
                 while ((line = sr.ReadLine()) != null)
                 {
                     CalibrationSourcePara csp = new CalibrationSourcePara();
                     //add new code here
                     csp.Description = line;

                     {
                         line = sr.ReadLine();
                         int tmp;
                         int.TryParse(line, out tmp);
                         csp.Count = tmp;
                     }

                     line = sr.ReadLine();
                     csp.datetime = Convert.ToDateTime(line);

                     line = sr.ReadLine();
                     while (line[0] != '#')
                     {
                         string[] data = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                         CSRow csr = new CSRow();

                         csr.核素 = data[0];
                         double tmp;
                         double.TryParse(data[1], out tmp);
                         csr.能量keV = tmp;
                         double.TryParse(data[2], out tmp);
                         csr.计数率 = tmp;
                         double.TryParse(data[3], out tmp);
                         csr.不确定度1 = tmp;
                         double.TryParse(data[4], out tmp);
                         csr.衰变周期 = tmp;
                         double.TryParse(data[5], out tmp);
                         csr.不确定度2 = tmp;
                         int tmp2;
                         int.TryParse(data[6], out tmp2);
                         csr.时间单位 = (timeUnit)tmp2;

                         csp.CSRows.Add(csr);

                         line = sr.ReadLine();
                     }


                     Settings.listcsp.Add(csp);
                 }
                 sr.Close();
             }
         }
         public static void listcspSave()
         {
             StreamWriter sw = new StreamWriter(CalibrationSourcePath);
             foreach (CalibrationSourcePara csp in Settings.listcsp)
             {
                 sw.WriteLine(csp.Description);
                 sw.WriteLine(csp.Count);
                 sw.WriteLine(csp.datetime.ToString());

                 foreach (CSRow csr in csp.CSRows)
                 {
                     sw.Write(csr.核素);
                     sw.Write(',');
                     sw.Write(csr.能量keV);
                     sw.Write(',');
                     sw.Write(csr.计数率);
                     sw.Write(',');
                     sw.Write(csr.不确定度1);
                     sw.Write(',');
                     sw.Write(csr.衰变周期);
                     sw.Write(',');
                     sw.Write(csr.不确定度2);
                     sw.Write(',');
                     sw.WriteLine(csr.时间单位);
                 }
                 sw.WriteLine('#');
             }
             sw.Close();
         }
         #endregion

         #region TransmissionSource
         public static List<TransmissionSourcePara> listtsp = new List<TransmissionSourcePara>();
         public static string TransmissionSourcePath = Settings.SettingsDirectory + "\\TransmissionSource.txt";
         public static void listtspLoad()
         {
             Settings.listtsp.Clear();

             if (!File.Exists(TransmissionSourcePath))
             {
                 File.Create(TransmissionSourcePath).Close();
             }
             else
             {
                 StreamReader sr = new StreamReader(TransmissionSourcePath);
                 String line;
                 while ((line = sr.ReadLine()) != null)
                 {
                     TransmissionSourcePara tsp = new TransmissionSourcePara();
                     //add new code here
                     tsp.Description = line;

                     line = sr.ReadLine();
                     while (line[0] != '#')
                     {
                         string[] data = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                         TSRow tsr = new TSRow();

                         tsr.核素 = data[0];
                         double tmp;
                         double.TryParse(data[1], out tmp);
                         tsr.光峰能量keV = tmp;
                         double.TryParse(data[2], out tmp);
                         tsr.衰变周期 = tmp;
                         int tmp2;
                         int.TryParse(data[3], out tmp2);
                         tsr.时间单位 = (timeUnit)tmp2;

                         tsp.TSRows.Add(tsr);

                         line = sr.ReadLine();
                     }


                     Settings.listtsp.Add(tsp);
                 }
                 sr.Close();
             }
         }

         public static void listtspSave()
         {
             StreamWriter sw = new StreamWriter(TransmissionSourcePath);
             foreach (TransmissionSourcePara tsp in Settings.listtsp)
             {
                 sw.WriteLine(tsp.Description);

                 foreach (TSRow tsr in tsp.TSRows)
                 {
                     sw.Write(tsr.核素);
                     sw.Write(',');
                     sw.Write(tsr.光峰能量keV);
                     sw.Write(',');
                     sw.Write(tsr.衰变周期);
                     sw.Write(',');
                     sw.WriteLine(tsr.时间单位);
                 }
                 sw.WriteLine('#');
             }
             sw.Close();
         }
         #endregion

         #region Detector
         public static List<DetectorPara> listdp = new List<DetectorPara>();
         public static string DetectorPath = Settings.SettingsDirectory + "\\Detector.txt";
         public static void listdpLoad()
         {
             Settings.listdp.Clear();

             if (!File.Exists(DetectorPath))
             {
                 File.Create(DetectorPath).Close();
             }
             else
             {
                 StreamReader sr = new StreamReader(DetectorPath);
                 String line;
                 while ((line = sr.ReadLine()) != null)
                 {
                     DetectorPara dp = new DetectorPara();
                     dp.Description = line;

                     line = sr.ReadLine();
                     dp.Information = line;

                     Settings.listdp.Add(dp);
                 }
                 sr.Close();
             }
         }
         public static void listdpSave()
         {
             StreamWriter sw = new StreamWriter(DetectorPath);
             foreach (DetectorPara dp in Settings.listdp)
             {
                 sw.WriteLine(dp.Description);
                 sw.WriteLine(dp.Information);
             }
             sw.Close();
         }
         #endregion
     }

     public class ContainerPara
     {

         public string Description { get; set; }
         //public double Height,Diameter,Depth, Volume,TareWeight,Thickness;
         public double[] nums;

         public ContainerPara()
         {
             nums = new double[10];
         }

         public void Copy(ContainerPara cp)
         {
             Description = cp.Description;
             for (int i = 0; i < 10; i++)
             {
                 nums[i] = cp.nums[i];
             }
         }
     }

     public class CalibrationSourcePara
     {
         public string Description { get; set; }
         public int Count { get; set; }
         public DateTime datetime { get; set; }
         public List<CSRow> CSRows { get; set; }
         public CalibrationSourcePara()
         {
             Description = string.Empty;
             Count = 1;
             datetime = DateTime.Now;
             CSRows = new List<CSRow>();
         }

         public void Copy(CalibrationSourcePara csp)
         {
             Description=csp.Description;
             Count = csp.Count;
             datetime = csp.datetime;
             CSRows.Clear();
             foreach(CSRow csr in csp.CSRows){
                 CSRows.Add(csr);
             }
             
         }

         

     }

     public enum timeUnit
     {
         Seconds,
         Minutes,
         Hours,
         Days,
         Months,
         Years
     }
     public class CSRow
     {
         public string 核素 { get; set; }
         public double 能量keV { get; set; }
         public double 计数率 { get; set; }
         public double 不确定度1 { get; set; }
         public double 衰变周期 { get; set; }
         public double 不确定度2 { get; set; }
         public timeUnit 时间单位 { get; set; }

     }
     public class TransmissionSourcePara
     {
         public string Description { get; set; }
         public List<TSRow> TSRows { get; set; }
         public TransmissionSourcePara()
         {
             Description = string.Empty;
             TSRows = new List<TSRow>();
         }

         public void Copy(TransmissionSourcePara tsp)
         {
             Description = tsp.Description;
             TSRows.Clear();
             foreach (TSRow tsr in tsp.TSRows)
             {
                 TSRows.Add(tsr);
             }

         }
     }
     public class TSRow
     {
         public string 核素 { get; set; }
         public double 光峰能量keV { get; set; }
         public double 衰变周期 { get; set; }
         public timeUnit 时间单位 { get; set; }
     }

     public class DetectorPara
     {

         public string Description { get; set; }
         public string Information { get; set; }

         public DetectorPara()
         {
             
         }

         public void Copy(DetectorPara cp)
         {
             Description = cp.Description;
             Information = cp.Information;
             
         }
     }

}
