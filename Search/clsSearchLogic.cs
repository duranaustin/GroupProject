using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Data;
/// <summary>
/// @author: Joe Dimmick, Ankit Dhamala, Austin Duran
/// @assignment: Group Project
/// </summary>
namespace GroupProject.Search
{
    public class clsSearchLogic
    {
        #region Attributes
        /// <summary>
        /// search class
        /// </summary>
        clsSearchSQL MyclsSearchSQL;

        /// <summary>
        /// invoice class
        /// </summary>
        clsInvoices  MyClsInvoices;

        #endregion

        #region properites
        /// <summary>
        /// property that sets invoices
        /// </summary>
        public clsInvoices CopyInvoices
        {
            set
            {
                MyClsInvoices = value;
            }
        }//end property
        #endregion

        #region Constructor
        /// <summary>
        /// constructor
        /// </summary>
        public clsSearchLogic()
        {
            MyclsSearchSQL = new clsSearchSQL();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns invoice numbers with given date.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<clsInvoices> ParseNum(ObservableCollection<clsInvoices> list)
        {
            try
            {
                ObservableCollection<clsInvoices> temp = new ObservableCollection<clsInvoices>();
                foreach (var clsInvoices in list)
                {
                    temp.Add(new clsInvoices
                    {
                        InvoiceNum = clsInvoices.InvoiceNum.ToString()
                    });
                }
                return temp;

            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }//end method 

        /// <summary>
        /// Returns invoice numbers with given date.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<clsInvoices> ParseCost(ObservableCollection<clsInvoices> list)
        {
            try
            {
                ObservableCollection<clsInvoices> temp = new ObservableCollection<clsInvoices>();
                foreach (var clsInvoices in list)
                {
                    temp.Add(new clsInvoices
                    {
                        TotalCost = clsInvoices.TotalCost.ToString()
                    });
                }
                return temp;
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }//end method

        /// <summary>
        /// this method selects invoices based on specifc data passed in
        /// </summary>
        public ObservableCollection<clsInvoices> OnlyInvoiceCost()
        {
            try
            {

                return MyclsSearchSQL.PopulateInvoiceCost();
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }//end method

        /// <summary>
        /// this method selects invoices based on specifc data passed in
        /// </summary>
        public ObservableCollection<clsInvoices> OnlyInvoiceNum()
        {
            try
            {

                return MyclsSearchSQL.PopulateInvoiceNum();
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }//end method

        /// <summary>
        /// this method selects invoices based on specifc data passed in
        /// </summary>
        public ObservableCollection<clsInvoices> AllInvoices()
        {
            try
            {
             
                return MyclsSearchSQL.SelectAllInvoices();
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }//end method

        public ObservableCollection<clsInvoices> SpecifiedInvoiceNum(string Num)
        {
            try
            {
                return MyclsSearchSQL.SelectInvoicesOnNumber(Num);
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }//end method

        /// <summary>
        /// this method selects invoices based on specifc data passed in
        /// </summary>
        public ObservableCollection<clsInvoices> SpecifiedInvoiceNumAndDate(string Num,string Date)
        {
            try
            {
                return MyclsSearchSQL.SelectInvoicesOnNumberAndDate(Num, Date);
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }//end method

        /// <summary>
        /// this method selects invoices based on specifc data passed in
        /// </summary>
        public ObservableCollection<clsInvoices> SpecifiedInvoiceNumAndDateAndCost(string Num, string Date,string Cost)
        {
            try
            {
                return MyclsSearchSQL.SelectInvoicesOnAll(Num, Date, Cost);
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }//end method

        /// <summary>
        /// this method selects invoices based on specifc data passed in
        /// </summary>
        public ObservableCollection<clsInvoices> SpecifiedInvoiceCost(string Cost)
        {
            try
            {
                return MyclsSearchSQL.SelectInvoicesOnCost(Cost);
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }//end method

        /// <summary>
        /// this method selects invoices based on specifc data passed in
        /// </summary>
        public ObservableCollection<clsInvoices> SpecifiedInvoiceDateAndCost(string Date, string Cost)
        {
            try
            {
                return MyclsSearchSQL.SelectInvoicesOnDateAndCost(Date, Cost);
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }//end method

        /// <summary>
        /// this method selects invoices based on specifc data passed in
        /// </summary>
        public ObservableCollection<clsInvoices> SpecifiedInvoiceDate(string Date)
        {
            try
            {
                return MyclsSearchSQL.SelectInvoicesOnDate(Date);
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }//end method

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
