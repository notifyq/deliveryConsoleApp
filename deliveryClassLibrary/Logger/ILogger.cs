﻿namespace deliveryClassLibrary.Logger
{
    public interface ILogger
    {
        void Log(string message, LogLevel level = LogLevel.Info);
    }
}
