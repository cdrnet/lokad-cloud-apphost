#region Copyright (c) Lokad 2011-2012
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;

namespace Lokad.Cloud.AppHost.Framework.Commands
{
    /// <summary>
    /// Check whether the HEAD deployment has changed, and load it if it did.
    /// </summary>
    [Serializable]
    public sealed class LoadCurrentHeadDeploymentCommand : IHostCommand
    {
    }
}
