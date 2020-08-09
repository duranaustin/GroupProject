using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// @author: Austin Duran
/// @assignment: Group Project
/// </summary>
namespace GroupProject.Items
{
    /// <summary>
    /// Item will represent an item as an object
    /// </summary>
    public class Item
    {
        /// <summary>
        /// itemDesc is the item description
        /// </summary>
        public string itemDesc { get; set; }
        /// <summary>
        /// itemCost is the item cost
        /// </summary>
        public string itemCost { get; set; }
        /// <summary>
        /// itemCode is the item code
        /// </summary>
        public string itemCode { get; set; }
        /// <summary>
        /// ToString overridden to string method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            try
            {
                return $"{itemCode} - {itemDesc}";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }
    }
}
