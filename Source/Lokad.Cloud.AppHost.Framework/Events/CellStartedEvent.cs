#region Copyright (c) Lokad 2011
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;

namespace Lokad.Cloud.AppHost.Framework.Events
{
    [Serializable]
    public class CellStartedEvent : IHostEvent
    {
        public string CellName { get; set; }

        public CellStartedEvent(string cellName)
        {
            CellName = cellName;
        }
    }
}
