using EM.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.DAL
{
    public interface IExpesneCategoryRepository : IGenericDataRepository<ExpenseCategory>
    {
    }

    public interface IExpenseRepository : IGenericDataRepository<Expense>
    {
    }

    public class ExpenseCategoryRepository : GenericDataRepository<ExpenseCategory>, IExpesneCategoryRepository
    {
    }

    public class ExpenseRepository : GenericDataRepository<Expense>, IExpenseRepository
    {
    }
}
