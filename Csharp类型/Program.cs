using System;

namespace APP2
{
    class APP2
    {
        public static void Main()
        {
            #region 接口
            EditBox editbox = new EditBox();
            var control = editbox;
            IDataBound databound = editbox;
            #endregion

            #region 枚举
            var turnip = SomeRootVegetable.Trunip;
            var spring = Seasons.Spring;
            var startingOnEquinox = Seasons.Spring | Seasons.Autumn;
            var theYear = Seasons.All;
            //Console.WriteLine("hello APP2,{0}-,{1}-,{2}-,{3}", turnip,spring,startingOnEquinox,theYear);
            #endregion

            #region 元组
            /*
             * 元组提供简洁的语法来将多个数据元素分组成一个轻型数据结构。
             */
            (double Sum, int Count) t2 = (5.5, 3);
            Console.WriteLine($"Sum of {t2.Count} elements is {t2.Sum}");
            #endregion

            Console.WriteLine("hello APP2");


        }
    }

    #region 接口样例
    interface IControl
    {
        void Paint();
    }

    interface ITextBox : IControl
    {
        void SetText(string text);
    }

    interface IListBox : IControl
    {
        void SetItems(string[] items);
    }

    interface IComboBox : ITextBox, IListBox { }

    interface IDataBound
    {
        void Bind(int b);
    }

    public class EditBox : IControl, IDataBound
    {
        public void Paint() { }
        public void Bind(int b) { }
    }
    #endregion
    

    #region 枚举

    /*
     * [Flags]的微软解释是“指示可以将枚举作为位域（即一组标志）处理。”
     * 其实就是在编写枚举类型时，上面附上Flags特性后，
     * 用该枚举变量是既可以象整数一样进行按位的“|”或者按位的“&”操作了。
     */
    public enum SomeRootVegetable
    {
        HorseRadish,
        Radish,
        Trunip
    }

    [Flags]
    public enum Seasons
    {
        None = 0,
        Summer = 1,
        Autumn = 2,
        Winter = 4,
        Spring = 8,
        All = Summer | Autumn | Winter | Spring
    }

    #endregion


}