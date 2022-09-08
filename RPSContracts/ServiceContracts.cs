/*	
	Author: Derek Ackworth
	Date: April 4th, 2019
	File: ServiceContracts.cs
    Purpose: Rock paper scissors service contracts
*/

using System.ServiceModel;

namespace RPSContracts
{
    // WCF ServiceContract for the Game "service"
    [ServiceContract(CallbackContract = typeof(ICallback))]
    public interface IGame
    {
        [OperationContract]
        bool Login(string username);
        [OperationContract(IsOneWay = true)]
        void Logout(string username);
        [OperationContract(IsOneWay = true)]
        void PlayerImageChange(int player, string image);
    }
}
