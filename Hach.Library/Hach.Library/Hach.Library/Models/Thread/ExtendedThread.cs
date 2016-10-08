namespace Hach.Library.Models.Thread
{
    /// <summary>
    /// Extension of a normal Thread with the Property, with which the Thread can be shut down safely
    /// </summary>
    /// <author>
    /// Christian Hahn, Jun-2016
    /// </author>
    public class ExtendedThread
    {
        /// <summary>
        /// Flag to determine if the current Thread should be shut downed
        /// </summary>
        public bool ShutDownForced { get; set; }

        /// <summary>
        /// Instance of a Thread
        /// </summary>
        public System.Threading.Thread CurrentThread { get; set; }
    }
}
