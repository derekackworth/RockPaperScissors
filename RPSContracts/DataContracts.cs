/*	
	Author: Derek Ackworth
	Date: April 4th, 2019
	File: DataContracts.cs
    Purpose: Rock paper scissors data contracts
*/

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RPSContracts
{
    // DataContract for CallbackInfo
    [DataContract]
    public class CallbackInfo
    {
        // DataMember properties
        [DataMember] public List<string> Queue { get; private set; }
        [DataMember] public string Player1Image { get; private set; }
        [DataMember] public string Player2Image { get; private set; }
        [DataMember] public string WinnerMessage { get; private set; }
        [DataMember] public int WinsAsKing { get; private set; }
        [DataMember] public bool CanLogout { get; private set; }

        // Constructor
        public CallbackInfo(List<string> q, string p1i, string p2i, string wm, int wak, bool cl)
        {
            Queue = q;
            Player1Image = p1i;
            Player2Image = p2i;
            WinnerMessage = wm;
            WinsAsKing = wak;
            CanLogout = cl;
        }
    }
}
