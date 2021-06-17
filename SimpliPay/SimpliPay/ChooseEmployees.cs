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
    public partial class ChooseEmployees : UserControl
    {
        //Creates empty references that will be used to store data passed on from other forms. 
        public Employee[] employee_list; //employee list
        private StartPayRoll parent_form; //parent form (StartPayRoll in this case)
        private FrmMain main_parent; //here to pass into finalizePayRoll
        private string pay_period; //the user's chosen pay period 
        private ChooseEmpObject[] matched_employees; //employees that have the same pay period. Their respective ChooseEmpObject objects will be stored in this array. 
        //^^made public because will be accessed in AddPayDialog
        public _Excel.Workbook wb; //will be used to store reference to COM object of database
        public _Excel.Worksheet ws; //same as above but for worksheet.
        //These are public because they will be accessed in FinalizePayRoll form. 

        public ChooseEmployees(StartPayRoll parent_form_passed, Employee[] employee_list_passed, string pay_prd_chosen, FrmMain form_main,
            _Excel.Worksheet ws_passed, _Excel.Workbook wb_passed)
        {
            main_parent = form_main; //coy of reference to main_parent form
            parent_form = parent_form_passed; //stores copy of parent form reference
            employee_list = employee_list_passed; //stores copy of employee_list reference
            pay_period = pay_prd_chosen; //stores copy of pay period reference. 
            ws = ws_passed; //stores reference to COM object of database
            wb = wb_passed; //same as above but for the workbook
            InitializeComponent();
        }

        private void ChooseEmployees_Load(object sender, EventArgs e) //event listener for when the user control loads.
        {
            Employee[] matched = new Employee[0]; //instantiates a 0 Length Employee object array. This is set to 0 in case there are no
            //Employees that have the same pay period. It saves me time because I don't have to write code to catch errors later on .

            for (int i = 0; i < employee_list.Length; i++) //for loop repeats once for each employee in employee_list
            {
                string emps_pay_prd = employee_list[i].pay_prd.ToLower(); //the ith employee's pay period is stored in this variable. Uses .Lower() to prevent any capitalization errors or misses. 
                if (emps_pay_prd == pay_period.ToLower()) //checks if employees pay period is equal to pay period employee chose. I don't need to worry about user's pay period being formatted differently
                    //because I already checked for that in InsertFilePrompt.
                {
                    Employee[] temp = new Employee[matched.Length]; //creates a placeholder array that is the same size as matched array.

                    for (int j = 0; j < matched.Length; j++) //for loop repeats once for each object in matched
                    {
                        temp[j] = matched[j]; //temporarily stores values in placeholder array. 
                    }

                    matched = new Employee[matched.Length + 1]; //reinstantiates the array but increases the length by 1

                    for (int j = 0; j < temp.Length; j++) //repeats once for each object in temp;
                    {
                        matched[j] = temp[j]; //moves the values from place holder array to resized matched array. 
                    }

                    matched[matched.Length - 1] = employee_list[i]; //stores this employee in the last index of the array. Final index will be empty since we increased size by 1. 
                }
            }

            matched_employees = new ChooseEmpObject[matched.Length]; //stores a new array in the previously declared reference
            //length of array is equal to number of employees with the same pay period

            for (int i = 0; i < matched.Length; i++) //for loop repeats once for each employee that has the same pay period
            {
                ChooseEmpObject matched_emp_object = new ChooseEmpObject(matched[i], this); //creates a new ChooseEmpObject object. Passes respective employee in constructor method.
                matched_employees[i] = matched_emp_object; //populates the previously instantiated array with the ChooseEmpObject.
                panelList.Controls.Add(matched_emp_object); //adds it to controls of panelList.
                matched_emp_object.Dock = DockStyle.Top; //Docks it to the object above it. 
                matched_employees[i].BringToFront(); //brings it to front. This also changes the order that the objects are initialized. The objects are the very front are initialized last.
                //As such, this object will dock to the object above it and not to the top of the panel. 

                string fname = matched[i].first_name.Trim().ToUpper(); //gets the first name of the ith employee and gets rid of leading and trailing white space characters.
                //Also makes it all uppercase letters.
                string mname = Convert.ToString(matched[i].middle_name); //gets the middle name of the ith employee.
                //Convert.ToString() is in case middle name returns null (since its not a mandatory field). Otherwise, it would throw an exception error. 
                string lname = matched[i].last_name.Trim(); //gets the last name of the ith employee and gets rid of leading and trailing white space characters.
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

                matched_employees[i].lblEmpName.Text = formatted_name; //sets the Text value of lblEmpName of respective employee's EmployeeListObject to their formatted name. 
                matched_employees[i].lblEarningType.Text = "Regular"; //sets the Text value of lblEarningType to type of earning. All earnings will regular at first. User may add other earnings using the "add" button
                matched_employees[i].txtPayRate.Text = matched[i].pay_rate.ToString(); //obtains the respective employee's pay rate and converts it to string. Makes it the Text value of txtPayRate

                if (matched[i].type.ToLower() == "salaried") //if the respective employee is salaried. 
                {
                    matched_employees[i].txtHours.Text = "Salaried"; //types salaried in the hours section because salaried employees don't have hours
                    matched_employees[i].txtHours.ReadOnly = true; //Makes it read only so the user can't edit it. 
                    matched_employees[i].txtTotal.Text = matched[i].pay_rate.ToString(); //since this is a salaried amount, they are most likely going to be paid their pay rate each payroll. 
                }
                else //if it's a wage-based employee
                {
                    matched_employees[i].txtHours.Text = "0"; //hours are initially set at 0.
                    matched_employees[i].txtTotal.Text = "0"; //total pay is also initially set at 0
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e) //event listener for if the back button is clicked. 
        {
            parent_form.Controls.Remove(this); //removes instance of THIS form from parent form's controls. 
            this.Dispose(); //clears it from memory
        }

        private void checkAll_CheckedChanged(object sender, EventArgs e) //event listener for if the check all box is clicked.
        {
            for (int i = 0; i < matched_employees.Length; i++) //loops one for each ChooseEmpObject object that is in matched_employees
            {
                if (checkAll.Checked == true) //if the check all box was clicked to be checked. .Checked checks if box is checked or not. false means its not. true means it is.
                {
                    matched_employees[i].checkEmp.Checked = true; //if it is checked, then the checkEmp value (the checkbox) of each ChooseEmpObject is checked.
                    //checkEmp has been made public which allows me to access it in this form. 
                }
                else if (checkAll.Checked == false) //if the check box has been clicked to be unchecked.
                {
                    matched_employees[i].checkEmp.Checked = false; //unchecks all employees
                }
            }
        }

        private void btnReview_Click(object sender, EventArgs e) //Event listener for if btnReview is clicked
        {
            FinalizePayRoll finalize_form = new FinalizePayRoll(parent_form, this, main_parent); //instantiates a new instance of FinalizePayRoll. Passes in reference to THIS instance as argument in constructor method
            this.Controls.Add(finalize_form); //adds the new form to this form's controls.
            finalize_form.BringToFront(); //Brings it to the very front so it looks its going to the next screen. This will keep THIS form still open in the back.
            //That ensures that fields are still filled incase the user presses the back button. Bringing it to the very front also ensures that finalize_form is not obscured by anything.
            finalize_form.Dock = DockStyle.Fill; //docks the control to all four edges of parent container.

            ChooseEmpObject[] to_finalize = new ChooseEmpObject[0];  //instantiates an array of ChooseEmpObject with a length of 0.
            //This array will store the ChooseEmpObjects that were checked off on the ChooseEmployees screen. 

            for (int i = 0; i < this.panelList.Controls.Count; i++) //loops through each control in panelList. panelList.Controls.Count returns the number of controls in panelList.
            {
                if (this.panelList.Controls[i] is ChooseEmpObject) //panelList.Controls[i] allows me to access ith Control of panelList.
                    //This line checks if that control is of ChooseEmpObject type. 
                {
                    ChooseEmpObject current = (ChooseEmpObject)this.panelList.Controls[i]; //If the control is of ChooseEmpObject type, it stores it in the this variable.
                    //I've casted it to (ChooseEmpObject) sice this.panelList.Controls[i] returns a Control type object. It needs to be casted back into a ChooseEmpObject.

                    if (current.checkEmp.Checked == true) //Checks if the chosen object's checkbox is ticked or not. If it is ticked, current.checkEmp.Checked would return true. 
                    {
                        FinalizeObj fin_emp_obj = new FinalizeObj(current.employee); //instantiates a new fin_emp_obj (finalize employee object) object.
                        //passes in employee from current into constructor method of FinalizeObj
                        finalize_form.panelList.Controls.Add(fin_emp_obj); //Adds it to controls of panelList on fnialize_form.
                        fin_emp_obj.BringToFront(); //Brings it to the very front. This ensures that objects behind it are loaded first. This will allow me to dock this object to the object above it.
                        //Otherwise, it would dock to the very top of the panel.
                        fin_emp_obj.Dock = DockStyle.Top; //Docks this to the object above it. 

                        //Populating fields of fin_emp_obj
                        fin_emp_obj.lblEmp.Text = current.lblEmpName.Text; //Employee's name is taken from ChooseEmpObject and transferred to fin_emp_obj.
                        fin_emp_obj.lblID.Text = current.employee.ID; //same as above but for employee's ID. current.employee.ID access the employee property that is stored in ChooseEmpObject.
                        //.ID allows me to access the propety of that Employee object stored in employee property. 
                        fin_emp_obj.lblEarnType.Text = current.lblEarningType.Text; //same as person's name but for their Earning Type.
                        fin_emp_obj.lblPayRate.Text = current.txtPayRate.Text; //same as above but for Pay Rate
                        fin_emp_obj.lblHours.Text = current.txtHours.Text; //same as above but for Hours worked.
                        fin_emp_obj.lblTotalPay.Text = current.txtTotal.Text; //same as above but for total pay. 
                    }
                }
            }
        }
    }
}
