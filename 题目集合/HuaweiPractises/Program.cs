using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaweiPractises
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("hello");

            //Pratise1();

            //while (true)
            //    Practise2();

            while (true)
            {
                Practise3();
            }
        }

        private static void Pratise1()
        {
            string numStr = Console.ReadLine();

            int num = int.Parse(numStr);

            List<string> originalStr = new List<string>();

            for (int i = 0; i < num; i++)
            {
                string str = Console.ReadLine();

                StringBuilder sb = new StringBuilder();

                for (int j = 0; j < str.Length; j++)
                {
                    sb.Append(str[j]);
                    if ((j + 1) % 8 == 0)
                    {
                        originalStr.Add(sb.ToString());
                        sb.Clear();
                    }
                }

                int ret = str.Length % 8;

                if (ret != 0)
                {
                    for (int j = 0; j < 8 - ret; j++)
                    {
                        sb.Append("0");
                    }

                    originalStr.Add(sb.ToString());
                    sb.Clear();
                }
            }

            originalStr.Sort();

            for (int i = 0; i < originalStr.Count; i++)
            {
                if (0 != i)
                {
                    Console.Write(" ");
                }

                Console.Write(originalStr[i]);
            }

            Console.ReadKey();
        }

        #region Practise2

        private static void Practise2()
        {
            string str = Console.ReadLine();

            string ret = GetStr(str, 1);

            Console.WriteLine(ret);

            //for (int i = ret.Length - 1; i >= 0; i--)
            //{
            //    Console.Write(ret[i]);
            //}

            Console.ReadKey();
        }

        private static string GetStr(string str, int num)
        {
            string retStr = string.Empty;

            StringBuilder sb1 = new StringBuilder();

            for (int i = 0; i < str.Length; i++)
            {
                if (IsDigital(str[i]))
                {
                    if (i + 2 < str.Length)
                    {
                        string originalStr = str.Substring(i + 2);
                        string otherStr = string.Empty;

                        GetStrFromKuo(ref originalStr, ref otherStr);

                        sb1.Append(GetStr(originalStr, str[i] - '0'));

                        if (!string.IsNullOrWhiteSpace(otherStr))
                        {
                            sb1.Append(GetStr(otherStr, 1));
                        }
                    }

                    break;
                }
                else
                {
                    sb1.Append(str[i]);
                }
            }

            retStr = sb1.ToString();

            // 乘以倍数
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < num; i++)
            {
                sb.Append(retStr);
            }

            return sb.ToString();
        }

        private static char[] _leftKuo = { '{', '[', '('};
        private static char[] _rightKuo = { '}', ']', ')'};

        /// <summary>
        /// 是否是左括号
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        private static bool IsLeftKuo(char ch)
        {
            return _leftKuo.Contains(ch);
        }

        /// <summary>
        /// 判断是否是右括号
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        private static bool IsRightKuo(char ch)
        {
            return _rightKuo.Contains(ch);
        }

        /// <summary>
        /// 判断是否是数字
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        private static bool IsDigital(char ch)
        {
            if (ch >= '0' && ch <= '9')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool GetStrFromKuo(ref string originalStr, ref string otherStr)
        {
            // sjhfjhf4(jfsh)fkj4(d4[ffjhjh]))fhjhfj

            int leftKuoNum = 0;
            int rightKuoNum = 0;

            int findIndex = 0;

            for (int i = 0; i < originalStr.Length; i++)
            {
                if (IsLeftKuo(originalStr[i]))
                {
                    leftKuoNum++;
                }
                else if (IsRightKuo(originalStr[i]))
                {
                    rightKuoNum++;
                }

                if (rightKuoNum > leftKuoNum)
                {
                    findIndex = i;
                    break;
                }
            }

            string oriStr = originalStr;
            originalStr = oriStr.Substring(0, findIndex);
            otherStr = oriStr.Substring(findIndex + 1);
            return true;
        }

        #endregion Practise2

        #region Practises3
        static int [,] array;
        static int[,] map;

        private static int num = 0;
        private static int m = 0;
        private static int n = 0;

        private static int startx;
        private static int starty;
        private static int endx;
        private static int endy;

        private static List<POINT> pathPoints;

        private static void Practise3()
        {
            pathPoints = new List<POINT>();
            string mstr = Console.ReadLine();
            string nstr = Console.ReadLine();

            m = int.Parse(mstr);
            n = int.Parse(nstr); 

            array = new int[m, n];
            map = new int[m,n];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    string str = Console.ReadLine();
                    map[i, j] = int.Parse(str);
                }
            }

            string str1 = Console.ReadLine();
            startx = int.Parse(str1);
            str1 = Console.ReadLine();
            starty = int.Parse(str1);

            str1 = Console.ReadLine();
            endx = int.Parse(str1);

            str1 = Console.ReadLine();
            endy = int.Parse(str1);

            Test3(startx, starty, -99);

            Console.WriteLine($"num is:{num % Math.Pow(10, 9)}");
        }

        private static void Test3(int x, int y, int originalVal)
        {

            if (!IsLocationAvailable(x, y, originalVal))
            {
                return;
            }

            if (x == endx && y == endy)
            {
                num++;

                Console.WriteLine("路径是:");
                foreach (var pathPoint in pathPoints)
                {
                    Console.Write($"{pathPoint.X},{pathPoint.Y}  ");
                }

                Console.Write($"{x},{y}  ");

                Console.WriteLine();
                return;
            }

            pathPoints.Add(new POINT(x,y));

            array[x, y] = 1;

            Test3(x+1, y, map[x,y]);

            Test3(x, y+1, map[x, y]);

            Test3(x -1, y, map[x, y]);

            Test3(x, y - 1, map[x, y]);

            array[x, y] = 0;
            pathPoints.RemoveAt(pathPoints.Count - 1);
        }

        private static bool IsLocationAvailable(int x, int y, int originalVal)
        {
            if (x < 0 || x >= m || y < 0 || y >= n)
            {
                return false;
            }

            if (map[x, y] <= originalVal)
            {
                return false;
            }

            return array[x, y] == 0;
        }

        #endregion Practise3
    }

    struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
