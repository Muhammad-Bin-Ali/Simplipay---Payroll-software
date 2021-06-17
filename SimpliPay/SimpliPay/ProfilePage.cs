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
    public partial class ProfilePage : UserControl
    {
        //Dicussed before but creates properties to store copies of references 
        private Employee employee; //the employee whose profile this is. 
        private EmployeeList ref_parent_form; //The EmployeeList instance this ProfilePage instance is docked to. 
        private int row; //row position of employee in excel
        private _Excel.Worksheet ws; //WS COM object
        private _Excel.Workbook wb; //wb COM object. 

        public ProfilePage(Employee employee_passed, EmployeeList form, int position,_Excel.Worksheet ws_passed, _Excel.Workbook wb_passed)
        //the constructor method has variables that are going to be passed into it. These arguments must be filled when an instance of this control is created.
        //Essentially, it helps communicate information between two forms. 
        {
            //Stores copies of passed arguments in properties declared before. This is important as the arguments would be lost as soon as the component is initialized
            employee = employee_passed;
            ref_parent_form = form;
            row = position;
            ws = ws_passed;
            wb = wb_passed;
            InitializeComponent();

            //fills out values in Windows Form Pie Chart. 
            //chartYTDEarnings refers to the name of the chart the data point is being added to.
            //.Series[<name>] represents the place where the data points will be stored. It can be accessed by going into designer, clicking on chart, and then clicking on Series.
            //.Points.AddXY simplys add a (x,y) coordinate point to graph. Since this is a pie chart, there is no x and y axis. As a result,
            //the x-value corresponds to a category and the y-value corresponds to its value. The program automatically calculates what each y-value is
            //in relation to the whole. 
            //employee.<value> allows me to access the employee's (whose profile page it is) financial values. 
            chartYTDEarnings.Series["YTD Earnings"].Points.AddXY("YTD REG.", employee.ytd_total_pay); //Adds an XY point for Year-to-date regular earnings.
            chartYTDEarnings.Series["YTD Earnings"].Points.AddXY("YTD VAC.", employee.ytd_vac_pay); //Adds an XY point for year-to-date vacation earnings.
            chartYTDEarnings.Series["YTD Earnings"].Points.AddXY("YTD OTHER", employee.ytd_other_pay); //Adds an XY point for Year-to-date other earnings.

            //Same idea as above but for chartYTDDed
            chartYTDDed.Series["YTD Deductions"].Points.AddXY("YTD CPP", employee.ytd_cpp); //Adds an XY point for YTD CPP deductions
            chartYTDDed.Series["YTD Deductions"].Points.AddXY("YTD EI", employee.ytd_EI); //Adds an XY point for YTD EI deductions 
            chartYTDDed.Series["YTD Deductions"].Points.AddXY("YTD TAX", employee.ytd_tax_ded); //Adds an XY point for YTD tax deductions
        }

        private void btnCancel_Click(object sender, EventArgs e) //event listener for it cancel button is pressed. 
        {
            ref_parent_form.Controls.Remove(this); //If it is pressed, this line simply remove an instance of "this" from the controls of its parent (ref_parent_form).
            //In extension, this visually removes the ProfilePage instance.
            this.Dispose(); //this line disposes of the instance in the memory. 
        }

        private void ProfilePage_Load(object sender, EventArgs e) //event listener for when profile page is finished loading. 
        {
            //Fills out the text boxes with the respective employee fields. 
            txtFName.Text = employee.first_name; //first name
            txtMName.Text = employee.middle_name; //middle name
            txtLName.Text = employee.last_name; //last name

            txtID.Text = employee.ID; //employee ID 
            txtSIN.Text = employee.SIN; //employee SIN
            txtEmail.Text = employee.email; //employee email
            txtPhone.Text = employee.phone_num; //employee phone number

            txtAdd1.Text = employee.address_1; //Address Line 1 
            txtAdd2.Text = employee.address_2; //Address Line 2

            txtCity.Text = employee.city; //City
            txtPostCode.Text= employee.postal_code; //Postal Code
            comboBoxProv.Text = employee.province; //Province

            dtDOB.Value = employee.birth_date; //fills out the DateTime object with the employee birth date DateTime value. 
            dtDOH.Value = employee.date_hire; //same as above but for date of hiring
            dtVacEntYear.Value = employee.vac_year; //same as above but for employee vacation entitlement year. 

            comboBoxPayPrd.Text = employee.pay_prd; //Pay Period
            txtPayRate.Text = employee.pay_rate.ToString(); //The employee's pay rate. Since it's stored as a double value, it needs to be converted to string. 
            comboBoxType.Text = employee.type; //employee type

            txtVacPayPerc.Text = employee.vac_pay_perc.ToString(); //Same idea as pay rate but for vacation pay percentage

            txtCC.Text = employee.cc;

            //YEAR-TO-DATE VALUES
            lblYTDTotalMoney.Text = employee.ytd_total_pay.ToString(); //YTD total regular earnings
            lblYTDVacMoney.Text = employee.ytd_vac_pay.ToString(); //YTD total vacation earnings.
            lblYTDOtherMoney.Text = employee.ytd_other_pay.ToString(); //YTD total other earnings
            lblYTDCPPMoney.Text = employee.ytd_cpp.ToString(); //YTD total CPP deductions
            lblYTDEIMoney.Text = employee.ytd_EI.ToString(); //YTD total EI deductions
            lblYTDTaxMoney.Text = employee.ytd_tax_ded.ToString(); //YTD total tax deductions. 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Checking if all the required fields are filled.
            //.Trim() is used to get rid of leading and trailing white space characters and string.Empty => empty string but not null. 
            if (txtFName.Text.Trim() == string.Empty || txtLName.Text.Trim() == string.Empty || txtSIN.Text.Trim() == string.Empty || txtID.Text.Trim() == string.Empty ||
                txtEmail.Text.Trim() == string.Empty || txtAdd1.Text.Trim() == string.Empty || txtCity.Text.Trim() == string.Empty || txtPostCode.Text.Trim() == string.Empty ||
                txtPayRate.Text.Trim() == string.Empty || txtVacPayPerc.Text.Trim() == string.Empty || comboBoxPayPrd.Text == string.Empty ||
                comboBoxProv.Text == string.Empty || comboBoxType.Text == string.Empty || txtCC.Text == string.Empty)
            {
                MessageBox.Show("Please ensure that all required fields are filled."); //Tells user to fill required fields.
                return; //prematurely ends the function
            }

            //CHECKING USER HAS NOT ENTERED NON-DIGIT CHARACTERS IN DOUBLE TYPE TEXTBOXES
            double pay_rate; //creates an out variable for pay_rate. Will be used for try parse. 
            double vac_pay_perc; //same as above but for vacation pay percentage.

            bool check = double.TryParse(txtPayRate.Text, out pay_rate); //tries parsing what the user has entered in the pay rate textbox into a double value.
            bool check_2 = double.TryParse(txtVacPayPerc.Text, out vac_pay_perc); //Same idea as above
            //This is done to ensure user has not entered a non-digit symbol in the textbox. 

            if (check == false && check_2 == false) //if both failed to parse.
            {
                MessageBox.Show("Please ensure there are no non-digit characters in the vacation pay percentage field and in the pay rate field."); //informs user
                return;//prematurely ends function
            }
            else if (check_2 == false) //if only vac_pay_perc failed to parse
            {
                MessageBox.Show("Please ensure there are no non-digit characters in the vacation pay percentage field."); //informs user
                return; //prematurely ends function
            }
            else if (check == false) //if only pay_rate failed to parse
            {
                MessageBox.Show("Please ensure there are no non-digit characters in the pay rate field."); //informs user
                return; //prematurely ends function
            }

            string claim_code = txtCC.Text;

            if (claim_code != "0" && claim_code != "1" && claim_code != "2" && claim_code != "3" && claim_code != "4" && claim_code != "5" &&
                claim_code != "6" && claim_code != "7" && claim_code != "8" && claim_code != "9" && claim_code != "10") //checks if the entered tax code is valid.
                                                                                                                        //In order for it to be valid, it must be between 0 and 10 (inclusive). If it's not equal to ALL of the values above, then the conditional returns true
            {
                MessageBox.Show("Please enter a valid tax claim code."); //tells user that their tax code is not active.
                return; //prematurely ends function
            }

            //UPDATING VALUES IN EXCEL
            //the function of ws.Cells[][] has been discussed many times before. This time, it's being used to write data to Excel rather than read it. 
            //row is the row that was passed into the constructor method. It is employee's vertical position in Excel. 
            ws.Cells[2][row].Value = string.Format(txtFName.Text + " " + txtMName.Text + " " + txtLName.Text); //formats the first, middle, and last name into one cohesive string => Employee Name
            //dont need to worry about any of them being null since textboxes always return a non-null value. 
            ws.Cells[4][row].Value = txtFName.Text; //first name being written into its respective cell.
            ws.Cells[5][row].Value = txtMName.Text; //same as above but for middle_name
            ws.Cells[6][row].Value = txtLName.Text; //same as above but for last_name

            ws.Cells[3][row].Value = txtID.Text; //same as above but for employee ID. 
            ws.Cells[7][row].Value = txtSIN.Text;  //Same idea as employee ID. 
            ws.Cells[8][row].Value = txtEmail.Text; //same as last_name but for email
            ws.Cells[11][row].Value = txtPhone.Text; //same as last_name but for phone number

            ws.Cells[9][row].Value = string.Format("'" + dtDOH.Value.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.GetCultureInfo("en-US"))); //discussed this before in InsertFilePrompt
            //Essentially, datetime object is converted to string and stored in Excel cell. Apostrophe is being added to prevent is from becoming date formatted in Excel.
            //CultureInfo ensures it's formatted with slashes (/) and not hyphens (-).
            ws.Cells[10][row].Value = string.Format("'" + dtDOB.Value.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.GetCultureInfo("en-US")));
            //Same as above but for date of birth. 

            ws.Cells[12][row].Value = txtAdd1.Text; //Same idea as last_name but for address line 1
            ws.Cells[13][row].Value = txtAdd2.Text; //Same as above but for address line 2
            ws.Cells[14][row].Value = txtCity.Text; //Same as above but for city
            ws.Cells[15][row].Value = txtPostCode.Text; //Same as above but for postal_code
            ws.Cells[16][row].Value = comboBoxProv.Text; //same as employee type but for province

            ws.Cells[17][row].Value = pay_rate; //pay_rate has already been successfully parsed so all I need to do is store it. No need to convert to text.                                  
            ws.Cells[18][row].Value = comboBoxPayPrd.Text; //same as employee type but for pay period 

            ws.Cells[19][row].Value = comboBoxType.Text; //same as above but for employee type. 

            ws.Cells[20][row].Value = string.Format("'" + dtVacEntYear.Value.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.GetCultureInfo("en-US")));// same as DOB or DOH
            ws.Cells[21][row].Value = vac_pay_perc; //Same idea as pay rate but for vacation pay percentage (will be stored in decimal form)
            ws.Cells[28][row].Value = txtCC.Text;

            wb.Save(); //same function as ctrl+s in Excel

            //UPDATING EMPLOYEE CLASS OBJECT IN PROGRAM DATA
            //all of this has been explained before. It's simply updating the Employee's object properties.
            employee.first_name = txtFName.Text; //first name
            employee.middle_name = txtMName.Text; //middle name
            employee.last_name = txtLName.Text; //last  name

            employee.ID = txtID.Text; //Employee ID
            employee.SIN = txtSIN.Text; //Employee SIN
            employee.email = txtEmail.Text; //email
            employee.phone_num = txtPhone.Text; //phone number

            employee.birth_date = dtDOB.Value; //date of birth
            employee.date_hire = dtDOH.Value; //date of hire

            employee.address_1 = txtAdd1.Text; //address line 1
            employee.address_2 = txtAdd2.Text; //address line 2
            employee.city = txtCity.Text;//city
            employee.postal_code = txtPostCode.Text; //postal code
            employee.province = comboBoxProv.Text; //province

            employee.pay_rate = pay_rate; //pay rate
            employee.pay_prd = comboBoxPayPrd.Text; //pay period

            employee.type = comboBoxType.Text; //employee type

            employee.vac_year = dtVacEntYear.Value;
            employee.vac_pay_perc = vac_pay_perc;

            employee.cc = txtCC.Text;

            ref_parent_form.Controls.Remove(this); //Once everything has been processed, this ProfilePage instance is needed no longer. It is removed from parent's controls. 
            this.Dispose(); //This instance is disposed of in the memory. 
        }

        private void txtSIN_KeyPress(object sender, KeyPressEventArgs e) //event listener for when key is pressed in txtSIN
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            //e.KeyChar the character on the key that is being pressed. All key argument are pressed into a variable "e" (check function's 2nd argument above),
            //and e.KeyChar allows me to access the character of the key that was pressed. char.IsControl checks if the key pressed is CTRL and char.IsDigit checks if
            //the key pressed is a digit key. The ! at the beginning of each conditional mean "not". So in words, the whole conditional means =>
            //if the key pressed is NOT control and the key being pressed is NOT a digit key.
            //I will discuss below why we are checking for if non-digit keys are pressed rather than if digit keys are pressed. 
            {
                if (e.KeyChar == '.') //This line simply checks if e.KeyChar returns "."
                {
                    e.Handled = false; //e.Handled essentially tells the program if the user is handling that keystroke or if the program should.
                    //e.Handled = true tells the program that the user is handling the keystroke, and as such, the program does not need to handle it.
                    //This in extension means that the program does not input the given key. 
                    //In this case, if the key pressed is ".", then we DO want to handle it because we want to allow the user to enter double values.
                    //e.Handled = false tells program that it should handle the key press. 
                }
                else //if its not a ".". If its not ".", then it must be a character that's not ctrl or a digit. 
                {
                    e.Handled = true; //In that case, we don't want the program outputting that so we tell it that we are handling it. 
                }
            }
        }

        private void txtCC_KeyPress(object sender, KeyPressEventArgs e) //event listener for when key is pressed in txtCC
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            //e.KeyChar the character on the key that is being pressed. All key argument are pressed into a variable "e" (check function's 2nd argument above),
            //and e.KeyChar allows me to access the character of the key that was pressed. char.IsControl checks if the key pressed is CTRL and char.IsDigit checks if
            //the key pressed is a digit key. The ! at the beginning of each conditional mean "not". So in words, the whole conditional means =>
            //if the key pressed is NOT control and the key being pressed is NOT a digit key.
            //I will discuss below why we are checking for if non-digit keys are pressed rather than if digit keys are pressed. 
            {
                if (e.KeyChar == '.') //This line simply checks if e.KeyChar returns "."
                {
                    e.Handled = false; //e.Handled essentially tells the program if the user is handling that keystroke or if the program should.
                    //e.Handled = true tells the program that the user is handling the keystroke, and as such, the program does not need to handle it.
                    //This in extension means that the program does not input the given key. 
                    //In this case, if the key pressed is ".", then we DO want to handle it because we want to allow the user to enter double values.
                    //e.Handled = false tells program that it should handle the key press. 
                }
                else //if its not a ".". If its not ".", then it must be a character that's not ctrl or a digit. 
                {
                    e.Handled = true; //In that case, we don't want the program outputting that so we tell it that we are handling it. 
                }
            }
        }

        private void txtPayRate_KeyPress(object sender, KeyPressEventArgs e) //event listener for when key is pressed in txtPayRate
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            //e.KeyChar the character on the key that is being pressed. All key argument are pressed into a variable "e" (check function's 2nd argument above),
            //and e.KeyChar allows me to access the character of the key that was pressed. char.IsControl checks if the key pressed is CTRL and char.IsDigit checks if
            //the key pressed is a digit key. The ! at the beginning of each conditional mean "not". So in words, the whole conditional means =>
            //if the key pressed is NOT control and the key being pressed is NOT a digit key.
            //I will discuss below why we are checking for if non-digit keys are pressed rather than if digit keys are pressed. 
            {
                if (e.KeyChar == '.') //This line simply checks if e.KeyChar returns "."
                {
                    e.Handled = false; //e.Handled essentially tells the program if the user is handling that keystroke or if the program should.
                    //e.Handled = true tells the program that the user is handling the keystroke, and as such, the program does not need to handle it.
                    //This in extension means that the program does not input the given key. 
                    //In this case, if the key pressed is ".", then we DO want to handle it because we want to allow the user to enter double values.
                    //e.Handled = false tells program that it should handle the key press. 
                }
                else //if its not a ".". If its not ".", then it must be a character that's not ctrl or a digit. 
                {
                    e.Handled = true; //In that case, we don't want the program outputting that so we tell it that we are handling it. 
                }
            }
        }

        private void txtVacPayPerc_KeyPress(object sender, KeyPressEventArgs e) //event listener for when key is pressed in txtVacPayPerc
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            //e.KeyChar the character on the key that is being pressed. All key argument are pressed into a variable "e" (check function's 2nd argument above),
            //and e.KeyChar allows me to access the character of the key that was pressed. char.IsControl checks if the key pressed is CTRL and char.IsDigit checks if
            //the key pressed is a digit key. The ! at the beginning of each conditional mean "not". So in words, the whole conditional means =>
            //if the key pressed is NOT control and the key being pressed is NOT a digit key.
            //I will discuss below why we are checking for if non-digit keys are pressed rather than if digit keys are pressed. 
            {
                if (e.KeyChar == '.') //This line simply checks if e.KeyChar returns "."
                {
                    e.Handled = false; //e.Handled essentially tells the program if the user is handling that keystroke or if the program should.
                    //e.Handled = true tells the program that the user is handling the keystroke, and as such, the program does not need to handle it.
                    //This in extension means that the program does not input the given key. 
                    //In this case, if the key pressed is ".", then we DO want to handle it because we want to allow the user to enter double values.
                    //e.Handled = false tells program that it should handle the key press. 
                }
                else //if its not a ".". If its not ".", then it must be a character that's not ctrl or a digit. 
                {
                    e.Handled = true; //In that case, we don't want the program outputting that so we tell it that we are handling it. 
                }
            }
        }
    }
}
