using System.Threading;
using System.Threading.Tasks;

namespace Twitter.StreamApp.Stream.Stream.Interfaces
{
    public interface IStreamService
    {
        Task StartStream(CancellationToken token);
        void StopStream(CancellationTokenSource cancellationSource);
    }
}