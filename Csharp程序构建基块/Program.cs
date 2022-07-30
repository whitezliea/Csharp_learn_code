using System;

namespace APP3
{
    class APP3
    {
        public static void Main()
        {
            #region 引用参数 ref
            int i1 = 1, i2 = 10;
            Swap(ref i1, ref i2);
            Console.WriteLine($"{i1}, {i2}");
            #endregion

            #region 输出参数 out
            Dvivide(10, 3, out int res, out int rem);
            Console.WriteLine($"res = {res},rem = {rem}");
            #endregion

            #region 参数数组 params
            int x = 3, y = 4, z = 5;
            string s = " x={0},y={1},z={2}. really?";
            object[] args = new object[3];
            args[0] = x;
            args[1] = y;
            args[2] = z;
            Console.WriteLine(s, args);
            #endregion

            #region 静态实例与方法
            Entity.SetNextSerialNo(10000);
            Entity e1 = new();
            Entity e2 = new();
            Console.WriteLine(e1.GetSerialNo());
            Console.WriteLine(e2.GetSerialNo());
            Console.WriteLine(Entity.GetNextSerialNo());
            #endregion

            #region 虚方法、重写方法和抽象方法
            Expression e = new Operation(new VariableReference("x"),
                '*',
                new Operation(new VariableReference("y"),
                '+',
                new Constant(2)));

            Dictionary<string, object> vars = new();
            vars["x"] = 3;
            vars["y"] = 5;
            Console.WriteLine("虚方法、重写方法和抽象方法 :" + e.Evaluate(vars));
            vars["x"] = 1.5;
            vars["y"] = 9;
            Console.WriteLine("虚方法、重写方法和抽象方法 :" + e.Evaluate(vars));

            #endregion

            Console.WriteLine("hello APP3!");



        }

        public static void Swap(ref int x, ref int y)
        {
            int temp = x;
            x = y;
            y = temp;
        }

        public static void Dvivide(int x, int y, out int reslut, out int remainder)
        {
            reslut = x / y;
            remainder = x % y;
        }

    }


    #region 静态和实例方法
    class Entity
    {
        static int s_nextSerialNo;
        int _serialNo;

        public Entity()
        {
            _serialNo = s_nextSerialNo++;
        }

        public int GetSerialNo()
        {
            return _serialNo;
        }

        public static int GetNextSerialNo()
        {
            return s_nextSerialNo;
        }

        public static void SetNextSerialNo(int value)
        {
            s_nextSerialNo = value;
        }

    }
    #endregion


    #region 虚方法、重写方法和抽象方法
    // 抽象类
    public abstract class Expression
    {
        public abstract double Evaluate(Dictionary<string, object> vars);  //字典
    }

    public class Constant : Expression
    {
        double _value;
        public Constant(double value)
        {
            _value = value;
        }

        public override double Evaluate(Dictionary<string, object> vars) //覆盖
        {
            return _value;
        }
    }

    public class VariableReference : Expression
    {
        string _name;

        public VariableReference(string name)
        {
            _name = name;
        }

        public override double Evaluate(Dictionary<string, object> vars)
        {
            object value = vars[_name] ?? throw new Exception($"unkown variable: {_name}");
            return Convert.ToDouble(value);
        }

    }
    public class Operation : Expression
    {
        Expression _left;
        char _op;
        Expression _right;

        public Operation(Expression left, char op, Expression right)
        {
            _left = left;
            _op = op;
            _right = right;
        }

        public override double Evaluate(Dictionary<string, object> vars)
        {
            double x = _left.Evaluate(vars);
            double y = _right.Evaluate(vars);
            switch (_op)
            {
                case '+': return x + y;
                case '-': return x - y;
                case '*': return x * y;
                case '/': return x / y;

                default: throw new Exception("Unknown operator");
            }

        }
    }




    #endregion

    #region 方法重载
    class OverloadingExample
    {

    }

    #endregion

}
