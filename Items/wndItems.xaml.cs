using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
/// @author: Austin Duran
/// @assignment: Group Project
/// </summary>
namespace GroupProject.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        /// <summary>
        /// itemsLogic handles the logic of our items
        /// </summary>
        public clsItemsLogic itemsLogic;
        /// <summary>
        /// addItemWindow enables the user to add an item
        /// </summary>
        public wndEditItem editItemWindow;
        /// <summary>
        /// addItemWindow enables the user to add an item
        /// </summary>
        public wndAddItem addItemWindow;
        /// <summary>
        /// list
        /// </summary>
        public ObservableCollection<Item> list;
        /// <summary>
        /// wndItems is our window for Items
        /// </summary>
        public wndItems()
        {
            try
            {
                InitializeComponent();
                itemsLogic = new clsItemsLogic();
                itemsDataGrid.ItemsSource = itemsLogic.getItems();//populate the datagrid with the items returned from getItems()
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// addButton_Click handles the addition of an item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                addItemWindow = new wndAddItem(itemsLogic);
                addItemWindow.ShowDialog();
                itemsDataGrid.ItemsSource = itemsLogic.getItems();//populate the datagrid with the items returned from getItems()
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }
        /// <summary>
        /// editButton_Click handles the editing of an item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                editItemWindow = new wndEditItem(itemsLogic);
                editItemWindow.ShowDialog();
                itemsDataGrid.ItemsSource = itemsLogic.getItems();//populate the datagrid with the items returned from getItems()
                editButton.IsEnabled = false;
                deleteButton.IsEnabled = false;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }
        /// <summary>
        /// deleteButton_Click handles the deletion of an item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!itemsLogic.checkInvoices(itemsLogic.selectedItem, itemsLogic))
                {
                    itemsLogic.deleteItem(itemsLogic.selectedItem); //populate the datagrid with the items returned from getItems()
                    itemsDataGrid.ItemsSource = itemsLogic.getItems(); //populate the datagrid with the items returned from getItems()
                    editButton.IsEnabled = false;
                    deleteButton.IsEnabled = false;
                    itemsLogic.invoicesWithItemToDelete.Clear();
                    editButton.IsEnabled = false;
                    deleteButton.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("Cannot delete Item as it is contained within invoice(s):"+"\n" + String.Join("\n", itemsLogic.invoicesWithItemToDelete));
                    itemsLogic.invoicesWithItemToDelete.Clear();
                    deleteButton.IsEnabled = false;
                    editButton.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }
        /// <summary>
        /// itemsDataGrid_SelectionChanged handles the selection of an item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                editButton.IsEnabled = true;
                deleteButton.IsEnabled = true;
                Item selectedItem = (Item) itemsDataGrid.SelectedItem;
                itemsLogic.selectedItem = selectedItem;
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
                                                              "HandleError Exception: " + ex.Message);
            }
        }
    }
}
