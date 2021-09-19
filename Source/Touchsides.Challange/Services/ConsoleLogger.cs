using System;
using System.Collections.Generic;
using System.Text;

namespace Touchsides.Challange.Services
{
    public class ConsoleLogger : ILogger
    {
        public void LogInfo(string message)
        {
            InvokeWithColorEmphasis(() =>
            {
                Console.WriteLine($"[{DateTimeOffset.UtcNow}] - INFO: {message}");
            }, ConsoleColor.Blue);
        }

        public void LogWarning(string message)
        {
            InvokeWithColorEmphasis(() =>
            {
                Console.WriteLine($"[{DateTimeOffset.UtcNow}] - WARN: {message}");
            }, ConsoleColor.Yellow);
        }

        public void LogWarning(Exception exception, string message)
        {
            InvokeWithColorEmphasis(() =>
            {
                Console.WriteLine($"[{DateTimeOffset.UtcNow}] - WARN: {message}");
                Console.WriteLine($"{GetExceptionBreadcrumps(exception)}");
            }, ConsoleColor.Yellow);
        }

        public void LogError(string message)
        {
            InvokeWithColorEmphasis(() =>
            {
                Console.WriteLine($"[{DateTimeOffset.UtcNow}] - ERROR: {message}");
            }, ConsoleColor.Red);
        }

        public void LogError(Exception exception, string message)
        {
            InvokeWithColorEmphasis(() =>
            {
                Console.WriteLine($"[{DateTimeOffset.UtcNow}] - ERROR: {message}");
                Console.WriteLine($"{GetExceptionBreadcrumps(exception)}");
            }, ConsoleColor.Red);
        }

        private void InvokeWithColorEmphasis(Action action, ConsoleColor color)
        {
            Console.BackgroundColor = color;
            action();
            Console.BackgroundColor = ConsoleColor.Black;
        }

        private string GetExceptionBreadcrumps(Exception exception)
        {
            Stack<string> breadcrumps = new Stack<string>();
            StringBuilder breadcrumpsBuilder = new StringBuilder();

            breadcrumps.Push(exception.Message);

            var innerException = exception.InnerException;
            while (innerException != null)
            {
                breadcrumps.Push(innerException.Message);
                innerException = innerException.InnerException;
            }

            for (int index = 0; index < breadcrumps.Count; index++)
            {
                breadcrumpsBuilder.AppendLine($"\t[{index + 1}] {breadcrumps.Pop()}");
            }

            return breadcrumpsBuilder.ToString();
        }
    }
}
