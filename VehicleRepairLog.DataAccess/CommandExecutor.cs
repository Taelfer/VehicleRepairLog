using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.CQRS.Commands;

namespace VehicleRepairLog.DataAccess
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly VehicleProfileStorageContext context;

        public CommandExecutor(VehicleProfileStorageContext context)
        {
            this.context = context;
        }

        public Task<TResult> Execute<TParameter, TResult>(CommandBase<TParameter, TResult> command)
        {
            return command.Execute(this.context);
        }
    }
}
