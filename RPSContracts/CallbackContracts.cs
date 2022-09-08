/*	
	Author: Derek Ackworth
	Date: April 4th, 2019
	File: CallbackContracts.cs
    Purpose: Rock paper scissors callback contracts
*/

using System.ServiceModel;

namespace RPSContracts
{
    // Callback contract for the client to implement
    public interface ICallback
    {
        [OperationContract(IsOneWay = true)] void UpdateGUI(CallbackInfo info);
    }
}
