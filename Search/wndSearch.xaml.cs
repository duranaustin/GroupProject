using GroupProject.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

/// <summary>
/// @author: Joe Dimmick, Ankit Dhamala, Austin Duran
/// @assignment: Group Project
/// </summary>
namespace GroupProject.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        #region Attributes
        /// <summary>
        /// invoice class 
        /// </summary>
        clsInvoices MyClsInvoices;

        /// <summary>
        /// my search logic class
        /// </summary>
        clsSearchLogic MyClsSearchLogic;

        /// <summary>
        /// my searchSQL class
        /// </summary>
        clsSearchSQL MyClsSearchSQL;
        #endregion

        #region Constructor
        /// <summary>
        /// the Search window Constructor 
        /// </summary>
        public wndSearch()
        {
            try
            {
                InitializeComponent();
                MyClsSearchSQL = new clsSearchSQL();
                MyClsSearchLogic = new clsSearchLogic();
                InvoicesDataGrid.ItemsSource = MyClsSearchLogic.AllInvoices();//Database to grid box
                cbChooseInvoice.ItemsSource = MyClsSearchLogic.OnlyInvoiceNum();//InvoiceDatabase to Invoice number box
                cbChooseCharge.ItemsSource = MyClsSearchLogic.OnlyInvoiceCost();//costDatabase to cost box 
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// handles the Index the user selected from the Invoice # drop down box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbChooseInvoice.SelectedItem != null)//if selected
                {
                    UserWantsToSee();
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }//end method

        /// <summary>
        /// handles the Index the user selected from the Total Charge's drop down box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TotalCharge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (cbChooseCharge.SelectedItem != null)//if selected
                {
                    UserWantsToSee();
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }//end method 

        /// <summary>
        /// handles the Index the user selected from the Invoice Date picker box 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceDate_SelectedDateChange(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (dpInvoiceDate.SelectedDate != null)//if selected
                {
                    UserWantsToSee();
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }///end method 

        /// <summary>
        /// this Method resets all prior selected indexs on combo box's and date picker for invoices
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //calls a method from the search logic class and 
                //Reset all selected indexs and display all invoices 
                InvoicesDataGrid.ItemsSource = MyClsSearchLogic.AllInvoices();//Database to grid box
                cbChooseInvoice.ItemsSource = MyClsSearchLogic.OnlyInvoiceNum();//Database to Invoice number box
                cbChooseCharge.ItemsSource = MyClsSearchLogic.OnlyInvoiceCost();//Database to Total charges box   
                dpInvoiceDate.SelectedDate = null;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }//end method 

        /// <summary>
        /// this Method takes the invoice the user selected and makes it available to other windows 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(InvoicesDataGrid.SelectedItem != null)//if selected
                {
                    clsInvoices Invoice = (clsInvoices)InvoicesDataGrid.SelectedItem;
                    MainWindow.MainWndwInvoice = Invoice;//send invoice to main window
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }//end method 

        /// <summary>
        /// this method finds out the specifc serach criteria the user wants to see
        /// </summary>
        private void UserWantsToSee()
        {
            try
            {
                clsInvoices InvoiceNum = (clsInvoices)cbChooseInvoice.SelectedItem;//specified invoice 
                clsInvoices InvoiceCost = (clsInvoices)cbChooseCharge.SelectedItem;//specified Cost 

                //specific date
                string sDate = dpInvoiceDate.SelectedDate.ToString();
                if (InvoiceCost != null && InvoiceNum != null)
                {
                    return;
                }
                else if (InvoiceCost == null && sDate == "")//only invoice number
                {
                    ObservableCollection<clsInvoices> Temp = MyClsSearchLogic.SpecifiedInvoiceNum(InvoiceNum.InvoiceNum);
                    InvoicesDataGrid.ItemsSource = Temp;//update datagrid
                }
                else if (InvoiceCost != null && sDate != "")//invoice cost and date selected
                {
                    ObservableCollection<clsInvoices> Temp = MyClsSearchLogic.SpecifiedInvoiceDateAndCost(sDate, InvoiceCost.TotalCost);
                    InvoicesDataGrid.ItemsSource = Temp;//update datagrid
                }
                else if (InvoiceNum != null && sDate != "")//invoice num and date selected
                {
                    ObservableCollection<clsInvoices> Temp = MyClsSearchLogic.SpecifiedInvoiceNumAndDate(InvoiceNum.InvoiceNum, sDate);
                    InvoicesDataGrid.ItemsSource = Temp;//update datagrid
                }
                else if (InvoiceCost != null && InvoiceNum == null && sDate == "")//only cost 
                {
                    ObservableCollection<clsInvoices> Temp = MyClsSearchLogic.SpecifiedInvoiceCost(InvoiceCost.TotalCost);
                    InvoicesDataGrid.ItemsSource = Temp;//update datagrid
                }
                else if (InvoiceCost == null && InvoiceNum == null)//only date
                {
                    ObservableCollection<clsInvoices> Temp = MyClsSearchLogic.SpecifiedInvoiceDate(sDate);
                    InvoicesDataGrid.ItemsSource = Temp;//update datagrid
                }
                else//Invoice number and date and cost
                {
                    ObservableCollection<clsInvoices> Temp = MyClsSearchLogic.SpecifiedInvoiceNumAndDateAndCost(InvoiceNum.InvoiceNum, sDate, InvoiceCost.TotalCost);
                    InvoicesDataGrid.ItemsSource = Temp;//update datagrid
                }
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }//end method 

        /// <summary>
        /// this method handles the window closing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                this.Hide();
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }//end method 

        /// <summary>
        /// this method takes out all invoice Numbers and retruns them
        /// </summary>
        public ObservableCollection<clsInvoices> FilterNum(ObservableCollection<clsInvoices> list)
        {
            try
            {
                return MyClsSearchLogic.ParseNum(list);
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }//end method

        /// <summary>
        /// this method selects takes out all invoice cost and retruns them
        /// </summary>
        public ObservableCollection<clsInvoices> FilterCost(ObservableCollection<clsInvoices> list)
        {
            try
            {
                return MyClsSearchLogic.ParseCost(list); 
            }
            catch (Exception ex)
            {                       //this is reflection for exception handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }//method

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
