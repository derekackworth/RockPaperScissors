/*	
	Author: Derek Ackworth
	Date: April 4th, 2019
	File: RPSServiceHost.cs
    Purpose: Rock paper scissors service host
*/

using RPSLibrary;
using System;
using System.ServiceModel;

namespace RPSServiceHost
{
    class RPSServer
    {
        static void Main(string[] args)
        {
            ServiceHost servHost = null;

            try
            {
                // Register the service Address
                servHost = new ServiceHost(typeof(Game));

                // Run the service
                servHost.Open();
                Console.WriteLine("Service started. Press any key to quit.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Wait for a keystroke
                Console.ReadKey();

                if (servHost != null)
                {
                    servHost.Close();
                }
            }
        }
    }
}
