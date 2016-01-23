namespace Ulysses.App.Core.Tests.Helpers
{
    public interface IInvokable<in T>
    {
        bool CanInvoke(T parameter);

        bool CanInvoke();

        void Invoke(T parameter);

        void Invoke();
    }
}