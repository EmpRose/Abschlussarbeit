using System;

namespace AAIC.API.CLient.Sage100.Mobile.App.Logger
{
    public interface IAppLogger
    {
        void Error(Exception exception);
        void Error(string message);
        void Info(string message);
        void Warn(string message);
    }
}