#region Copyright (c) Lokad 2011
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;

namespace Lokad.Cloud.AppHost.Framework.Events
{
    [Serializable]
    public class CellFatalErrorRestartedEvent : IHostEvent
    {
        public Exception Exception { get; private set; }
        public string CellName { get; private set; }

        public CellFatalErrorRestartedEvent(Exception exception, string cellName)
        {
            Exception = exception;
            CellName = cellName;
        }
    }
}
