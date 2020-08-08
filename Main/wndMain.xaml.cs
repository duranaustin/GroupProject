using System;
using System.Collections.Generic;
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
        /// search window
        /// </summary>
        public wndSearch SearchWindow;
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
        /// <summary>
        /// holds an invoice that the other classes can write too.
        /// </summary>
        public static clsInvoices MainWndwInvoice { get; set; }
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
                //Maybe delete this line.
                MainWndwInvoice = new clsInvoices();

                dataGridList = new ObservableCollection<Item>();
                itemsWindow = new wndItems();
                SearchWindow = new wndSearch();

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
                if (MainWndwInvoice.InvoiceNum != null)
                {
                    var DateTime = Convert.ToDateTime(MainWndwInvoice.InvoiceDate);
                    string[] list = new string[1];
                    list[0] = MainWndwInvoice.InvoiceNum;

                    datePicker.SelectedDate = DateTime;

                    invoiceComboBox.SelectedItem = MainWndwInvoice.InvoiceNum;
                    invoiceComboBox.ItemsSource = list;
                    dataGridList = mainLogic.PopulateLineItemsOnInvoiceNum(MainWndwInvoice.InvoiceNum);
                    invoiceDataGrid.ItemsSource = dataGridList;
                }
            }
            catch (Exception ex)
            {               //this is reflection
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
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
                    Total_TextBox.Text = totalCost.ToString();
                    dataGridList = new ObservableCollection<Item>();
                    invoiceDataGrid.ItemsSource = dataGridList;
                    selectDateErrorLabel.Visibility = (Visibility)1;
                    InitialUIState();
                    addItemsCanvas.IsEnabled = true;
                    invoiceNumberTextBox.Text = "TBD";
                    invoiceLookUpCanvas.IsEnabled = true;
                    invoice_Deleted_Label.Visibility = (Visibility)1;
                    ItemInventory.IsEnabled = false;
                }
                
            }
            catch (Exception ex)
            {               //this is reflection
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
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
                if(invoiceNumberTextBox.Text == "TBD") //if invoiceNumberTextBox is TBD then the invoice is a new invoice.
                {                                      //and an INSERT INTO statement is required.
                    if (dataGridList.Count == 0)
                    {
                        noItemsAddedLabel.Visibility = (Visibility)0;

                    }
                    else
                    {
                        mainLogic.AddInvoice(datePicker.SelectedDate.ToString(), totalCost.ToString(),dataGridList); //add invoice to the DB
                        addItemsCanvas.IsEnabled = false;
                        invoiceNumberTextBox.Text = "";
                        invoiceSavedLabel.Visibility = (Visibility)0;
                        itemsComboBox.Text = "";
                        invoiceComboBox.Text = "";
                        datePicker.Text = "Please select a date";
                    }
                }
                else //if invoicenumbertextbox is not TBD then the invoice is one already in the database and 
                {    // an UPDATE statement will be required.
                    var selectedInvoice = (clsInvoices)invoiceComboBox.SelectedItem;
                    mainLogic.UpdateInvoice(selectedInvoice.InvoiceNum, totalCost.ToString(), dataGridList);
                    invoiceLookUpCanvas.IsEnabled = true;
                    invoiceSavedLabel.Visibility = (Visibility)0;
                    itemsComboBox.Text = "";
                    invoiceComboBox.Text = "";
                    datePicker.Text = "Please select a date";
                }
                dataGridList.Clear();
                invoiceDataGrid.ItemsSource = dataGridList;
                Cost_TextBox.Text = "";
                Total_TextBox.Text = "";
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
            dataGridList.Clear();
            invoiceDataGrid.ItemsSource = dataGridList;
            Cost_TextBox.Text = "";
            Total_TextBox.Text = "";
            invoiceSavedLabel.Visibility = (Visibility)1;
            noItemsAddedLabel.Visibility = (Visibility)1;
            ItemInventory.IsEnabled = true;
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
                invoice_Deleted_Label.Visibility = (Visibility)1;
                date = datePicker.SelectedDate.Value;               

                mainLogic.Date = date;                

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
                if(selectedInvoice != null)
                {
                    invoiceNumberTextBox.Text = selectedInvoice.InvoiceNum;
                    dataGridList = mainLogic.PopulateLineItemsOnInvoiceNum(selectedInvoice.InvoiceNum);
                }                
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
                Item_Already_invoiced_Error_label.Visibility = (Visibility)1;
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
                if (dataGridList.Contains(currentlySelectedItem))
                {
                    Item_Already_invoiced_Error_label.Visibility = (Visibility)0;
                }
                else
                {
                    noItemsAddedLabel.Visibility = (Visibility)1;
                    dataGridList.Add((Item)itemsComboBox.SelectedItem);
                    invoiceDataGrid.ItemsSource = dataGridList;
                    totalCost = totalCost + Int32.Parse(currentlySelectedItem.itemCost);

                    Total_TextBox.Text = totalCost.ToString();
                }
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
                Select_an_invoiced_item_label.Visibility = (Visibility)1;
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
                if(itemsInvoicedComboBox.SelectedItem != null)
                {
                    var currentlySelectedItem = (Item)itemsInvoicedComboBox.SelectedItem;
                    dataGridList.Remove((Item)itemsInvoicedComboBox.SelectedItem);
                    invoiceDataGrid.ItemsSource = dataGridList;
                    totalCost = totalCost - Int32.Parse(currentlySelectedItem.itemCost);
                    Total_TextBox.Text = totalCost.ToString();
                }
                else
                {
                    Select_an_invoiced_item_label.Visibility = 0;
                }   
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
        /// <summary>
        /// handles the delete invoice button click by deleteing the currently selected invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datePicker.SelectedDate == null)
                {
                    selectDateErrorLabel.Visibility = (Visibility)0;
                }
                else if (invoiceComboBox.SelectedItem == null)
                {
                    Selecet_Invoice_Error_Label.Visibility = (Visibility)0;
                }
                else
                {
                    clsInvoices selectedInvoice = (clsInvoices)invoiceComboBox.SelectedItem;
                    invoiceDataGrid.ItemsSource = dataGridList;
                    selectDateErrorLabel.Visibility = (Visibility)1;
                    Selecet_Invoice_Error_Label.Visibility = (Visibility)1;
                    invoiceNumberTextBox.Text = selectedInvoice.InvoiceNum;
                    mainLogic.DeleteInvoice(selectedInvoice.InvoiceNum);
                    dataGridList.Clear();
                    invoiceDataGrid.ItemsSource = dataGridList;
                    itemsComboBox.Text = "";
                    invoiceComboBox.Text = "";
                    datePicker.Text = "Please select a date";
                    invoice_Deleted_Label.Visibility = (Visibility)0;
                }

            }
            catch (Exception ex)
            {               //this is reflection
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// handles the edit button click, enabling the add items interface.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datePicker.SelectedDate == null)
                {
                    selectDateErrorLabel.Visibility = (Visibility)0;
                }
                else if (invoiceComboBox.SelectedItem == null)
                {
                    Selecet_Invoice_Error_Label.Visibility = (Visibility)0;
                }
                else
                {
                    clsInvoices selectedInvoice = (clsInvoices)invoiceComboBox.SelectedItem;
                    invoiceDataGrid.ItemsSource = dataGridList;
                    selectDateErrorLabel.Visibility = (Visibility)1;
                    Selecet_Invoice_Error_Label.Visibility = (Visibility)1;
                    addItemsCanvas.IsEnabled = true;
                    invoiceNumberTextBox.Text = selectedInvoice.InvoiceNum;
                    invoiceLookUpCanvas.IsEnabled = false;
                    Total_TextBox.Text = selectedInvoice.TotalCost;
                    ItemInventory.IsEnabled = false;
                }

            }
            catch (Exception ex)
            {               //this is reflection
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
       /// <summary>
       /// Calls the db when the combobox is opened to populate with the latest avaible invoices
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void invoiceComboBox_DropDownOpened(object sender, EventArgs e)
        {
            try
            {
                invoiceComboBox.ItemsSource = mainLogic.PopulateInvoiceNumOnDate();
                invoiceSavedLabel.Visibility = (Visibility)1;
                invoice_Deleted_Label.Visibility = (Visibility)1;
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
