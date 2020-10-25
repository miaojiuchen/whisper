using System.Threading.Tasks;
using Whisper.Common;

namespace Whisper.Command
{
    public interface ICommand { }

    public interface ICommand<TSession, TPackageModel> : ICommand
        where TSession : ISession
    {
        void Execute(TSession session, TPackageModel package);
    }

    public interface ICommand<TPackageModel> : ICommand<ISession, TPackageModel> { }

    public interface IAsyncCommand<TSession, TPackageModel> : ICommand
        where TSession : ISession
    {
        ValueTask ExecuteAsync(TSession session, TPackageModel package);
    }

    public interface IAsyncCommand<TPackageModel> : ICommand<ISession, TPackageModel> { }
}