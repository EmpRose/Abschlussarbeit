using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AAIC.API.CLient.Sage100.Mobile.App.Logger
{
    public class OutputWindowLogger : IAppLogger
    // Diese Klasse stellt Informationen zur Fehlern,Warnungen oder Information zur Verfügung
    {
        public void Error(string message)
            => Debug.WriteLine("ERROR: " + message);
        public void Error(Exception exception)
            => Debug.WriteLine("ERROR: " + exception.ToString());
        public void Info(string message)
            => Debug.WriteLine("Info: " + message);
        public void Warn(string message)
            => Debug.WriteLine("Warn: " + message);
    }
}
