using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EM.ViewModels
{
    public class ExpenseVM
    {
        public int Id { get; set; }
        public string ExpenseFor { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public System.DateTime AddDate { get; set; }
        public int ExpenseCategoryId { get; set; }
        public string Note { get; set; }

        public ExpenseCategoryVM ExpenseCategory { get; set; }
    }
    public class ExpenseSumDTO
    {
      
        public Nullable<decimal> Amount { get; set; }
        public System.DateTime OnDate { get; set; }
     
    }
    public class ExpenseCategoryVM
    {
        public ExpenseCategoryVM()
        {
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }

    }
    public class ExpenseCategorySpendVM
    {
        public ExpenseCategorySpendVM()
        {
        }

        public string CategoryName { get; set; }
        public decimal? TotalAmount { get; set; }

    }
   
}