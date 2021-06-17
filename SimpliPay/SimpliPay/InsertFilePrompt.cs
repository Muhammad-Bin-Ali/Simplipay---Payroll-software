using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; //for file input and output
using SimpliPay.Properties; //to access project's embedded files
using _Excel = Microsoft.Office.Interop.Excel; //The variable Excel is now equal to Microsoft.Office.Interop.Excel. This makes it easier for me to use features from that libary as I won't have to type out the entire thing. 
using System.Runtime.InteropServices; //used for ReleaseCOMObject method


namespace SimpliPay
{
    public partial class InsertFilePrompt : Form
    {
        public InsertFilePrompt()
        {
            InitializeComponent();
        }

        //These are public because they will be accessed by FrmMain. If they were private, then that wouldn't be possible. 
        public Employee[] returned_emp_list; //Create a property of InsertFilePrompt. This property is going to be used to "return" value from child form to parent form and it is an Employee object array. 
        public string returned_path; //Creates another property of InsertFilePrompt. This one is going to be used to "return" the path of user's file. 

        private void close_excel_COM(_Excel.Application excel, _Excel.Workbook wb, _Excel.Worksheet ws) //The specifics of what a method is are discussed in the method below.
            //This method is responsible for releasing (closing) the COM objects that were created in the method below. 
        {
            //COM CLEANUP - important as without this, the Excel application would still be running in the background and as a result, the user would not be able to edit the file they uploaded. 
            Marshal.ReleaseComObject(ws); //This method is used to release the COM object that's within the brackets. By releasing the COM object, it is essentially ending it and preventing it from running in the background after the program is closed. 
            //This line is used to release the Excel Worksheet. 

            wb.Close(); //This line simply closes the Workbook that is open in the background. 
            Marshal.ReleaseComObject(wb); //Same task as above except for the Workbook. 

            excel.Quit(); //This lines quit the Excel application and fully terminates the Excel task in the background. Without this, Excel would still be running in the background. 
            Marshal.ReleaseComObject(excel); //Same task as above except for Excel application. 
        }

        private void resetYTD(_Excel.Workbook wb,_Excel.Worksheet ws, Employee employee, DateTime last_date, int row) //This is a private method since it won't be accessed outside of this class.
            //It is used to reset YTD values. 
        {
            DateTime tax_year = new DateTime(DateTime.Now.Year, 4, 30); //creates a new DateTime object for tax due date for the given year. DateTime.Now.Year returns the current year. DateTime(year, month, day) is used to create a new object

            if (last_date > tax_year) //All datetime objects are converted to ticks. As a result, if a date comes later than another date, then it would have a larger tick value. 
                ///In this case, it checks if last_date (last opened date) comes later than the tax year due date . If it doesn't, then it doesn't need to reset the YTD values since they were reset the last time it was opened.
                ///It just updates the last opened date.
            {
                //do NOTHING
            }
            else if (last_date < tax_year && DateTime.Now > tax_year)  //All datetime objects are converted to ticks. As a result, if a date comes later than another date, then it would have a larger tick value. 
            ///In this case, it checks if last_date (last opened date) comes before than the tax year due date. If it does, then it needs to reset the YTD values since they weren't reset the last time it was opened.
            ///In addition, the current date should also be past April 30th (DateTime.Now returns current date). If both conditionals are met, then YTD values are reset. 
            {
                //SETS THE EMPLOYEE YTD PROPERTIES TO 0
                employee.ytd_total_pay = 0;
                employee.ytd_vac_pay = 0;
                employee.ytd_other_pay = 0;
                employee.ytd_cpp = 0;
                employee.ytd_EI = 0;
                employee.ytd_tax_ded = 0;

                ws.Cells[22][row + 3].Value2 = 0; //This line resets the values in the Excel sheets. 
                ws.Cells[23][row + 3].Value2 = 0; //Same as above but for ytd total vacation pay
                ws.Cells[24][row + 3].Value2 = 0; //Same as above but for ytd total cpp deductions
                ws.Cells[25][row + 3].Value2 = 0; //Same as above but for ytd total EI deductions
                ws.Cells[26][row + 3].Value2 = 0; //Same as above but for ytd total fed/prov total tax deductions. 
                ws.Cells[27][row + 3].Value2 = 0; //Same as above but for ytd total other earnings

                ws.Cells[22][row + 3].Style = "Accounting"; //This line sets the cell's format to Accounting. 
                ws.Cells[23][row + 3].Style = "Accounting"; //Same as above but for ytd total vacation pay
                ws.Cells[24][row + 3].Style = "Accounting"; //Same as above but for ytd total cpp deductions
                ws.Cells[25][row + 3].Style = "Accounting"; //Same as above but for ytd total EI deductions
                ws.Cells[26][row + 3].Style = "Accounting"; //Same as above but for ytd total fed/prov total tax deductions. 
                ws.Cells[27][row + 3].Style = "Accounting"; //Same as above but for ytd total other earnings
            }
        }

        private bool check_headings(string path)
            ///This is a method. In short, it is a group of processes that the user can reuse by simply calling the method name. For example,
            ///let's say that we have a method named CalcAge() and its only argument is a date of birth datetime object. And let's say that this method performs several processes
            ///such as obtaining today's date, calculating the difference between today's date and date of birth, and converting that to age. Whenever the user needs
            ///those group of processes, the user can simply type CalcAge(dateofbirth) instead of typing those processes out. Methods are used to increase effeciency and make the code less cluttered.
           
            ///The first word is the access modifier. I have discussed this in more detail in the Employee class file but in short, private means that this method can be called from anywhere within this class but not from outside this form class..
            ///The second word refers to what data type the method will be returning. If the method is not returning anything, "void" would be written here.
            ///In the previous example, since the method would be returning an integer age, that word would be int. Next comes the method name which is check_headings in this case.
            ///Finally, the brackets after the method name are populated with arguments that the method would be accepting. These arguments are essentially the values that the user will be passing into the method for processing.
            ///The only argument this method has is a string variable named path. The name of the argument variable does not have to correspond with the variable that is passed into the method.
            ///The argument variable name is simply the name of the variable when it is used inside the method. For example, I can pass in a variable named "cows" into this method.
            ///However, in this method, the value of "cows" will be referred to as "path".
            ///FUNCTION: This method is used to determine if the headings have been formatted properly and if the headings are in the right order. 
        {
            bool format_correct = true; //A variable named format_correct is declaring and assigned a value of true. This variable is going to be changed to false if any of the headings
            //are not formatted correctly. 

            string[] headings = new string[] { "Employee Name", "Employee I.D.", "First Name",
            "Middle Name", "Last Name", "SIN", "Email", "Date of Hire", "Date of Birth", "Phone Number",
            "Address Line 1", "Address Line 2", "City", "Postal Code", "Province", "Pay Rate", "Pay Period", "Type of Employee",
            "Vacation Entitlement Year", "Vacation Pay Percentage", "Year-to-Date Total Earned Pay", "Year-to-Date Total Vacation Pay", "Year-to-Date Total C.P.P. Deductions",
            "Year-to-Date Total E.I. Deductions", "Year-to-Date Total Fed./Prov. Tax Deductions", "Year-to-Date Total Other Earnings", "Claim Code"};
            //Declares a string array called headings and assigns it the correctly formatted and correctly ordered headings.

            _Excel.Application excel;
            _Excel.Workbook wb;
            _Excel.Worksheet ws;

            try //The following lines will throw an exception if the database is open.
            {
                excel = new _Excel.Application(); //This line uses Excel Interop to create an Excel Application COM (Component Object Model) object. 
                ///In short, a COM object is a component that allows it to interact with other objects. It is a non-platform-specific object that can be used independently 
                ///of the language that it was programmed in. The COM components are usually registered with the system upon installation (such as how Excel Interop is automatically installed on machines that have Excel).
                ///In this case, even though Excel may not be based in C#, I can take advantage of Excel Interop to create a new Excel application named "excel".
                ///This application is not visible to the user as it is strictly present in the backend and is used for processing data. 
                 wb = excel.Workbooks.Open(path); //This line creates an Excel Workbook COM object and uses it to open an existing Excel file in the backend. 
                //.Open() method takes the path of the Excel file to be opened and opens it in the background. In other words, the line above "opens" Excel itself and this line 
                //"opens" the Excel Workbook. The name of this Excel Workbook COM object is wb. 
                ws = new _Excel.Worksheet(); //This line instantiates an empty Excel Worksheet COM object. 
            }
            catch //If the ws or wb throws an exception, the program would have still created a COM object that would be running in the back.
            ///Due to a lack of time, I am unable to implement a more functional and user-friendly solution. As a result, I've told the user to 
            ///close all files from the task manager. 
            {
                MessageBox.Show("Please ensure that the database worksheet is not open in the task manager and/or is not set to read-only. Also ensure that all Excel files are closed in the task manager. Then restart program."); //tells user to ensure that their database or any other excel files are not open in task manager. 
                return false; // returns false saying its not formatted correctly.
            }
           

            try //I've put the following code in try catch in case the user decides to delete the main Database worksheet or rename it. 
            {
                ws = wb.Sheets["Database"]; //Same idea as above except that it opens the first worksheet in the wb Workbook COM object. "Database" is used to 
                //access the worksheet using its name. I can also use non-zero based indices but the user may accidentally create a new worksheet which would present complications.
            }
            catch
            {
                MessageBox.Show("Please ensure that the database worksheet is named \'Database\'"); //tells user to ensure that their database is named right
                close_excel_COM(excel, wb, ws); //closes COM objects before returning false
                return false; // returns false saying its not formatted correctly.
            }

            for (int i = 2; i < 28; i++) //This is a for loop. This is used to repeat a block of code for a given number of times.
            ///The first part declares a counter variable i and assigns it a starting value of 2 (reasoning for 2 discussed later).
            ///The second part declares the conditional that must return true for the for loop to repeat. If it returns false, then the program will continue forward and the for loop will not repeat. 
            ///The third line declares the increment. i++ can also be written as i = i + 1 and it simply means that for every time the for loop iterates, the program will add 1 to the value of i. 
            ///In this case, the following block of code is going to repeat 25 times. This is because there are 27 headings and the block of code is going to check each one to ensure that it
            ///has been formatted correctly and that the headings are in the correct order. 
            ///Since Excel is non-zero based, and since my first heading is located in cell B2, I assigned i a value of 2.
            ///Since the for loop has to repeat 27 times, I have set the conditional such that it returns true if i is less than 28. The value of i will go up till 28 and when its value is 28,
            ///the for loop would not repeat again as it would have already repeated 27 times.
            {
                string heading_value = "null"; //Declares a variable named heading_value and assigns it a placeholder value of "null".
                ///This variable is going to be used to store the headings of the database that the user has inputted. 
                ///Since I am using try catch to get the headings, I need a placeholder value for heading_value when declaring it.
                ///Otherwise, the value of heading_value will initially be empty or NULL and the following conditional statement would lead to a build error. 

                try //this is a try catch statement. It will "try" the code within the following curly brackets and if it raises any sort of error, the code within the catch curly braces will be executed. 
                {
                    heading_value = ws.Cells[i][2].Value.ToString(); //assigns heading_value the value within cell [i][2]
                    ///ws.Cells[i][j] is used to access the cell where the ith column and jth row intersect, in the ws worksheet.
                    ///Since all the headings are located in the second row, I put 2 for the second index value (Excel is not zero-based so it would 2 for the second index, not 1).
                    ///Since the headings are located from column 2 to column 26 (B to AB), the value of i starts at 2 and goes up till 28 before the for loop exits.
                    ///This allows the program to loop through all the headings. 
                    ///.Value simply returns the value within the respective cell.
                    ///.ToString() converts any non-letter values into a string. This is incase all the headings are in the right place but there is a non-letter value in one of the headings.
                    ///If that's the case, then that will be delt with in a later block of code. 
                    ///This section is simply to ensure that the user's headings start at B2 and finish at AB2. 
                    ///If the user's values start and end at different cells, then the program will end up converting EMPTY values to string which would raise an error. If such an error is raised, it will trigger the "catch" block of code. 
                    ///If, for some reason, the user has cells B2 - Y2 populated with data but their headings start and end at different cells, then a later block  of code with deal with it. 
                }
                catch //the following code will be executed if the code above raises an error (reason for this explained below).
                {
                    MessageBox.Show("Ensure that the top-left cell of your database is cell B2 and that there is no empty heading from B2 to AB2."); //Shows a dialog box that tells the user to ensure that the top left cell of their database is cell B2.
                    format_correct = false; //Since this is a formatting error, the value of format_correct is changed to false.
                    close_excel_COM(excel, wb, ws); //runs the close_excel_COM function and passes the previously created COM objects. This function will release and close the COM objects so they don't remain open in the background. 
                    return format_correct; //returns the value of format_correct. If the database starts in the wrong cell, then there is no point in executing the rest of the code. 
                }
          
                if (headings[i - 2].ToLower() != heading_value.Trim().ToLower())
                    ///This conditional compares the value of each heading to the value that it is supposed to be. headings[i-2] is used to access the correctly formatted heading from the array instantiated previously.
                    ///Since the value of i starts at 2, i-2 is used to get the index that would correspond to the headings array. .ToLower() is used to convert the retrieved heading to all lowercase letters.
                    ///This is because it would be ineffecient to raise an error if the user has formatted everything correctly but forgot to make a letter uppercase. 
                    ///!= is a comparision operator that stands for "not equal to". On the right side of the conditional statement, I have also used .ToLower() to convert the user's heading to all lowercase letters.
                    ///I have also used .Trim() to remove any leading and trailing white space characters. Any extra leading or trailing white space characters would also fulfill the conditional and it would be ineffecient for the user
                    ///if everything is formatted correctly but they have a trailing or leading white space. However, this code will still detect multiple spaces (or tabs) between words. This is incase the user accidentally presses tab or accidentally presses
                    ///their space bar multiple times without noticing. 
                {
                    MessageBox.Show(heading_value + " should be formatted as " + headings[i - 2] + ". Ensure that it is formatted correctly before reuploading the database.");
                    //Tells the user which heading_value is formatted wrong and how it should be formatted
                    format_correct = false; //Changes the value of format_correct to false. 
                }
            }

            close_excel_COM(excel, wb, ws);//runs the close_excel_COM function and passes the previously created COM objects. This function will release and close the COM objects so they don't remain open in the background.

            return format_correct; //returns the format_correct variable. 
        }

        private Tuple<Employee[], bool> scan_employees(string path) //This method is used to scan employee data into the program and to ensure that all dates and currency values are formatted correctly. 
            ///It will also check employee type, pay period, and province to ensure that all are following a consistent format => Otherwise may throw error when displaying profiles.
            ///It's just dates and currency values as those are the ones that will raise an exception error later. I am trusting the user that strings (names, SIN, IDs, etc.) will be formatted properly.
            ///This is a private method, meaning it cannot be accessed outside of this class.
            ///It returns a Tuple object with two Items => an Employee array, and a boolean value
            ///A Tuple is simply a data structure in which you can store objects of different data types. However, a major limitation is that a Tuple can only hold up to 8 items (Nested Tuples is a workaround).
            ///The only argument this method takes is the path of user's database. 
        {
            //Rather than keeping the previous COM object open, I decided to close them and reopen them. This may decrease speed of program but will ensure that any COM objects made are closed.
            //I also want to ensure that the headings are in the right place and in the right order before collecting the employees and their information.
            _Excel.Application excel = new _Excel.Application(); //Same task as when used before
            _Excel.Workbook wb = excel.Workbooks.Open(path); //Same task as when used before
            _Excel.Worksheet ws = wb.Sheets["Database"]; //Same task as when used before 
            //no need to use try catch. If it passed check_headings, then everything is formatted correctly. 

          

            //COUNTING NUMBER OF EMPLOYEES
            int employee_count = 0; //declares int variable called employee_count and assigns it a value of 0. Int is used because there may be more than 255 employees.
            int counter = 3; //declares int variable called counter and assigns it a value of 3. This counter variable will be used to loop through the entries under the "Employee Name" Heading.
            //Since the first entry of "Employee Heading" will be in the third row, I've assigned the counter variable a value of 3. 

            while (ws.Cells[2][counter].Value != null) //A while loop is a loop that repeats until the conditional becomes false. 
                //This while loop will repeat till it comes across a cell (under the "Employee Name" heading) that is null (empty).
                //ws.Cells[i][j] is used to access cell that is in ith column and jth row. .Value is used to access the value of the cell. 
            {
                employee_count++; //if it encounters a line that is not empty, meaning there is a name there, it adds one to the value of employee_count. employee_count++ => employee_count = employee_count + 1
                counter++; //same as above. 
            }

            //POPULATING ARRAY
            Employee[] employee_list = new Employee[employee_count]; //Instantiates an Employee object type array called employee_list. The number of objects in the array is equal to the number of employees in the database.
            //Array is still empty. It will be populated later. 

            for (int i = 0; i < employee_count; i++) //This for loop is repeated once for however many employee there are. The specifics of the syntax of a for loop were discussed in the method above. 
            {
                employee_list[i] = new Employee(); //Create an employee object and stores it in the ith index of employee_list array. 
                ///Since the for loop repeats once for each employee and the size of the array is the same as the number of employees, this for loop for populate all the indices. 
            }

            //CHECKING IF DATES, CURRENCY VALUES, and OTHER FIELDS ARE FORMATTED PROPERLY
            byte[] datetime_columns_check = new byte[] { 9, 10, 20 }; //Instantiates an array with the column numbers of values that are going to be checked. This array
            ///is populated with column numbers that are going to have dates in them, and as such, are going to be processed as DateTime objects by the program. 
            ///Its important to check if the dates have been formatted correctly to prevent the program from raising an exception later on. 
            ////I've used byte instead of int as all items are less than 255. 
            ///Values from left to right: Date of hire, Date of birth, Vacation Entitlement Year 
            byte[] double_columns_check = new byte[] { 17, 22, 23, 24, 25, 26, 27 }; //Same idea as above but for columns that are going to contain currency (converted to double) values. 
            //Values from left to right: Pay rate, Year to date total earned pay, Year to date total vacation pay, Year to date total CPP deductions, Year to date total EI deductions, Year to date total fed/prov tax deductions.

            string datetime_format = "MM/dd/yyyy"; //Declares a string variable and assigns it the format that dates should be formatted as.
            //This is going to be used for DateTime.TryParseExact().

            //CHECKING IF FILE HAS A LAST OPENED VALUE
            DateTime last_date_opened = DateTime.Now; //gives the variable a placeholder value of today's time and date.
            try //this is in case the user has tampered with the last opened date. Without a try catch, it will raise an error. 
            {
                last_date_opened = DateTime.ParseExact(ws.Cells[31][3].Value2, datetime_format, new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None); //discussed below. This line obtains the last opened value. 
            }
            catch
            {
                MessageBox.Show("Please ensure that your database has a \'last opened\' value. Otherwise, the database cannot be uploaded to be used in the program."); //Informs the user that their file is missing a last opened date.
                close_excel_COM(excel, wb, ws); //closes COM objects so they don't remain open in the background. 
                return new Tuple<Employee[], bool>(employee_list, false); //same idea as before
            }

            for (int i = 0; i < employee_count; i++) //Same idea as when used previously. Repeats once for each employee. 
            {
                //CHECKING CLAIM CODES
                string claim_code = Convert.ToString(ws.Cells[28][i + 3].Value); //retrieves value in cell column 28 row i + 3. Convert.ToString is used incase the value returns null
                // or if it returns a double value. 

                if (claim_code != "0" && claim_code != "1" && claim_code != "2" && claim_code != "3" && claim_code != "4" && claim_code != "5" &&
                    claim_code != "6" && claim_code != "7" && claim_code != "8" && claim_code != "9" && claim_code != "10") 
                    //checks if the employee's claim code is valid. It can only be from 0 to 10 inclusive. Any other value is invalid. 
                    //If text entered is not equal to ANY of the values above, then conditional returns true. If it's equal any of the values above, then the entire conditional would return false.
                {
                    MessageBox.Show("Please ensure that " + ws.Cells[2][i + 3].Value + "'s tax claim code is formatted correctly."); //tells user to check if ith employee's claim code is formatted correctly
                    close_excel_COM(excel, wb, ws); //closes COM objects so they don't remain open in the background. 
                    return new Tuple<Employee[], bool>(employee_list, false); //Creates and returns a new Tuple object that contains an employee list (populated with empty Employee objects) and a boolean 
                    //value indicating that a value was incorrectly formatted and thus, the program should not proceed forward. 
                }


                //CHECKING DATES
                for (int j = 0; j < datetime_columns_check.Length; j++) //This is a nested for loop. It repeats for a given number of times every time the parent for loop repeats. 
                    //This for loop is going to be used to iterate over the values in datetime_columns_check. 
                {
                    DateTime temp_object; //Creates a temporary DateTime object. 

                    byte column = datetime_columns_check[j]; //stores the column value that is being checked.
                    string date = ws.Cells[column][i+3].Value2; //obtains the value that is entered in the jth column of the ith employee.
                    //i + 3 is being done because Excel is non-zero based and because the employee entries start in the third row. 
                    //.Value2 performs the same function as .Value except that it returns the data within the cell without any currency or date formatting. 

                    bool check = DateTime.TryParseExact(date.Trim(), datetime_format, new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out temp_object);
                    ///TryParseExact is similar to TryParse in the sense that it doesn't raise an exception if it is unable to parse the given string. The first argument of this method is
                    ///the string that is going to be passed. The second argument is the format that the method is going to try to parse it to. The third argument is called the 
                    ///IFormatProvider. It is used to apply culture specific formatting. For example, dates in fr-FR (french-France) are going to formatted differently 
                    ///then en-US (english - United States) => France: 01/06/2009 16:37:00 | United States: 6/1/2009 4:37:00 PM. The fourth argument is used to account for more specific DateTime formats.
                    ///For example, DateTimeStyles.AllowTrailingWhites will tell the program to ignore trailing white space characters when parsing. I have already taken leading and trailing white spaces
                    ///into account by using .Trim(). The final argument is the out variable or the variable where the parsed DateTime object is going to be stored if the parsing is successful. In this case,
                    ///it will store it in the temporary DateTime object I created above. 
                    ///Since DateTime.TryParseExact() also returns a true and false boolean value (depending on if it parsed successfully or not), I've also declared a bool type variable called check. 

                    if (check == false) //If check is false (if date didn't parse correctly).
                    {
                        MessageBox.Show("One of " + ws.Cells[2][i + 3].Value + "'s dates is not formatted correctly. " + date + " should be formatted using MM/dd/yyyy"); //tells user which employee's date is not formatted correctly, what the incorrectly 
                        //formatted date is, and how to properly format it. 
                        close_excel_COM(excel, wb, ws); //closes COM objects so they don't remain open in the background. 
                        return new Tuple<Employee[], bool>(employee_list, false); //Creates and returns a new Tuple object that contains an employee list (populated with empty Employee objects) and a boolean 
                        //value indicating that a value was incorrectly formatted and thus, the program should not proceed forward. 
                    }
                }
                //CHECKING CURRENCIES
                for (int j = 0; j < double_columns_check.Length; j++) //same idea as previous nested for loop but for double_columns_check
                {
                    double temp_object; //same idea as DateTime temp_object

                    byte column = double_columns_check[j]; //same idea as before
                    string value = Convert.ToString(ws.Cells[column][i + 3].Value2); //same idea as before but Convert.ToString() is used to convert the data to a string. 
                    //If the user has formatted everything correctly, .Value2 will return a double data type. However, if I do => double value = ws.Cells[column][i + 3].Value2 =>
                    //It will raise an exception if the user has formatted it incorrectly. For example, if there is a non-digit symbol in there, it will raise an error if it stored into a double data type variable.
                    //That's why I'm converting the returned value to string regardless of whether it's valid or not. 
                    
                    bool check = double.TryParse(value, out temp_object); //Same idea as before but with try parse. First argument is string to be parsed and second argument is the out variable
                    //where the value will be stored if it is parsed successfully. TryParse returns true or false depending on if it parsed successfully. That value is stored in a bool variable called check. 

                    if (check == false) //Same idea as before
                    {
                        MessageBox.Show("One of " + ws.Cells[2][i + 3].Value + "'s currency values is not formatted correctly. Ensure that " + value + " is formatted correctly. Ensure that no non-digit values are present."); //Same idea as before
                        close_excel_COM(excel, wb, ws); //closes COM objects so they don't remain open in the background. 
                        return new Tuple<Employee[], bool>(employee_list, false); //same idea as before
                    }
                }

                //ENSURING employee type is formatted correctly. Otherwise it may raise error when displaying profiles
                string emp_type = Convert.ToString(ws.Cells[19][i + 3].Value); //obtains employee type from (i + 3)th row 19th column
                try //if the employee type field is null, .ToLower() will raise an error. 
                {
                    if (emp_type.ToLower() != "salaried" && emp_type.ToLower() != "wage-based") //checks if emp_type.ToLower (.ToLower makes it all lower-case) is neither exactly "salaried" or "wage-based". && is AND operator. Both conditionals needs to return true.
                    {
                        MessageBox.Show(ws.Cells[2][i + 3].Value + "'s employee type is not formatted correctly. Ensure that " + emp_type + " is formatted correctly. It should be either \'Salary\' or \'Wage-based\'"); //Same idea as before
                        close_excel_COM(excel, wb, ws); //closes COM objects so they don't remain open in the background. 
                        return new Tuple<Employee[], bool>(employee_list, false); //same idea as before
                    }
                }
                catch //if employee type is null
                {
                    MessageBox.Show(ws.Cells[2][i + 3].Value + "'s employee type is not formatted correctly. Ensure that " + emp_type + " is formatted correctly. It should be either \'Salary\' or \'Wage-based\'"); //Same idea as before
                    close_excel_COM(excel, wb, ws); //closes COM objects so they don't remain open in the background. 
                    return new Tuple<Employee[], bool>(employee_list, false); //same idea as before
                }

                //ENSURING pay period is formatted correctly. Otherwise it may raise error when displaying profiles
                string pay_period = Convert.ToString(ws.Cells[18][i + 3].Value); //obtains pay period from (i + 3)th row 18th column

                try //if the pay period field is null, .ToLower() will raise an error. 
                {
                    if (pay_period.ToLower() != "52/year" && pay_period.ToLower() != "26/year" && pay_period.ToLower() != "24/year" && pay_period.ToLower() != "12/year")
                    //checks if pay_period.ToLower is not formatted as either of the values above. 
                    {
                        MessageBox.Show(ws.Cells[2][i + 3].Value + "'s  pay period is not formatted correctly. Ensure that " + pay_period + " is formatted correctly. It should be formatted as either 52/year, 26/year, 24/year, or 12/year."); //Same idea as before
                        close_excel_COM(excel, wb, ws); //closes COM objects so they don't remain open in the background. 
                        return new Tuple<Employee[], bool>(employee_list, false); //same idea as before
                    }
                }
                catch //if pay period is null
                {
                    MessageBox.Show(ws.Cells[2][i + 3].Value + "'s  pay period is not formatted correctly. Ensure that the pay period field is not empty."); //Same idea as before
                    close_excel_COM(excel, wb, ws); //closes COM objects so they don't remain open in the background. 
                    return new Tuple<Employee[], bool>(employee_list, false); //same idea as before
                }

                //ENSURING province is formatted correctly. Otherwise it may raise error when displaying profiles
                string province = Convert.ToString(ws.Cells[16][i + 3].Value); //obtains provice from (i + 3)th row 18th column

                try //if the province field is null, .ToLower() will raise an error. 
                {
                    if (province.ToLower() != "alberta" && province.ToLower() != "british columbia" && province.ToLower() != "manitoba" && province.ToLower() != "new brunswick"
                    && province.ToLower() != "newfoundland and labrador" && province.ToLower() != "northwest territories" && province.ToLower() != "nova scotia" && province.ToLower() != "nunavut"
                    && province.ToLower() != "ontario" && province.ToLower() != "prince edward island" && province.ToLower() != "quebec" && province.ToLower() != "saskatchewan" && province.ToLower() != "yukon")
                    //checks if province.ToLower is not formatted as either of the values above. 
                    {
                        MessageBox.Show(ws.Cells[2][i + 3].Value + "'s  province is not formatted correctly. Ensure that " + province + " is formatted and spelled correctly."); //Same idea as before
                        close_excel_COM(excel, wb, ws); //closes COM objects so they don't remain open in the background. 
                        return new Tuple<Employee[], bool>(employee_list, false); //same idea as before
                    }
                }
                catch //This code will run if the field is null.
                {
                    MessageBox.Show(ws.Cells[2][i + 3].Value + "'s  province is not formatted correctly. Ensure that it is not an empty field."); //Same idea as before
                    close_excel_COM(excel, wb, ws); //closes COM objects so they don't remain open in the background. 
                    return new Tuple<Employee[], bool>(employee_list, false); //same idea as before
                }


                //FILLING EMPLOYEE OBJECT PROPERTIES
                ///employee_list array is populated with Employee class objects that are empty. The following lines will fill out their properties. The employees in the array will be in the same order as they 
                ///are in the database from top to bottom. employee_list[i] is used to retrieve an Employee object in the ith position from the employee_list array. Object.<propertyname> is used to assign values
                ///to properties of class objects. For example, if I have a Car object with the property color. In order to assign a value to that property, I would need to write => Car.color = "red";
                ///Value assigned must match the data type that the property was declared with when the class was created.
                ///As discussed before, i + 3 gives the employee's row number in Excel. 
                ///Numbers 3 to 26 are used to access the columns where the respective values are located. For example, are first_name values are located in column 4.
                employee_list[i].first_name = Convert.ToString(ws.Cells[4][i + 3].Value); //assigns ith employee their first_name. Convert.ToString() is used in case ws.Cells[][].Value returns a null value.
                //.ToString() or string.Format() end up raising an exception.
                employee_list[i].middle_name = Convert.ToString(ws.Cells[5][i + 3].Value); //same as above but for middle_name
                employee_list[i].last_name = Convert.ToString(ws.Cells[6][i + 3].Value); //same as above but for last_name

                employee_list[i].ID = Convert.ToString(ws.Cells[3][i + 3].Value); //same as above but for employee ID. If the user uses number-only employee IDs, then ws.Cells[][].Value will return a double data type.
                //Employee ids are not going to experience any arithmetic processing and such, don't need to be stored in a numeric data type. 
                employee_list[i].SIN = Convert.ToString(ws.Cells[7][i + 3].Value);  //Same idea as employee ID. 
                employee_list[i].email = Convert.ToString(ws.Cells[8][i + 3].Value); //same as last_name but for email
                employee_list[i].phone_num = Convert.ToString(ws.Cells[11][i + 3].Value); //same as last_name but for phone number

                employee_list[i].date_hire = DateTime.ParseExact(ws.Cells[9][i + 3].Value2, datetime_format, new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
                //ParseExact is similar to TryParseExact except that it raises an exception if date is formatted in correctly. In this case, an exception would be impossible as I've already ensured that dates are formatted correctly.
                //The arguments of ParseExact are the same as TryParseExact and in extension, have been discussed before. The only difference is that ParseExact returns the successfully parsed data and does not have an out variable. 
                //This line parses data_hire value to a valid DateTime object. 
                employee_list[i].birth_date = DateTime.ParseExact(ws.Cells[10][i + 3].Value2, datetime_format, new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
                //Same as above but for date of birth. 

                employee_list[i].address_1 = Convert.ToString(ws.Cells[12][i + 3].Value); //Same idea as last_name but for address line 1
                employee_list[i].address_2 = Convert.ToString(ws.Cells[13][i + 3].Value); //Same as above but for address line 2
                employee_list[i].city = Convert.ToString(ws.Cells[14][i + 3].Value); //Same as above but for city
                employee_list[i].postal_code = Convert.ToString(ws.Cells[15][i + 3].Value); //Same as above but for postal_code
                employee_list[i].province = province; //same as employee type but for province

                employee_list[i].pay_rate = ws.Cells[17][i + 3].Value2; //Function of .Value2 was discussed before. I've already checked if the employee's respective pay rate value is 
                //parsable or not. If the code gets here, then it has already determined that it is parsable and as a result, I don't need to use TryParse. Same idea applies for all the following double values. 
                employee_list[i].pay_prd = pay_period; //same as employee type but for pay period 

                employee_list[i].type = emp_type; //value for employee type was already stored in a variable before so no point in using COM object. 

                employee_list[i].vac_year = DateTime.ParseExact(ws.Cells[20][i + 3].Value2, datetime_format, new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
                //Same idea as previous DateTime objects but for vacation entitlement year. 
                employee_list[i].vac_pay_perc = ws.Cells[21][i + 3].Value; //Same idea as pay rate but for vacation pay percentage (will be stored in decimal form)

                employee_list[i].ytd_total_pay = ws.Cells[22][i + 3].Value2; //Same idea as pay rate but for ytd total earned pay
                employee_list[i].ytd_vac_pay = ws.Cells[23][i + 3].Value2; //Same as above but for ytd total vacation pay
                employee_list[i].ytd_cpp = ws.Cells[24][i + 3].Value2; //Same as above but for ytd total cpp deductions
                employee_list[i].ytd_EI = ws.Cells[25][i + 3].Value2; //Same as above but for ytd total EI deductions
                employee_list[i].ytd_tax_ded = ws.Cells[26][i + 3].Value2; //Same as above but for ytd total fed/prov total tax deductions. 
                employee_list[i].ytd_other_pay = ws.Cells[27][i + 3].Value2; //Same as above but for ytd total other earnings
                employee_list[i].cc = ws.Cells[28][i + 3].Value.ToString(); //Same as above but for tax claim code

                //RESETTING YTD VALUES
                resetYTD(wb, ws, employee_list[i], last_date_opened, i); //uses the previously established method to reset (if applicable) the YTD values of each employee. 
            }

            if (wb.ReadOnly) //This line checks if wb workbook is ReadOnly. The database will only be read only if its either open in the front
                //or in the task manager. If that's the case, wb.Save() will throw an error. As a result, if it is ReadOnly, I want to tell the user to close all Excel files
                //before exiting the function prematurely. 
            {
                MessageBox.Show("Please ensure that your Excel file is not open in the task manager and/or is not set to read-only.");
                close_excel_COM(excel, wb, ws); //closes COM objects so they don't remain open in the background. 
                return new Tuple<Employee[], bool>(employee_list, false); //same idea as before
            }

            //UPDATING LAST OPENED
            ws.Cells[31][3].Value = string.Format("'" + DateTime.Now.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.GetCultureInfo("en-US"))); //ws.Cells[i][j] accesses the cell in the ith column, jth row.
            //.Value allows the program to access the cell's value. DateTime.Now uses the datetime library to create a datetime object with the current date and time.
            //.ToString() allows user to choose the format they want the datetime value to be parsed as. CultureInfo was discussed in TryParseExact but essentially, it ensures that the date is formatted with slashes and not hyphens. 
           
            
            wb.Save(); //Same function as ctrl+s in Excel
           
            
            //CLEANUP AND RETURN
            close_excel_COM(excel, wb, ws); //closes COM objects

            returned_emp_list = employee_list; //sets the return_emp_list class property equal to the employee_list. This is simply creating a copy of the reference to
            //the Employee object array, not a copy of the array itself. 
            returned_path = path; //Same idea as above since path is a string. This also creates a copy of the reference to the value of path that is stored in the memory.
            //The path variable is also a reference to its value that is stored in the memory. 

            return new Tuple<Employee[], bool>(employee_list, true); //if it gets to this point, then everything is formatted correctly and I can return true as my second TUPLE item. 
        }

        private void btnAddFile_Click(object sender, EventArgs e) //Event listener for if Add Excel File button is pressed
        {
            btnContinue.Visible = false; //the btnContinue object's Visible property is changed to false. This is to ensure that if a decides to upload another database after uploading a previous one, then the continue button would 
            //become "invisible" till the program is finished loading all the data.

            OpenFileDialog fileDialog = new OpenFileDialog(); //Creates an instance of the OpenFileDialog class and stores it in a variable called fileDialog.
            ///This class allows user to select a file from their system and open it in the program. This line simply creates an instance of the class and nothing else. 
            fileDialog.Filter = "Excel File (*.xlsx)|*.xlsx"; //This line creates a filter that is going to be used when the fileDialog dialog box is displayed to the user.
            ///In other words, this line corresponds to the file filters that appear in the bottom right when the dialog is open. For example, for Microsoft Word, the 
            ///filter may only allow the user to open PDF or Word documents. In this case, the user will only be able to see the folders and the Excel documents present on their system.
            ///Thus, they will only be able to select and upload Excel files.

            if (fileDialog.ShowDialog() == DialogResult.OK)
            ///This is a conditional. The code within the curly braces is only executed if the code between the brackets above returns a true boolean value.
            ///fileDialog.ShowDialog() refers back to the previously instantiated instance of the OpenFileDialog class and .ShowDialog() displays the dialog box to the user (it shows the dialog box where the user can pick which file they want to open).
            ///.ShowDialog() also returns a type of DialogResult value.
            ///DialogResult.OK checks if the user has pressed the OK button, or in this case, has successfully chosen a file and pressed OPEN. 
            ///The line above is saying that if the OpenFileDialog dialog returns OK, then the comparision statement is true.
            ///This, in extension, means that if the user presses the open button, then the code within the curly braces is executed. 
            {
                string path = fileDialog.FileName; //this line obtains the path of the file that the user has chosen and stores it in a string variable called "path".
                ///fileDialog stores a reference to the file the user has selected. As a result, .FileName allows me to access the file path property of the reference file.
                ///If I wanted to actual name of the file, I would use .SafeFileName. 
                lblFileLocation.Text = path; //This line sets the Text property of the label object to the chosen file's path. 
                //This provides the user with the path of the file they chose and can help them confirm that they chose the right file. 
                //.Text allows me to access the Text property of the lblFileLocation object. 

                bool heading_check = check_headings(path); //declares a bool type variable (true or false) and assigns it the value that check_headings() returns.

                if (heading_check) //if check_headings() returns true. If it doesn't return true, the continue button will not be made visible. 
                {
                    Tuple<Employee[], bool> return_vals = scan_employees(path); //the TUPLE value returned from scan_employees() is stored in a variable called return_vals

                    if (return_vals.Item2 == true) //Tuples don't use indices. They use .Item<number> to get Item. They are also non-zero based so Item2 refers to second item.
                        //Conditional checks if second item of returned values is true. It would mean that everything was formatted correctly and the data is loaded successfully. 
                    {
                        btnContinue.Visible = true; //the btnContinue object's Visible property is changed to true. This allows the user to close the modal InsertFilePrompt form and continue on to the main program.
                    }
                    else //the following code is executed if the if conditional is not fulfilled
                    {
                        lblFileLocation.Text = ""; //changes lblFileLocation label Text value to "".
                        //This happens if the user uploads a database that's not formatted properly. It removes the file path from the label so the user doesn't think that the file is still loaded.
                        //Indirectly tells them that they need to reupload the file. 
                    }
                }
                else
                {
                    lblFileLocation.Text = ""; //discussed above
                }
            }
        }

        private void btnCreateData_Click(object sender, EventArgs e) //Event listener for if Create New button is pressed
        {
            SaveFileDialog saveDialog = new SaveFileDialog(); //similar to OpenFileDialog except this creates an instance of the SaveFileDialog class.
            //This class is used to save files. The user can choose to name the file whatever they wish. 
            saveDialog.Filter = "Excel File (*.xlsx)|*.xlsx"; //Same idea as before but for SaveFileDialog. OpenFileDialog and SaveFileDialog both inherit from the FileDialog class.
            //As a result, .Filter works with both of them. 
           
            if (saveDialog.ShowDialog() == DialogResult.OK) //same idea as before except that .ShowDialog() returns OK if the user presses the SAVE button.
            {
                string newpath = saveDialog.FileName; //same idea as before except that this obtains the path to where the user wants to save the file. 
                try
                {
                    File.WriteAllBytes(newpath, Resources.database_template);
                    ///The line above is responsible for creating a copy of the database_template.xlsx file (stored in Resources). 
                    ///I have embedded the database template file into the project resources. As a result, the template file has become a permanent part of the project's assembly.
                    ///Doing this is helpful as I won't have to write extra code to obtain the relative path of the template file on the user's machine. I can simply refer to it using code.
                    ///Properties.Resouces.<filename> is used to recall the embedded file from the project's resources. 
                    ///The template file is an Excel file and since Excel files are encoded in Binary, they are considered to be a Binary file type by the program.
                    ///As such, the program accesses the file as byte[]. File.WriteAllBytes(path, file) is used to create a copy of the template file.
                    ///As evident, the first argument is where the copy is going to be stored and the second argument is the file that is being copied. 
                    ///Using .WriteAllBytes is necessary as the program is accessing the Excel template file as bytes[].
                }
                catch
                {
                    MessageBox.Show("The program currently does not have the functionality to overwrite files. Please save the file separately.");
                    //Due to limited time, I decided not to implement a solution that would allow the user to overwrite files.
                    return; //prematurely exists the function so no COM objects are created
                }
                returned_path = newpath; //This  creates a copy of the reference to the value of newpath that is stored in the memory.
                //The path variable is also a reference to its value that is stored in the memory. The reference is stored in the class property which will be used to access
                //the value from the parent form. 
                returned_emp_list = new Employee[0]; //sets the property value to a 0 length array. This is so the property doesn't return null and cause errors. 

                //SAVING LAST OPENED DATE 
                _Excel.Application excel = new _Excel.Application(); //Same task as when used before
                _Excel.Workbook wb = excel.Workbooks.Open(newpath); //Same task as when used before
                _Excel.Worksheet ws = wb.Sheets["Database"]; //Same task as when used before 

                ws.Cells[30][3].Value = string.Format("'" + DateTime.Now.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.GetCultureInfo("en-US"))); //ws.Cells[i][j] accesses the cell in the ith column, jth row.
                //.Value allows the program to access the cell's value. DateTime.Now uses the datetime library to create a datetime object with the current date and time.
                //.ToString() allows user to choose the format they want the datetime value to be parsed as. 
                wb.Save(); //This performs the same function as pressing Ctrl S in Excel.

                close_excel_COM(excel, wb, ws); //closes all COM objects

                this.DialogResult = DialogResult.OK; //Changes the DialogResult value of this form to DialogResult.OK. Otherwise, it would be DialogResult.Cancel.
                Close(); //This lines simply closes the InsertFilePrompt form. Once the user has created a new database, there is no need to 
                ///check if the database and its information is formatted correctly. The database is empty so it has no information and since it was generated using the provided template, it is formatted correctly.
                ///Closing the InsertFilePrompt form gives the user access to the main program. 
            }
        }

        private void btnContinue_Click(object sender, EventArgs e) //event listener for if the Continue button is pressed
        {
            this.DialogResult = DialogResult.OK;
            Close(); //closes the InsertFilePrompt form
        }
    }
}
