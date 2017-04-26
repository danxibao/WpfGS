﻿using System;
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

namespace WpfGS
{
    class GSBase
    {
        public ModbusIpMaster Mot, Det;
        public GSBase(TcpClient MotTcp, TcpClient DetTcp)
        {
            if (null == MotTcp) throw new Exception("电机未连接");
            Mot = ModbusIpMaster.CreateIp(MotTcp);

            if (null == DetTcp) throw new Exception("探测器未连接");
            Det = ModbusIpMaster.CreateIp(DetTcp);

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
        


    }
    class SGS:GSBase
    {
        SGS(TcpClient MotTcp,TcpClient DetTcp)
            :base(MotTcp,DetTcp)
        {
        }
        void FixedPos(ushort Height)
        {
            
        }
        void LayerDet(int Start,int Step,int End,string ID)
        {
            
            int NOofLayers = (End - Start) / Step + 1;

            for (int i = 0; i < NOofLayers; ++i)
            {
                ushort Height=(ushort)(Start+Step*i);
                //MotorControl(Height,);

            }
        }

    }

    class TSGS : GSBase
    {
        TSGS(TcpClient MotTcp,TcpClient DetTcp)
            :base(MotTcp,DetTcp)
        {
        }
    }

    class TGS : GSBase
    {
        TGS(TcpClient MotTcp,TcpClient DetTcp)
            :base(MotTcp,DetTcp)
        {
        }
    }
}