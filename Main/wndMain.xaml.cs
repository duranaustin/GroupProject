using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
        /// <summary>
        /// Facilitates Logic for main window
        /// </summary>
        public clsMainLogic mainLogic;
        /// <summary>
        /// Holds the data being displayed in the datagrid.
        /// </summary>
        public static ObservableCollection<Item> dataGridList;
        /// <summary>
        /// Tracks total cost of an invoice.
        /// </summary>
        private int totalCost;
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
                dataGridList = new ObservableCollection<Item>();

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
                if (datePicker.SelectedDate == null)
                {
                    selectDateErrorLabel.Visibility = (Visibility)0;
                }
                else
                {
                    totalCost = 0;
                    dataGridList = new ObservableCollection<Item>();
                    invoiceDataGrid.ItemsSource = dataGridList;
                    selectDateErrorLabel.Visibility = (Visibility)1;
                    InitialUIState();
                    addItemsCanvas.IsEnabled = true;
                    invoiceNumberTextBox.Text = "TBD";
                }
                
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
                dataGridList.Clear();
                invoiceDataGrid.ItemsSource = dataGridList;
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
            //TODO This is not saving the invoice to the database.
            try
            {
                if(invoiceNumberTextBox.Text == "TBD") //if invoiceNumberTextBox is TBD then the invoice is a new invoice.
                {                                      //and an INSERT INTO statement is required.
                    if (dataGridList.Count == 0)
                    {
                        noItemsAddedLabel.Visibility = (Visibility)0;

                    }
                    else
                    {// this line is not working.
                        mainLogic.AddInvoice(datePicker.SelectedDate.ToString(), totalCost.ToString());
                        invoiceSavedLabel.Visibility = (Visibility)0;
                    }
                }
                else //if invoicenumbertextbox is not TBD then the invoice is one already in the database and 
                {    // an UPDATE statement will be required.

                }
                
            }
            catch (Exception ex)
            {               //this is reflection
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Helper method to reset the UI state.
        /// </summary>
        private void InitialUIState()
        {
            addItemsCanvas.IsEnabled = false;
            invoiceNumberTextBox.Text = "";
   
            invoiceSavedLabel.Visibility = (Visibility)1;
            noItemsAddedLabel.Visibility = (Visibility)1;
        }
        /// <summary>
        /// Helper method to clear all the error messages.
        /// </summary>
        private void clearErrorMessages()
        {
            invoiceSavedLabel.Visibility = (Visibility)1;
            noItemsAddedLabel.Visibility = (Visibility)1;
            chooseDateErrorLabel.Visibility = (Visibility)1;
        }        
        /// <summary>
        /// Sends the selected date to the logic populates invoice combo box with any invoices that may exsist.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DateTime date = new DateTime();

                dataGridList.Clear();

                chooseDateErrorLabel.Visibility = (Visibility)1;
                selectDateErrorLabel.Visibility = (Visibility)1;
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
        /// <summary>
        /// Displays the selected invoice data in the data grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void invoiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            try
            {
                var selectedInvoice = (clsInvoices)invoiceComboBox.SelectedItem;
                invoiceNumberTextBox.Text = selectedInvoice.InvoiceNum;
                dataGridList = mainLogic.PopulateInvoicesOnInvoiceNum(selectedInvoice.InvoiceNum);
                invoiceDataGrid.ItemsSource = dataGridList;
            }
            catch (Exception ex)
            {               //this is reflection
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Populates the items combobox with items when the drop down is open.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemsComboBox_DropDownOpened(object sender, EventArgs e)
        {            
            try
            {
                itemsComboBox.ItemsSource = mainLogic.PopulateAllItems();
            }
            catch (Exception ex)
            {               //this is reflection
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Adds an item to the invoice and updates the total cost of the invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Item_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var currentlySelectedItem = (Item)itemsComboBox.SelectedItem;
                noItemsAddedLabel.Visibility = (Visibility)1;
                dataGridList.Add((Item)itemsComboBox.SelectedItem);
                invoiceDataGrid.ItemsSource = dataGridList;
                totalCost = totalCost + Int32.Parse(currentlySelectedItem.itemCost);

                Total_TextBox.Text = totalCost.ToString();
            }
            catch (Exception ex)
            {               //this is reflection
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// populates the items invoiced into the items invoiced comboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemsInvoicedComboBox_DropDownOpened(object sender, EventArgs e)
        {
            try
            {
                itemsInvoicedComboBox.ItemsSource = dataGridList;
            }
            catch (Exception ex)
            {               //this is reflection
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Removes the item selected in the itemsInvoicedComboBox from the invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Remove_Item_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var currentlySelectedItem = (Item)itemsInvoicedComboBox.SelectedItem;
                dataGridList.Remove((Item)itemsInvoicedComboBox.SelectedItem);
                invoiceDataGrid.ItemsSource = dataGridList;
                totalCost = totalCost - Int32.Parse(currentlySelectedItem.itemCost);
                Total_TextBox.Text = totalCost.ToString();
            }
            catch (Exception ex)
            {               //this is reflection
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Populates the Cost field of the selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var currentlySelectedItem = (Item)itemsComboBox.SelectedItem;
                if(currentlySelectedItem != null)
                {
                    Cost_TextBox.Text = currentlySelectedItem.itemCost;
                }
                
            }
            catch (Exception ex)
            {               //this is reflection
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion //---------------------------------------------------------------------
    }
}
