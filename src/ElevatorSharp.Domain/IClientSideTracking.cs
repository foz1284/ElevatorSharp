using System.Collections.Generic;

namespace ElevatorSharp.Domain
{
    public interface IClientSideTracking
    {
        int Index { get; }

        /// <summary>
        /// Used to keep track of newly added destinations. Used for client-side sync.
        /// </summary>
        Queue<int> NewDestinations { get; set; }

        /// <summary>
        /// Used to keep track of newly added destinations. Used for client-side sync.
        /// </summary>
        Queue<int> JumpQueueDestinations { get; set; }
    }
}