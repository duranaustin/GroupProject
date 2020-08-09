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
using GroupProject.Main;
using GroupProject.Search;

/// <summary>
/// @author: Austin Duran
/// @assignment: Group Project
/// </summary>
namespace GroupProject.Items
{
    /// <summary>
    /// clsItemsLogic takes care of the logic in our items window
    /// </summary>
    public class clsItemsLogic
    {
        /// <summary>
        /// class that accesses the database 
        /// </summary>
        DataAccess db;
        /// <summary>
        /// items represents all the items we are currently dealing with 
        /// </summary>
        public ObservableCollection<Item> items { get; set; }
        /// <summary>
        /// itemUpdated is a placeholder to notify other screens if an item has been updated
        /// </summary>
        public bool itemUpdated = false;
        /// <summary>
        /// mainSQL provides sql queries from the main windo
        /// </summary>
        public clsMainSQL mainSQL;
        /// <summary>
        /// selectItem is our currently selected item for deletion or editing
        /// </summary>
        public Item selectedItem;
        /// <summary>
        /// invoiceWithItemToDelete is the invoice that contains the item we're trying to delete
        /// </summary>
        public ObservableCollection<string> invoicesWithItemToDelete;
        /// <summary>
        /// clsItemsLogic Constructor
        /// </summary>
        public clsItemsLogic()
        {
            try
            {
                db = new DataAccess();
                items = getItems();
                invoicesWithItemToDelete = new ObservableCollection<string>();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        /// <summary>
        /// getItems returns all items in our inventory
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Item> getItems()
        {
            try
            {
                ObservableCollection<Item> list = new ObservableCollection<Item>();
                string sSQL;
                int iRet = 0; //Number of return values
                DataSet ds = new DataSet(); //Holds the return values

                //Create the SQL statement to extract the items
                sSQL = clsItemsSQL.getItemDetails();

                //Extract the items and put them into the DataSet
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

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
        /// deleteItem deletes an item from our items list
        /// </summary>
        /// <param name="item"></param>
        public void deleteItem(Item item)
        {
            ObservableCollection<Item> list = new ObservableCollection<Item>();
            string sSQL;
            int iRet = 0; //Number of return values
            int result = 0;
            DataSet ds = new DataSet(); //Holds the return values

            //Create the SQL statement to extract the items
            sSQL = clsItemsSQL.deleteItem(item.itemCode);

            //Extract the items and put them into the DataSet
            result = db.ExecuteNonQuery(sSQL);

            //Create the SQL statement to extract the items
            sSQL = clsItemsSQL.getItemDetails();

            //Extract the items and put them into the DataSet
            ds = db.ExecuteSQLStatement(sSQL, ref iRet);

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

            items = list;
            
        }
        /// <summary>
        /// checkInvoices checks all invoices to see if the item exists in any of the invoices
        /// </summary>
        public bool checkInvoices(Item item, clsItemsLogic itemsLogic)
        {
            ObservableCollection<clsInvoices> invoicesList = new ObservableCollection<clsInvoices>();
            ObservableCollection<clsInvoices> itemsList = new ObservableCollection<clsInvoices>();
            clsMainSQL mainSQL = new clsMainSQL();
            bool itemExistsInInvoices = false;
            string sSQL;
            int iRet = 0; //Number of return values
            int iRet2 = 0; //Number of return values
            int result = 0;
            DataSet ds = new DataSet(); //Holds the return values
            DataSet ds2 = new DataSet(); //Holds the return values
            //Create the SQL statement to extract the invoices
            sSQL = mainSQL.SelectLineItems(); // sql statement to get all current invoices

            //Extract the invoices and put them into the DataSet
            ds = db.ExecuteSQLStatement(sSQL, ref iRet);

            //Loop through the data and create an Invoice class

            for (int i = 0; i < iRet; i++) //for the length of all invoices
            {
                sSQL = mainSQL.SelectLineItemsOnInvoiceNum(ds.Tables[0].Rows[i][0].ToString()); //grab the current invoice and select all the items within the invoice
                ds2 = db.ExecuteSQLStatement(sSQL, ref iRet2);
                for (int j = 0; j < iRet2; j++)//for the length of items
                {
                    if (ds2.Tables[0].Rows[j][0].ToString() == item.itemCode)//if our item code matches the current item selected, return true
                    {
                        itemsLogic.invoicesWithItemToDelete.Add(ds.Tables[0].Rows[i][0].ToString());
                        itemExistsInInvoices = true;
                    }
                }
            }

            if (itemExistsInInvoices)
            {
                return true;
            }
            else
            {
                return false;
            }
            return false;
        }
        /// <summary>
        /// editItem updates an item from our items list
        /// </summary>
        /// <param name="item"></param>
        public void editItem(Item item)
        {
            ObservableCollection<Item> list = new ObservableCollection<Item>();
            string sSQL;
            int iRet = 0; //Number of return values
            int result = 0;
            DataSet ds = new DataSet(); //Holds the return values

            //Create the SQL statement to extract the items
            sSQL = clsItemsSQL.updateItem(item.itemDesc, item.itemCost, item.itemCode);

            //Extract the items and put them into the DataSet
            result = db.ExecuteNonQuery(sSQL);

            //Create the SQL statement to extract the items
            sSQL = clsItemsSQL.getItemDetails();

            //Extract the items and put them into the DataSet
            ds = db.ExecuteSQLStatement(sSQL, ref iRet);

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
            items = list;
        }
        /// <summary>
        /// addItem adds an item to our items list
        /// </summary>
        /// <param name="item"></param>
        public void addItem(string itemDesc, string itemCost)
        {
            ObservableCollection<Item> list = new ObservableCollection<Item>();
            string sSQL;
            int iRet = 0; //Number of return values
            int result = 0;
            DataSet ds = new DataSet(); //Holds the return values
            Item item = new Item();
            item.itemDesc = itemDesc;
            item.itemCost = itemCost;
            //Create the SQL statement to extract the items
            sSQL = clsItemsSQL.addItem(item.itemDesc, item.itemCost);

            //Extract the items and put them into the DataSet
            result = db.ExecuteNonQuery(sSQL);

            //Create the SQL statement to extract the items
            sSQL = clsItemsSQL.getItemDetails();

            //Extract the items and put them into the DataSet
            ds = db.ExecuteSQLStatement(sSQL, ref iRet);

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
            items = list;
        }
    }
}
