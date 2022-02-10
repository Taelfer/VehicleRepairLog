using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.CQRS.Queries;

namespace VehicleRepairLog.DataAccess
{
    public class QueryExecutor : IQueryExecutor
    {
        private readonly VehicleProfileStorageContext context;

        public QueryExecutor(VehicleProfileStorageContext context)
        {
            this.context = context;
        }

        public Task<TResult> Execute<TResult>(QueryBase<TResult> query)
        {
            return query.Execute(this.context);
        }
    }
}
