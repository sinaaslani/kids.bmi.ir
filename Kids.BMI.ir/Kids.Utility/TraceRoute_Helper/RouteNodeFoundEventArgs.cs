using System;


namespace Kids.Utility.TraceRoute_Helper
{
    /// <summary>
    /// Provides data for the RouteCompleted event of Tracert
    /// </summary>
    public class RouteNodeFoundEventArgs : EventArgs
    {
        private readonly bool _isLastNode;

        private readonly TracertNode _node;

        protected internal RouteNodeFoundEventArgs(TracertNode node, bool isDone)
        {
            _node = node;
            _isLastNode = isDone;
        }

        /// <summary>
        /// Indicates whether the value of the Node propert is the last node
        /// found by Tracert
        /// </summary>
        public bool IsLastNode
        {
            get { return _isLastNode; }
        }

        /// <summary>
        /// A node encountered during the route tracing.
        /// </summary>
        public TracertNode Node
        {
            get { return _node; }
        }
    }
}