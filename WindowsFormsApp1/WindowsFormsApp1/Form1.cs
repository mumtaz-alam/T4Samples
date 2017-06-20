using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Type t = typeof(Car);
            GetParametersInfo(t);

        }

        public T GetDefault<T>(T tob)
        {
            return default(T);
        }
        public void GetParametersInfo(Type t)
        {
            richTextBox1.Text = string.Format("***** GetParametersInfo *****");
            MethodInfo[] mi = t.GetMethods();
            foreach (MethodInfo m in mi)
            {
                string mname = m.Name;
                // Get return value. 
                string retVal = m.ReturnType.FullName;
                StringBuilder paramInfo = new StringBuilder();
                paramInfo.Append("(");

                // Get params. 
                foreach (ParameterInfo pi in m.GetParameters())
                {
                    Type tp = pi.GetType();
                    paramInfo.Append(string.Format("{0} {1} ", (pi.ParameterType.Default()==null)?"null": pi.ParameterType.Default().ToString(), pi.Name));
                }
                paramInfo.Append(")");

                // Now display the basic method sig. 
                richTextBox1.Text = richTextBox1.Text + string.Format("\n->{0} {1} {2}", retVal, m.Name, paramInfo);
            }
            //Console.WriteLine("");
            //Console.ReadLine();
        }
    }
    public class Car
    {
        //public variables 
        public string Color;

        //private variables 
        //String licensePlate; // e.g. "Californi 111 222" 
        //double maxSpeed;     // in kilometers per hour 
        //int startMiles; // Stating odometer reading 
        //int endMiles; // Ending odometer reading 
        //double gallons; // Gallons of gas used between the readings  

        //private vaiables 
        private int _speed;

        //Speed - read-only property to return the speed 
        public int Speed
        {
            get { return _speed; }
        }

        //Accelerate - add mph to the speed 
        public void Accelerate(int accelerateBy)
        {
            //Adjust the speed 
            _speed += accelerateBy;
        }

        //IsMoving - is the car moving? 
        public bool IsMoving()
        {
            //Is the car's speed zero? 
            if (Speed == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Constructor 
        public Car()
        {
            //Set the default values 
            Color = "White";
            _speed = 0;
        }

        //Over loaded constructor 
        public Car(string color, int speed)
        {
            Color = color;
            _speed = speed;
        }

        //methods 
        public double calculateMPG(int startMiles, int endMiles, double gallons)
        {
            return (endMiles - startMiles) / gallons;
        }
    }
    public static class DefaultExtention{

        public static T GetDefaultValue<T>()
        {
            // We want an Func<T> which returns the default.
            // Create that expression here.
            Expression<Func<T>> e = Expression.Lambda<Func<T>>(
                // The default value, always get what the *code* tells us.
                Expression.Default(typeof(T))
            );

            // Compile and return the value.
            return e.Compile()();
        }

        public static object GetDefaultValue(this Type type)
        {
            // Validate parameters.
            if (type == null) throw new ArgumentNullException("type");

            // We want an Func<object> which returns the default.
            // Create that expression here.
            Expression<Func<object>> e = Expression.Lambda<Func<object>>(
                // Have to convert to object.
                Expression.Convert(
                    // The default value, always get what the *code* tells us.
                    Expression.Default(type), typeof(object)
                )
            );

            // Compile and return the value.
            return e.Compile()();
        }
        public static object Default(this Type type)
        {
            object output = null;

            if (type.IsValueType)
            {
                output = Activator.CreateInstance(type);
            }

            return output;
        }
        public static object GetDefault(this Type type)
        {
            return type.IsValueType ? (!type.IsGenericType ? Activator.CreateInstance(type) : type.GenericTypeArguments[0].GetDefault()) : null;
        }

    }
}
