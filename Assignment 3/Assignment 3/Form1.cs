using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Assignment_3
{/// <summary>
/// Sebastian Romero Gonzalez - 100886859
/// </summary>
    public partial class FormCarServiceShop : Form
    {
        // Hols the elements of the list, all the table with the infor about customer, info about the car, and info about services.
        private SortedList carServiceList = new SortedList();
        // Hold the id number of the current owner of the car, starts empty by default.
        private string currentCustomerIdentificationNumber = string.Empty;
        // set the form in edit mode, starts as false, that means it can't edit until something happends.
        private bool editMode = false;
        //Constants, hold the cost of the services.
        private const decimal EngineOilChangeCost = 60;
        private const decimal TransOilChangeCost = 120;
        private const decimal AirFilterChangeCost = 40.5m;
        //  hold the cost of the taxes.
        private const decimal TaxRate = 0.13m;

        public FormCarServiceShop()
        {
            InitializeComponent();
            // Event handlers, in this case, has the checkboxes of the services
            // Has a relation between the events, and controls of the form.
            CheckBoxEngineOilChange.CheckedChanged += CheckBoxEngineOilChange_CheckedChanged;
            CheckBoxTransmissionOilChange.CheckedChanged += CheckBoxTransmissionOilChange_CheckedChanged;
            CheckBoxAirFilterChange.CheckedChanged += CheckBoxAirFilterChange_CheckedChanged;
        }

        private void FormCarServiceShop_Load(object sender, EventArgs e)
        {

        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            Services services;
            ListViewItem serviceItem;
            try
            {


                if (IsValidInput())
                {
                    //set the edit flag to true
                    editMode = true;
                    //If the current customer identification number has a no value
                    //then this is not an existing item from the listview
                    if (currentCustomerIdentificationNumber.Trim().Length == 0)
                    {
                        //create a new customer object using the parameterized constructor
                        services = new Services(TextBoxFirstName.Text, TextBoxLastName.Text, int.Parse(TextBoxPhone.Text), ComboBoxMake.Text, TextBoxModel.Text, TextBoxColour.Text, int.Parse(NumericUpDownYear.Text), CheckBoxEngineOilChange.Checked, CheckBoxTransmissionOilChange.Checked, CheckBoxAirFilterChange.Checked, 0);
                        // Calculate the total cost of the services and display them in the textbox
                        decimal totalCost = 0;

                        if (services.serviceEngineOilChange)
                        {
                            totalCost += EngineOilChangeCost;
                        }

                        if (services.serviceTransOilChange)
                        {
                            totalCost += TransOilChangeCost;
                        }

                        if (services.serviceAirFilterChange)
                        {
                            totalCost += AirFilterChangeCost;
                        }
                        decimal taxes = totalCost * TaxRate;
                        decimal totalWithTaxes = totalCost + taxes;

                        // Assign the total valur from the textbox to the list in a currency format
                        TextBoxCost.Text = totalWithTaxes.ToString("C");

                        services.serviceCost = totalCost;
                        //Add the id of the customer to the list.
                        carServiceList.Add(services.customerIdentificationNumber.ToString(), services);
                    }
                    else
                    {
                        //if the current customer identification number has a value
                        //then the user has selected something from the list view
                        //so the data in the customer object in the customerList collection
                        //must be updated

                        //so get the customer from the customers collection
                        //using the selected key
                        services = (Services)carServiceList[currentCustomerIdentificationNumber];

                        //update the data in the specific object from the controls

                        services.customerFirstName = TextBoxFirstName.Text;
                        services.customerLastName = TextBoxLastName.Text;
                        services.customerPhoneNumber = int.Parse(TextBoxPhone.Text);
                        services.carMake = ComboBoxMake.Text;
                        services.carModel = TextBoxModel.Text;
                        services.carYear = int.Parse(NumericUpDownYear.Text);
                        services.carColour = TextBoxColour.Text;
                        services.serviceEngineOilChange = CheckBoxEngineOilChange.Checked;
                        services.serviceTransOilChange = CheckBoxTransmissionOilChange.Checked;
                        services.serviceAirFilterChange = CheckBoxAirFilterChange.Checked;
                    }
                    ListViewSummary.Items.Clear();
                    foreach (DictionaryEntry customerEntry in carServiceList)
                    {
                        //instantiate a new ListViewItem
                        serviceItem = new ListViewItem();
                        //get the customer from the list
                        services = (Services)customerEntry.Value;

                        //assign the values to the ckecked control and the subitems

                        serviceItem.SubItems.Add(services.customerIdentificationNumber.ToString());
                        serviceItem.SubItems.Add(services.customerFirstName);
                        serviceItem.SubItems.Add(services.customerLastName);
                        serviceItem.SubItems.Add(services.customerPhoneNumber.ToString());
                        serviceItem.SubItems.Add(services.carMake);
                        serviceItem.SubItems.Add(services.carModel);
                        serviceItem.SubItems.Add(services.carYear.ToString());
                        serviceItem.SubItems.Add(services.carColour);

                        if (services.serviceEngineOilChange)
                        // Adds only if serviceEngineOilChange is clicked
                        {
                            serviceItem.SubItems.Add("Engine Oil Change");
                        }
                        // Adds only if serviceTransOilChange is clicked
                        if (services.serviceTransOilChange)
                        {
                            serviceItem.SubItems.Add("Trans Oil Change");
                        }
                        // Adds only if serviceAirFilterChange is clicked
                        if (services.serviceAirFilterChange)
                        {
                            serviceItem.SubItems.Add("Air Filter Change");
                        }
                        // Add the cost of the service to the list.
                        serviceItem.SubItems.Add(services.serviceCost.ToString("C"));

                        //add the new instantiated and populated ListViewItem to the listview control
                        ListViewSummary.Items.Add(serviceItem);
                    }

                    //Clear the controls
                    clearForm();
                    //set the edit flag to false
                    editMode = false;
                }
            }
            catch (Exception ex)
            {
                // if try-catch gets an error, display an error message
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Method to validate input of the user.
        private bool IsValidInput()
        {
            string warningMessage = string.Empty;
            //If the entered data is equal to zero, shows an error message.
            if (TextBoxFirstName.Text.Trim().Length == 0)
            {
                warningMessage += "Please, write the client's firstname." + Environment.NewLine;
                return false;
            }
            //If the entered data is equal to zero, shows an error message.
            if (TextBoxLastName.Text.Trim().Length == 0)
            {
                warningMessage += "Please, write the client's lastname." + Environment.NewLine;
                return false;
            }
            //If the entered data is equal to zero, shows an error message.
            if (TextBoxPhone.Text.Trim().Length == 0)
            {
                warningMessage += "Please, write the client's phone number." + Environment.NewLine;
                return false;
            }
            //If the entered data is equal to zero, shows an error message.
            if (TextBoxColour.Text.Trim().Length == 0)
            {
                warningMessage += "Please, write the car's colour." + Environment.NewLine;
                return false;
            }
            //If the user don't select anything from the combo box, shows an error message.
            if (ComboBoxMake.SelectedIndex == -1)
            {
                warningMessage += "Please, select the car's make.." + Environment.NewLine;
                return false;
            }
            //If the entered data is equal to zero, shows an error message.
            if (NumericUpDownYear.Text.Trim().Length == 0)
            {
                warningMessage += "Please, write the car's year." + Environment.NewLine;
                return false;
            }
            //Check if the message is empty, if has content, become as false
            if (!string.IsNullOrEmpty(warningMessage))
            {
                //show an error in the message
                throw new Exception(warningMessage);
            }

            return true;
        }
        // Method to set as default the inputs from the user, clean all the inputs and their information and show them as brand new
        private void clearForm()
        {
            TextBoxFirstName.Text = string.Empty;
            TextBoxLastName.Text = string.Empty;
            TextBoxPhone.Text = string.Empty;
            TextBoxModel.Text = string.Empty;
            TextBoxColour.Text = string.Empty;
            TextBoxCost.Text = string.Empty;
            CheckBoxEngineOilChange.Checked = false;
            CheckBoxTransmissionOilChange.Checked = false;
            CheckBoxAirFilterChange.Checked = false;
            ComboBoxMake.SelectedIndex = -1;
            NumericUpDownYear.Text = string.Empty;
            currentCustomerIdentificationNumber = string.Empty;
        }
        // Button exit, closes the application, but before, set the form as default values.
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            clearForm();
            Close();
            
        }
        // If the check box is clicked, update the cost of the service, independently of other check boxes
        private void CheckBoxEngineOilChange_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTotalCost();
        }
        // If the check box is clicked, update the cost of the service, independently of other check boxes
        private void CheckBoxTransmissionOilChange_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTotalCost();
        }
        // If the check box is clicked, update the cost of the service, independently of other check boxes
        private void CheckBoxAirFilterChange_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTotalCost();
        }
        // Calculate the total cost of the services.
        private decimal CalculateTotalCost()
        {
            //The variable starts as zero.
            decimal totalCost = 0;
            // If the check box is clicked, add the cost to the variable, even if is 1, 2 or 3 services.
            if (CheckBoxEngineOilChange.Checked)
            {
                totalCost += EngineOilChangeCost;
            }

            if (CheckBoxTransmissionOilChange.Checked)
            {
                totalCost += TransOilChangeCost;
            }

            if (CheckBoxAirFilterChange.Checked)
            {
                totalCost += AirFilterChangeCost;
            }
            // Set the taxes of the total cost.
            decimal taxes = totalCost * TaxRate;
            // Add the taxes to the final result.
            decimal totalWithTaxes = totalCost + taxes;

            return totalWithTaxes;
        }
        // Method: Update the total cost of the services, and displays it in the textbox Cost.
        private void UpdateTotalCost()
        {   
            // The variable hold the value from the CalculateTotalCost method.
            decimal totalWithTaxes = CalculateTotalCost();
            // Displays it in the text box as a string in currency format.
            TextBoxCost.Text = totalWithTaxes.ToString("C");
        }
        // Remove all information in the form, set as a default, even the list.
        private void ButtonRemoveAll_Click(object sender, EventArgs e)
        {
            clearForm();
            //Clear the information of the list.
            ListViewSummary.Clear();
        }

        private void ListViewSummary_SelectedIndexChanged(object sender, EventArgs e)
        {   
            // try - catch is used if occurs any error in the recuperation and asignation of the data.
            try
            {
                // 
                const int idService = 1;
                // get the service id from the listview
                currentCustomerIdentificationNumber = ListViewSummary.Items[ListViewSummary.FocusedItem.Index].SubItems[idService].Text;
                Services services = (Services)carServiceList[currentCustomerIdentificationNumber];
                // set the values from the textboxes (inputs) to the services.
                TextBoxFirstName.Text = services.customerFirstName;
                TextBoxLastName.Text = services.customerLastName;
                TextBoxPhone.Text = services.customerPhoneNumber.ToString();
                ComboBoxMake.Text = services.carMake;
                TextBoxModel.Text = services.carModel;
                NumericUpDownYear.Text = services.carYear.ToString();
                TextBoxColour.Text = services.carColour;
                TextBoxCost.Text = services.serviceCost.ToString();
                CheckBoxEngineOilChange.Checked = services.serviceEngineOilChange;
                CheckBoxTransmissionOilChange.Checked = services.serviceTransOilChange;
                CheckBoxAirFilterChange.Checked = services.serviceAirFilterChange;
                
            }
            catch (Exception ex)
            {
                // if try-catch gets an error, display an error message
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListViewSummary_ItemChecked(object sender, ItemCheckedEventArgs e)
        {

        }

        private void ListViewSummary_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //If it is not in edit mode
            if (!editMode)
            {
                //the new value to the current value
                //so it cannot be set in the listview by the user
                e.NewValue = e.CurrentValue;
            }
        }
        // New button set the form as default values, and clear the list, ready to recieve a new service.
        private void ButtonNew_Click(object sender, EventArgs e)
        {
            clearForm();
            ListViewSummary.SelectedItems.Clear();
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            
            
        }
    }
}
