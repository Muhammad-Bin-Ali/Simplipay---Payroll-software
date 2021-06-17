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
using System.Runtime.InteropServices; //used for ReleaseCOMObject method
using SimpliPay.Properties; //to access project's embedded files
using System.IO;

namespace SimpliPay
{
    public partial class FinalizePayRoll : UserControl
    {
        private StartPayRoll parent_parent; //to store parent instance of startpayroll
        private ChooseEmployees parent; //declares property to store ChooseEmployees reference
        private FrmMain super_parent; //to store the very main form. 

        public FinalizePayRoll(StartPayRoll parent_parent_passed, ChooseEmployees parent_form_passed, FrmMain form_passed) //parent_form is passed into constructor method
        {
            super_parent = form_passed; //creates reference
            parent_parent = parent_parent_passed; //creates reference
            parent = parent_form_passed; //it's stored in a class-wide property. 
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e) //event listener for when btnBack is clicked
        {
            parent.Controls.Remove(this); //removes instance of THIS form from parent form's controls. 
            this.Dispose(); //clears it from memory
        }

        private void close_excel_COM(_Excel.Application excel, _Excel.Workbook wb, _Excel.Worksheet ws) //The specifics of what a method is are discussed in the method below.
        //This method is responsible for releasing (closing) the COM objects that were created in the method below. 
        {
            //COM CLEANUP - important as without this, the Excel application would still be running in the background and as a result, the user would not be able to edit the file they uploaded. 
            Marshal.ReleaseComObject(ws); //This method is used to release the COM object that's within the brackets. By releasing the COM object, it is essentially ending it and preventing it from running in the background after the program is closed. 
            //This line is used to release the Excel Worksheet. 

            wb.Close(0); //This line simply closes the Workbook that is open in the background. 
            Marshal.ReleaseComObject(wb); //Same task as above except for the Workbook. 

            excel.Quit(); //This lines quit the Excel application and fully terminates the Excel task in the background. Without this, Excel would still be running in the background. 
            Marshal.ReleaseComObject(excel); //Same task as above except for Excel application. 
        }

        private double get_tax_ded(string cc, double pay, Employee employee, string path) //this method will be used to calculate and store tax deduction for each employee
        {
            double pay_period = 0; //declares an empty variable called pay_period. 
            byte prd_index = 0; //Same as above but for period_index. Byte because the highest value it will ever have is 3.

            if (employee.pay_prd.ToLower() == "12/year") //If passed employee's pay period is 12 per year
            {
                prd_index = 0; //sets prd_index to 0 (discussed later)
            }
            else if (employee.pay_prd.ToLower() == "24/year") //same as above but if its 24 per year
            {
                prd_index = 1; //sets prd_index to 1
            }
            else if (employee.pay_prd.ToLower() == "26/year") //same as above but if its 26 per year
            {
                prd_index = 2; //sets value to 2
            }
            else if (employee.pay_prd.ToLower() == "52/year") //If its 52 per year
            {
                prd_index = 3; //sets index to 3
            }

            //DECLARING 2-D ARRAYS THAT CONTAIN EMBEDDED TAX DEDUCTION TABLES FOR EACH PROVINCE
            byte[][] alberta = new byte[][] { Resources.alberta12, Resources.alberta24, Resources.alberta26, Resources.alberta52};
            ///Excel files are interpreted as byte[]. Since I needed an array of "arrays", I needed to use byte[][] instead of just byte[].
            ///Byte[][] declares a 2 dimensional array that can populated with single dimensional arrays. This array stores deduction tables for Alberta.
            ///Index 0 is 12 pay periods/year, index 1 is 24, index 2 is 26, and index 3 is 52.
            byte[][] bc = new byte[][] { Resources.bc12, Resources.bc24, Resources.bc26, Resources.bc52 }; //same as above but for british columbia
            byte[][] manitoba = new byte[][] { Resources.manitoba12, Resources.manitoba24, Resources.manitoba26, Resources.manitoba52 }; //same as above but for manitoba
            byte[][] nb = new byte[][] { Resources.nb12, Resources.nb24, Resources.nb26, Resources.nb52 }; //same as above but for new brunswick
            byte[][] nfld = new byte[][] { Resources.nfld12, Resources.nfld24, Resources.nfld26, Resources.nfld52 }; //same as above but for Newfoundland and Labrador
            byte[][] nwt = new byte[][] { Resources.nwt12, Resources.nwt24, Resources.nwt26, Resources.nwt52 }; //same as above but for northwest territories
            byte[][] ns = new byte[][] { Resources.ns12, Resources.ns24, Resources.ns26, Resources.ns52 }; // same as above but for nova scotia
            byte[][] nunavut = new byte[][] { Resources.nunavut12, Resources.nunavut24, Resources.nunavut26, Resources.nunavut52 }; //same as above but for nunavut
            byte[][] ontario = new byte[][] { Resources.ontario12, Resources.ontario24, Resources.ontario26, Resources.ontario52 }; //same as above but for ontario
            byte[][] pei = new byte[][] { Resources.pei12, Resources.pei24, Resources.pei26, Resources.pei52 }; //same as above but for prince edward island
            byte[][] quebec = new byte[][] { Resources.quebec12, Resources.quebec24, Resources.quebec26, Resources.quebec52 }; //same as above but for quebec
            byte[][] sask = new byte[][] { Resources.sask12, Resources.sask24, Resources.sask26, Resources.sask52 }; //same as above but for saskatchewan
            byte[][] yukon = new byte[][] { Resources.yukon12, Resources.yukon24, Resources.yukon26, Resources.yukon52 }; //same as above but for yukon

            byte[][][] tax_tables = new byte[][][] //instantiates a 3d dimension array. This arry will contain the previously instantiated 2d arrays.
            {
                alberta, bc, manitoba, nb, nfld, nwt, ns, nunavut, ontario, pei, quebec, sask, yukon //populates array in order. 
            };

            string[] provinces = new string[] {"alberta", "british columbia", "manitoba", "new brunswick",
                "newfoundland and labrador", "northwest territories", "nova scotia", "nunavut", "ontario", "prince edward island", "quebec", "saskatchewan","yukon"};
            //creates a paralle string array. Each index of this array corresponds to its deduction tables in the "tax_tables" object. 

            int index = Array.IndexOf(provinces, employee.province.ToLower()); //Array.IndexOf(array, object) finds index of object in given array.
            //In this case, it checks which index the employee's province falls on. As mentionted before, this index will correspond to the tax_tables used.
            //If employee is from Alberta, it would return 0. I would use that to access the 0th index in tax_tables.

            //FEDERAL AND PROVINCIAL TAX DED.
            double fed_ded = 0; //declares empty variable to hold federal deduction
            double prov_ded = 0; //declares empty variable to hold provincial deduction
            int upper = 0; //upper pay limit of given row.
            int lower = 0; //low pay limit of given row.
            //If it said 100 - 121, 100 would be lower and 121 would be upper. 

            string file_path = Path.Combine(path, "tempfile.xlsx"); //Uses Path.Combine(pathargs) to create a path for a temporary file named tempfile.xlsx.
            //Path.Combine simply combines arguments into one cohesive path string. It takes care of all the formatting. It returns a string. 
            File.WriteAllBytes(file_path, tax_tables[index][prd_index]); //This was disccused before in InsertFilePrompt but essentially, it creates a copy of an Excel file.
            //First argument is where the copy will be stored. The second argument is the file itself. In this case, the file is being accessed from the tax_tables.
            //The index that was returned from cross-reference the "provinces" string array is used to access the respective byte[][] array. The second index corresponds to the
            //pay period in question. At the start of the method, we assigned pay period a value. If the employee's pay prd was 52, they would have a prd_index of 3.
            //This corresponds to the 4th object in each byte[][] object. 

            //Discussed these many times before. These lines simply create COM objects to write to or read from excel files.
            _Excel.Application temp_excel = new _Excel.Application();
            _Excel.Workbook temp_wb = temp_excel.Workbooks.Open(file_path);
            _Excel.Worksheet temp_ws = temp_wb.Sheets[1];

            if (index != 10) //if returned index is not equal to 10. In other words, if the employee's province is not quebec. (quebec has different format for its tax_table).
            {
                //FEDERAL
                for (int i = 3; i <= 57; i++) //the first federal tax_table of each province starts at row 3 and ends at row 57. This for loop repeats 54 times and the value of
                    //i goes from 3 to 58 b4 the for loop quits.  i will be used to access rows.
                {
                    string untrimmed = Convert.ToString(temp_ws.Cells[1][i].Value); //Pay ranges are in first column in each row.
                    //I retrieve the value and convert it to string in case it returns "" (it shouldn't but just in case).
                    string[] trimmed = untrimmed.Split(' '); //the untrimmed area is split according to the spaces. Each untrimmed object has 3 values.
                    //the first is the lower limit. The second is a hyphen. The third is the upper limit.
                    //These 3 objects are separated by white spaces. .Split(' ') essentially returns an array after the initial string has been cut up 
                    //at the char that was provided in the .Split argument.

                    if (trimmed.Length == 1) //The only range that has one object is the first one. It doesn't explicitly say 0 - (upper limit). It just says (upper limit)
                    {
                        lower = 0; //makes lower 0 since that's what its supposed to be
                        upper = int.Parse(trimmed[trimmed.Length - 1]); //the upper limit is always the last object in the trimmed array.
                        //They are stored in ints because the ranges are all ints.
                    }
                    else //If it has an explicit lower and upper limit value 
                    {
                        lower = int.Parse(trimmed[0]); //the first object in the array will always be the lower limit.S
                        upper = int.Parse(trimmed[trimmed.Length - 1]); //discussed before. 
                    }

                    if (lower <= pay && pay < upper) //If the pay is equal to greater than the lower limit but smaller than the upper limit.
                    {
                        byte tax_code = byte.Parse(cc); //gets the employee's claim code and converts it to byte. cc will always be less than 10 so no need for int.

                        try //This is used in case the employee's pay is low and they don't have an entry for their given cc. In other words, if their cell 
                        //does not contain a deduction value, it would create an error.
                        {
                            fed_ded = temp_ws.Cells[tax_code + 2][i].Value;  //tax_code + 2 is done because that's how the columns are arranged.
                            //tax_code + 2 gives me access to the column I need for the given cc.
                        }
                        catch
                        {
                            //DO NOTHING. The person's pay range and cc don't have a value.
                            break; //breaks out of this for loop. The employee doesn't have a value in the federal table. No point in looping through the rest. 
                        }
                    }
                }

                //Each page is separated by a 2 row thick header. All but the first page. The first page has a 3 row thick header. AS a result, I've separated the first page and the remaining pages.
                int start = 61; //Second page of federal table starts on row 61
                int end = 115; //ends on row 115. 

                for (int page = 0; page < 6; page++) //loops 6 times because 6 pages left. 
                {
                    if (fed_ded != 0) //If a value for already detected on page 1 or a previous page, no need to loop through the full loop. 
                    {
                        break; //breaks for loop
                    }

                    for (int i = start; i <= end; i++) //Same idea as before. Loops from first row of page to last row. S
                    {
                        //SAME IDEA AS BEFORE
                        string untrimmed = Convert.ToString(temp_ws.Cells[1][i].Value);
                        string[] trimmed = untrimmed.Split(' ');

                        lower = int.Parse(trimmed[0]);
                        upper = int.Parse(trimmed[trimmed.Length - 1]);

                        if (trimmed.Length == 1)
                        {
                            lower = 0;
                            upper = int.Parse(trimmed[trimmed.Length - 1]);
                        }
                        else
                        {
                            lower = int.Parse(trimmed[0]);
                            upper = int.Parse(trimmed[trimmed.Length - 1]);
                        }

                        if (lower <= pay && pay < upper)
                        {
                            byte tax_code = byte.Parse(cc);

                            try
                            {
                                fed_ded = temp_ws.Cells[tax_code + 2][i].Value;
                            }
                            catch
                            {
                                //DO NOTHING. The person's pay range and cc don't have a value.
                                break;
                            }
                        }
                    }

                    // adds 57 to both start and end to move to next page. 
                    start += 57;
                    end += 57;
                }

                //provincial
                start = 403; //provincial table starts at row 403
                end = 457; //first page ends at row 457
                for (int i = start; i <= end; i++) //same idea as previous for loop
                {
                    //Same idea as before
                    string untrimmed = Convert.ToString(temp_ws.Cells[1][i].Value);
                    string[] trimmed = untrimmed.Split(' ');

                    if (trimmed.Length == 1)
                    {
                        lower = 0;
                        upper = int.Parse(trimmed[trimmed.Length - 1]);
                    }
                    else
                    {
                        lower = int.Parse(trimmed[0]);
                        upper = int.Parse(trimmed[trimmed.Length - 1]);
                    }

                    if (lower <= pay && pay < upper)
                    {
                        byte tax_code = byte.Parse(cc);

                        try
                        {
                            prov_ded = temp_ws.Cells[tax_code + 2][i].Value;
                        }
                        catch
                        {
                            //DO NOTHING. The person's pay range and cc don't have a value.
                            close_excel_COM(temp_excel, temp_wb, temp_ws); //closes COM objects
                            File.Delete(file_path); //deletes the temporary file that was created. The temp file was used to read tables. Excel Interop cannot access embedded resources directly. As such, I need to make a copy of them. 
                            return (prov_ded + fed_ded); //If there is no value for employee on table, no point in continue. Returns both values. prov_ded would have a value of 0.
                            //A SUM IS RETURNED BECAUSE TAX DEDUCTION IS FED. + PROV.
                        }
                    }
                }

                start = 461; //Same idea as before. Second page of prov tables starts at row 461
                end = 515; //ends at row 515

                for (int page = 0; page < 6; page++) //Same idea as before. 6 Pages left. For loop repeats 6 times. 
                {
                    for (int i = start; i <= end; i++) //same as before
                    {
                        //Same as before
                        string untrimmed = Convert.ToString(temp_ws.Cells[1][i].Value);
                        string[] trimmed = untrimmed.Split(' ');

                        lower = int.Parse(trimmed[0]);
                        upper = int.Parse(trimmed[trimmed.Length - 1]);

                        if (trimmed.Length == 1)
                        {
                            lower = 0;
                            upper = int.Parse(trimmed[trimmed.Length - 1]);
                        }
                        else
                        {
                            lower = int.Parse(trimmed[0]);
                            upper = int.Parse(trimmed[trimmed.Length - 1]);
                        }

                        if (lower <= pay && pay < upper)
                        {
                            byte tax_code = byte.Parse(cc);

                            try
                            {
                                prov_ded = temp_ws.Cells[tax_code + 2][i].Value;
                            }
                            catch
                            {
                                //DO NOTHING. The person's pay range and cc don't have a value.
                                close_excel_COM(temp_excel, temp_wb, temp_ws);
                                File.Delete(file_path);
                                return (prov_ded + fed_ded);
                            }
                        }
                    }
                    //same as before
                    start += 57;
                    end += 57;
                }
                
                //If both tables have been iterated through, then prov_ded and fed_ded should have a value by now. The only way they won't is if the employee is paid an amount
                //that's not listed on the table. In that case, their employer would use a different formula to calculate their tax deductions. Due to time contraints, I am unable to implement that. 
                close_excel_COM(temp_excel, temp_wb, temp_ws); // closes COM objects
                File.Delete(file_path); //deletes temp files
                return prov_ded + fed_ded; //returns files
            }

            else //if employee is employed in Quebec
            {
                //FEDERAl
                //SAME IDEA AS BEFORE
                //Quebec's files only have a federal table. Aside from that, they follow the same format. As such, I can copy past my code from above. 
                for (int i = 3; i <= 57; i++) //loops from row 3 to 57 on first page.
                {
                    //SAME IDEA AS BEFORE
                    string untrimmed = Convert.ToString(temp_ws.Cells[1][i].Value);
                    string[] trimmed = untrimmed.Split(' ');

                    if (trimmed.Length == 1)
                    {
                        lower = 0;
                        upper = int.Parse(trimmed[trimmed.Length - 1]);
                    }
                    else
                    {
                        lower = int.Parse(trimmed[0]);
                        upper = int.Parse(trimmed[trimmed.Length - 1]);
                    }

                    if (lower <= pay && pay < upper)
                    {
                        byte tax_code = byte.Parse(cc);

                        try
                        {
                            fed_ded = temp_ws.Cells[tax_code + 2][i].Value;
                        }
                        catch
                        {
                            //DO NOTHING. The person's pay range and cc don't have a value.
                            close_excel_COM(temp_excel, temp_wb, temp_ws);
                            File.Delete(file_path);
                            return (prov_ded + fed_ded); //SAME IDEA AS BEFORE
                        }
                    }
                }

                //SAME IDEA AS OTHER FEDERAL TABLES
                int start = 61;  //second page starts on row 61
                int end = 115; //ends on row 115

                for (int page = 0; page < 6; page++) //for loop repeats 6 times, once for each page left. 
                {
                    if (fed_ded != 0) //if a value for fed_ded was found on page 1. No need to run this loop
                    {
                        break; //breaks it
                    }

                    for (int i = start; i < end; i++) //SAME IDEA AS BEFOER
                    {
                        //SAME AS BEFORES
                        string untrimmed = Convert.ToString(temp_ws.Cells[1][i].Value);
                        string[] trimmed = untrimmed.Split(' ');

                        lower = int.Parse(trimmed[0]);
                        upper = int.Parse(trimmed[trimmed.Length - 1]);

                        if (trimmed.Length == 1)
                        {
                            lower = 0;
                            upper = int.Parse(trimmed[trimmed.Length - 1]);
                        }
                        else
                        {
                            lower = int.Parse(trimmed[0]);
                            upper = int.Parse(trimmed[trimmed.Length - 1]);
                        }

                        if (lower <= pay && pay < upper)
                        {
                            byte tax_code = byte.Parse(cc);

                            try
                            {
                                fed_ded = temp_ws.Cells[tax_code + 2][i].Value;
                            }
                            catch
                            {
                                //DO NOTHING. The person's pay range and cc don't have a value.
                                close_excel_COM(temp_excel, temp_wb, temp_ws);
                                File.Delete(file_path);
                                return (prov_ded + fed_ded); //SAME AS BEFORE
                            }
                        }
                    }

                    //SAME AS BEFORE
                    start += 57;
                    end += 57;
                }
                close_excel_COM(temp_excel, temp_wb, temp_ws);
                File.Delete(file_path);
                return fed_ded;
                //same idea as before. If the employee gets to here, they should have a value for fed_ded. If they don't then their pay is not on table. 
                //Quebec's tables don't have prov deductions. No need to return that. 
            }
        }

        private double get_CPP_ded(double pay, Employee employee, string path)
        {
            byte prd_index = 0; //Same as above but for period_index. Byte because the highest value it will ever have is 3.

            if (employee.pay_prd.ToLower() == "12/year") //If passed employee's pay period is 12 per year
            {
                prd_index = 0; //sets prd_index to 0 (discussed later)
            }
            else if (employee.pay_prd.ToLower() == "24/year") //same as above but if its 24 per year
            {
                prd_index = 1; //sets prd_index to 1
            }
            else if (employee.pay_prd.ToLower() == "26/year") //same as above but if its 26 per year
            {
                prd_index = 2; //sets value to 2
            }
            else if (employee.pay_prd.ToLower() == "52/year") //If its 52 per year
            {
                prd_index = 3; //sets index to 3
            }

            //DECLARING 2-D ARRAYS THAT CONTAIN EMBEDDED CPP TABLES
            byte[][] federal = new byte[][] { Resources.cpp12, Resources.cpp24, Resources.cpp26, Resources.cpp52 }; //same idea as when used before but for CPP tables
            byte[][] quebec = new byte[][] { Resources.quebecqpp12, Resources.quebecqpp24, Resources.quebecqpp26, Resources.quebecqpp52 }; //same as above but for QPP Tables

            byte[] federal_pages = new byte[] { 140, 71, 66, 34 }; //creates a parallel array to federal for how many there are in ith file.

            double lower = 0; //lower limit of range
            double upper = 0; //upper limit of range

            if (employee.province.ToLower() != "quebec") //if employee provice is not quebec (quebec has different numbers)
            {
                string file_path = Path.Combine(path, "tempfile.xlsx"); //Uses Path.Combine(pathargs) to create a path for a temporary file named tempfile.xlsx.
                //Path.Combine simply combines arguments into one cohesive path string. It takes care of all the formatting. It returns a string. 
                File.WriteAllBytes(file_path, federal[prd_index]); //This was disccused before in InsertFilePrompt but essentially, it creates a copy of an Excel file.
                //First argument is where the copy will be stored. The second argument is the file itself. In this case, the file is being accessed from the tax_tables.
                //If the province is not quebec, the federal files are used. As discussed before, prd_index is used to access the file that corresponds to the employee's pay period. 

                //Discussed these many times before. These lines simply create COM objects to write to or read from excel files.
                _Excel.Application temp_excel = new _Excel.Application();
                _Excel.Workbook temp_wb = temp_excel.Workbooks.Open(file_path);
                _Excel.Worksheet temp_ws = temp_wb.Sheets[1];

                //Same idea as before. Start of first page and end of first page. This time, all pages are separated equally. As such, I won't need to make 2 for loops.
                int start = 3; //first row of first page
                int end = 74; //last row of first page

                byte pages = federal_pages[prd_index]; //gets the number of pages the chosen federal file has. 

                for (int i = 0; i < pages; i++) //loop repeats once for each page. 
                {
                    for (int j = 1; j < 17; j+=4) //roop repeats once for each column. 4 columns so loop will repeat 4 times. 
                    {
                        for (int k = start; k <= end; k++) //loop will repeat once for each row on page.
                        {
                            double upper_corner = 0; //value for upper range in the bottom right corner of page

                            if (j == 9) //if it's column number 9. Due to merging caused by PDF to excel converters, I've had to use a less effecient and a more cluttered solution
                            {
                                try //All values should be parsable. But this is just in case. 
                                {
                                    lower = double.Parse(temp_ws.Cells[j][k].Value2.ToString()); //gets the lower limit 
                                    upper = double.Parse(temp_ws.Cells[j + 3][k].Value2.ToString()); //gets the upper 
                                    upper_corner = double.Parse(temp_ws.Cells[16][end].Value2.ToString()); //upper limit for bottom right corner. 
                                }
                                catch
                                {
                                    //DO NOTHING. If an error is raised, there's no need to do anything. 
                                }

                                if (lower <= pay && pay <= upper) //if pay is higher than or equal to lower limit and smaller than or equal to upper limit
                                {
                                    double cpp = double.Parse(temp_ws.Cells[j + 4][k].Value2.ToString()); //stores it in variable called cpp. Has different first index due to the formatting issues caused by merged cells.
                                    close_excel_COM(temp_excel, temp_wb, temp_ws); //closes COM objects
                                    File.Delete(file_path); //deletes temp file
                                    return cpp; //returns value
                                }

                                if (pay > upper_corner) //if the value of pay is higher than the upper limit in the bottom right corner cell, then the value we're looking for is not on this page.
                                {
                                    j += 5; //adds 5 to j and breaks this for loop. Causes the program to skip the page by skipping columns. 
                                    break;
                                }

                                j +=5; //adds 5 instead of 4 to make up for irregular columns
                                continue; //loops back into for loop
                            }

                            else if (j == 14) //if its columm 14
                            { 
                                try //same idea as before, different indices
                                {
                                    lower = double.Parse(temp_ws.Cells[j][k].Value2.ToString());
                                    upper = double.Parse(temp_ws.Cells[j + 2][k].Value2.ToString());
                                    upper_corner = double.Parse(temp_ws.Cells[16][end].Value2.ToString());
                                }
                                catch
                                {
                                    //DO NOTHING
                                }

                                if (lower <= pay && pay <= upper) //same as before
                                {

                                    double cpp = double.Parse(temp_ws.Cells[j + 3][k].Value2.ToString()); //All these lines serve the same function as before. Just different index. 
                                    close_excel_COM(temp_excel, temp_wb, temp_ws);
                                    File.Delete(file_path);
                                    return cpp;
                                }

                                if (pay > upper_corner) //same as before
                                {
                                    //no need to do j+=5 here because this is the last column on the page. We don't need to account for any irregularities here. 
                                    break;
                                }
                                continue; //if neither condition is fulfilled, the loop repeats instead of executing following code. 
                            }

                            try //same idea as before.
                            {
                                //Same idea as before for these lines
                                lower = double.Parse(temp_ws.Cells[j][k].Value2.ToString());
                                upper = double.Parse(temp_ws.Cells[j + 2][k].Value2.ToString());
                                upper_corner = double.Parse(temp_ws.Cells[16][end].Value2.ToString());
                            }
                            catch
                            {
                                //DO NOTHING
                            }

                            if (pay > upper_corner) //Same idea as before
                            {
                                break;
                            }

                            if (lower <= pay && pay <= upper) //Same idea as before
                            {

                                double cpp = double.Parse(temp_ws.Cells[j + 3][k].Value2.ToString()); //Same idea as before for all these lines
                                close_excel_COM(temp_excel, temp_wb, temp_ws);
                                File.Delete(file_path);
                                return cpp;
                            }
                        }
                    }

                    start += 74; //adds 74 to move on to the first row of next page
                    end += 74; //last row of next page
                }

                close_excel_COM(temp_excel, temp_wb, temp_ws); //if value still isn't found.
                File.Delete(file_path); 
                return 0; //In this case, the employer will take advantage of the other information they have to enter the values manually. 

            }
            else
            {
                string file_path = Path.Combine(path, "tempfile.xlsx"); //Uses Path.Combine(pathargs) to create a path for a temporary file named tempfile.xlsx.
                //Path.Combine simply combines arguments into one cohesive path string. It takes care of all the formatting. It returns a string. 
                File.WriteAllBytes(file_path, quebec[prd_index]); //This was disccused before in InsertFilePrompt but essentially, it creates a copy of an Excel file.
                //First argument is where the copy will be stored. The second argument is the file itself. In this case, the file is being accessed from the tax_tables.
                //If the province is quebec, the quebec files are used. As discussed before, prd_index is used to access the file that corresponds to the employee's pay period. 

                //Discussed these many times before. These lines simply create COM objects to write to or read from excel files.
                _Excel.Application temp_excel = new _Excel.Application();
                _Excel.Workbook temp_wb = temp_excel.Workbooks.Open(file_path);
                _Excel.Worksheet temp_ws = temp_wb.Sheets[1];

                //____________________________________________________________________________________________________________________________________
                //DUE TO TIME CONSTRAINTS, I HAVE NOT IMPLEMENTED QPP. AS A RESULT, EMPLOYEES BASED IN QUEBEC WOULD HAVE 0 IN THEIR QPP CONTRIBUTIONS.
                //____________________________________________________________________________________________________________________________________

                close_excel_COM(temp_excel, temp_wb, temp_ws); //Same idea as before.
                File.Delete(file_path);
                return 0; //returns 0.
            }
        }

        private double get_EI(double pay, Employee employee, string path) //method for calculating EI
        {
            //DECLARING file THAT WILL HOLD REFERENCE TO EI TABLE
            byte[] EI = Resources.EI;
            byte[] quebec = Resources.qppEI;

            if (employee.province.ToLower() != "quebec")
            {
                string file_path = Path.Combine(path, "tempfile.xlsx"); //Uses Path.Combine(pathargs) to create a path for a temporary file named tempfile.xlsx.
                //Path.Combine simply combines arguments into one cohesive path string. It takes care of all the formatting. It returns a string. 
                File.WriteAllBytes(file_path, EI); //This was disccused before in InsertFilePrompt but essentially, it creates a copy of an Excel file.
                //First argument is where the copy will be stored. The second argument is the file itself. In this case, the file is being accessed from the tax_tables.
                //If the province is not quebec, the federal files are used. As discussed before, prd_index is used to access the file that corresponds to the employee's pay period. 

                //Discussed these many times before. These lines simply create COM objects to write to or read from excel files.
                _Excel.Application temp_excel = new _Excel.Application();
                _Excel.Workbook temp_wb = temp_excel.Workbooks.Open(file_path);
                _Excel.Worksheet temp_ws = temp_wb.Sheets[1];

                //Same idea as before.
                int start = 3; //first row of first page
                int end = 74; //last row of first page

                double lower = 0; //lower limit
                double upper = 0; //upper limit

                for (int i = 0; i < 20; i++) //repeats once for each page
                {
                    for (int j = 1; j <= 10; j+=2) // Repeats once for each column
                    {
                        for (int k = start; k <= end; k++) //once for each row on page.
                        {
                            //Same as before
                            try //exact same idea as when doing federal and provincial tax deductions.
                            {
                                string untrimmed = Convert.ToString(temp_ws.Cells[j][k].Value); //retrieves untrimmed range
                                string[] trimmed = untrimmed.Split(' '); //trims it

                                lower = double.Parse(trimmed[0]); //obtains lower limit
                                upper = double.Parse(trimmed[trimmed.Length - 1]); //upper limit
                            }
                            catch
                            {
                                //do nothing. NO need to do anything because 99.9999% of values will parse successfully. I've only put this here just in case 
                            }

                            if (lower <= pay && pay < upper) //same idea as before. If pay is greater than or equal to lower limit but lower than upper limit
                            {
                                double ei_ded = temp_ws.Cells[j + 1][k].Value; //stores it in variable called ei_ded
                                close_excel_COM(temp_excel, temp_wb, temp_ws); //closes COM objects
                                File.Delete(file_path);//deletes file
                                return ei_ded; //returns it
                            }

                            if (j == 4) //This is here due to merged columns once again. 
                            {
                                j += 3; //This adds 3 to the counter instead of 2. Accounts for the messsed up formatting
                                continue; //repeats loop
                            }
                        }
                    }

                    start += 74; //first row of next page
                    end += 74; //last row of next page
                }

                //Same functionality as before
                close_excel_COM(temp_excel, temp_wb, temp_ws);
                File.Delete(file_path);
                return 0; //If it gets to here, employee's pay is not on table. In that case, employer will need to take other peices into account and do it manually. 
            }

            else
            {
                //EXACT SAME THING AS ABOVE BUT FOR EI IN QUEBEC. 
                string file_path = Path.Combine(path, "tempfile.xlsx"); //Uses Path.Combine(pathargs) to create a path for a temporary file named tempfile.xlsx.
                //Path.Combine simply combines arguments into one cohesive path string. It takes care of all the formatting. It returns a string. 
                File.WriteAllBytes(file_path, quebec); //This was disccused before in InsertFilePrompt but essentially, it creates a copy of an Excel file.
                //First argument is where the copy will be stored. The second argument is the file itself. In this case, the file is being accessed from the tax_tables.
                //If the province is not quebec, the federal files are used. As discussed before, prd_index is used to access the file that corresponds to the employee's pay period. 

                //Discussed these many times before. These lines simply create COM objects to write to or read from excel files.
                _Excel.Application temp_excel = new _Excel.Application();
                _Excel.Workbook temp_wb = temp_excel.Workbooks.Open(file_path);
                _Excel.Worksheet temp_ws = temp_wb.Sheets[1];
                //same idea as above
                int start = 3;
                int end = 74;

                double lower = 0;
                double upper = 0;

                //SAME IDEA AS ABOVE
                for (int i = 0; i < 20; i++)
                {
                    for (int j = 1; j <= 10; j++) //same idea as above
                    {
                        for (int k = start; k <= end; k++) //same as before
                        {
                            //Same as before
                            try
                            {
                                string untrimmed = Convert.ToString(temp_ws.Cells[j][k].Value); //These lines serve the same functionality
                                string[] trimmed = untrimmed.Split(' ');

                                lower = double.Parse(trimmed[0]);
                                upper = double.Parse(trimmed[trimmed.Length - 1]);
                            }
                            catch
                            {
                                //do nothing
                            }

                            if (lower <= pay && pay < upper) //Same idea
                            {
                                double ei_ded = temp_ws.Cells[j + 1][k].Value;
                                close_excel_COM(temp_excel, temp_wb, temp_ws);
                                File.Delete(file_path);
                                return ei_ded;
                            }

                            if (j == 4) //Same idea as before
                            {
                                j += 3;
                                continue;
                            }
                        }
                    }

                    start += 74; //same idea as before
                    end += 74;
                }

                close_excel_COM(temp_excel, temp_wb, temp_ws); //Same idea as before
                File.Delete(file_path);
                return 0; //Same idea 
            }
        }

        private void store_employee_info(Employee employee, _Excel.Worksheet paystub_ws, _Excel.Workbook paystub_wb) //function used to tranfer employee information
            //onto pay stub.
        {
            paystub_ws.Cells[2][4].Value = employee.address_1; //transfers employee's address line1
            paystub_ws.Cells[6][4].Value = employee.province; //province
            paystub_ws.Cells[8][4].Value = employee.city; //city
            paystub_ws.Cells[9][4].Value = employee.postal_code; //postal code

            paystub_ws.Cells[4][7].Value = employee.ID; //their employee ID
            paystub_ws.Cells[5][7].Value = employee.SIN; //their SIN number

            paystub_ws.Cells[6][7].Value = string.Format("'" + parent_parent.calFROM.SelectionStart.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.GetCultureInfo("en-US"))); //the FROM date of pay period
            paystub_ws.Cells[7][7].Value = string.Format("'" + parent_parent.calTO.SelectionStart.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.GetCultureInfo("en-US"))); //the TO date of pay period
            paystub_ws.Cells[8][7].Value = string.Format("'" + DateTime.Now.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.GetCultureInfo("en-US"))); //The processing date

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

            paystub_ws.Cells[2][7].Value = formatted_name; //the formatted name 
        }

        private void add_pay(FinalizeObj pay, _Excel.Worksheet pay_stub, _Excel.Workbook pay_wb) //method used to add types of pay 
        {
            string type = pay.lblEarnType.Text.ToLower(); //retrieves the type of pay this is

            string[] pay_types = new string[] { "regular", "overtime", "vacation", "commission", "sick pay", "bonus", "personal day", "other earnings" }; //instantiates array that carries different possible types
            byte[] pay_rows = new byte[] { 10, 11, 12, 13, 14, 15, 16, 17 }; //instantiates parallel array that corresponds to row numbers in paystub template

            int index = Array.IndexOf(pay_types, type); //returns int

            pay_stub.Cells[3][pay_rows[index]].Value = pay.lblPayRate.Text; //fills out the rate column of the respective row with the payrate entered on the FinalizeObj object,
            pay_stub.Cells[4][pay_rows[index]].Value = pay.lblHours.Text; //Same as above but for hours
            pay_stub.Cells[5][pay_rows[index]].Value = pay.lblTotalPay.Text; //same as above but for total pay
        }

        private void btnFinalize_Click(object sender, EventArgs e) //event listener for if finalize button is pressed.
        {
            FolderBrowserDialog pathDialog = new FolderBrowserDialog(); //Same thing as opendialog but with FolderBrowserDialog. Prompts user to pick a folder to save in.

            string path = ""; //Empty string is created to store path

            if (pathDialog.ShowDialog() == DialogResult.OK) //same idea as before except that .ShowDialog() returns OK if the user presses the SAVE button.
            {
                path = pathDialog.SelectedPath; //same idea as before except that this obtains the path to where the user wants to save the file. 
            }
            else
            {
                return; //if the user presses cancel, this function is ended prematurely. 
            }

            MessageBox.Show("Please do not close the program till it's finished"); //tells user not to close program.

            int count = 1; //declares a counter variable. Int because it could be over 255.
            string newpath = ""; //placeholder value for newpath.

            while(true) //whilst true until broken. Will repeat until broken by programmer. S
            {
                newpath = Path.Combine(path, "Payroll" + count); //Creates a new path with payroll(count) directory at the very end
                if (Directory.Exists(newpath)) //checks if it already exists. If it does:
                {
                    count++; //adds 1 to value of count
                    continue; //iterates in while loop again.
                }
                else //if it does NOT exist
                {
                    Directory.CreateDirectory(newpath); //creates a folder in that path
                    break;//breaks while loop
                }
            }
            
            //CHECKING NUMBER OF DISTINCT EMPLOYEES
            Employee[] employees = new Employee[0]; //instantiates array of employees with 0 length

            for (int i = 0; i < panelList.Controls.Count; i++) //Loops once for each control in panelList
            {
                if (panelList.Controls[i] is FinalizeObj) //panenList.Controls[i] returns ith control of panelList. If that control is FinalizeObj type, then it returns true. 
                {
                    FinalizeObj pay_obj = (FinalizeObj)panelList.Controls[i]; //discussed in another form but obtain the control, casts it from Control type to given object type,
                    //and stores a reference in a variable

                    if (employees.Contains(pay_obj.employee) == false) //If the employee associated with pay_obj doesn't exist in employees array. 
                    {
                        Employee[] temp = new Employee[employees.Length]; //placeholder array

                        for (int j = 0; j < employees.Length; j++) //repeats once for each object in employees
                        {
                            temp[j] = employees[j]; //transfers objects
                        }

                        employees = new Employee[temp.Length + 1]; //increases length by 1

                        for (int j = 0; j < temp.Length; j++) //loops once for each object in temp
                        {
                            employees[j] = temp[j]; //repopulates original array
                        }

                        employees[employees.Length - 1] = pay_obj.employee; //last object in array is assigned the employee pay_obj is associated with. 
                    }
                }
            }

            //Making pay stub for each employee
            for (int i = 0; i < employees.Length; i++) //repeats once for each employee in employees
            {
                double total_pay = 0; //declares total pay variable for each employee. Sets it to 0.
                Employee employee = employees[i]; //Creates reference for ith employee.

                //GENERATING FILE FOR PAYSTUB
                string pay_stub_path = Path.Combine(newpath, string.Format("{0}_{1}.xlsx", employee.first_name, employee.last_name)); //creates path with employee's name as file name
                File.WriteAllBytes(pay_stub_path, Resources.paystub_template); //uses embedded template resource to copy all bytes to specified path
                //initiates COM objects that will be used to communicate with the given excel file. 
                _Excel.Application paystub_excel = new _Excel.Application(); //Application
                _Excel.Workbook paystub_wb = paystub_excel.Workbooks.Add(pay_stub_path); //Workbook
                _Excel.Worksheet paystub_ws = paystub_wb.Sheets[1]; //Worksheet

                int row = 0; //will be used to store row number of respective employee
                bool stored = false; //just a boolean value telling the program if employee info has been stored in pay stub yet or no

                for ( int j = 0; j < panelList.Controls.Count; j++) //repeats once for each control in panelList
                {
                    if (panelList.Controls[j] is FinalizeObj) //if control is of FinalizeObj type.
                    {
                        double pay; //declares empty variable called pay.
                        FinalizeObj pay_obj = (FinalizeObj)panelList.Controls[j]; //stores control

                        if (pay_obj.employee != employee) //if the employee associated with this object is not the employee we are looping over, it moves forward to the next control.

                        {
                            continue; //moves forward to next control
                        }

                        bool check = double.TryParse(pay_obj.lblTotalPay.Text, out pay); //tries parsing lblTotalPay of pay_obj. It won't parse if the field is empty.

                        if (check == false) //if it doens't parse
                        {
                            MessageBox.Show("Please ensure that you don't have any non-zero totals."); //tell user field is empty
                            close_excel_COM(paystub_excel, paystub_wb, paystub_ws); //closes COM objects so they don't remain open
                            return; //prematurely end function
                        }
                        else //if it does parse correctly
                        {
                            total_pay += pay; //add the pay associated with that object to total pay for employee.

                            if (pay_obj.lblEarnType.Text.ToLower() == "regular") //checks if the pay type associated with this pay_obj is regular
                            {
                                employee.ytd_total_pay += pay; //if it is, adds it to employee's year to date total earned pay. 
                                
                                for (int z = 0; z < parent.employee_list.Length; z++) //loops once for each employee in employee_list
                                {//Essentially, will be used to loop through the ID column. <= so we can find which row employee is in .
                                    if (Convert.ToString(parent.ws.Cells[3][z+3].Value) == employee.ID)  //if ID of that row matches employee's ID
                                    {
                                        parent.ws.Cells[22][z + 3].Value = employee.ytd_total_pay;; //updates employee's ytd total earned pay
                                        row = z + 3; //stores row. 
                                    }
                                }
                            }
                            else if (pay_obj.lblEarnType.Text.ToLower() == "vacation") //Same idea as above but for vacation
                            {
                                employee.ytd_vac_pay += pay; //same idea but for ytd vacation pay

                                for (int z = 0; z < parent.employee_list.Length; z++) //Same idea as before
                                {
                                    if (Convert.ToString(parent.ws.Cells[3][z + 3].Value) == employee.ID) //Same idea as before
                                    {
                                        //Same idea as before
                                        parent.ws.Cells[23][z + 3].Value = employee.ytd_total_pay;
                                        row = z + 3;
                                    }
                                }
                            }
                            else //this is for all other kinds of earnings. Ideally, my software would have a YTD account for each type of earning.
                            //However, given the limited time, it wasn't realistic. 
                            {
                                employee.ytd_other_pay += pay; //adds it to employee's ytd other pay.
                                for (int z = 0; z < parent.employee_list.Length; z++) //same idea as before
                                {
                                    if (Convert.ToString(parent.ws.Cells[3][z + 3].Value) == employee.ID) //same idea as before
                                    {
                                        parent.ws.Cells[27][z + 3].Value = employee.ytd_total_pay; //same idea as before
                                        row = z + 3; //same idea as before
                                    }
                                }
                            }

                            add_pay(pay_obj, paystub_ws, paystub_wb); //runs the add_pay() method with the respective arguments.
                            //This method is used to loop over an employee's payments. For example, if John had a vacation payment and a regular one,
                            //when first invoked, this method will add the vacation payment. Second time, it will add the regular one. 

                            if (stored == false) //if employee info has not been stored in pay stub yet.
                            {
                                store_employee_info(employee, paystub_ws, paystub_wb); //invokes method that stores information
                                stored = true; //changes value to true so COM objects don't perform this method repeatedly
                            }
                        }
                    }
                }

                double CPP = get_CPP_ded(total_pay, employee, newpath); //invokes getCPPDed method to obtain employee's CPP deduction. **QPP NOT CODED YET**
                double EI = get_EI(total_pay, employee, newpath); //same as above but for EI
                double tax_ded = get_tax_ded(employee.cc, total_pay, employee, newpath); //Same as above but for prov and federal tax deductions. These values are stored in double variables

                //FILLING OUT PAYSTUBS
                paystub_ws.Cells[8][10].Value = tax_ded; //fills out the respective tax deduction sections. This line is for prov and federal ded.
                paystub_ws.Cells[8][11].Value = CPP; //This is for CPP ded.
                paystub_ws.Cells[8][12].Value = EI; //This is for EI ded.

                parent.ws.Cells[24][row].Value = employee.ytd_cpp + CPP; //this is to update YTD CPP in original database
                parent.ws.Cells[25][row].Value = employee.ytd_tax_ded + tax_ded; //this is to update YTD federal and prov tax ded.
                parent.ws.Cells[26][row].Value = employee.ytd_EI + EI; //this is to update YTD EI
                parent.wb.Save(); //saves original database

                paystub_ws.Cells[6][10].Value = employee.ytd_total_pay; //Fills out the YTD values on the pay stub. This line fills out YTD total earned pay
                paystub_ws.Cells[6][12].Value = employee.ytd_vac_pay; //same as above but for ytd total vac pay
                paystub_ws.Cells[6][17].Value = employee.ytd_other_pay; //same as above but for ytd other pay
                paystub_ws.Cells[9][10].Value = employee.ytd_tax_ded; //same as above but for YTD prov and federal deductions
                paystub_ws.Cells[9][11].Value = employee.ytd_EI; //same as above but for YTD EI
                paystub_ws.Cells[9][12].value = employee.ytd_cpp; //same as above but for YTD CPP

                paystub_wb.Save(); //saves paystub workbook
                
                //UPDATING CLASS OBJECTS
                employee.ytd_cpp += CPP; //updates YTD CPP 
                employee.ytd_EI += EI; //updates YTD EI
                employee.ytd_tax_ded += tax_ded; //updates ytd tax_ded

                //CLEAN UP 
                close_excel_COM(paystub_excel, paystub_wb, paystub_ws); //closes and release COM objects
                //repeats again if there is another employee in pay run.
            }

            //AFTER ALL EMPLOYEES HAVE BEEN PAID
            MessageBox.Show("Your payroll has been completed"); //tells user their payroll has been completed

            super_parent.panelMain.Controls.Remove(parent_parent); //removes this object's very parent form. As a result, it removes this and its children as well. 
            parent_parent.Dispose(); //Disposes of the parent in the memory. Since this form and its preceding forms were children of that parent (StartPayRoll instance),
            //they too will be disposed of. 

        }
    }
}
