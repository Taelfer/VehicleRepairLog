using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.CQRS.Queries;

namespace VehicleRepairLog.DataAccess
{
    public interface IQueryExecutor
    {
        Task<TResult> Execute<TResult>(QueryBase<TResult> query);
    }
}
