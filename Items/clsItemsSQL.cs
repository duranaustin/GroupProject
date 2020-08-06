using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

/// <summary>
/// @author: Austin Duran
/// @assignment: Group Project
/// </summary>
namespace GroupProject.Items
{
    /// <summary>
    /// clsItemsSQL holds all the relevant sql queries for items in invoices
    /// </summary>
    public class clsItemsSQL
    {
        /// <summary>
        /// getItemDetails returns a dataset of our invoice db
        /// </summary>
        /// <returns></returns>
        public static string getItemDetails()
        {
            try
            {
                string sql = "SELECT ItemDesc.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost " +
                             "FROM ItemDesc " +
                             "ORDER BY ItemCode";
                return sql;
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// getInvoiceNum returns an invoice number from the invoice DB
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string getInvoiceNum(string itemCode)
        {
            try
            {
                string sql = "SELECT DISTINCT(InvoiceNum) FROM LineItems WHERE ItemCode = '" + itemCode + "'";
                return sql;
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// updateItem updates an item in our in memory instance of the Invoice DB
        /// </summary>
        /// <param name="itemDesc"></param>
        /// <param name="itemCost"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string updateItem(string itemDesc, string itemCost, string itemCode)
        {
            try
            {
                string sql = "UPDATE ItemDesc SET ItemDesc = '" + itemDesc + "', Cost = " + itemCost +
                             " WHERE ItemCode = '" + itemCode + "'";
                return sql;
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// addItem adds an item to our in memory instance of the Invoice DB
        /// </summary>
        /// <param name="itemDesc"></param>
        /// <param name="itemCost"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string addItem(string itemDesc, string itemCost, string itemCode)
        {
            try
            {
                string sql = "INSERT INTO ItemDesc(ItemCode, ItemDesc, Cost) Values('" + itemCode + "', '" + itemDesc + "', " + itemCost + ")";
                return sql;
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// deleteItem deletes an item from our in memory instance of the Invoice DB
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string deleteItem(string itemCode)
        {
            try
            {
                string sql = "DELETE FROM ItemDesc WHERE ItemCode = '" + itemCode + "'";
                return sql;
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }

}
