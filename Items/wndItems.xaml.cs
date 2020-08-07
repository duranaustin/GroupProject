using System;
using System.Collections.Generic;
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
        public wndAddItem addItemWindow;
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
                //when a selection is made on the datagrid for a certain item, the edit button is enabled
                //when clicked a dialog box opens ready for the user to make changes to that item
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
                //when a selection is made on the datagrid for a certain item, the delete button is enabled
                //when clicked a dialog box opens ready for the user to delete that item
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
                //when a selection is made on the datagrid for a certain item, the delete button and edit button are both enabled
                //these items are added to a list for potential editing or deleting
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
