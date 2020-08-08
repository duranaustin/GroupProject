using GroupProject.Items;
using GroupProject.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    class clsMainSQL
    {
        /// <summary>
        /// SQL query that inserts an Invoice into the database.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public string New_Invoice(string date, string total)
        {
            try // not working. Invoices are not being saved to the db.
            {
                return "INSERT INTO Invoices (InvoiceDate, TotalCost)" +
                      $" VALUES (#{date}#, {total})";
                //return "INSERT INTO Invoices (InvoiceDate, TotalCost) VALUES (#" + date + "#, " + total + ")";

            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Queries DB for Inoivces using date.
        /// </summary>
        /// <param name="InvoiceDate"></param>
        /// <returns></returns>
        internal string SelectLineItemsOnInvoiceNum(string InvoiceNum)
        {
            try
            {
                return "SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost " +
                    "FROM LineItems, ItemDesc " +
                    $"Where LineItems.ItemCode = ItemDesc.ItemCode And LineItems.InvoiceNum = {InvoiceNum}";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }
        /// <summary>
        /// SQL query that updates the given invoice number with the given total.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public string Update_Invoice_Total(string invoiceNumber, string total)
        {
            try
            {
                return $"UPDATE Invoices SET TotalCost = {total} WHERE InvoiceNum = {invoiceNumber}";
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// SQL query that deletes the passed invoice from the database.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public string Delete_Invoice(string invoiceNumber)
        {
            try
            {
                return $"DELETE FROM Invoices WHERE InvoiceNum = {invoiceNumber}";
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// this method selects invoice numbers based on date.
        /// </summary>
        public string SelectInvoiceNumOnDate(string InvoiceDate)
        {
            try
            {
                return $"SELECT InvoiceNum FROM Invoices WHERE InvoiceDate = #{InvoiceDate}#";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }
        /// <summary>
        /// Returns the invoice with highest InvoiceNum
        /// </summary>
        /// <returns></returns>
        internal string SelectMaxInvoice()
        {
            try
            {
                return "SELECT InvoiceNum, InvoiceDate, TotalCost " +
                    "FROM Invoices " +
                    "WHERE InvoiceNum = " +
                    "(SELECT MAX(InvoiceNum) FROM Invoices)";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }
        /// <summary>
        /// Inserts data into LineItems table.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="lineItemNum"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        internal string InsertLineItems(string invoiceNum, string lineItemNum, string itemCode)
        {
            try
            {
                return "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) " +
                    $"VALUES ({invoiceNum}, {lineItemNum}, '{itemCode}')";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }
        /// <summary>
        /// Deletes Line Items where Invoice number is equal to passed invoice number.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        internal string DeleteLineItemsOnInvoiceNum(string invoiceNum)
        {
            try
            {
                return "DELETE FROM LineItems " +
                    $"WHERE InvoiceNum = {invoiceNum}";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }
    }

}
