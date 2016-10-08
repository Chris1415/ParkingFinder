using System;
using System.Collections.Generic;
using Hach.Library.Models.Thread;

namespace Hach.Library.Repositories.Implementation
{
    /// <summary>
    /// Static Data Structure to hold information about running Data Collection Threads
    /// </summary>
    /// <author>
    /// Christian Hahn, Jun-2016
    /// Christian Hahn, Sep-2016
    /// </author>
    public static class ThreadRepository
    {
        /// <summary>
        /// Dictionary to map a Thread to a specific guid
        /// </summary>
        public static Dictionary<Guid, ExtendedThread> RunningThreads = new Dictionary<Guid, ExtendedThread>();
    }
}
