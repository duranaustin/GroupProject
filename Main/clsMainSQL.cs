using System;
using System.Collections.Generic;
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
        }
        /// <summary>
        /// SQL query that inserts an Invoice into the database.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public string New_Invoice(string date, string total)
        {
            try
            {
                return $"INSERT INTO Invoices (InvoiceDate, TotalCost VALUES ({date.ToString()}, {total})";
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
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
    }

}
