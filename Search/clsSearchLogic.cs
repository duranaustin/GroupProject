using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection;
/// <summary>
/// @author: Joe Dimmick, Ankit Dhamala, Austin Duran
/// @assignment: Group Project
/// </summary>
namespace GroupProject.Search
{
    public class clsSearchLogic
    {
        #region Attributes
        //make an attribute that can store selected index from the data grid and then accsessed through a prperty
        //clsInvoice Invoice = (clsInvoice)InvoicesDataGrid.SelectedItem;

        #endregion

        #region properties
        //create a property that allows other windows to retrive the invoice selected
        #endregion

        #region Methods

        private void SpecifiedInvoiceNum(Object invoice)
        {
            try
            {
                //send the invoice number 
                //SelectInvoicesOnNumber(invoice.invoiceNum);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


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
        }//end method 
        #endregion
    }//end class
}//end namespace
