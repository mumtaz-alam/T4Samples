using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1;

namespace WindowsFormsApp11
{
    class Class1
    {
        public void test()
        {
            Car c = new Car();
            string s = "";
            s=s.EndsWith(",") ? s.TrimEnd(','):s;
        }
    }
}
