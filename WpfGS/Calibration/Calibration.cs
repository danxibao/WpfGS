using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WpfGS
{
    public class ECPara
    {
        public double Density{get;set;}
        public double dY{ get;set;}
        public double Height { get; set; }
        public List<evsr> list;
        public ECPara()
        {
            list = new List<evsr>();
        }
    }
    public class evsr : IComparable
    {
        public double energy { get; set; }
        public double rate { get; set; }
        public evsr(double a,double b)
        {
            energy = a;
            rate = b;
        }
        #region 实现比较接口的CompareTo方法
        public int CompareTo(object obj)
        {
            int res = 0;
            try
            {
                evsr sObj = (evsr)obj;
                if (this.energy > sObj.energy)
                {
                    res = 1;
                }
                else if (this.energy < sObj.energy)
                {
                    res = -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("比较异常", ex.InnerException);
            }
            return res;
        }
        #endregion
    }

    public class Node
    {
        public string Name { get; set; }
        public List<Node> father { get; set; }
        public DirectoryInfo fpath { get; set; }

        public string Kind { get; set; }
        public List<Node> Nodes { get; set; }
        public Node()
        {
            Nodes = new List<Node>();
            Kind = "dataString";
        }

    }

    
}
