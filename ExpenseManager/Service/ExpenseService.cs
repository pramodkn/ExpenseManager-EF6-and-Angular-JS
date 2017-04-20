using EM.BOL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class ExpenseService : EntityFrameworkReadOnlyRepository<BOL.EMEntities>
    {
        public ExpenseService() : base(new BOL.EMEntities()) { }
    }
}
