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
    public partial class EmployeeList : UserControl
    {
        //PRIVATE BECAUSE THESE WON'T NEED TO ACCESSED BY ANY OTHER CLASS EXCEPT EMPLOYEE_LIST
        public Employee[] employee_list; //creates private property of this class that is going to be used to store reference to Employee object array.
        private _Excel.Worksheet ws;
        private _Excel.Workbook wb;
        private FrmMain parent;

        public EmployeeList(FrmMain parent_form, Employee[] employee_list_from_parent, _Excel.Worksheet ws_passed, _Excel.Workbook wb_passed) //this is the constructor method (the method that creates the class). 
            //Whenever this class is created, it will take an Employee[] array variable. 
            //it will also take the worksheet COM object reference and the workbook COM object
            //It also takes reference to parent form. 
        {
            ws = ws_passed; //copies reference
            wb = wb_passed; //copies reference
            parent = parent_form; //copies reference
            employee_list = employee_list_from_parent; //creates a copy of the reference that was passed on by the parent form before the user control is initialized
            InitializeComponent();
        }

        private void EmployeeList_Load(object sender, EventArgs e) //event listener for when the user control loads
        {
            EmployeeListObject[] employees = new EmployeeListObject[employee_list.Length]; //Instantiates an empty array of EmployeeListObject objects. 
            //The length of this array is the same as the number of employees there are in the database currently. 

            for (int i = 0; i < employees.Length; i++) //this for loop repeats once for each employee there is
                //It is going to be used to populate the "employees" array and create and populate EmployeeListObject to show to user.
                //Each EmployeeListObject is going to communicate its respective employee's most essential info
            {
                employees[i] = new EmployeeListObject(employee_list[i], this, i+3, ws, wb); //populates ith index of employee array with a new EmployeeListObject object. Also passes ith Employee class object into constructor method.
                //Also pass the instance of this control (using "this") as well as the row number of employee. 
                panelEmpList.Controls.Add(employees[i]); //adds the new object to the control of panelEmpList
                employees[i].Dock = DockStyle.Top; //docks the EmployeeListObject object to whatever control is above it.
                employees[i].BringToFront(); //This brings the created object to the very front of the control collection. As a result, its order o
                //initialization is also moved towards the end. This ensures that it docks to the heading panel, and not the container panel. 

                string fname = employee_list[i].first_name.Trim().ToUpper(); //gets the first name of the ith employee and gets rid of leading and trailing white space characters.
                //Also makes it all uppercase letters.
                string mname = Convert.ToString(employee_list[i].middle_name); //gets the middle name of the ith employee.
                //Convert.ToString() is in case middle name returns null (since its not a mandatory field). Otherwise, it would throw an exception error. 
                string lname = employee_list[i].last_name.Trim(); //gets the last name of the ith employee and gets rid of leading and trailing white space characters.
                //No need to make it all uppercase because the name will be displayed in full. 
                string formatted_name = "null"; //declares it a variable and assigns a placeholder string value. 

                if (mname == null || mname == string.Empty) //if middle name returns null.
                {
                    formatted_name = string.Format("{0}. {1}", fname[0], lname); //Uses placeholders to format the name into one cohesive string. {k} refers to kth variable after the initial string.
                    //fname[0] gives me the first inital of the employee's name. I'm using initals instead of full first names as full names might be too long. 
                }
                else //if there is a middle name
                {
                    formatted_name = string.Format("{0}. {1}. {2}", fname[0], (mname.Trim().ToUpper())[0], lname); //Sane idea as before, except with middle name. 
                }

                employees[i].lblEmpName.Text = formatted_name; //sets the Text value of lblEmpName of respective employee's EmployeeListObject to their formatted name. 
                employees[i].lblEmpID.Text = employee_list[i].ID; //Same as above but for employee ID and lblEmpID.
                employees[i].lblEmpType.Text = employee_list[i].type; //Same as above but for employee type and lblEmpType.
                employees[i].lblPayPeriod.Text = employee_list[i].pay_prd; //Same as above but for pay period and lblPayPeriod. 
                employees[i].lblPayRate.Text = string.Format("{0:C}", employee_list[i].pay_rate); //Same as above but for pay rate and lblPayRate. It uses string.format() to format
                //the value into a currency value. That's what {0:C} is for. 
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e) //event listener for when btnRefresh is clicked
        {
            parent.Controls.Remove(this); //removes it from this instance's controls. 
            this.Dispose(); //disposes it in the memory

            //Relaunches it, effectively refreshing. 
            EmployeeList EmpListForm = new EmployeeList(parent, parent.employee_list, ws, wb); //this creates a new instance of the EmployeeList user control.
            ///Creating an instance doesn't display it. It simply creates it in the memory. I've also passed my employee list Employee[] array as my
            ///first argument since that is required by the constructor method of EmployeeList. It's parent.employee_list because I want it to create a copy of the parent reference,
            ///and not a copy of a copy of a reference to employee_list in memory. 
            parent.panelMain.Controls.Add(EmpListForm); //this line adds the EmpListForm control to a panel named panelMain. A control is simply a reusable container that
            //allows the programmer to create functionality (i.e. textbox, buttom).
            EmpListForm.Dock = DockStyle.Fill; //This line docks the EmpListForm control to its parent container which is panelMain.
            ///Docking is the processing of pinning the edges of a container to its parent container. .Dock allows me to access the dock property of the given control.
            ///Dockstyle is similar to DialogResult. It has its own data types. In this case, DockStyle.Fill fills the available space in the parent container with the 
            ///given control. All four edges of the control are docked. 
            parent.EmployeeListForm = EmpListForm; //storing the reference in the parent form's property. Important, otherwise user won't able to switch to other pages. 
        }

        private void picAdd_Click(object sender, EventArgs e) //event listener for if add employee is clicked. 
        {
            CreateProfile create_profile = new CreateProfile(parent, ws, wb); //creates a new instance of create_profile with necessary arguments.
            this.Controls.Add(create_profile); //adds it to controls of this instance of EmployeeList
            create_profile.BringToFront(); //brings it to the very front
            create_profile.Dock = DockStyle.Fill; //docks it to all four edges of parent container.
        }
    }
}
