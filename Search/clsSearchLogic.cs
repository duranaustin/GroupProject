using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection;
using System.Collections.ObjectModel;
/// <summary>
/// @author: Joe Dimmick, Ankit Dhamala, Austin Duran
/// @assignment: Group Project
/// </summary>
namespace GroupProject.Search
{
    public class clsSearchLogic
    {
        #region Attributes
        //the search window 
        wndSearch MyWndSearch;

        //the searchSQL class
        clsSearchSQL MyclsSearchSQL;

        //the invoices class
        clsInvoices  MyClsInvoices;


        //make an attribute that can store selected index from the data grid and then accsessed through a prperty
        //clsInvoices Invoice = (clsInvoices)InvoicesDataGrid.SelectedItem;

        #endregion
        public clsInvoices CopyInvoices
        {
            set
            {
                MyClsInvoices = value;
            }
        }
        #region properties
        //create a property that allows other windows to retrive the invoice selected
        #endregion

        #region Constructor
        public clsSearchLogic()
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// this method selects invoices based on specifc data passed in
        /// </summary>
        public ObservableCollection<clsInvoices> AllInvoices()
        {
            try
            {
                //send the invoice number 
                return MyclsSearchSQL.SelectAllInvoices();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        public ObservableCollection<clsInvoices> SpecifiedInvoiceNum(string Num)
        {
            try
            {
                //send the invoice number 
                //SelectInvoicesOnNumber(invoice.invoiceNum);
                return MyclsSearchSQL.SelectInvoicesOnNumber(Num);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// this method selects invoices based on specifc data passed in
        /// </summary>
        public ObservableCollection<clsInvoices> SpecifiedInvoiceNumAndDate(string Num,string Date)
        {
            try
            {
                //send the invoice number 
                //SelectInvoicesOnNumber(invoice.invoiceNum);
                return MyclsSearchSQL.SelectInvoicesOnNumberAndDate(Num, Date);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// this method selects invoices based on specifc data passed in
        /// </summary>
        public ObservableCollection<clsInvoices> SpecifiedInvoiceNumAndDateAndCost(string Num, string Date,string Cost)
        {
            try
            {
                //send the invoice number 
                //SelectInvoicesOnNumber(invoice.invoiceNum);
                return MyclsSearchSQL.SelectInvoicesOnAll(Num, Date, Cost);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// this method selects invoices based on specifc data passed in
        /// </summary>
        public ObservableCollection<clsInvoices> SpecifiedInvoiceCost(string Cost)
        {
            try
            {
                //send the invoice number 
                //SelectInvoicesOnNumber(invoice.invoiceNum);
                return MyclsSearchSQL.SelectInvoicesOnCost(Cost);

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// this method selects invoices based on specifc data passed in
        /// </summary>
        public ObservableCollection<clsInvoices> SpecifiedInvoiceDateAndCost(string Date, string Cost)
        {
            try
            {
                //send the invoice number 
                //SelectInvoicesOnNumber(invoice.invoiceNum);
                return MyclsSearchSQL.SelectInvoicesOnDateAndCost(Date, Cost);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// this method selects invoices based on specifc data passed in
        /// </summary>
        public ObservableCollection<clsInvoices> SpecifiedInvoiceDate(string Date)
        {
            try
            {
                //send the invoice number 
                //SelectInvoicesOnNumber(invoice.invoiceNum);
                return MyclsSearchSQL.SelectInvoicesOnDate(Date);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


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
