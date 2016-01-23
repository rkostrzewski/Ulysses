using Ulysses.App.Core.Commands;

namespace Ulysses.App.Core.Tests.Helpers
{
    public class TestNoParameterCommand<T> : NoParameterCommand
    {
        private readonly IInvokable<T> _invokable;

        public TestNoParameterCommand(IInvokable<T> invokable)
        {
            _invokable = invokable;
        }

        public override bool CanExecute()
        {
            return _invokable.CanInvoke();
        }

        public override void Execute()
        {
            _invokable.Invoke();
        }
    }

    public class TestCompositeCommand<T> : CompositeCommand<T>
    {
    }
}