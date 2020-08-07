using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection;
using System.Data;
using System.Collections.ObjectModel;

/// <summary>
/// @author: Joe Dimmick, Joey Sanchez, Austin Duran
/// @assignment: Group Project
/// </summary>
namespace GroupProject.Search
{
    class clsSearchSQL
    {
        #region attributes
        /// <summary>
        /// class that accesses the database 
        /// </summary>
        DataAccess db;

        /// <summary>
        /// declaring an obserable collection to store the invoices
        /// </summary>
        private static ObservableCollection<clsInvoices> lstOfInvoices;
        #endregion

        #region constructors
        /// <summary>
        /// clsSearchSQL class constructor
        /// </summary>
        public clsSearchSQL()
        {
            //initiating database object
            db = new DataAccess();
            lstOfInvoices = new ObservableCollection<clsInvoices>();

            string sSQL;
            int iRet = 0;   //Number of return values
            DataSet ds = new DataSet(); //Holds the return values

            //Create the SQL statement to extract the Invoices
            sSQL = "SELECT* FROM Invoices";

            //Extract the Invoices and put them into the DataSet
            ds = db.ExecuteSQLStatement(sSQL, ref iRet);

            //Loop through the data and create an Invoice class
            for (int i = 0; i < iRet; i++)
            {
                lstOfInvoices.Add(new clsInvoices
                {
                    InvoiceNum = ds.Tables[0].Rows[i][0].ToString(),
                    InvoiceDate = ds.Tables[0].Rows[i]["InvoiceDate"].ToString(),
                    TotalCost = ds.Tables[0].Rows[i]["TotalCost"].ToString()
                });
            }
        }//end constructor
        #endregion

        #region Methods 
        /// <summary>
        /// this method selects invoices based on specifc data passed in
        /// </summary>
        public ObservableCollection<clsInvoices> SelectAllInvoices()
        {
            try
            {
                return lstOfInvoices;
            }//end try 
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }//end method 

        /// <summary>
        /// this method selects invoices based on specifc data passed in
        /// </summary>
        public ObservableCollection<clsInvoices> SelectInvoicesOnNumber(string InvoiceNum)
        {
            try
            {
                string sSQL;
                int iRet = 0;   //Number of return values
                DataSet ds = new DataSet(); //Holds the return values

                //Create the SQL statement to extract the Invoices
                sSQL = "SELECT* FROM Invoices WHERE InvoiceNum = "+ InvoiceNum + "";

                //Extract the Invoices and put them into the DataSet
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Loop through the data and create an Invoice class
                for (int i = 0; i < iRet; i++)
                {
                    lstOfInvoices.Add(new clsInvoices
                    {
                        InvoiceNum = ds.Tables[0].Rows[i][0].ToString(),
                        InvoiceDate = ds.Tables[0].Rows[i]["InvoiceDate"].ToString(),
                        TotalCost = ds.Tables[0].Rows[i]["TotalCost"].ToString()
                    });
                }
                return lstOfInvoices;
            }//end try 
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }//end method 

        /// <summary>
        /// this method selects invoices based on specifc data passed in
        /// </summary>
        public ObservableCollection<clsInvoices> SelectInvoicesOnNumberAndDate(string InvoiceNum,string InvoiceDate)
        {
            try
            {
                string sSQL;
                int iRet = 0;   //Number of return values
                DataSet ds = new DataSet(); //Holds the return values

                //Create the SQL statement to extract the Invoices
                sSQL = "SELECT* FROM Invoices WHERE InvoiceNum = 5000 AND "+ InvoiceNum + " = #"+ InvoiceDate + "#";

                //Extract the Invoices and put them into the DataSet
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Loop through the data and create an Invoice class
                for (int i = 0; i < iRet; i++)
                {
                    lstOfInvoices.Add(new clsInvoices
                    {
                        InvoiceNum = ds.Tables[0].Rows[i][0].ToString(),
                        InvoiceDate = ds.Tables[0].Rows[i]["InvoiceDate"].ToString(),
                        TotalCost = ds.Tables[0].Rows[i]["TotalCost"].ToString()
                    });
                }
                return lstOfInvoices;
            }//end try 
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }//end method 

        /// <summary>
        /// this method selects invoices based on specifc data passed in
        /// </summary>
        public ObservableCollection<clsInvoices> SelectInvoicesOnAll(string InvoiceNum, string InvoiceDate, string TotalCost)
        {
            try
            {
                string sSQL;
                int iRet = 0;   //Number of return values
                DataSet ds = new DataSet(); //Holds the return values

                //Create the SQL statement to extract the Invoices
                sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = "+ InvoiceNum + " AND InvoiceDate = #"+ InvoiceDate + "# AND TotalCost = "+ TotalCost + "";

                //Extract the Invoices and put them into the DataSet
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Loop through the data and create an Invoice class
                for (int i = 0; i < iRet; i++)
                {
                    lstOfInvoices.Add(new clsInvoices
                    {
                        InvoiceNum = ds.Tables[0].Rows[i][0].ToString(),
                        InvoiceDate = ds.Tables[0].Rows[i]["InvoiceDate"].ToString(),
                        TotalCost = ds.Tables[0].Rows[i]["TotalCost"].ToString()
                    });
                }
                return lstOfInvoices;
            }//end try 
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }//end method 

        /// <summary>
        /// this method selects invoices based on specifc data passed in
        /// </summary>
        public ObservableCollection<clsInvoices> SelectInvoicesOnCost(string TotalCost)
        {
            try
            {
                string sSQL;
                int iRet = 0;   //Number of return values
                DataSet ds = new DataSet(); //Holds the return values

                //Create the SQL statement to extract the Invoices
                sSQL = "SELECT * FROM Invoices WHERE TotalCost = "+ TotalCost + "";

                //Extract the Invoices and put them into the DataSet
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Loop through the data and create an Invoice class
                for (int i = 0; i < iRet; i++)
                {
                    lstOfInvoices.Add(new clsInvoices
                    {
                        InvoiceNum = ds.Tables[0].Rows[i][0].ToString(),
                        InvoiceDate = ds.Tables[0].Rows[i]["InvoiceDate"].ToString(),
                        TotalCost = ds.Tables[0].Rows[i]["TotalCost"].ToString()
                    });
                }
                return lstOfInvoices;
            }//end try 
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }//end method 

        /// <summary>
        /// this method selects invoices based on specifc data passed in
        /// </summary>
        public ObservableCollection<clsInvoices> SelectInvoicesOnDateAndCost(string InvoiceDate, string TotalCost)
        {
            try
            {
                string sSQL;
                int iRet = 0;   //Number of return values
                DataSet ds = new DataSet(); //Holds the return values

                //Create the SQL statement to extract the Invoices
                sSQL = "SELECT* FROM Invoices WHERE TotalCost = "+ InvoiceDate + " and InvoiceDate = #"+ TotalCost + "#";

                //Extract the Invoices and put them into the DataSet
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Loop through the data and create an Invoice class
                for (int i = 0; i < iRet; i++)
                {
                    lstOfInvoices.Add(new clsInvoices
                    {
                        InvoiceNum = ds.Tables[0].Rows[i][0].ToString(),
                        InvoiceDate = ds.Tables[0].Rows[i]["InvoiceDate"].ToString(),
                        TotalCost = ds.Tables[0].Rows[i]["TotalCost"].ToString()
                    });
                }
                return lstOfInvoices;
            }//end try 
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }//end method 

        /// <summary>
        /// this method selects invoices based on specifc data passed in
        /// </summary>
        public ObservableCollection<clsInvoices> SelectInvoicesOnDate(string InvoiceDate)
        {
            try
            {
                string sSQL;
                int iRet = 0;   //Number of return values
                DataSet ds = new DataSet(); //Holds the return values

                //Create the SQL statement to extract the Invoices
                sSQL = "SELECT * FROM Invoices WHERE InvoiceDate = #"+ InvoiceDate + "#";

                //Extract the Invoices and put them into the DataSet
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Loop through the data and create an Invoice class
                for (int i = 0; i < iRet; i++)
                {
                    lstOfInvoices.Add(new clsInvoices
                    {
                        InvoiceNum = ds.Tables[0].Rows[i][0].ToString(),
                        InvoiceDate = ds.Tables[0].Rows[i]["InvoiceDate"].ToString(),
                        TotalCost = ds.Tables[0].Rows[i]["TotalCost"].ToString()
                    });
                }
                return lstOfInvoices;
            }//end try 
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
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
