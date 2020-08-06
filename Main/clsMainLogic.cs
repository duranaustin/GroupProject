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
        public clsMainLogic()
        {
            SQL = new clsMainSQL();
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

        internal ObservableCollection<Item> PopulateAllItems()
        {
            try
            {
                return SQL.SelectAllItems();
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }

}
