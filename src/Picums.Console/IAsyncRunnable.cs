using System.Threading;
using System.Threading.Tasks;

namespace Picums.Console
{
    public interface IAsyncRunnable
    {
        Task Run(CancellationToken token);
    }
}