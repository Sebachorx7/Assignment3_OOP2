using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    internal class Services
    {
        // Properties.
        //Produce a list of objecs numbered.
        private static int Count;
        // Hold the id number of the owner of the car as integer.
        private int IdentificationNumber = 0;
        // Hold the first name of the customer, starts empty by default.
        private string firstName = string.Empty;
        // Hold the last name of the customer, starts empty by default.
        private string lastName = string.Empty;
        // Hold the phone number of the customer as integer.
        private int phoneNumber;
        // Hold the make of the car.
        private string make;
        // Hold the model of the car, starts empty by default.
        private string model = string.Empty;
        // Hold the colour of the car, starts empty by default.
        private string colour = string.Empty;
        // Hold the year of the car as integer.
        private int year;
        // Hold information about the change services, if is tru or false
        private bool EngineOilChange;
        private bool TransOilChange;
        private bool AirFilterChange;
        // Hold the cost of the service as decimal number.
        private decimal Cost;
        // Set the cost of the change services.
        private const decimal EngineOilChangeCost = 60;
        private const decimal TransOilChangeCost = 120;
        private const decimal AirFilterChangeCost = 40.5m;
        // Set the cost of the taxes.
        private const decimal TaxRate = 0.13m;
        // Constructor of class Services
        public Services()
        {
            //  Incremens the value of Count variable
            Count++;
            // Set the same value of Count to Id of the customer 
            IdentificationNumber = Count;
        }

        // Constructor to inicialize the properties of the instances.
        public Services(string FirstName, string LastName, int PhoneNumber, string Make, string Model, string Colour, int Year, bool engineOilChange, bool transOilChange, bool airFilterChange, decimal cost)
        {
            // Each of the  arguments, represents the variables used, make a relations between the arguments and the variables
            firstName = FirstName;
            lastName = LastName;
            phoneNumber = PhoneNumber;
            make = Make;
            model = Model;
            colour = Colour;
            year = Year;
            EngineOilChange = engineOilChange;
            TransOilChange = transOilChange;
            AirFilterChange = airFilterChange;
            ServicesCost = cost;
            // If the next operations are true, add the cost of the service to the ServiceCost variable.
            // If its selected two or more options, the ServiceCost variable is going to hold the total.
            if (EngineOilChange)
            {
                ServicesCost += EngineOilChangeCost;
            }
            if (TransOilChange)
            {
                ServicesCost += TransOilChangeCost;
            }
            if (AirFilterChange)
            {
                ServicesCost += AirFilterChangeCost;
            }
            // Calculates the final cost, add to the cost of taxes.
            ServicesCostWithTaxes = ServicesCost * (1 + TaxRate);
        }


        // Set properties.
        // Used to access to the change services values.
        public decimal ServicesCost { get; set; }
        // Used to access to the final cost with taxes of the services.
        public decimal ServicesCostWithTaxes { get; private set; }
        // Obtain the value of Count variable, read only.
        public static int customerCount
        {
            get { return Count; }
        }
        // Obtain the value of the Id of the customer variable, read only.
        public int customerIdentificationNumber
        {
            get { return IdentificationNumber; }
        }
        // Obtain the value of the first name of the customer variable, allows you to establish a new value for the variable. 
        public string customerFirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        // Obtain the value of the last name of the customer variable, allows you to establish a new value for the variable.
        public string customerLastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        // Obtain the value of the phone number of the customer variable, allows you to establish a new value for the variable.
        public int customerPhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        // Obtain the value of the make of the car variable, allows you to establish a new value for the variable.
        public string carMake
        {
            get { return make; }
            set { make = value; }
        }
        // Obtain the value of the model of the car variable, allows you to establish a new value for the variable.
        public string carModel
        {
            get { return model; }
            set { model = value; }
        }
        // Obtain the value of the color of the car variable, allows you to establish a new value for the variable.
        public string carColour
        {
            get { return colour; }
            set { colour = value; }
        }
        // Obtain the value of the year  of the car variable, allows you to establish a new value for the variable.
        public int carYear
        {
            get { return year; }
            set { year = value; }
        }
        // Obtain the value of the changes services variables, allows you to establish a new value for the variables.
        public bool serviceEngineOilChange
        {
            get { return EngineOilChange; }
            set { EngineOilChange = value; }
        }
        public bool serviceTransOilChange
        {
            get { return TransOilChange; }
            set { TransOilChange = value; }
        }
        public bool serviceAirFilterChange
        {
            get { return AirFilterChange; }
            set { AirFilterChange = value; }
        }
        // Obtain the value of the cost of the service variable, allows you to establish a new value for the variable.
        public decimal serviceCost
        {
            get { return Cost; }
            set { Cost = value; }
        }

       
    }
}
