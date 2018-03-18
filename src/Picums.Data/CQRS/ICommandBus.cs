using System;
using System.Threading.Tasks;

namespace Picums.Data.CQRS
{
    public interface ICommandBus : IDisposable
    {
        Task Execute<TCommand>(TCommand command) where TCommand : ICommand;
    }
}