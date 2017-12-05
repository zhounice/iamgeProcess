using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSImageProcessing
{
    /// <summary>
    /// 图像滤波
    /// </summary>
    public class IamgeFilter
    {
        private static byte[] gray = new byte[0];

        private static double byte0 = 0;
        private static double byte45 = 0;
        private static double byte90 = 0;
        private static double byte135 = 0;
        #region 图像滤波
        /// <summary>
        /// 图片3*3窗口均值处理
        /// </summary>
        /// <param name="imageBytes"></param>
        /// <param name="width"></param>
        /// <param name="heigh"></param>
        /// <returns></returns>
        public static byte[] WorkinAve(byte[] imageBytes, int width, int heigh)
        {
            if (width * heigh != imageBytes.Length)
            {

                return null;
            }
            byte[] imageNewBytes = new byte[width * heigh];
            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < heigh - 1; j++)
                {
                    byte b00 = imageBytes[(i - 1) * width + (j - 1)];
                    byte b01 = imageBytes[(i - 1) * width + (j - 0)];
                    byte b02 = imageBytes[(i - 1) * width + (j + 1)];
                    byte b10 = imageBytes[(i) * width + (j - 1)];
                    byte b12 = imageBytes[(i) * width + (j + 1)];
                    byte b20 = imageBytes[(i + 1) * width + (j - 1)];
                    byte b21 = imageBytes[(i + 1) * width + (j - 0)];
                    byte b22 = imageBytes[(i + 1) * width + (j + 1)];
                    imageNewBytes[i * width + j] = GetAveValue(b00, b01, b02, b10, b12, b20, b21, b22);
                }
            }
            return imageNewBytes;
        }
        /// <summary>
        /// 图片中值滤波
        /// </summary>
        /// <param name="imageBytes"></param>
        /// <param name="width"></param>
        /// <param name="heigh"></param>
        /// <returns></returns>
        public static byte[] WorkinMid(byte[] imageBytes, int width, int heigh)
        {
            if (width * heigh != imageBytes.Length)
            {
                return null;
            }
            byte[] imageNewBytes = new byte[width * heigh];
            for (int i = 1; i < heigh - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    byte b00 = imageBytes[(i - 1) * width + (j - 1)];
                    byte b01 = imageBytes[(i - 1) * width + (j - 0)];
                    byte b02 = imageBytes[(i - 1) * width + (j + 1)];
                    byte b10 = imageBytes[(i) * width + (j - 1)];
                    byte b11 = imageBytes[(i) * width + (j - 0)];
                    byte b12 = imageBytes[(i) * width + (j + 1)];
                    byte b20 = imageBytes[(i + 1) * width + (j - 1)];
                    byte b21 = imageBytes[(i + 1) * width + (j - 0)];
                    byte b22 = imageBytes[(i + 1) * width + (j + 1)];
                    imageNewBytes[i * width + j] = GetMidValue(b00, b01, b02, b10, b11, b12, b20, b21, b22);
                }
            }
            return imageNewBytes;
        }
        /// <summary>
        /// 得到九个数中的中值
        /// </summary>
        /// <param name="b00"></param>
        /// <param name="b01"></param>
        /// <param name="b02"></param>
        /// <param name="b10"></param>
        /// <param name="b11"></param>
        /// <param name="b12"></param>
        /// <param name="b20"></param>
        /// <param name="b21"></param>
        /// <param name="b22"></param>
        /// <returns></returns>
        private static byte GetMidValue(byte b00, byte b01, byte b02, byte b10, byte b11, byte b12, byte b20, byte b21, byte b22)
        {


            byte[] bytes = new byte[9] { b00, b01, b02, b10, b11, b12, b20, b21, b22 };
            for (int i = 0; i < 9; i++)
            {
                for (int j = i; j < 9; j++)
                {
                    if (bytes[i] < bytes[j])
                    {
                        byte temp = bytes[j];
                        bytes[j] = bytes[i];
                        bytes[i] = temp;
                    }
                }

            }
            return bytes[4];
        }
        /// <summary>
        /// 得到九个数的均值
        /// </summary>
        /// <param name="b00"></param>
        /// <param name="b01"></param>
        /// <param name="b02"></param>
        /// <param name="b10"></param>
        /// <param name="b12"></param>
        /// <param name="b20"></param>
        /// <param name="b21"></param>
        /// <param name="b22"></param>
        /// <returns></returns>
        private static byte GetAveValue(byte b00, byte b01, byte b02, byte b10, byte b12, byte b20, byte b21, byte b22)
        {
            return (byte)((b00 + b01 + b02 + b10 + b12 + b20 + b21 + b22) / 8);
        }
        /// <summary>
        /// 高斯低通滤波
        /// </summary>
        /// <param name="imageBytes">源数据</param>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <param name="α">α</param>
        /// <returns></returns>
        public static byte[] GuassFilter(byte[] imageBytes, int with, int heigh)
        {
            int length = imageBytes.Length;
            byte[] newImageBytes = new byte[length];
            for (int i = 2; i < heigh - 2; i++)
            {
                for (int j = 2; j < with - 2; j++)
                {
                    newImageBytes[i * with + j] = (byte)(imageBytes[(i) * with + j] * 0.162 + imageBytes[(i) * with + j - 2] * 0.0219 + imageBytes[(i) * with + j - 1] * 0.0983 + imageBytes[(i) * with + j + 2] * 0.0219 + imageBytes[(i) * with + j + 1] * 0.0983
                        + imageBytes[(i - 2) * with + j] * 0.0219 + imageBytes[(i - 2) * with + j - 2] * 0.003 + imageBytes[(i - 2) * with + j - 1] * 0.0133 + imageBytes[(i - 2) * with + j + 2] * 0.003 + imageBytes[(i - 2) * with + j + 1] * 0.0113
                        + +imageBytes[(i - 1) * with + j] * 0.0983 + imageBytes[(i - 1) * with + j - 2] * 0.0133 + imageBytes[(i - 1) * with + j - 1] * 0.0596 + imageBytes[(i - 1) * with + j + 2] * 0.0133 + imageBytes[(i - 2) * with + j + 1] * 0.0596
                        + +imageBytes[(i + 2) * with + j] * 0.0219 + imageBytes[(i + 2) * with + j - 2] * 0.003 + imageBytes[(i + 2) * with + j - 1] * 0.0133 + imageBytes[(i + 2) * with + j + 2] * 0.003 + imageBytes[(i + 2) * with + j + 1] * 0.0113
                        + +imageBytes[(i + 1) * with + j] * 0.0983 + imageBytes[(i + 1) * with + j - 2] * 0.0133 + imageBytes[(i + 1) * with + j - 1] * 0.0596 + imageBytes[(i + 1) * with + j + 2] * 0.0133 + imageBytes[(i + 2) * with + j + 1] * 0.0596);

                }

            }
            return newImageBytes;
        }
        /// <summary>
        /// 高斯低通滤波计算
        /// </summary>
        /// <param name="b">源像素</param>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <param name="α">α</param>
        /// <returns></returns>
        private static byte GaussLowpasFilterWork(byte b, int x, int y, double α)
        {
            double arg = Math.Pow(Math.E, ((x * x + y * y) / (2 * α * α)));
            b = (byte)(arg / (2 * Math.PI * α * α));
            return b;
        }
        /// <summary>
        /// Prewitt算法（边缘增强）
        /// </summary>
        /// <param name="imageBytes">源数据</param>
        /// <param name="width">图片宽度</param>
        /// <param name="heigh">图片高度</param>
        /// <returns></returns>
        public static byte[] PrewittArithm(byte[] imageBytes, int width, int heigh)
        {
            int length = imageBytes.Length;
            byte[] newImageBytes = new byte[length];
            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < heigh - 1; j++)
                {
                    byte b00 = imageBytes[(j - 1) * width + (i - 1)];
                    byte b01 = imageBytes[(j - 1) * width + (i - 0)];
                    byte b02 = imageBytes[(j - 1) * width + (i + 1)];
                    byte b10 = imageBytes[(j) * width + (i - 1)];
                    byte b12 = imageBytes[(j) * width + (i + 1)];
                    byte b20 = imageBytes[(j + 1) * width + (i - 1)];
                    byte b21 = imageBytes[(j + 1) * width + (i - 0)];
                    byte b22 = imageBytes[(j + 1) * width + (i + 1)];
                    newImageBytes[j * width + i] = PrewittWork(b00, b01, b02, b10, b12, b20, b21, b22);
                }
            }
            return newImageBytes;
        }
        /// <summary>
        /// 利用Prewitt算子得出结果，，该算法是欠缺合理的，会造成误判，因为许多噪声点的灰度值也很大。
        /// 而且对于幅值较小的边缘点，其边缘会丢失。
        /// </summary>
        /// <param name="b00"></param>
        /// <param name="b01"></param>
        /// <param name="b02"></param>
        /// <param name="b10"></param>
        /// <param name="b12"></param>
        /// <param name="b20"></param>
        /// <param name="b21"></param>
        /// <param name="b22"></param>
        /// <returns></returns>
        private static byte PrewittWork(byte b00, byte b01, byte b02, byte b10, byte b12, byte b20, byte b21, byte b22)
        {


            int bt1 = b02 + b12 + b22 - b00 - b10 - b20;
            int bt2 = b22 + b21 + b22 - b00 - b01 - b02;
            //if (bt1 >= bt2)
            //{
            //    return (byte)bt1;
            //}
            //else
            //{
            //    return (byte)bt2;
            //}
            return (byte)Math.Sqrt(bt1 * bt1 + bt2 * bt2);
        }
        /// <summary>
        /// 拉普朗斯算法（图像增强）
        /// </summary>
        /// <param name="imageBytes"></param>
        /// <param name="width"></param>
        /// <param name="heigh"></param>
        /// <returns></returns>
        public static byte[] LaplacianAruthm(byte[] imageBytes, int width, int heigh)
        {
            byte[] newImageBytes = new byte[width * heigh];
            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < heigh - 1; j++)
                {
                    byte b01 = imageBytes[(j - 1) * width + (i - 0)];
                    byte b10 = imageBytes[(j) * width + (i - 1)];
                    byte b11 = imageBytes[j * width + i];
                    byte b12 = imageBytes[(j) * width + (i + 1)];
                    byte b21 = imageBytes[(j + 1) * width + (i - 0)];
                    newImageBytes[j * width + i] = LaplacianAruthmWork(b01, b10, b11, b12, b21);
                }
            }
            return newImageBytes;
        }
        /// <summary>
        /// 利用拉普朗斯算子得出结果,该算子是一种二阶导数算子，将在边缘处行生一个陡峭的零交叉。
        /// 拉普朗斯算子是各项同性，能对任何走向的边界和线条进行锐化，无方向性。这是拉普朗斯算子区别于其他算子的最大优点。
        /// </summary>
        /// <param name="b01"></param>
        /// <param name="b10"></param>
        /// <param name="b11"></param>
        /// <param name="b12"></param>
        /// <param name="b21"></param>
        /// <returns></returns>
        private static byte LaplacianAruthmWork(byte b01, byte b10, byte b11, byte b12, byte b21)
        {
            return (byte)(4 * b11 - b01 - b10 - b12 - b21);
        }

        public static byte[] TDDSJQ(byte[] imageBytes, int width, int heigh)
        {
            byte[] newImageBytes = new byte[width * heigh];
            for (int i = 1; i < heigh - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    byte b00 = imageBytes[(i - 1) * width + (j - 1)];
                    byte b01 = imageBytes[(i - 1) * width + (j - 0)];
                    byte b02 = imageBytes[(i - 1) * width + (j + 1)];
                    byte b10 = imageBytes[(i) * width + (j - 1)];
                    byte b11 = imageBytes[(i) * width + (j - 0)];
                    byte b12 = imageBytes[(i) * width + (j + 1)];
                    byte b20 = imageBytes[(i + 1) * width + (j - 1)];
                    byte b21 = imageBytes[(i + 1) * width + (j - 0)];
                    byte b22 = imageBytes[(i + 1) * width + (j + 1)];
                    newImageBytes[i * width + j] = TDDSJQQWork(b00, b01, b02, b10, b11, b12, b20, b21, b22);
                }
            }
            return newImageBytes;
        }

        private static byte TDDSJQQWork(byte b00, byte b01, byte b02, byte b10, byte b11, byte b12, byte b20, byte b21, byte b22)
        {
            byte b = 0;
            double du00 = 1.0 / b00;
            double du01 = 1.0 / b01;
            double du02 = 1.0 / b02;
            double du10 = 1.0 / b10;
            double du12 = 1.0 / b12;
            double du20 = 1.0 / b20;
            double du21 = 1.0 / b21;
            double du22 = 1.0 / b22;
            double gyh = 2*(du00 + du01 + du02 + du10 + du12 + du20 + du21 + du22);
            b = (byte)((du00 / gyh) * b00 + (du01 / gyh) * b01 + (du02 / gyh) * b02 + (du10 / gyh) * b10 + (du12 / gyh) * b12 + (du20 / gyh) * b20 + (du21 / gyh) * b21 + (du22 / gyh) * b22+b11*0.5);
            return b;
        }
        #region Canny算子

        public static byte[] CannyAruthm(byte[] imageBytes, int width, int heigh)
        {
            int length = imageBytes.Length;
            byte[] newIamgeBytes = new byte[length];

            byte0 = 0;
            byte45 = 0;
            byte90 = 0;
            byte135 = 0;
            Histogram his = new Histogram(imageBytes);
            HistogramValue[] hiss = his.GetHistogramValue();
            int len = hiss.Length;
            gray = new byte[hiss.Length];
            for (int i = 0; i < len; i++)
            {
                gray[i] = hiss[i].Value;
            }
            for (int i = 0; i < len; i++)
            {
                for (int j = i + 1; j < len; j++)
                {
                    if (gray[i] > gray[j])
                    {
                        byte b = gray[i];
                        gray[i] = gray[j];
                        gray[j] = b;
                    }
                }
            }
            double p0 = Get0HDGSJZ(imageBytes, len, width, heigh);
            double p45 = Get45HDGSJZ(imageBytes, len, width, heigh);
            double p90 = Get90HDGSJZ(imageBytes, len, width, heigh);
            double p135 = Get135HDGSJZ(imageBytes, len, width, heigh);
            double Fave = (p0 + p45 + p90 + p135) / 4;
            double E = (byte0 + byte45 + byte90 + byte135) / 4;
            double sita = 0.8 + 0.1 * Math.Log(Fave + 1.1 * E);
            double TH = 0.13 + 0.02 * Math.Log(Fave + 1.2 * E);
            double[] fdz = FDZ(imageBytes, width, heigh);
            BYPoint[] BYPonts = new BYPoint[(width - 2) * (heigh - 2)];
            for (int i = 1; i < heigh - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    if (fdz[i * width + j] > TH && fdz[i * width + j] > fdz[i * width + j - 1] && fdz[i * width + j] > fdz[i * width + j + 1] && fdz[i * width + j] > fdz[(i - 1) * width + j] && fdz[i * width + j] > fdz[(i + 1) * width + j]
                         && fdz[i * width + j] > fdz[(i - 1) * width + j - 1] && fdz[i * width + j] > fdz[(i + 1) * width + j + 1] && fdz[i * width + j] > fdz[(i - 1) * width + j + 1] && fdz[i * width + j] > fdz[(i + 1) * width + j - 1])
                    {
                        BYPoint byd = new BYPoint(j, i);
                        BYPonts[(i - 1) * (width - 2) + j - 1] = byd;
                    }
                    else
                    {
                        BYPoint byd = new BYPoint();
                        BYPonts[(i - 1) * (width - 2) + j - 1] = byd;

                    }
                }
            }
            BYPonts = DGBYpoints(BYPonts, 0.5 * TH, fdz, width, heigh);

            foreach (var item in BYPonts)
            {
                if (item.Y <= 0 || item.X <= 0 || item.Y >= heigh - 2 || item.X >= width - 2)
                {
                    continue;
                }

                newIamgeBytes[item.Y * width + item.X] = imageBytes[item.Y * width + item.X];
            }
            return newIamgeBytes;
        }

        private static BYPoint[] DGBYpoints(BYPoint[] bpoings, double helfth, double[] fdz, int widh, int heigh)
        {


            bool BO = false;
            foreach (var item in bpoings)
            {
                if (item.Y <= 1 || item.X <= 1 || item.Y >= heigh - 2 || item.X >= widh - 2)
                {
                    continue;
                }
                //if (fdz[item.Y * widh + item.X] > fdz[item.Y * widh + item.X - 1] && fdz[item.Y * widh + item.X] > fdz[item.Y * widh + item.X + 1] && fdz[item.Y * widh + item.X] > fdz[(item.Y - 1) * widh + item.X] && fdz[item.Y * widh + item.X] > fdz[(item.Y + 1) * widh + item.X]
                //         && fdz[item.Y * widh + item.X] > fdz[(item.Y - 1) * widh + item.X + 1] && fdz[item.Y * widh + item.X] > fdz[(1 + item.Y) * widh + 1 + item.X] && fdz[item.Y * widh + item.X] > fdz[(item.Y - 1) * widh + item.X + 1] && fdz[item.Y * widh + item.X] > fdz[(item.Y + 1) * widh + item.X - 1])
                //{


                if (fdz[item.Y * widh + item.X - 1] >= helfth && bpoings[(item.Y - 1) * (widh - 2) + item.X - 1 - 1].X == 0)
                {
                    bpoings[(item.Y - 1) * (widh - 2) + item.X - 1 - 1] = new BYPoint(item.X - 1, item.Y);
                    BO = true;
                }
                if (fdz[item.Y * widh + item.X + 1] >= helfth && bpoings[(item.Y - 1) * (widh - 2) + item.X - 1 + 1].X == 0)
                {
                    bpoings[(item.Y - 1) * (widh - 2) + item.X - 1 + 1] = new BYPoint(item.X + 1, item.Y);
                    BO = true;
                }
                if (fdz[(1 + item.Y) * widh + item.X] >= helfth && bpoings[(1 + item.Y - 1) * (widh - 2) + item.X - 1].X == 0)
                {
                    bpoings[(1 + item.Y - 1) * (widh - 2) + item.X - 1] = new BYPoint(item.X, item.Y + 1);
                    BO = true;
                }
                if (fdz[(item.Y - 1) * widh + item.X] >= helfth && bpoings[(item.Y - 1 - 1) * (widh - 2) + item.X - 1].X == 0)
                {
                    bpoings[(item.Y - 1 - 1) * (widh - 2) + item.X - 1] = new BYPoint(item.X, item.Y - 1);
                    BO = true;
                }
                if (fdz[(item.Y - 1) * widh + item.X - 1] >= helfth && bpoings[(item.Y - 1 - 1) * (widh - 2) + item.X - 1 - 1].X == 0)
                {
                    bpoings[(item.Y - 1 - 1) * (widh - 2) + item.X - 1 - 1] = new BYPoint(item.X - 1, item.Y - 1);
                    BO = true;
                }
                if (fdz[(1 + item.Y) * widh + item.X + 1] >= helfth && bpoings[(1 + item.Y - 1) * (widh - 2) + item.X - 1 + 1].X == 0)
                {
                    bpoings[(1 + item.Y - 1) * (widh - 2) + item.X - 1 + 1] = new BYPoint(item.X + 1, item.Y + 1);
                    BO = true;
                }
                if (fdz[(1 + item.Y) * widh + item.X - 1] >= helfth && bpoings[(1 + item.Y - 1) * (widh - 2) + item.X - 1 - 1].X == 0)
                {
                    bpoings[(1 + item.Y - 1) * (widh - 2) + item.X - 1 - 1] = new BYPoint(item.X - 1, item.Y + 1);
                    BO = true;
                }
                if (fdz[(item.Y - 1) * widh + item.X + 1] >= helfth && bpoings[(item.Y - 1 - 1) * (widh - 2) + item.X - 1 + 1].X == 0)
                {
                    bpoings[(item.Y - 1 - 1) * (widh - 2) + item.X - 1 + 1] = new BYPoint(item.X + 1, item.Y - 1);
                    BO = true;
                }
                // }
            }
            if (BO)
            {
                DGBYpoints(bpoings, helfth, fdz, widh, heigh);
            }

            return bpoings;
        }
        private static double Get0HDGSJZ(byte[] iamgeBytes, int len, int width, int heigh)
        {
            double HDGSJZ = 0;
            int[] P = new int[len * len];
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    for (int n = 0; n < heigh; n++)
                    {
                        for (int m = 0; m < width - 1; m++)
                        {
                            if (iamgeBytes[width * n + m] == gray[i] && iamgeBytes[width * n + m + 1] == gray[j])
                            {
                                P[i * len + j]++;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {

                    HDGSJZ += ((P[i * len + j] * 1.0) / (heigh * (width - 1))) * ((gray[i] - gray[j]) * (gray[i] - gray[j]));
                    byte0 += ((P[i * len + j] * 1.0) / (heigh * (width - 1))) * ((P[i * len + j] * 1.0) / (heigh * (width - 1)));
                }

            }
            return HDGSJZ;
        }
        /// <summary>
        /// 计算方向位45°的灰度共生矩阵
        /// </summary>
        /// <param name="iamgeBytes">图像数据</param>
        /// <param name="len">灰度级个数</param>
        /// <param name="width">图像宽度</param>
        /// <param name="heigh">图像长度</param>
        /// <returns></returns>
        private static double Get45HDGSJZ(byte[] iamgeBytes, int len, int width, int heigh)
        {

            double HDGSJZ = 0;
            int[] P = new int[len * len];
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    for (int n = 1; n < heigh; n++)
                    {
                        for (int m = 0; m < width - 1; m++)
                        {
                            if (iamgeBytes[width * n + m] == gray[i] && iamgeBytes[width * (n - 1) + m + 1] == gray[j])
                            {
                                P[i * len + j]++;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {

                    HDGSJZ += ((P[i * len + j] * 1.0) / (heigh * (width - 1))) * ((gray[i] - gray[j]) * (gray[i] - gray[j]));//幅度值
                    byte45 += ((P[i * len + j] * 1.0) / (heigh * (width - 1))) * ((P[i * len + j] * 1.0) / (heigh * (width - 1)));//能量
                }

            }
            return HDGSJZ;
        }
        private static double Get90HDGSJZ(byte[] iamgeBytes, int len, int width, int heigh)
        {

            double HDGSJZ = 0;
            int[] P = new int[len * len];

            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    for (int n = 1; n < heigh; n++)
                    {
                        for (int m = 0; m < width; m++)
                        {
                            if (iamgeBytes[width * n + m] == gray[i] && iamgeBytes[width * (n - 1) + m] == gray[j])
                            {
                                P[i * len + j]++;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    HDGSJZ += ((P[i * len + j] * 1.0) / (heigh * (width - 1))) * ((gray[i] - gray[j]) * (gray[i] - gray[j]));
                    byte90 += ((P[i * len + j] * 1.0) / (heigh * (width - 1))) * ((P[i * len + j] * 1.0) / (heigh * (width - 1)));
                }

            }
            return HDGSJZ;
        }
        private static double Get135HDGSJZ(byte[] iamgeBytes, int len, int width, int heigh)
        {

            double HDGSJZ = 0;
            int[] P = new int[len * len];

            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    for (int n = 1; n < heigh; n++)
                    {
                        for (int m = 1; m < width - 1; m++)
                        {
                            if (iamgeBytes[width * n + m] == gray[i] && iamgeBytes[width * (n - 1) + m - 1] == gray[j])
                            {
                                P[i * len + j]++;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    HDGSJZ += ((P[j * len + j] * 1.0) / (heigh * (width - 1))) * ((gray[i] - gray[j]) * (gray[i] - gray[j]));
                    byte135 += ((P[i * len + j] * 1.0) / (heigh * (width - 1))) * ((P[i * len + j] * 1.0) / (heigh * (width - 1)));
                }

            }
            return HDGSJZ;
        }
        private static double[] FDZ(byte[] imageBytes, int width, int heigh)
        {
            int length = imageBytes.Length;
            double[] fdz = new double[length];
            for (int i = 1; i < heigh - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {

                    fdz[i * width + j] = Math.Sqrt(1.0 * (imageBytes[(i - 1) * width + j] - imageBytes[(i + 1) * width + j]) * (imageBytes[(i - 1) * width + j] - imageBytes[(i + 1) * width + j]) +
                        1.0 * (imageBytes[i * width + j + 1] - imageBytes[(i) * width + j - 1]) * (imageBytes[i * width + j + 1] - imageBytes[i * width + j - 1]));
                }
            }
            return fdz;
        }


        #endregion
        #endregion
    }
    /// <summary>
    /// 图像拉伸
    /// </summary>
    public class ImageStretch
    {
        #region 图像拉伸
        /// <summary>
        /// 返回区域线性拉伸之后的结果
        /// </summary>
        /// <param name="imageBytes">数据源</param>
        /// <param name="a">f(x,y)最小值</param>
        /// <param name="b">f(x,y)最大值</param>
        /// <param name="c">g(x,y)最小值</param>
        /// <param name="d">g(x,y)最大值</param>
        /// <returns></returns>
        public static byte[] QYXXImangeStretcher(byte[] imageBytes, int a, int b, int c, int d)
        {

            int len = imageBytes.Length;
            byte[] newImageBytes = new byte[len];
            for (int i = 0; i < len; i++)
            {
                newImageBytes[i] = (byte)QYXXStretcher(imageBytes[i], a, b, c, d);
            }
            return newImageBytes;
        }
        /// <summary>
        /// 区域线性拉伸返回值
        /// </summary>
        /// <param name="bt">源灰度值</param>
        /// <param name="a">f(x,y)最小值</param>
        /// <param name="b">f(x,y)最大值</param>
        /// <param name="c">g(x,y)最小值</param>
        /// <param name="d">g(x,y)最大值</param>
        /// <returns></returns>
        private static int QYXXStretcher(int bt, int a, int b, int c, int d)
        {
            if (bt >= b)
            {
                bt = d;

            }
            else if (bt <= a)
            {
                bt = c;

            }
            else if (bt > a && bt < b)
            {
                bt = ((d - c) / (b - a)) * (bt - a) + c;
                //  MessageBox.Show("sdsd");
            }
            return bt;
        }
        /// <summary>
        /// 分段线性拉伸
        /// </summary>
        /// <param name="imageBytes">数据源</param>
        /// <param name="a">f(x,y)最小值</param>
        /// <param name="b">f(x,y)最大值</param>
        /// <param name="c">g(x,y)最小值</param>
        /// <param name="d">g(x,y)最大值</param>
        /// <returns></returns>
        public static byte[] FDXXImageStretcher(byte[] imageBytes, int a, int b, int c, int d)
        {
            int len = imageBytes.Length;
            byte[] newImageBytes = new byte[len];
            for (int i = 0; i < len; i++)
            {
                newImageBytes[i] = (byte)FDXXStretcher(imageBytes[i], a, b, c, d);
            }
            return newImageBytes;
        }
        /// <summary>
        /// 分段线性拉伸返回值
        /// </summary>
        /// <param name="bt">源灰度值</param>
        /// <param name="a">f(x,y)最小值</param>
        /// <param name="b">f(x,y)最大值</param>
        /// <param name="c">g(x,y)最小值</param>
        /// <param name="d">g(x,y)最大值</param>
        /// <returns></returns>
        private static int FDXXStretcher(int bt, int a, int b, int c, int d)
        {
            if (bt >= b)
            {
                bt = (255 - d / 255 - b) * (bt - a) + c;
            }
            else if (bt <= a)
            {
                bt = ((c) / (a)) * (bt);
            }
            else if (bt > a && bt < b)
            {
                bt = ((d - c) / (b - a)) * (bt - a) + c;
            }
            return bt;
        }

        public static byte[] ZFTIamgeStreatcher(byte[] imageBytes, int maxGray, int minGray)
        {
            int length = imageBytes.Length;
            byte[] newImageBytes = new byte[length];
            Histogram his = new Histogram(imageBytes);
            HistogramValue[] hisv = his.GetHistogramValue();
            int fj = hisv.Length;
            if (maxGray - minGray < fj) return null;
            HistogramValue[] hisv1 = new HistogramValue[hisv.Length];
            hisv1 = GetPLMDandPaixuHisValue(hisv);
            ZFTJH[] zftjh = new ZFTJH[fj];
            int jiange = (maxGray - minGray) / fj;
            double jiangemd = 1.0 / fj;
            ZFTJH z = new ZFTJH();
            zftjh[0] = z;
            zftjh[0].Gray = (byte)minGray;
            zftjh[0].Ljmd = jiangemd;
            for (int i = 1; i < fj - 1; i++)
            {
                z = new ZFTJH();
                zftjh[i] = z;
                zftjh[i].Gray = (byte)(minGray + i * jiange);
                zftjh[i].Ljmd = (i + 1) * jiangemd;
            }
            if (fj > 2)
            {
                z = new ZFTJH();
                zftjh[fj - 1] = z;
                zftjh[fj - 1].Gray = (byte)maxGray;
                zftjh[fj - 1].Ljmd = 1;
            }
            Dictionary<byte, byte> dic = ZFTStreatcher(hisv, zftjh);
            for (int i = 0; i < length; i++)
            {
                byte b = imageBytes[i];
                newImageBytes[i] = dic[imageBytes[i]];
            }

            return newImageBytes;
        }
        private static HistogramValue[] GetPLMD(HistogramValue[] hisv)
        {
            int length = hisv.Length;
            double count = 0;
            for (int i = 0; i < length; i++)
            {
                count += hisv[i].Probability;
                hisv[i].Plmd = count;
            }
            return hisv;
        }
        /// <summary>
        /// 对每个灰度级进行排序并得到其累计频率
        /// </summary>
        /// <param name="hisv">在直方图中的灰度级类</param>
        /// <returns></returns>
        private static HistogramValue[] GetPLMDandPaixuHisValue(HistogramValue[] hisv)
        {
            int length = hisv.Length;
            double LJPL = 0;//累计频率
            for (int i = 0; i < length; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    if (hisv[i].Value > hisv[j].Value)//利用冒泡排序对灰度级进行从小到大排序
                    {
                        HistogramValue h = hisv[i];
                        hisv[i] = hisv[j];
                        hisv[j] = h;

                    }
                }
            }
            for (int i = 0; i < length; i++)
            {
                LJPL += hisv[i].Probability;//对排好序的灰度级频率累加，得到累计频率
                hisv[i].Plmd = LJPL;
            }
            return hisv;
        }
        /// <summary>
        /// 将均衡前后的像素值对应起来
        /// </summary>
        /// <param name="hisv">直方图中的灰度级</param>
        /// <param name="zftjh">均衡之后的灰度级</param>
        /// <returns></returns>
        private static Dictionary<byte, byte> ZFTStreatcher(HistogramValue[] hisv, ZFTJH[] zftjh)
        {

            Dictionary<byte, byte> JHH= new Dictionary<byte, byte>();
            for (int i = 0; i < hisv.Length - 1; i++)
            {
                if (hisv[i].Plmd < zftjh[0].Ljmd)
                {
                    JHH.Add(hisv[i].Value, zftjh[0].Gray);
                    continue;
                }
                for (int j = 0; j < hisv.Length - 1; j++)
                {
                    if ((hisv[i].Plmd - zftjh[j].Ljmd) * (hisv[i].Plmd - zftjh[j + 1].Ljmd) <= 0)
                    {
                        if (Math.Abs((hisv[i].Plmd - zftjh[j].Ljmd)) > Math.Abs((hisv[i].Plmd - zftjh[j + 1].Ljmd)))//判断两者之间的频率密度差距
                        {
                            JHH.Add(hisv[i].Value, zftjh[j + 1].Gray);
                            continue;
                        }
                        else
                        {
                            JHH.Add(hisv[i].Value, zftjh[j].Gray);
                            continue;
                        }
                    }
                }

            }
            JHH.Add(hisv[hisv.Length - 1].Value, zftjh[zftjh.Length - 1].Gray);
            return JHH;
        }
        #endregion
    }
    /// <summary>
    /// 图像运算
    /// </summary>
    public class ImageOperation
    {

        #region 遥感图像运算
        //add subtract multiply divide
        /// <summary>
        /// 对两张图片求平均值，可减少图像的随机噪声
        /// </summary>
        /// <param name="imageBytes0">第一张图片数据</param>
        /// <param name="imageBytes1">第二张图片数据</param>
        /// <returns></returns>
        public static byte[] AverageImage(byte[] imageBytes0, byte[] imageBytes1)
        {
            if (imageBytes1.Length != imageBytes0.Length)
            {
                return null;
            }
            int len = imageBytes1.Length;
            byte[] newImagebytes = new byte[len];
            for (int i = 0; i < len; i++)
            {
                newImagebytes[i] = (byte)((imageBytes0[i] + imageBytes1[i]) / 2);
            }
            return newImagebytes;
        }
        /// <summary>
        /// 对多个图片求平均值，可减少图像的随机噪声
        /// </summary>
        /// <param name="imageBytes">多个图片的数据源</param>
        /// <returns></returns>
        public static byte[] AverageImage(List<byte[]> imageBytes)
        {
            int len = imageBytes[0].Length;
            int count = imageBytes.Count;
            byte[] newImagebytes = new byte[len];
            if (imageBytes.Count < 2)
            {
                return null;
            }
            for (int j = 0; j < count; j++)
            {
                if (imageBytes[j].Length != imageBytes[j + 1].Length)
                {

                    return null;
                }
            }

            for (int i = 0; i < len; i++)
            {
                int value = 0;
                for (int m = 0; m < count; m++)
                {
                    value += imageBytes[m][i];
                }
                newImagebytes[i] = (byte)(value / count);
            }
            return newImagebytes;
        }
        /// <summary>
        /// 遥感图像的掩膜处理
        /// </summary>
        /// <param name="imageBytes">源图片数据</param>
        /// <param name="maskBytes">掩膜图片数据</param>
        /// <returns></returns>
        public static byte[] ImageMask(byte[] imageBytes, byte[] maskBytes)
        {
            if (imageBytes.Length != maskBytes.Length)
                return null;
            int length = imageBytes.Length;
            byte[] newImageBytes = new byte[length];
            for (int i = 0; i < length; i++)
            {

                newImageBytes[i] = (byte)(imageBytes[i] * maskBytes[i]);

            }
            return newImageBytes;
        }
        /// <summary>
        /// 遥感图片变化检测
        /// </summary>
        /// <param name="imageOld">较早图片</param>
        /// <param name="imageLater">较晚图片</param>
        /// <returns></returns>
        public static byte[] ImageChangeDetection(byte[] imageOld, byte[] imageLater)
        {
            if (imageLater.Length != imageOld.Length)
                return null;
            int length = imageOld.Length;
            byte[] newImageBytes = new byte[length];
            for (int i = 0; i < length; i++)
            {
                newImageBytes[i] = (byte)(imageLater[i] - imageOld[i]);
            }
            return newImageBytes;
        }

        #endregion
    }

    public class ZFTJH
    {
        public ZFTJH()
        {

        }
        byte gray;

        public byte Gray
        {
            get { return gray; }
            set { gray = value; }
        }
        double ljmd;

        public double Ljmd
        {
            get { return ljmd; }
            set { ljmd = value; }
        }
    }
    public class BYPoint
    {
        public BYPoint()
        {

        }
        public BYPoint(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        int x;

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        int y;

        public int Y
        {
            get { return y; }
            set { y = value; }
        }
    }


}
