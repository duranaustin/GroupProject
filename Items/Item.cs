using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Item constructor
        /// </summary>
        /// <param name="itemDesc"></param>
        /// <param name="itemCost"></param>
        /// <param name="itemCode"></param>
        //public Item(string itemDesc, string itemCost, string itemCode)
        //{
        //    this.itemDesc = itemDesc;
        //    this.itemCost = itemCost;
        //    this.itemCode = itemCode;
        //}

        public override string ToString()
        {
            return $"{itemCode} - {itemDesc}";
        }
    }
}
