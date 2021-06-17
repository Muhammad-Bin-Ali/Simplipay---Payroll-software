using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _Excel = Microsoft.Office.Interop.Excel; //The variable Excel is now equal to Microsoft.Office.Interop.Excel. This makes it easier for me to use features from that libary as I won't have to type out the entire thing. 


namespace SimpliPay
{
    public partial class StartPayRoll : UserControl
    {
        private FrmMain parent_form; //creates empty reference for instance of FrmMain
        private Employee[] employee_list; //creates empty reference for Employee object array
        private _Excel.Workbook wb;
        private _Excel.Worksheet ws; 

        public StartPayRoll(FrmMain parent_form_passed, Employee[] employee_list_passed, _Excel.Workbook wb_passed, _Excel.Worksheet ws_passed) //constructor method has arguments passed into it for data communication
            //The parent form and the employee list are passed
        {
            parent_form = parent_form_passed; //copy of parent form reference 
            wb = wb_passed;
            ws = ws_passed;
            employee_list = employee_list_passed; //copy of employee list
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)  //event listener for if btnNext is clicked
        {
            string pay_period = comboBoxPayPrd.Text; //retrieves the pay period that the user has entered
            DateTime from = calFROM.SelectionRange.Start; //retrieves the FROM date the user has entered
            DateTime to = calTO.SelectionRange.Start;  //retrieves the TO date the user has entered. 

            ChooseEmployees choosing_emp_form = new ChooseEmployees(this, employee_list, pay_period, parent_form, ws, wb); //instantiates a ChooseEmployees object with the 
            //right arguments. An instance of this form, the employee_list, and the chosen pay_period.
            this.Controls.Add(choosing_emp_form); //adds the newly created control to controls of this user control.
            choosing_emp_form.BringToFront(); //brings it to front so its not obscured
            choosing_emp_form.Dock = DockStyle.Fill; //Docks it to all 4 edges of parent container.
        }
    }
}
