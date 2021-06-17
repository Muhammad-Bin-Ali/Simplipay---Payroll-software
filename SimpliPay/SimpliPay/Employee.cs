using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SimpliPay 
{
    public class Employee
        ///This is a class. A class is a concept that is fairly prevalent in object-oriented programming and it is used to create "templates" for objects.
        ///For example, let's say that we wanted to make a class for cars so that we are able to create car objects on the fly while we are coding.
        ///In our car template, we can choose the parameters that the user may choose to define upon the creation of a car object.
        ///Parameters such as the colour of the car, its manufacturer, how many KM it has driven, etc.
        ///Once we have created a functioning class, we can refer back to it whenever we need to create an object of that class.
        ///Extending the previous example, let's say that I now need to store a car object in my computer's memory. I would instantiate a Car class object and I would fill out
        ///the values for the parameters of the class. If the car is from Hyundai, I can store that piece of information in that given Car object. 
        
        //the word public is an access modifier and it is used to tell the program that the user wants to be able to access this class from anywhere within this namespace. 
        //If it said private, we won't be able to use the Employee class 

        ///The Employee class has been created for employee profiles. Rather than storing an employee's information in hashed lists or parallel arrays, storing it
        ///in their respective Employee class object will allow me to code more effeciently and effectively. The fields of this class have been discussed in my proposal
        ///but essentially, these are the fields that each employee profile in the payroll software will have. 
    {
        //all these variables have been declared as public so they can be accessed from outside of the class.
        public string first_name; //Employee's first name. String data type because names are words and not composed of numbers. They also don't need to undergo any arithmetic operations.
        public string middle_name; //Same as above but for Employee's middle name.
        public string last_name; //Same as above but for Employee's last name. 

        public string SIN; //Employee's Social Insurance Number. Also a string data type because, even though its composed of digits, it will not undergo any sort of arithmetic processing.

        public DateTime birth_date; //This is a DateTime object. The DateTime libary is a library provided by Microsoft. Its used for variables that are responsible for storing dates.
        //Creating a DateTime object instead of storing the date as a string makes manipulating the stored date significantly easier. This variable is used for employee's date of birth. 
        public DateTime date_hire; //Same as above but for employee's date of hire. 

        public string ID; //Employee's employee number. Reason for string data type is same as sin_num. 
        public string email; //Same as above but for employee's email.
        public string phone_num; //Same as sin_num but for employee's phone number.

        public string address_1; //Same as first_name but for employee's address line 1.
        public string address_2; //Same as above but for employee's address line 2.

        public string postal_code;
        public string city; //Same as above but for employee's city of work.
        public string province; //Same as above but for employee's province of work.

        public string pay_prd; //Same as above but for employee's pay period. 
        public string type; //Same as above but for employee type. Whether they are salaried or non-salaried.
        public double pay_rate; //Employee's pay rate. This is a double data type as it will undergo arithmetic operations. It may also be a decimal number which is why it's not an int/long data type. 

        public DateTime vac_year; //Same as emp_date_hire but for employee's vacation entitlement year. 

        public double vac_pay_perc; //Same as emp_pay_rate but for employee's vacation pay percentage. This number will always be below 1.00. However, since that's still a decimal, double seemed like the most appropriate data type choice. 

        public double ytd_total_pay; //Year-to-date total earned pay. How much they have been paid so far for the tax year. Resets every year on April 30. double because it may be a decimal number
        public double ytd_vac_pay; //Year-to-date vacation pay. Same as above but for vacation pay.
        public double ytd_cpp; //Year-to-date total CPP deductions. Same as above but for CPP deductions.
        public double ytd_EI; //Year-to-date total EI deductions. Same as above but for EI deductions.
        public double ytd_tax_ded; //Year-to-date prov/federal tax deductions. Same as above but for prov/federal tax deductions. 
        public double ytd_other_pay; //Year-to-date other earnings.

        public string cc; //Tax claim code. Not going to go under any arithmetic processing so it's best to store in string. 
    }
}
