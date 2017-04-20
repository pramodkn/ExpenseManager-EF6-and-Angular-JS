using EM.DAL;
using EM.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EM.BusinessLayer
{
    namespace Mm.BusinessLayer
    {
        public interface IBusinessLayer
        {
            IList<ExpenseCategory> GetAllCategory();
            ExpenseCategory GetExpenseCategoryByName(string departmentName);
            void AddExpenseCategory(params ExpenseCategory[] departments);
            void UpdateExpenseCategory(params ExpenseCategory[] departments);
            void RemoveExpenseCategory(params ExpenseCategory[] departments);
            IList<Expense> GetAllExpense();
            IList<Expense> GetAllExpensePaged(Expression<Func<Expense, bool>> filter = null,
            Expression<Func<Expense, object>> orderBy = null,
            int? skip = default(int?),
            int? take = default(int?),
            params Expression<Func<Expense, object>>[] navigationProperties);
            PagedResult<Expense> GetAllExpensePagedResult(Expression<Func<Expense, bool>> filter = null,
           string orderBy = null,
           bool isAscending = true,
           int? skip = default(int?),
           int? take = default(int?),
           params Expression<Func<Expense, object>>[] navigationProperties);
            IList<Expense> GetEmployeesByDepartmentName(string departmentName);
            void AddExpense(Expense employee);
            void UpdateExpense(Expense employee);
            void RemoveExpense(Expense employee);
        }
       
        public class BuinessLayer : IBusinessLayer
        {
            private readonly IExpesneCategoryRepository _expenseCategoryRepository;
            private readonly IExpenseRepository _expenseRepository;

            public BuinessLayer()
            {
                _expenseCategoryRepository = new ExpenseCategoryRepository();
                _expenseRepository = new ExpenseRepository();
            }

            public BuinessLayer(IExpesneCategoryRepository deptRepository,
                IExpenseRepository employeeRepository)
            {
                _expenseCategoryRepository = deptRepository;
                _expenseRepository = employeeRepository;
            }

            public IList<ExpenseCategory> GetAllCategory()
            {
                return _expenseCategoryRepository.GetAll();
            }

            public ExpenseCategory GetExpenseCategoryByName(string departmentName)
            {
                return _expenseCategoryRepository.GetSingle(
                    d => d.CategoryName.Equals(departmentName),
                    d => d.Expense); //include related employees
            }

            public void AddExpenseCategory(params ExpenseCategory[] departments)
            {
                /* Validation and error handling omitted */
                _expenseCategoryRepository.Add(departments);
            }

            public void UpdateExpenseCategory(params ExpenseCategory[] departments)
            {
                /* Validation and error handling omitted */
                _expenseCategoryRepository.Update(departments);
            }

            public void RemoveExpenseCategory(params ExpenseCategory[] departments)
            {
                /* Validation and error handling omitted */
                _expenseCategoryRepository.Remove(departments);
            }

            public IList<Expense> GetEmployeesByDepartmentName(string departmentName)
            {
                return _expenseRepository.GetList(e => e.ExpenseFor.Equals(departmentName));
            }

            public IList<Expense> GetAllExpense()
            {
                /* Validation and error handling omitted */
                return _expenseRepository.GetAll(e => e.ExpenseCategory);
            }

            public void AddExpense(Expense employee)
            {
                /* Validation and error handling omitted */
                _expenseRepository.Add(employee);
            }

            public void UpdateExpense(Expense employee)
            {
                /* Validation and error handling omitted */
                _expenseRepository.Update(employee);
            }

            public void RemoveExpense(Expense employee)
            {
                /* Validation and error handling omitted */
                _expenseRepository.Remove(employee);
            }

            public IList<Expense> GetAllExpensePaged(Expression<Func<Expense, bool>> filter = null,
            Expression<Func<Expense, object>> orderBy = null,
            int? skip = default(int?),
            int? take = default(int?),
            params Expression<Func<Expense, object>>[] navigationProperties)
            {

                return _expenseRepository.Get(filter, orderBy, skip, take, navigationProperties);
            }

            public PagedResult<Expense> GetAllExpensePagedResult(Expression<Func<Expense, bool>> filter = null, string orderBy = null, bool isAscending = true, int? skip = default(int?), int? take = default(int?), params Expression<Func<Expense, object>>[] navigationProperties)
            {
                return _expenseRepository.GetPaggedResult(filter, orderBy, isAscending, skip, take, navigationProperties);
            }
        }
    }
}
