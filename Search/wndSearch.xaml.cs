using System;
using System.Collections.Generic;
using System.Linq;
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
        #region Constructor
        /// <summary>
        /// the Search window Constructor 
        /// </summary>
        public wndSearch()
        {
            try
            {
                InitializeComponent();

                //EVERY TIME WINDOW IS OPEN
                //upon loadinf, make sure to load all invoices without search criteria 
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
                //calls a method from the search logic class and 
                //This method should take the invoice number that was selected by the user and limit the invoices
                //displayed based on that criteria 
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
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
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
