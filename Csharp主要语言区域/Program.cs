using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace APP4
{
    class APP4
    {
        #region async/await
        public static async Task<int> RetrieveDocsHomePage()
        {
            var client = new HttpClient();
            byte[] content = await client.GetByteArrayAsync("https://docs.microsoft.com/zh-cn/dotnet/csharp/tour-of-csharp/features/");

            Console.WriteLine($"{nameof(RetrieveDocsHomePage)}: Finished downloading. " + content.Length);
            return content.Length;
        }
        #endregion

        public static void display_array(double[] d)
        {
            Console.Write($"{nameof(d)}");
            foreach (var v in d)
            {
                Console.Write(" " + v);
            }
            Console.WriteLine();
        }

        public static void Main()
        {
            #region 数组
            int[] a = new int[3];
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = (i + 1) * (i + 2);
            }
            for (int j = 0; j < a.Length; j++)
            {
                Console.WriteLine(a[j]);
            }

            int[] a1 = new int[3]; // 一维数组
            int[,] a2 = new int[3, 5];  // 二维数组
            int[,,] a3 = new int[3, 4, 5]; // 三维数组

            // 交错数组
            int[][] a4 = new int[3][];
            a4[0] = new int[10];
            a4[1] = new int[5];
            a4[2] = new int[20];

            int[] t = { 9, 8, 7 };
            int[] a5 = t;
            foreach (int item in a5)
            {
                Console.WriteLine("foreach: " + item);
            }

            #endregion

            #region 字符串内插
            Console.WriteLine($"The low and high temperature on {a[0]}");
            Console.WriteLine($"    was {a[1]} and {a[2]}.");

            #endregion

            #region 委托和lambda
            double[] d = { 0.0, 0.5, 1.0 };
            double[] squares = DelegateExample.Apply(d, (x) => x * x); // lambda表达式与delegate
            display_array(squares);
            double[] sines = DelegateExample.Apply(d, Math.Sin);    // 系统函数与delegate
            display_array(sines);
            Multiplier m = new(100.0);
            double[] doubles = DelegateExample.Apply(d, m.Multiply);    // 类函数与delegate
            display_array(doubles);

            // 注意： 引用的方法必须具有与委托相同的参数和返回类型。
            #endregion

            #region async/await
            _ = RetrieveDocsHomePage();
            #endregion
            
            Console.WriteLine("hello APP4");
        }



    }

    #region 委托和lambda
    // 委托类型表示对具有特定参数列表和返回类型的方法的引用。
    // 通过委托，可以将函数视为可分配给变量并可作为参数传递的实体。类似于函数指针
    delegate double Function(double x);

    public class Multiplier
    {
        double _factor;
        public Multiplier(double factor) => _factor = factor;
        public double Multiply(double x) => x * _factor;
    }

    class DelegateExample
    {
        public static double[] Apply(double[] a, Function f)
        {
            var result = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                result[i] = f(a[i]);
            }
            return result;
        }
    }

    #endregion



}