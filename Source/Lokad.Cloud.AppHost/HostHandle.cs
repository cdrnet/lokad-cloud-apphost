#region Copyright (c) Lokad 2009-2011
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;
using Lokad.Cloud.AppHost.Framework;

namespace Lokad.Cloud.AppHost
{
    /// <summary>
    /// Handle to communicate with the application host from the outside:
    /// - Send commands to the host
    /// - Query (stale) info about the host (like current deployment)
    /// </summary>
    internal class HostHandle
    {
        internal readonly Action<IHostCommand> SendCommand;

        internal HostHandle(Action<IHostCommand> sendCommand)
        {
            SendCommand = sendCommand;
        }
    }
}
