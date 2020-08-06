namespace GroupProject.Search
{
    /// <summary>
    /// Holds invoice information InvoiceNum, InvoiceDate, TotalCost
    /// </summary>
    public class clsInvoices
    {
        #region Attributes
        /// <summary>
        /// InvoiceNum represented as a string
        /// </summary>
        public string InvoiceNum { get; set; }
        /// <summary>
        /// InvoiceDate represented as a string
        /// </summary>
        public string InvoiceDate { get; set; }
        /// <summary>
        /// TotalCost represented as a string
        /// </summary>
        public string TotalCost { get; set; }
        #endregion
        #region Methods
        /// <summary>
        /// ToString returns an invoice with format [Invoice Number] [InvoiceDate] [Total Cost]
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{InvoiceNum} {InvoiceDate} {TotalCost}";
        }
        #endregion
    }
}