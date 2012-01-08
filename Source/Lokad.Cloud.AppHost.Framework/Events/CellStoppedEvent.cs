#region Copyright (c) Lokad 2011
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;

namespace Lokad.Cloud.AppHost.Framework.Events
{
    [Serializable]
    public class CellStoppedEvent : IHostEvent
    {
        public CellLifeIdentity Cell { get; private set; }

        public CellStoppedEvent(CellLifeIdentity cell)
        {
            Cell = cell;
        }
    }
}
