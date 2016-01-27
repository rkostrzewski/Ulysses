using System;

namespace Ulysses.App.Core.Exceptions
{
    [Serializable]
    public sealed class CannotExecuteCommandException : Exception
    {
        public const string CommandTypeKey = "CommandType";

        public CannotExecuteCommandException(Type commandType)
        {
            if (!Data.Contains(CommandTypeKey))
            {
                Data.Add(CommandTypeKey, commandType);
            }
        }
    }
}