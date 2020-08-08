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
        /// <summary>
        /// Constructor
        /// </summary>
        public clsMainLogic()
        {
            SQL = new clsMainSQL();
            db = new DataAccess();
        }
        /// <summary>
        /// Returns invoice numbers with given date.
        /// </summary>
        /// <returns></returns>
        internal ObservableCollection<clsInvoices> PopulateInvoiceNumOnDate()
        {
            try
            {
                ObservableCollection<clsInvoices> list = new ObservableCollection<clsInvoices>();
                int iRet = 0;   //Number of return values
                DataSet ds = new DataSet(); //Holds the return values

                ds = db.ExecuteSQLStatement(SQL.SelectInvoiceNumOnDate(Date.ToString()),ref iRet);
                //Loop through the data and create an Invoice class
                for (int i = 0; i < iRet; i++)
                {
                    list.Add(new clsInvoices
                    {
                        InvoiceNum = ds.Tables[0].Rows[i][0].ToString()
                    });
                }
                return list;
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
        internal ObservableCollection<Item> PopulateLineItemsOnInvoiceNum(string InvoiceNum)
        {
            try
            {
                ObservableCollection<Item> list = new ObservableCollection<Item>();
                int iRet = 0;   //Number of return values
                DataSet ds = new DataSet(); //Holds the return values

                //Extract the Invoices and put them into the DataSet
                ds = db.ExecuteSQLStatement(SQL.SelectLineItemsOnInvoiceNum(InvoiceNum), ref iRet);

                //Loop through the data and create an Invoice class
                for (int i = 0; i < iRet; i++)
                {
                    list.Add(new Item
                    {
                        itemCode = ds.Tables[0].Rows[i][0].ToString(),
                        itemDesc = ds.Tables[0].Rows[i]["ItemDesc"].ToString(),
                        itemCost = ds.Tables[0].Rows[i]["Cost"].ToString()

                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }
        /// <summary>
        /// Populates all items.
        /// </summary>
        /// <returns></returns>
        internal ObservableCollection<Item> PopulateAllItems()
        {
            var items = new clsItemsLogic();
            try
            {
                return items.getItems();
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
        internal void AddInvoice(string selectedDate, string totalCost, ObservableCollection<Item> InvoicedItems)
        {
            try
            { 
                int rows = db.ExecuteNonQuery(SQL.New_Invoice(selectedDate, totalCost));
                AddLineItems(InvoicedItems);
                
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Returns the invoice with the highest invoice number
        /// </summary>
        /// <returns></returns>
        private clsInvoices GetMaxInvoice()
        {
            try
            {
                clsInvoices invoice = new clsInvoices();
                int iRet = 0;   //Number of return values
                DataSet ds = new DataSet(); //Holds the return values

                ds = db.ExecuteSQLStatement(SQL.SelectMaxInvoice(), ref iRet);

                invoice.InvoiceNum = ds.Tables[0].Rows[0][0].ToString();
                invoice.InvoiceDate = ds.Tables[0].Rows[0][1].ToString();
                invoice.TotalCost = ds.Tables[0].Rows[0]["TotalCost"].ToString();
                return invoice;
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Inserts data into the LineItems table
        /// </summary>
        /// <param name="InvoicedItems"></param>
        private void AddLineItems(ObservableCollection<Item> InvoicedItems)
        {
            //string invoiceNum, int lineItemNum, string itemCode
            try
            {
                clsInvoices invoice = GetMaxInvoice();

                foreach (Item item in InvoicedItems) // add LineItem to DB
                {
                    int LineItemNum = 1;
                    int row = db.ExecuteNonQuery(SQL.InsertLineItems(invoice.InvoiceNum, LineItemNum.ToString(), item.itemCode));
                    LineItemNum++;
                }
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Updates an invoice by deleteing the record from line items and re-inserting the data.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="totalCost"></param>
        /// <param name="dataGridList"></param>
        internal void UpdateInvoice(string invoiceNum, string totalCost, ObservableCollection<Item> dataGridList)
        {
            try
            {
                Delete_Line_Items(invoiceNum);
                db.ExecuteNonQuery(SQL.Update_Invoice_Total(invoiceNum, totalCost));
                foreach (Item item in dataGridList) // add LineItem to DB
                {
                    int LineItemNum = 1;
                    int row = db.ExecuteNonQuery(SQL.InsertLineItems(invoiceNum, LineItemNum.ToString(), item.itemCode));
                    LineItemNum++;
                }
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Deletes data from LineItems
        /// </summary>
        /// <param name="invoiceNum"></param>
        private void Delete_Line_Items(string invoiceNum)
        {
            try
            {
                int row = db.ExecuteNonQuery(SQL.DeleteLineItemsOnInvoiceNum(invoiceNum));
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Deletes an invoice from the data base
        /// </summary>
        /// <param name="invoiceNum"></param>
        internal void DeleteInvoice(string invoiceNum)
        {
            try
            {
                Delete_Line_Items(invoiceNum);
                int row = db.ExecuteNonQuery(SQL.Delete_Invoice(invoiceNum));
                
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }

}
