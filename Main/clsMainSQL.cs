﻿using GroupProject.Search;
using System;
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
        /// class that accesses the database 
        /// </summary>
        DataAccess db;

        //ObservableCollection<clsInvoices> lstOfInvoices;
        public clsMainSQL()
        {
            db = new DataAccess();
            //lstOfInvoices = new ObservableCollection<clsInvoices>();
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
        /// <summary>
        /// this method selects invoice numbers based on date.
        /// </summary>
        public ObservableCollection<clsInvoices> SelectInvoiceNumOnDate(string InvoiceDate)
        {
            try
            {
                ObservableCollection<clsInvoices> list = new ObservableCollection<clsInvoices>();
                string sSQL;
                int iRet = 0;   //Number of return values
                DataSet ds = new DataSet(); //Holds the return values

                //Create the SQL statement to extract the Invoices
                sSQL = $"SELECT InvoiceNum FROM Invoices WHERE InvoiceDate = #{InvoiceDate}#";

                //Extract the Invoices and put them into the DataSet
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Loop through the data and create an Invoice class
                for (int i = 0; i < iRet; i++)
                {
                    list.Add(new clsInvoices
                    {
                        InvoiceNum = ds.Tables[0].Rows[i][0].ToString()
                    });
                }
                return list;
            }//end try 
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }//end method 
    }

}
