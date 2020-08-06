using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GroupProject.Items;
using GroupProject.Main;
using GroupProject.Search;

namespace GroupProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Attributes
        /// <summary>
        /// Window Items
        /// </summary>
        public wndItems itemsWindow;
        public clsMainLogic mainLogic;
        #endregion
        #region Methods
        /// <summary>
        /// Constructor for main window and application entry point.
        /// </summary>
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                mainLogic = new clsMainLogic();
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;//close the application when the main window is closed

                itemsWindow = new wndItems();
                //this.Hide(); //temporary for austin's development
                //itemsWindow.Show(); //temporary for austin's development
            }
            catch (Exception ex)
            {               //this is reflection
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Handles the Exit menu item being clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {               //this is reflection
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                //Code to be ran no matter what. Closing connnections to dbs/web
            }
        }
        /// <summary>
        /// Handles the Item Inventory Menu item click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemInventory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndItems wndItems = new wndItems();
                wndItems.ShowDialog();
            }
            catch (Exception ex)
            {               //this is reflection
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                //Code to be ran no matter what. Closing connnections to dbs/web
            }
        }
        /// <summary>
        /// Handles the Search Invoices menu item click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchInvoices_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndSearch wndSearch = new wndSearch();
                wndSearch.ShowDialog();
            }
            catch (Exception ex)
            {               //this is reflection
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                //Code to be ran no matter what. Closing connnections to dbs/web
            }
        }
        /// <summary>
        /// Exception handler.
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {//would write to a file or database here.
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine +
                                            "HandleError Exception: " + ex.Message);
            }
        }
        /// <summary>
        /// Handles the New Invoice button. Disable Invoice Lookup UI and enable add item UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InitialUIState();
                addItemsCanvas.IsEnabled = true;
                invoiceNumberTextBox.Text = "TBD";
                invoiceDataGrid.IsEnabled = true;
            }
            catch (Exception ex)
            {               //this is reflection
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                //Code to be ran no matter what. Closing connnections to dbs/web
            }
        }
        /// <summary>
        /// Handles Void current button click by activing invoice look up ui,
        /// disabling the add item ui and clears the datagrid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void voidCurrentInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InitialUIState();
            }
            catch (Exception ex)
            {               //this is reflection
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                //Code to be ran no matter what. Closing connnections to dbs/web
            }
        }
        /// <summary>
        /// Handles the Save Invoice Button being clicked. Checks if items exists in the datagrid.
        /// If no items exist the Invoice will not be saved.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime date = new DateTime();
                if(invoiceDataGrid.HasItems)
                {
                    date = datePicker.SelectedDate.Value;
                    invoiceSavedLabel.Visibility = (Visibility)0;
                }
                else
                {
                    noItemsAddedLabel.Visibility = (Visibility)0;
                }
                InitialUIState();
                invoiceSavedLabel.Visibility = 0;
            }
            catch (Exception ex)
            {               //this is reflection
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                //Code to be ran no matter what. Closing connnections to dbs/web
            }
        }
        private void InitialUIState()
        {
            addItemsCanvas.IsEnabled = false;
            invoiceNumberTextBox.Text = "";
            invoiceDataGrid.Items.Clear();
            invoiceSavedLabel.Visibility = (Visibility)1;
            noItemsAddedLabel.Visibility = (Visibility)1;
        }
        private void clearErrorMessages()
        {
            invoiceSavedLabel.Visibility = (Visibility)1;
            noItemsAddedLabel.Visibility = (Visibility)1;
            chooseDateErrorLabel.Visibility = (Visibility)1;
        }

        #endregion //---------------------------------------------------------------------

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DateTime date = new DateTime();
                
                chooseDateErrorLabel.Visibility = (Visibility)1;
                invoiceDataGrid.Items.Clear();
                date = datePicker.SelectedDate.Value;

                mainLogic.Date = date;
                
                invoiceComboBox.ItemsSource = mainLogic.PopulateInvoiceNumOnDate();

                InitialUIState();
            }
            catch (Exception ex)
            {               //this is reflection
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}
