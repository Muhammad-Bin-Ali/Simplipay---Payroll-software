using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpliPay
{
    public partial class FinalizeObj : UserControl
    {
        public Employee employee; //public property to store employee associated with this object. It will be accessed in FinalizePayRoll and AddPayDialog

        public FinalizeObj(Employee employee_passed)
        {
            employee = employee_passed; //creates copy of reference of employee passed in constructor method
            InitializeComponent();
        }
    }
}
