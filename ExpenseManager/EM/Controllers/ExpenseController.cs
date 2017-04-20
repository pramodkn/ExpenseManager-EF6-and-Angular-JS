

namespace EM.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Data.Entity;
    using EM.ViewModels;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using DomainModel;
    using BusinessLayer.Mm.BusinessLayer;
    using MappingProfile;
    using System.Linq.Expressions;

    [RoutePrefix("api")]
    public class ExpenseController : ApiController
    {


        [Route("expensesOnCategory")]
        public List<ExpenseCategorySpendVM> GetExpenseOnCategory()
        {

            IBusinessLayer businessLayer = new BuinessLayer();

            IList<Expense> expenses = businessLayer.GetAllExpense();
            List<ExpenseCategorySpendVM> expenseDTOList = new List<ExpenseCategorySpendVM>(); ;


            var lmbda = from d in expenses
                        group d by new { d.ExpenseCategoryId, d.ExpenseCategory.CategoryName } into groupedByCategory
                        select new
                        {
                            CategoryID = groupedByCategory.Key.ExpenseCategoryId,
                            CategoryName = groupedByCategory.Key.CategoryName,
                            TotalExpense = groupedByCategory.Sum(item => item.Amount)
                        };


            foreach (var data in lmbda)
                expenseDTOList.Add(new ExpenseCategorySpendVM
                {
                    CategoryName = data.CategoryName,
                    TotalAmount = data.TotalExpense

                });
            return expenseDTOList;

        }



        [Route("expenses")]
        public List<ExpenseSumDTO> GetExpense()
        {
            IBusinessLayer businessLayer = new BuinessLayer();

            IList<Expense> expenses = businessLayer.GetAllExpense();

            var expensesL = expenses
                        .GroupBy(d => d.AddDate.Date)
            .Select(p => new ExpenseSumDTO
            {
                OnDate = p.Key.Date,
                Amount = p.Sum(s => s.Amount)
            })
            .ToList();

            return expensesL;
        }


        [Route("expenses/paged")]
        public PagedResult<ExpenseVM> GetExpensePaged(int skip = 0,
        int take = 10,
        string sortBy = "Id",
        bool isAscending = true,
        string search = null)
        {
            var predicateOr = PredicateBuilder.False<Expense>();
            if (search != null)
            {
                predicateOr = predicateOr.Or(c => c.ExpenseFor.Contains(search));
                predicateOr = predicateOr.Or(c => c.Note.Contains(search));
                predicateOr = predicateOr.Or(c => c.ExpenseCategory.CategoryName.Contains(search));
                predicateOr = predicateOr.Or(c => c.Amount.ToString().Contains(search));
                predicateOr = predicateOr.Or(c => c.AddDate.ToString().Contains(search));
            }
            IBusinessLayer businessLayer = new BuinessLayer();
            PagedResult<Expense> expensePagedResult = businessLayer.GetAllExpensePagedResult(string.IsNullOrEmpty(search) ? null : predicateOr, sortBy, isAscending, skip, take, Q => Q.ExpenseCategory);
           

            //not able to direcctly map because i have navigation propertie. if i exclude navigation propertie it will map
            /*PagedResult<ExpenseVM> f = new BaseMapper<Expense, ExpenseVM>().MapToDtoPagedResult(expensePagedResult);*/

            PagedResult<ExpenseVM> expensePagedResultVM = new PagedResult<ExpenseVM>();
            expensePagedResultVM.CurrentPage = expensePagedResult.CurrentPage;
            expensePagedResultVM.PageCount = expensePagedResult.PageCount;
            expensePagedResultVM.PageSize = expensePagedResult.PageSize;
            expensePagedResultVM.RowCount = expensePagedResult.RowCount;
            foreach (Expense e in expensePagedResult.Results)
            {
                expensePagedResultVM.Results.Add(new ExpenseVM()
                {
                    AddDate = e.AddDate,
                    Amount = e.Amount,
                    Note = e.Note,
                    ExpenseFor = e.ExpenseFor,
                    ExpenseCategory = new ExpenseCategoryVM()
                    {
                        CategoryName = e.ExpenseCategory.CategoryName
                    }
                });
            }

            // to get all expenses including expense category (pagination isn't done) for total record count

            /*IList<ExpenseVM> expenseVMIList = null; ;
            IList<Expense> expenseIList = businessLayer.GetAllExpensePaged(null, d => d.AddDate.ToString(), 2, 5, Q => Q.ExpenseCategory);
            expenseVMIList = MappingProfile.Mapper.MapperConfiguration.Map<IList<Expense>, IList<ExpenseVM>>(expenseIList);*/



            return expensePagedResultVM;
        }


        [Route("expenseCategories")]
        public List<ExpenseCategoryVM> GetExpenseCategory()
        {
            IBusinessLayer businessLayer = new BuinessLayer();
            IList<ExpenseCategory> expenseCategories = businessLayer.GetAllCategory();
            List<ExpenseCategoryVM> expense = new List<ExpenseCategoryVM>();
            expense = MappingProfile.Mapper.MapperConfiguration.Map<List<ExpenseCategory>, List<ExpenseCategoryVM>>(expenseCategories.ToList());

            return expense;
        }

        [Route("expenseSave")]
        [HttpPost]
        public ExpenseVM SaveExpense(ExpenseVM expenseDTO)
        {
            IBusinessLayer businessLayer = new BuinessLayer();

            Expense expense = new Expense();
            MappingProfile.Mapper.MapperConfiguration.Map(expenseDTO, expense);
            businessLayer.AddExpense(expense);

            return expenseDTO;
        }

    }
}