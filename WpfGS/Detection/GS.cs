using System;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

using Modbus.Data;
using Modbus.Device;
using Modbus.Utility;

using System.IO;

namespace WpfGS
{
    class GSBase
    {
        public ModbusIpMaster Mot, Det;
        public MainView pMain;
        public List<DetPara> listDet;
        public GSBase(TcpClient MotTcp, TcpClient DetTcp,MainView main)
        {
            
            if (null == MotTcp) throw new Exception("电机未连接");
            Mot = ModbusIpMaster.CreateIp(MotTcp);

            if (null == DetTcp) throw new Exception("探测器未连接");
            Det = ModbusIpMaster.CreateIp(DetTcp);
            
            pMain = main;
            listDet = new List<DetPara>();

        }
        
        
        public void MotorControl(ushort Height,ushort RotationMode,ushort Spd,ushort Pos)
        {
            List<ushort> s = new List<ushort>(255);
            //r[0]:1=start/on execution,0=finished execution
            s.Add(1);
            //r[1]:left lifting motor height
            s.Add(Height);
            //r[2]:right lifting motor height
            s.Add(Height);
            //r[3]:Rotation Motor Mode:
            //0=clockwise step,1=counterclockwise step,
            //2=clockwise speed,3=counterclockwise speed
            s.Add(RotationMode);
            //r[4]:Rotation angle/speed
            s.Add(Spd);
            //r[5]:Translation motor positon
            s.Add(Pos);
            //r[6]:error alarm,0=normal
            s.Add(0);

            Mot.WriteMultipleRegisters(1, 0, s.ToArray());

            ushort[] receive;
            do
            {
                Thread.Sleep(1000);
                receive = Mot.ReadHoldingRegisters(1, 0, 10);
                if (receive[6] != 0) throw new Exception("电机报警");
            }
            while (receive[0] == 1);
        }

        public void DetectorControl(string ID,int i,ushort Time,bool Mode)
        {
            string OutputFileName = ID + "_" + i;
            Det.WriteMultipleRegisters(1, 0, TimeNameToUshort(Time, OutputFileName));

            //[0]start detection or not,
            //[1]1=live or 0=real model,
            //[2]alarm flag
            bool[] Coils = { true, Mode, false };
            Det.WriteMultipleCoils(1, 0, Coils);

            bool[] b;
            do
            {
                Thread.Sleep(1000);
                b = Det.ReadCoils(1, 0, 3);
                if (b[2]) throw new Exception("探测器报警");
            }
            while (b[0]);
        }

        ushort[] TimeNameToUshort(ushort t, string str)
        {
            List<ushort> r = new List<ushort>(255);
            ushort len = (ushort)str.Length;
            r.Add(t);
            r.Add(len);
            for (int i = 0; i < len; i++)
                r.Add(str[i]);

            return r.ToArray();


        }


        public void addDet(double Angle, double dY, double Height, string filename)
        {
            DetPara det = new DetPara();
            det.Angle = Angle;
            det.dY = dY;
            det.Height = Height;

            string Path = Settings.DetAddr + "\\" + filename;
            StreamReader sr = new StreamReader(Path);
            string line;
            do
            {
                line = sr.ReadLine();
            } while (line != "   Peak  ROI  ROI    Peak    Energy   Net Peak Net Area  Continuum  Tentative");
            sr.ReadLine();
            sr.ReadLine();

            line = sr.ReadLine();
            while (line != "")
            {
                string[] sArray = line.Split(' ');

                int i = 0;
                double ee = -1, cc = -1;
                foreach (string str in sArray)
                {
                    if (str == "") continue;

                    if (i == 4)
                    {
                        ee = double.Parse(str);
                    }
                    else if (i == 7)
                    {
                        cc = double.Parse(str);
                    }
                    i++;
                }
                line = sr.ReadLine();

                if (ee != -1) det.dict[ee] = cc;

            }
            listDet.Add(det);
        }

        //save 
        public void SaveFile(string path)
        {
            string fileNameExt = path.Substring(path.LastIndexOf("\\") + 1); //获取文件名，不带路径
            //"_EmisDect.dat"  "_TransDect.dat"
            //如果文件不存在，则创建；存在则覆盖 
            File.Create(path).Close();
            
            using (StreamWriter sw = new StreamWriter(path))
            {
                string str;
                sw.WriteLine("TITLE=\"" + fileNameExt + "\"");
                str = "VARIABLES= \"Angle\" \"dY\" \"Height\" ";

                Dictionary<double, double> Energy=new Dictionary<double,double>();
                foreach (DetPara det in listDet)
                {
                    foreach (double k in det.dict.Keys)
                    {
                        Energy[k] = 1;
                    }
                }
                foreach (double e in Energy.Keys)
                {
                    str += "\"" + e + "\" ";
                }
                sw.WriteLine(str);

                sw.WriteLine("ZONE T=\"3D Data\" I=1 J=1 K=9 F=POINT");

                foreach (DetPara det in listDet)
                {
                    str = det.Angle.ToString("E8") + " ";
                    str += det.dY.ToString("E8") + " ";
                    str += det.Height.ToString("E8") + " ";

                    
                    foreach (double k in Energy.Keys)
                    {
                        if(det.dict.ContainsKey(k))
                            str += det.dict[k].ToString("E8") + " ";
                        else
                            str += (0).ToString("E8") + " ";
                    }
                    sw.WriteLine(str);
                }

                sw.WriteLine("END");
            }
        }
        


    }
    class SGS:GSBase
    {
        //layer detection
        ushort RotationMode, Spd, Pos, Time;
        bool Mode;


        public SGS(TcpClient MotTcp,TcpClient DetTcp,MainView main)
            :base(MotTcp,DetTcp,main)
        {
            using (StreamReader sr = new StreamReader(".\\Settings\\SGS.txt"))
            {
                string line;
                while ((line = sr.ReadLine())[0] == '#') ;
                string[] s = line.Split(new Char[] { ' ' });
                RotationMode = ushort.Parse(s[0]);
                Spd = ushort.Parse(s[1]);
                Pos = ushort.Parse(s[2]);
                Time = ushort.Parse(s[3]);
                Mode = int.Parse(s[4])==1;
            }

        }


        public void FixedPos(ushort Height)
        {
            
        }
        public void LayerDet(int Start,int Step,int End,string ID)
        {
            
            int NOofLayers = (End - Start) / Step + 1;

            for (int i = 0; i < NOofLayers; ++i)
            {
                ushort Height=(ushort)(Start+Step*i);

                //progressbar update
                pMain.ProgressUpdate(Step);

                MotorControl(Height,RotationMode,Spd,Pos);

                DetectorControl(ID,i,Time,Mode);

                addDet(0, 0, (double)Height, ID + '_' + i + ".rpt");

            }


        }

        

    }

    class STGS : GSBase
    {
        public STGS(TcpClient MotTcp, TcpClient DetTcp, MainView main)
            :base(MotTcp,DetTcp,main)
        {
        }
    }

    class TGS : GSBase
    {
        public TGS(TcpClient MotTcp, TcpClient DetTcp, MainView main)
            :base(MotTcp,DetTcp,main)
        {
        }
    }

    public class DetPara
    {
        public double Angle{get;set;}
        public double dY { get; set; }
        public double Height { get; set; }
        public Dictionary<double, double> dict { get; set; }

        public DetPara()
        {
            dict = new Dictionary<double, double>();
        }
    }
}
