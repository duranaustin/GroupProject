using GroupProject.Items;
using GroupProject.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
/// <summary>
/// @author: Joe Dimmick, Ankit Dhamala, Austin Duran
/// @assignment: Group Project
/// </summary>
namespace GroupProject.Main
{
    public class clsMainLogic
    {
        /// <summary>
        /// Main window SQL
        /// </summary>
        clsMainSQL SQL;
        /// <summary>
        /// Holder for selected Date
        /// </summary>
        public DateTime Date { get; internal set; }
        /// <summary>
        /// Facilitates talking to the DB.
        /// </summary>
        DataAccess db;
        public clsMainLogic()
        {
            SQL = new clsMainSQL();
           // db = new DataAccess();
        }
        /// <summary>
        /// Returns invoice numbers with given date.
        /// </summary>
        /// <returns></returns>
        internal ObservableCollection<clsInvoices> PopulateInvoiceNumOnDate()
        {
            try
            {
                return SQL.SelectInvoiceNumOnDate(Date.ToString());
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Populates the invoice using the invoice number.
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <returns></returns>
        internal ObservableCollection<Item> PopulateInvoicesOnInvoiceNum(string InvoiceNum)
        {
            try
            {
                return SQL.SelectLineItemsOnInvoiceNum(InvoiceNum);
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Populates all items.
        /// </summary>
        /// <returns></returns>
        internal ObservableCollection<Item> PopulateAllItems()
        {
            try
            {
                return clsItemsLogic.items;
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Facilitates adding an invoice to the DB.
        /// </summary>
        /// <param name="selectedDate"></param>
        /// <param name="totalCost"></param>
        internal void AddInvoice(string selectedDate, string totalCost)
        {
            try
            { // not working. Invoices are not being saved to the db.
                SQL.New_Invoice(selectedDate, totalCost);
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }

}
