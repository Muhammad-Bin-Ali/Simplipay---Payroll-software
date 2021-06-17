using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpliPay
{
    public partial class AddPayDialog : Form
    {
        private ChooseEmployees parent; //I need to store a copy of the reference to the parent instance so I can access its properties. 
        private ChooseEmpObject parent_object; //stores copy of reference to the ChooseEmpObject that initiated this form. 
        private Employee employee; //stores copy of reference to employee to whom we are adding the pay to

        public AddPayDialog(ChooseEmployees parent_passed, ChooseEmpObject parent_object_passed, Employee employee_passed)
            //Constructor method takes three arguments. The parent instance of parent_passed. The parent instance of ChooseEmpObject. and Employee whose pay we are adding. 
        {
            parent = parent_passed; //stores and creates copy of reference to parent form
            parent_object = parent_object_passed; //same as above but for ChooseEmpObject
            employee = employee_passed; //same as above but for employee
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e) //event listener for if btnAdd is clicked. 
        {
            //ENSURING EVERYTHING IS FORMATTED 
            if (comboBoxTypePay.Text == string.Empty) //if there is no pay type selected and the user still pressed btnAdd
            {
                MessageBox.Show("Please pick a pay type."); //tell user to enter a pay type
                return; //end function prematurely
            }

            string pay_type = comboBoxTypePay.Text; //store chosen pay_type in string variable
            double hours; //declare an out variable for hours
            double pay_rate; //declare an out vraiable for pay_rate
            double total; //declare an out variable for total pay
            //all double because might be decimal values 

            bool check = double.TryParse(txtHours.Text, out hours); //tries parsing text in txtHours into double. Stores whether it's successful or not in check. If it's successful, the value is stored in out variable. 
            bool check2 = double.TryParse(txtPayRate.Text, out pay_rate); //same as above but for txtPayRate
            bool check3 = double.TryParse(txtTotal.Text, out total); //same as above but for txtTotal

            if (pay_type.ToLower() == "regular" || pay_type.ToLower() == "overtime" || pay_type.ToLower() == "other earnings")
                //if the pay_time chosen is either regular, overtime, or other earnings. I've used ToLower() to prevent any misses (there shouldn't be any but just in case).
            {
                if (check == false || check2 == false || check3 == false) //checks if any of the three values didn't parse
                {
                    MessageBox.Show("Please ensure there are no non-digit values in the boxes or the boxes are not empty (if available)."); //tells user that they must have entered a non-digit value 
                    //or must've left them blank. I've programmed the textboxes in such a way that the user can only type double values but just in case.
                    return; //ends function prematurely
                }
            }
            else //checks if its any other kind of pay
            {
                if (check3 == false) //if its any other kind of pay, the user is not allowed to enter pay rate or hours. They can only enter total pay.
                    //As such, I only need to check if the total pay box parsed successfully. 
                {
                    MessageBox.Show("Please ensure there are no non-digit values in the total pay box or that the box is not empty.");
                    //same idea as before
                    return; //same as before
                }
            }
           
            //ADDING PAY
            ChooseEmpObject added_pay = new ChooseEmpObject(employee, parent); //creates a new ChooseEmpObject object. This is going to be used to represent the added pay. 
            added_pay.lblEarningType.Text = pay_type; //changes its lblEarningType object's text value to the pay_type that was chosen
            added_pay.txtHours.Text = hours.ToString(); //Same idea as above but for hours entered and txtHours. If the user didn't enter any hours, or wasn't allowed to,
            //this would equal 0. This is because double variables, when declared, have a default value of 0. As a result, if its value is not changed, it would be 0. 
            added_pay.txtPayRate.Text = pay_rate.ToString(); //Same idea as above but for txtPayRate and pay rate entered (if applicable)
            added_pay.txtTotal.Text = total.ToString(); //Same idea as above but for total pay and txtTotal.

            string fname = employee.first_name.Trim().ToUpper(); //gets the first name of the ith employee and gets rid of leading and trailing white space characters.
            //Also makes it all uppercase letters.
            string mname = Convert.ToString(employee.middle_name); //gets the middle name of the ith employee.
            //Convert.ToString() is in case middle name returns null (since its not a mandatory field). Otherwise, it would throw an exception error. 
            string lname = employee.last_name.Trim(); //gets the last name of the ith employee and gets rid of leading and trailing white space characters.
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

            added_pay.lblEmpName.Text = formatted_name; //changes lblEmpName text property to the formatted name. 

            if (pay_type.ToLower() == "regular" || pay_type.ToLower() == "overtime" || pay_type.ToLower() == "other earnings")
                //same function as before. 
            {
                added_pay.txtHours.ReadOnly = false; //If the user did choose any of those pay types, then I want the txtHours and txtPayRate objects editable in the ChooseEmpObject.
                //This line ensures that added_pay.txtHours is editable and not just read only.
                added_pay.txtPayRate.ReadOnly = false; //same as above but for txtPayRate
            }
            else //if the user chose any other pay type.
            {
                added_pay.txtHours.ReadOnly = true; //if the user chose any other pay type, then  I don't want txtHours and txtPayRate editable. 
                //In a real payroll software, they would be, but due to time limitations, I cannot implement code that would deal with that. As such, user would only have access to Total Pay textbox. 
                //This line ensures that txtHours is ReadOnly (non-editable).
                added_pay.txtPayRate.ReadOnly = true; //same as above but for txtPayRate
            }

            parent.panelList.Controls.Add(added_pay); //its add the new ChooseEmpObject to the parent instance of ChooseEmployee's panel named panelList's controls.
            int index = parent.panelList.Controls.IndexOf(parent_object); //The parent object (the object that initiated this instance is recorded).
            //By index, its referring to its position in the controls of panelList. That index is stored in index. int and not byte because there may be more than 255 employees. 
            parent.panelList.Controls.SetChildIndex(added_pay, index); //This function is similar to List.Insert. .SetChildIndex takes two arguments.
            ///The first is the control being added and the second is the index it's being inserted into. The index corresponds to <controlname>.ControlCollection. 
            ///Controls with a lower index are towards the top of the z-order. Meaning, the lower the index, the more in front they appear. As a result, the lower the index, the
            ///more "forward" the control is and the later its initiated. If there's already an object in the index, then the object is repositioned to its original index + 1. All other
            ///items are moved accordingly. Being instantiated later ensures that I can dock the added_pay object to the ChooseEmpObject above it. Otherwise, it may go behind controls or may dock to the top of the panel. 
            ///In this line, it ensures that the added pay appears after the employee's regular pay object and that the added pay object attaches to the bottom of the regular pay control.
            ///However, if the user presses + on an object that has a pay type thats not "regular earnings", then the newly added object will appear below the object it was added to.
            added_pay.Dock = DockStyle.Top; //docks added_pay to object above it. In this

            this.Close(); //closes this dialog
            this.Dispose(); //clears it in memory
        }

        private void btnCancel_Click(object sender, EventArgs e) //event listener for if btnCancel is pressed
        {
            this.Close(); //closed this dialog
            this.Dispose(); //clears it in memory
        }

        private void txtHours_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtPayRate_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtHours_KeyUp(object sender, KeyEventArgs e) //after key is released in txtHours
        {
            //AUTOMATICALLY CALCULATING TOTAL
            double hours; //out variable for hours, to be used by TryParse
            double pay_rate; //out variable for pay_rate, to be used by pay_rates;

            //Even though I made sure that users cannot enter non-digit numbers, I still put TryParse in case the user finds a work around.
            //Trying to parse a string would throw an exception. 
            bool check = double.TryParse(txtHours.Text, out hours); //Tries parsing txtHours.Text. If successful, stores true in check.
            bool check_2 = double.TryParse(txtPayRate.Text, out pay_rate); //Same as above but for txtPayRate.Text

            if (check && check_2) //if both values parse successfully
            {
                txtTotal.Text = (hours * pay_rate).ToString(); //txtTotal.Text is changed to hours * pay_rate
            }
        }

        private void txtPayRate_KeyUp(object sender, KeyEventArgs e) //after key is released in txtPayRate
        {
            //AUTOMATICALLY CALCULATING TOTAL
            double hours; //out variable for hours, to be used by TryParse
            double pay_rate; //out variable for pay_rate, to be used by pay_rates;

            //Even though I made sure that users cannot enter non-digit numbers, I still put TryParse in case the user finds a work around.
            //Trying to parse a string would throw an exception. 
            bool check = double.TryParse(txtHours.Text, out hours); //Tries parsing txtHours.Text. If successful, stores true in check.
            bool check_2 = double.TryParse(txtPayRate.Text, out pay_rate); //Same as above but for txtPayRate.Text

            if (check && check_2) //if both values parse successfully
            {
                txtTotal.Text = (hours * pay_rate).ToString(); //txtTotal.Text is changed to hours * pay_rate
            }
        }

        private void comboBoxTypePay_SelectedValueChanged(object sender, EventArgs e) //event listener for when value of comboBoxTypePay is changed.
        {
            string pay_type = comboBoxTypePay.Text; //stores the pay_type the user select by accessing the text property of control. 

            if (pay_type.ToLower() == "regular" || pay_type.ToLower() == "overtime" || pay_type.ToLower() == "other earnings")
                //if the user chose any of those earnings^
            {
                txtHours.Text = ""; //refreshes txtHours and ensures it contains no text. It's done to ensure that the boxes are empty when the user switches pay types.
                //This presents a problem if the user switches from regular to let's say vacation. With vacation pay type selected, they won't be able to edit hours and pay rate.
                //However, if there was already a value there before the pay type was switched, it would remain there unless the textbox is cleared. These lines
                //just ensure that textboxes are cleaned every time they change pay type. 
                txtPayRate.Text = ""; //same as above but for txtPayRate
                txtTotal.Text = ""; //same as above but for txtTotal
                txtHours.ReadOnly = false; //makes sure txtHours is editable if it wasn't before.
                txtPayRate.ReadOnly = false; //same as above but for txtPayRate
            }
            else //if user picks a different pay type
            { 
                txtHours.Text = ""; //same as before
                txtPayRate.Text = ""; //same as before
                txtTotal.Text = ""; //same as before
                txtHours.ReadOnly = true; //same as before but this one makes it uneditable
                txtPayRate.ReadOnly = true; //same as above
                //This is to ensure that the user cannot enter hours or pay rate if the pay type selected is not regular, overtime, or other earnings. 
            }
        }
    }
}
