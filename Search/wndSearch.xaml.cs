using GroupProject.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

/// <summary>
/// @author: Joe Dimmick, Ankit Dhamala, Austin Duran
/// @assignment: Group Project
/// </summary>
namespace GroupProject.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        #region Attributes
        /// <summary>
        /// invoice class 
        /// </summary>
        clsInvoices MyClsInvoices;

        /// <summary>
        /// my search logic class
        /// </summary>
        clsSearchLogic MyClsSearchLogic;

        /// <summary>
        /// this is the specifc invoice the user selected
        /// </summary>
        clsInvoices UserSelectedInvoice;



        ObservableCollection<Item> MyObservableCollection;
        #endregion

        #region Properties
        public clsInvoices SetUserInvoice
        { 
            get
            {
                return UserSelectedInvoice;
            }
            set
            {
                UserSelectedInvoice = value;
            }
        }
        public ObservableCollection<Item> SetMyObservableCollection
        {
            set
            {
                MyObservableCollection = value;
            }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// the Search window Constructor 
        /// </summary>
        public wndSearch()
        {
            try
            {
                InitializeComponent();

                MyClsSearchLogic = new clsSearchLogic();
                InvoicesDataGrid.ItemsSource = MyClsSearchLogic.AllInvoices();

                //clsInvoices invoice = (clsInvoices)InvoicesDataGrid.ItemsSource;
                //cbChooseInvoice.ItemsSource = invoice.printNum;
                //cbChooseCharge.ItemsSource = MyClsSearchLogic.AllInvoices();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// handles the Index the user selected from the Invoice # drop down box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //getting specific selected invoices 
                //clsInvoices invoice = (clsInvoices)cbChooseInvoice.SelectedItem;
                //cbChooseInvoice.ItemsSource = MyClsSearchLogic.SpecifiedInvoiceNum(invoice.InvoiceNum);
                //calls a method from the search logic class and 
                //SpecifiedInvoiceNum(invoice);
                //This method should take the invoice number that was selected by the user and limit the invoices
                //displayed based on that criteria
                //specifc invoice 
                UserWantsToSee();


            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }//end method

        /// <summary>
        /// handles the Index the user selected from the Total Charge's drop down box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TotalCharge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {    //calls a method from the search logic class and 
                 //This method should take the Total Charge's amount that was selected by the user and limit the invoices
                 //displayed based on that criteria 
                UserWantsToSee();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }//end method 

        /// <summary>
        /// handles the Index the user selected from the Invoice Date picker box 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceDate_SelectedDateChange(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //calls a method from the search logic class and 
                //This method should take the Invoice's date that was selected by the user and limit the invoices
                //displayed based on that criteria 
                UserWantsToSee();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }///end method 

        /// <summary>
        /// this Method resets all prior selected indexs on combo box's and date picker for invoices
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //calls a method from the search logic class and 
                //Reset all selected indexs and display all invoices 
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }//end method 

        /// <summary>
        /// this Method takes the invoice the user selected and makes it available to other windows 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //calls a method from the search logic class and 
                //take the invoice selected from the data grid and make it available to other windows 
                //close this form 
                clsInvoices Invoice = (clsInvoices)InvoicesDataGrid.SelectedCells;
                UserSelectedInvoice = Invoice;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }//end method 

        /// <summary>
        /// this method finds out the specifc serach criteria the user wants to see
        /// </summary>
        private void UserWantsToSee()
        {
            //specific invoice
            clsInvoices Invoice = (clsInvoices)cbChooseInvoice.SelectedItem;
            //specific date
            string sDate = dpInvoiceDate.SelectedDate.ToString();

            if (cbChooseCharge.SelectedItem == null && sDate == "")//only invoice number
            {

                MyClsSearchLogic.SpecifiedInvoiceNum(Invoice.InvoiceNum);
            }
            else if (cbChooseCharge.SelectedItem == null)//invoice number and date selected
            {
                MyClsSearchLogic.SpecifiedInvoiceNumAndDate(Invoice.InvoiceNum, sDate);
            }
            else//Invoice number and date and cost
            {
                //specific invoice
                clsInvoices InvoiceCost = (clsInvoices)cbChooseCharge.SelectedItem;
                MyClsSearchLogic.SpecifiedInvoiceNumAndDateAndCost(Invoice.InvoiceNum, sDate, InvoiceCost.TotalCost);
            }
        }//end method 


        /// <summary>
        /// this method handles the window closing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                this.Hide();
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }//end method 

        /// <summary>
        /// HandleError shows the error to the user and saves to root directory
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C://Error.txt", Environment.NewLine +
                                             "HandleError Excpetion: " + ex.Message);
            }
        }//end method 
        #endregion
    }//end class
}//end namespace
