using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSImageProcessing
{
  public  class HistogramValue
    {
        private double plmd = 0;

        public double Plmd
        {
            get { return plmd; }
            set { plmd = value; }
        }
        private byte value;

        public byte Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        private int count=0;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        private double probability;

        public double Probability
        {
            get { return probability; }
            set { probability = value; }
        }
       public HistogramValue(byte value)
       {
           
           this.value = value;
       }
       
    }
    /// <summary>
    /// 直方图构造函数
    /// </summary>
  public  class Histogram
    {
      public Histogram()
      {

      }
        List<HistogramValue> hisValue = new List<HistogramValue>();
        int bytesCunt=0;
        public Histogram(byte[] bytes)
        {
            hisValue = GetHistorgramCountandProbability(bytes);
        }
        /// <summary>
        /// 得到每个像素值以及相对应的频率及频数
        /// </summary>
        /// <param name="bytes">图像数据</param>
        /// <returns></returns>
        private List<HistogramValue> GetHistorgramCountandProbability(byte[] bytes)
        {
            
            int Length=bytes.Length;
            List<HistogramValue> hiss = new List<HistogramValue>();
          
            HistogramValue his = new HistogramValue(bytes[0]);
            his.Count++;
            hiss.Add(his);
            for (int i = 1; i < Length; i++)
            {
                bool NoAdd = true;
                foreach (HistogramValue item in hiss)
                {
                    if (item.Value == bytes[i])
                    {
                        item.Count++;
                        NoAdd = false;
                        continue;
                    }
                }
                if (NoAdd)
                {
                      HistogramValue h = new HistogramValue(bytes[i]);
                      h.Count++;
                      hiss.Add(h);
                      NoAdd = true;
                }

            }
           
                foreach (var item in hiss)
                {
                    item.Probability = (item.Count * 1.0) / Length;
                }
           
            return hiss;

        }

        public HistogramValue[] GetHistogramValue()
        {
            return hisValue.ToArray();
        }
    }
}
