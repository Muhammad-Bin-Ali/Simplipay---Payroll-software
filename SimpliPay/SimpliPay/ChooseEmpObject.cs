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
    public partial class ChooseEmpObject : UserControl
    {
        //declaring  properties to store copies of important references
        public Employee employee; //employee who this object belongs to. Public because it will be referenced in ChooseEmployees
        private ChooseEmployees parent; //The parent instance of ChooseEmployees

        public ChooseEmpObject(Employee employee_passed, ChooseEmployees parent_passed) //constructor method recieves an Employee object (whose object this is)
            //and the parent form (an instance of ChooseEmployees)
        {
            parent = parent_passed; //creates and stores copy of reference to parent
            employee = employee_passed; //same as above but for employee
            InitializeComponent();
        }

        private void txtPayRate_KeyPress(object sender, KeyPressEventArgs e) //This event listener checks if the txtPayRate textbox is selected and if a key has been pressed.
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

        private void txtHours_KeyPress(object sender, KeyPressEventArgs e) //event listener for if something is typed in txtHours
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

        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e) //event listener for if something is typed in txtTotal
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

        private void txtPayRate_KeyUp(object sender, KeyEventArgs e) //event listener for after key is released
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

        private void txtHours_KeyUp(object sender, KeyEventArgs e) //event listener for after key is released
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

        private void picPayAdd_Click(object sender, EventArgs e) //event listener for if picPayAdd is clicked
        {
            AddPayDialog add_pay = new AddPayDialog(parent, this, employee); //creates new instance of add_pay. Passes parent instance of ChooseEmployee, and instance of this object,
            //and reference to employee
            add_pay.ShowDialog(); //since add_pay is a form and not a user control, .ShowDialog() displays it on the screen as a modal dialog (dialog that prevents user from accessing the program till the dialog is closed).
        }

        private void pictureRemove_Click(object sender, EventArgs e) //event listener for if pictureRemove is clicked
        {
            parent.Controls.Remove(this); //removes this object from controls of parent
            this.Dispose(); //clears it in memory
        }
    }
}
