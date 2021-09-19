using System;

namespace Touchsides.Challange.Services
{
    public interface ILogger
    {
        void LogInfo(string message);

        void LogWarning(string message);
        void LogWarning(Exception exception, string message);

        void LogError(string message);
        void LogError(Exception exception, string message);
    }
}