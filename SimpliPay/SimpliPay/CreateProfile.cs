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

//THIS USER CONTROL IS SIMILAR TO PROFILEPAGE. I won't be elaborating on code as that's already been done in ProfilePage.cs
namespace SimpliPay
{
    public partial class CreateProfile : UserControl
    {
        //Discussed before but creates properties to store copies of references
        private FrmMain ref_parent_form; //form that contains the parent reference t employee_list. In this case, that's frmmain.
        private _Excel.Worksheet ws; //reference for COM object to employee database
        private _Excel.Workbook wb; //same as above but for workbook


        public CreateProfile(FrmMain form, _Excel.Worksheet ws_passed, _Excel.Workbook wb_passed)
        {
            //the constructor method has variables that are going to be passed into it. These arguments must be filled when an instance of this control is created.
            //Essentially, it helps communicate information between two forms.
            //Stores copies of passed arguments in properties declared before. This is important as the arguments would be lost as soon as the component is initialized
            ref_parent_form = form;
            ws = ws_passed;
            wb = wb_passed;
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e) //Event listener for if cancel button is pressed. 
        {
            ref_parent_form.Controls.Remove(this); //removes instance of this control from parent's controls.
            this.Dispose(); //disposed in memory
        }

        private void btnSave_Click(object sender, EventArgs e) //event listener for save button.
        {
            Employee employee = new Employee(); //instantiates an empty Employee object. 

            //checking if the required fields are filled.
            if (txtFName.Text.Trim() == string.Empty || txtLName.Text.Trim() == string.Empty || txtSIN.Text.Trim() == string.Empty || txtID.Text.Trim() == string.Empty ||
                txtEmail.Text.Trim() == string.Empty || txtAdd1.Text.Trim() == string.Empty || txtCity.Text.Trim() == string.Empty || txtPostCode.Text.Trim() == string.Empty ||
                txtPayRate.Text.Trim() == string.Empty || txtVacPayPerc.Text.Trim() == string.Empty || comboBoxPayPrd.Text == string.Empty ||
                comboBoxProv.Text == string.Empty || comboBoxType.Text == string.Empty || txtCC.Text == string.Empty) //checks if any of the required fields return string.Empty
            {
                MessageBox.Show("Please ensure that all required fields are filled."); //informing user they're not
                return; //prematurely ending function
            }

            //CHECKING IF NON-DIGIT CHARACTERS HAVE BEEN ENTERED INTO TEXTBOX
            double pay_rate; //out variable for pay_rate TryParse
            double vac_pay_perc; //out variable for vac_pay_perc

            bool check = double.TryParse(txtPayRate.Text, out pay_rate); //checking if user's input parses
            bool check_2 = double.TryParse(txtVacPayPerc.Text, out vac_pay_perc); //same as above

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

            //Filling out the Employee object properties
            employee.first_name = txtFName.Text; //first name
            employee.middle_name = txtMName.Text; //middle name
            employee.last_name = txtLName.Text; //last name

            employee.ID = txtID.Text; //ID
            employee.SIN = txtSIN.Text; //SIN
            employee.email = txtEmail.Text; //email 
            employee.phone_num = txtPhone.Text; //phone number

            employee.birth_date = dtDOB.Value; //Date of birth
            employee.date_hire = dtDOH.Value; //Date of hire

            employee.address_1 = txtAdd1.Text; //Address Line 1
            employee.address_2 = txtAdd2.Text; //Address Line 2
            employee.city = txtCity.Text; //City
            employee.postal_code = txtPostCode.Text; //POstal code
            employee.province = comboBoxProv.Text; //province

            employee.pay_rate = pay_rate; //pay rarte
            employee.pay_prd = comboBoxPayPrd.Text; //pay period

            employee.type = comboBoxType.Text; //employee type

            employee.vac_year = dtVacEntYear.Value; //vacation entitlement year
            employee.vac_pay_perc = vac_pay_perc; //vacation pay percentage
            employee.cc = txtCC.Text;

            int row = ref_parent_form.employee_list.Length + 3; //calculates which row employee is going to be placed into. Its length of employee_list (how many employees are already in there) + 3 to get the respective Excel row. 
            ws.Rows[row].Insert(); //this insert a row in the 4th row the worksheet. In Excel, when you insert a row in ith row, it pushes all rows down
            //and generates a new row in ith row. 

            ws.Cells[2][row].Value = string.Format(txtFName.Text + " " + txtMName.Text + " " + txtLName.Text); //formats name into one cohesive string
            ws.Cells[4][row].Value = txtFName.Text; //first name is outputted to Excel
            ws.Cells[5][row].Value = txtMName.Text; //same as above but for middle_name
            ws.Cells[6][row].Value = txtLName.Text; //same as above but for last_name

            ws.Cells[3][row].Value = txtID.Text; //same as above but for employee ID. 
            ws.Cells[7][row].Value = txtSIN.Text;  //Same idea as employee ID. 
            ws.Cells[8][row].Value = txtEmail.Text; //same as last_name but for email
            ws.Cells[11][row].Value = txtPhone.Text; //same as last_name but for phone number

            ws.Cells[9][row].Value = string.Format("'" + dtDOH.Value.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.GetCultureInfo("en-US"))); //discussed this before in ProfilePage
            ws.Cells[10][row].Value = string.Format("'" + dtDOB.Value.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.GetCultureInfo("en-US")));
            //Same as above but for date of birth. 

            ws.Cells[12][row].Value = txtAdd1.Text; //Same idea as last_name but for address line 1
            ws.Cells[13][row].Value = txtAdd2.Text; //Same as above but for address line 2
            ws.Cells[14][row].Value = txtCity.Text; //Same as above but for city
            ws.Cells[15][row].Value = txtPostCode.Text; //Same as above but for postal_code
            ws.Cells[16][row].Value = comboBoxProv.Text; //same as employee type but for province

            ws.Cells[17][row].Value = pay_rate; //pay rate is outputted
            ws.Cells[18][row].Value = comboBoxPayPrd.Text; //same as employee type but for pay period 

            ws.Cells[19][row].Value = comboBoxType.Text; //value for employee type was already stored in a variable before so no point in using COM object. 

            ws.Cells[20][row].Value = string.Format("'" + dtVacEntYear.Value.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.GetCultureInfo("en-US"))); //discussed before.
            ws.Cells[21][row].Value = vac_pay_perc; //Same idea as pay rate but for vacation pay percentage (will be stored in decimal form)
            ws.Cells[28][row].Value = txtCC.Text;

            //FILLING YTD FIELDS
            for (int i = 22; i < 28; i++) //YTD fields are located in columns 22-27. This for loop will loop from 22 to 27.
            {
                ws.Cells[i][row].Value = 0; //It sets all YTD values to 0 since this is a new employee and they haven't been paid yet. 
            }

            wb.Save(); //Ctrl+S Excel

            //UPDATING PROGARM DATA
            Employee[] temp = new Employee[ref_parent_form.employee_list.Length]; //creates a placeholder array that is the same length as Employee List

            for (int i = 0; i < ref_parent_form.employee_list.Length; i++) //for loop repeats once for each employee
            {
                temp[i] = ref_parent_form.employee_list[i]; //stores the Employee objects in temporary array. 
            }

            ref_parent_form.employee_list = new Employee[temp.Length + 1]; //reinstantiates the array but increases its size by one.

            for (int i = 0; i < temp.Length; i++) //loops once for each object in temp.
            {
                ref_parent_form.employee_list[i] = temp[i]; //repopulates the original list, but leaving the newly added index empty
            }

            ref_parent_form.employee_list[ref_parent_form.employee_list.Length - 1] = employee; //places new employee in final index of employee_list array. 
            //If .Length returns 3, then final index would 2. Because 0,1,2.

            ref_parent_form.Controls.Remove(this); //removes this instance of CreateProfile from screen
            this.Dispose(); //gets rid of it in memory. 
        }

        private void txtSIN_KeyPress(object sender, KeyPressEventArgs e) //event listener for when a key is pressed in txtSIN
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

        private void txtVacPayPerc_KeyPress(object sender, KeyPressEventArgs e) //event listener for when key is pressed in txtvacPayPerc
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
