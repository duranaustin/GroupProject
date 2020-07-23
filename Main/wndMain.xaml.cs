using System;
using System.Collections.Generic;
using System.Linq;
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

namespace GroupProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public wndItems itemsWindow;
        public MainWindow()
        {
            InitializeComponent();
            itemsWindow = new wndItems(); 
            //this.Hide(); //temporary for austin's development
            //itemsWindow.Show(); //temporary for austin's development
            
        }
    }
}
