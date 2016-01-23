using Ulysses.App.Core.Commands;

namespace Ulysses.App.Core.Tests.Helpers
{
    public class TestCommand<T> : Command<T>
    {
        private readonly IInvokable<T> _invokable;

        public TestCommand(IInvokable<T> invokable)
        {
            _invokable = invokable;
        }

        public override bool CanExecute(T parameter)
        {
            return _invokable.CanInvoke(parameter);
        }

        public override void Execute(T parameter)
        {
            _invokable.Invoke(parameter);
        }
    }
}