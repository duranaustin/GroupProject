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

        //public List<Item> getItems()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
