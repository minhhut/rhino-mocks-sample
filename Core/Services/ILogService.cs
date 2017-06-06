using System;

namespace Core.Services
{
    public interface ILogService
    {
        void LogError(Exception ex);
    }
}
