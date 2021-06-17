using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _Excel = Microsoft.Office.Interop.Excel; //The variable Excel is now equal to Microsoft.Office.Interop.Excel. This makes it easier for me to use features from that libary as I won't have to type out the entire thing. 
using System.Runtime.InteropServices; //used for ReleaseCOMObject method

//Name: Muhammad Ali
//Date: April 22nd, 2021
//Assignment: Final Culminating Task 
//Purpose: This is a simplified payroll software. It will allow the user to perform payrolls and to automatically generate paystubs for employees being paid.

namespace SimpliPay
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        public Employee[] employee_list; //creates public property of this class that is going to be used to store reference to Employee object array.
        private string path; //same idea as above but for path of file user uploaded. 
        //These are public because they are going to be accessed by other forms. 
        public _Excel.Application excel; //Creates properties so COM objects can be accessed from anywhere within this class. The objects are created with the create_connection method and the references to the objects are stored in these variables. 
        public _Excel.Workbook wb; //same as above but for the Workbook
        public _Excel.Worksheet ws; //same as above but for the Worksheet

        private void create_connection(string path)
        {
            excel = new _Excel.Application(); //This line uses Excel Interop to create an Excel Application COM (Component Object Model) object. 
            ///In short, a COM object is a component that allows it to interact with other objects. It is a non-platform-specific object that can be used independently 
            ///of the language that it was programmed in. The COM components are usually registered with the system upon installation (such as how Excel Interop is automatically installed on machines that have Excel).
            ///In this case, even though Excel may not be based in C#, I can take advantage of Excel Interop to create a new Excel application named "excel".
            ///This application is not visible to the user as it is strictly present in the backend and is used for processing data. 
            wb = excel.Workbooks.Open(path); //This line creates an Excel Workbook COM object and uses it to open an existing Excel file in the backend. 
            //.Open() method takes the path of the Excel file to be opened and opens it in the background. In other words, the line above "opens" Excel itself and this line 
            //"opens" the Excel Workbook. The name of this Excel Workbook COM object is wb. 
            ws = wb.Sheets["Database"]; //Same idea as above except that it opens the first worksheet in the wb Workbook COM object. "Database" is used to 
            //access the worksheet using its name. I can also use non-zero based indices but the user may accidentally create a new worksheet which would present complications. 
        }

        private void close_excel_COM(_Excel.Application excel, _Excel.Workbook wb, _Excel.Worksheet ws) //The specifics of what a method is are discussed in the method below.
       //This method is responsible for releasing (closing) the COM objects that were created in the method below. 
        {
            try //I've put this in a try catch because if the form is closed before the COM objects are created, it throws an exception error.
            //The only time that would happen is if the user launches the program but closes it without uploading or creating a database. 
            {
                //COM CLEANUP - important as without this, the Excel application would still be running in the background and as a result, the user would not be able to edit the file they uploaded. 
                Marshal.ReleaseComObject(ws); //This method is used to release the COM object that's within the brackets. By releasing the COM object, it is essentially ending it and preventing it from running in the background after the program is closed. 
                                              //This line is used to release the Excel Worksheet. 

                wb.Close(); //This line simply closes the Workbook that is open in the background. 
                Marshal.ReleaseComObject(wb); //Same task as above except for the Workbook. 

                excel.Quit(); //This lines quit the Excel application and fully terminates the Excel task in the background. Without this, Excel would still be running in the background. 
                Marshal.ReleaseComObject(excel); //Same task as above except for Excel application. 
            }
            catch
            {
                //Do Nothing
            }
        }

        private void SimpliPay_Shown(object sender, EventArgs e) //Event listener for when SimpliPay form had loaded and is shown on screen
        {
            using (InsertFilePrompt InsertForm = new InsertFilePrompt())
            ///The "using" statement is a method that is used for waste management. Any object declared within the brackets (an instance of InsertFilePrompt in this case) 
            ///is destroyed, or disposed of, once the program exits the curly braces of this method. In this case, it will ensure that InsertForm is disposed of and is not taking
            ///extra memory. 
            {
                DialogResult result = InsertForm.ShowDialog(); //stores the value that is returned by InsertForm.ShowDialog(). This was further discussed in the add_file method
                //in the InsertFilePrompt class. 

                if (result == DialogResult.OK) //If value of result is DialogResult.OK. In this context, it will return OK if the user created a new database or pressed the continue button.
                {
                    employee_list = InsertForm.returned_emp_list; //creates a copy of the reference to Employee object array that is stored in the memory. 
                    path = InsertForm.returned_path; //Same idea as above
                    create_connection(path); //immediately creates a connection to ensure user does not edit or change the database file
                }
                else if (result == DialogResult.Cancel) //result will only equal cancel if the user decides to press x on the InsertForm form. 
                {
                    Close(); //It will close FrmMain as well. The user should not have access to the main program if they have not uploaded or created a new database. 
                }
            }
        }

        private void SimpliPay_FormClosed(object sender, FormClosedEventArgs e) //event listener for if form is closed
        {
            close_excel_COM(excel, wb, ws); //ensures that no COM object is left open. 
        }

        public EmployeeList EmployeeListForm; //reference object created so control can be referenced in other functions
        //Made public so it can be accessed by EmployeeList

        private void btnEmpList_Click(object sender, EventArgs e) //event listener for if "Employee List" button is pressed
        {
            if (panelMain.Controls.Contains(start_pay_roll)) //If panelMain already contains an instance of StartPayRoll
                //This if statement is necessary as it ensures that the EmployeeList control doesn't open behind the instance of StartPayRoll. 
            {
                panelMain.Controls.Remove(start_pay_roll);  //Removes the user control from the page.
                start_pay_roll.Dispose(); //clears it in the memory
            }
            else if (panelMain.Controls.Contains(EmployeeListForm)) //if there is already an instance of EmployeeList open.
            {
                return; //prematurely end the function. In extension, no new object is created. 
            }

            EmployeeList EmpListForm = new EmployeeList(this, employee_list, ws, wb); //this creates a new instance of the EmployeeList user control.
            ///Creating an instance doesn't display it. It simply creates it in the memory. I've also passed my employee list Employee[] array as my
            ///first argument since that is required by the constructor method of EmployeeList.
            EmployeeListForm = EmpListForm; //stores reference of EmployeeListForm
            panelMain.Controls.Add(EmpListForm); //this line adds the EmpListForm control to a panel named panelMain. A control is simply a reusable container that
            //allows the programmer to create functionality (i.e. textbox, buttom).
            EmpListForm.Dock = DockStyle.Fill; //This line docks the EmpListForm control to its parent container which is panelMain.
            ///Docking is the processing of pinning the edges of a container to its parent container. .Dock allows me to access the dock property of the given control.
            ///Dockstyle is similar to DialogResult. It has its own data types. In this case, DockStyle.Fill fills the available space in the parent container with the 
            ///given control. All four edges of the control are docked. 
        }

        private StartPayRoll start_pay_roll; //reference object created so control can be referenced in other functions

        private void btnPayRoll_Click(object sender, EventArgs e)
        {
            if (panelMain.Controls.Contains(EmployeeListForm)) //same idea as before but checks if an instance of EmployeeList exists this time
            {
                panelMain.Controls.Remove(EmployeeListForm); //if it does, remove it from panelMain's controls
                EmployeeListForm.Dispose(); //clear it in the memory
            }
            else if (panelMain.Controls.Contains(start_pay_roll)) //if an instance of StartPayRoll is already open
            {
                return; //end function prematurely
            }

            StartPayRoll start_pr = new StartPayRoll(this, employee_list, wb, ws); //same idea as above. Creates an instance of StartPayRoll.
            start_pay_roll = start_pr; //stores a copy of reference in a variable declared outside scope of function.
            panelMain.Controls.Add(start_pr); //Adds it to panelMain.Controls.
            start_pr.Dock = DockStyle.Fill; //Same function as before. 
        }
    }
}
    