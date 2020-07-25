using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
/// <summary>
/// @author: Austin Duran
/// @assignment: Group Project
/// </summary>
namespace GroupProject.Items
{
    public class clsItemsLogic
    {
        /// <summary>
        /// items represents all the items we are currently dealing with 
        /// </summary>
        public List<Item> items { get; set; }
        /// <summary>
        /// itemUpdated is a placeholder to notify other screens if an item has been updated
        /// </summary>
        public bool itemUpdated = false;

        //public List<Item> getItems()
        //{
        //    sql call to get items from db
        //    data is returned and added to our items field as Item objects
        //    throw new NotImplementedException();
        //}
    }
}
