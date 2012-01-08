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
        public CellLifeIdentity Cell { get; private set; }
        public Exception Exception { get; private set; }

        public CellFatalErrorRestartedEvent(CellLifeIdentity cell, Exception exception)
        {
            Cell = cell;
            Exception = exception;
        }
    }
}
