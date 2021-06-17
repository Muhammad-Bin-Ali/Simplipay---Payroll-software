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
    public partial class EmployeeListObject : UserControl
    {
        //Private properties of class are being declared to store copies of references. Otherwise, I won't be able to edit them in instances of this form/user control. 
        private Employee employee; //the employee this EmployeeListObject corresponds to
        private EmployeeList ref_parent_form; //reference to parent form / control
        private int row; //row position of employee in Excel
        private _Excel.Worksheet ws; //WS COM object ref
        private _Excel.Workbook wb; //WB COM object ref

        public EmployeeListObject(Employee employee_passed, EmployeeList form, int position, _Excel.Worksheet ws_passed, _Excel.Workbook wb_passed)
            //the constructor method has variables that are going to be passed into it. These arguments must be filled when an instance of this control is created.
            //Essentially, it helps communicate information between two forms.
        {
            //Stores copies of passed arguments in properties declared before. This is important as the arguments would be lost as soon as the component is initialized. 
            ws = ws_passed;
            employee = employee_passed;
            ref_parent_form = form;
            row = position;
            wb = wb_passed;
            InitializeComponent();
        }

        private void btnEdit_Click(object sender, EventArgs e) //Event listener for if Edit button is click.
        {
            //Discussed in FrmMain before.
            //Whenever edit is pressed, it creates an instance of ProfilePage ON TOP of the EmployeeList instance. 
            ProfilePage employee_profile = new ProfilePage(employee, ref_parent_form, row, ws, wb); //creates instance of ProfilePage object and passes the necessary arguments for data communication.
            ref_parent_form.Controls.Add(employee_profile); //adds it to the control of the parent of EmployeeListObject. In this context, the parent will always be an instance of EmployeeList. 
            employee_profile.BringToFront(); //Brings the ProfilePage instance to front so it is not obscured by the EmployeeList instance itself. 
            employee_profile.Dock = DockStyle.Fill; //Docks the ProfilePage instance on all four corners to parent container.
        }
    }
}
