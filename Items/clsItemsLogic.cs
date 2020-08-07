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
        public static ObservableCollection<Item> items { get; set; }
        /// <summary>
        /// itemUpdated is a placeholder to notify other screens if an item has been updated
        /// </summary>
        public bool itemUpdated = false;
        /// <summary>
        /// clsItemsLogic Constructor
        /// </summary>
        public clsItemsLogic()
        {
            try
            {
                db = new DataAccess();
                items = getItems();
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
        /// deleteItem deletes an item from or items list
        /// </summary>
        /// <param name="item"></param>
        public void deleteItem(Item item)
        {
            ObservableCollection<Item> list = new ObservableCollection<Item>();
            string sSQL;
            int iRet = 0; //Number of return values
            DataSet ds = new DataSet(); //Holds the return values

            //Create the SQL statement to extract the items
            sSQL = clsItemsSQL.deleteItem(item.itemCode);

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
        /// editItem updates an item from our items list
        /// </summary>
        /// <param name="item"></param>
        public void editItem(Item item)
        {
            ObservableCollection<Item> list = new ObservableCollection<Item>();
            string sSQL;
            int iRet = 0; //Number of return values
            DataSet ds = new DataSet(); //Holds the return values

            //Create the SQL statement to extract the items
            sSQL = clsItemsSQL.updateItem(item.itemDesc, item.itemCost, item.itemCode);

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
            DataSet ds = new DataSet(); //Holds the return values
            Item item = new Item();
            item.itemDesc = itemDesc;
            item.itemCost = itemCost;
            //Create the SQL statement to extract the items
            sSQL = clsItemsSQL.addItem(item.itemDesc, item.itemCost);

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
