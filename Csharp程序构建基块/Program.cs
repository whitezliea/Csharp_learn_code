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
            string s1 = " x={0},y={1},z={2}. really?";
            object[] args = new object[3];
            args[0] = x;
            args[1] = y;
            args[2] = z;
            Console.WriteLine(s1, args);
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

            #region 方法重载
            OverloadingExample.UsageExample();
            #endregion

            #region 其他函数成员: 构造函数、属性、索引器、事件、运算符和终结器
            // 构造函数
            MyList<string> list1 = new();
            MyList<string> list2 = new(10);
            
            // 属性
            MyList<string> list3 = new();
            list3.Capacity = 100;
            int l1 = list3.Count;
            int l2 = list3.Capacity;

            // 索引器
            MyList<string> list4 = new();
            list4.Add("test1");
            list4.Add("test2");
            list4.Add("tets3");
            for (int i=0;i<list4.Count;i++)
            {
                string s2 = list4[i];
                list4[i] = s2.ToUpper();
                Console.WriteLine("索引器：" + s2 + " " + list4[i] ); 

            }

            // 事件管理
            EventExample.Usage();

            // 重载运算符
            MyList<int> a = new();
            a.Add(1);
            a.Add(2);
            MyList<int> b = new();
            b.Add(1);
            b.Add(2);
            Console.Write("a == b ? => ");
            Console.WriteLine(a == b);
            b.Add(3);
            Console.Write("a != b ? => ");
            Console.WriteLine(a != b);
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
        static void F() => Console.WriteLine("F()");
        static void F(object x) => Console.WriteLine("F(object)");
        static void F(int x) => Console.WriteLine("F(int)");
        static void F(double x) => Console.WriteLine("F(double)");
        static void F<T>(T x) => Console.WriteLine($"F<T>(T), T is {typeof(T)}");
        static void F(double x, double y)
        {
            Console.WriteLine("F(double,double"); 
        }

        public static void UsageExample()
        {
            F();
            F(1);
            F(1.0);
            F("ABC");
            F((double)1);
            F((object)1);
            F<int>(1);
            F(1, 1);
        }

    }


    #endregion

    #region 其他函数成员: 构造函数、属性、索引器、事件、运算符和终结器
    public class MyList<T>
    {
        const int DefaultCapacity = 4;

        T[] _items;
        int _count;

        public MyList(int capacity = DefaultCapacity)
        {
            _items = new T[capacity];
        }
        
        //public int Count => _count;
        public int Count
        {
            get { return _count; }
        }

        public int Capacity
        {
            // get 访问器读取该值。 set 访问器写入该值。
            get => _items.Length;
            set
            {
                if (value < _count)
                    value = _count;
                if (value != _items.Length)
                {
                    T[] newItems = new T[value];
                    Array.Copy(_items, 0, newItems, 0, _count);    // array copy
                    _items = newItems;
                }
            }
        }

        public T this[int index]
        {
            get => _items[index];
            set
            {
                _items[index] = value;
                OnChange();
            }
        }

        public void Add(T item)
        {
            if (_count == Capacity)
                Capacity = _count * 2;
            _items[_count] = item;
            _count++;
            OnChange(); // Changed 事件成员，指明已向列表添加了新项
        }

        protected virtual void OnChange() => // 检查事件是否是Null（既不含任何处理程序）
            Changed?.Invoke(this, EventArgs.Empty);

        public override bool Equals(object other) =>
            Equals(this, other as MyList<T>);


        static bool Equals(MyList<T>a ,MyList<T>b)
        {
            if (Object.ReferenceEquals(a,null))
                return Object.ReferenceEquals(b,null);
            if (Object.ReferenceEquals(b,null) || a._count != b._count)
                return false;
            for (int i = 0; i < a._count; i++)
            {
                if (!object.Equals(a._items[i], b._items[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public event EventHandler Changed;

        public static bool operator ==(MyList<T> a, MyList<T> b) =>
            Equals(a, b);

        public static bool operator !=(MyList<T>a, MyList<T> b) =>
            !Equals(a, b);

    }

    // 事件管理
    class EventExample
    {
        static int s_ChangeCount;
        static void ListChanged(object sender, EventArgs e)
        {
            s_ChangeCount++;
        }

        public static void Usage()
        {
            var list = new MyList<string>();
            list.Changed += new EventHandler(ListChanged);
            list.Add("Liz");
            list.Add("Martha");
            list.Add("Beth");
            Console.WriteLine("事件的个数："+s_ChangeCount);
        }
    }

    // 终结器 https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/classes-and-structs/finalizers

    #endregion

}
