//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EM.DomainModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Expense
    {
        public int Id { get; set; }
        public string ExpenseFor { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime AddDate { get; set; }
        public int ExpenseCategoryId { get; set; }
        public string Note { get; set; }
        public string Photo { get; set; }
    
        public virtual ExpenseCategory ExpenseCategory { get; set; }
    }
}
