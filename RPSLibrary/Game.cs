/*	
	Author: Derek Ackworth
	Date: April 4th, 2019
	File: Game.cs
    Purpose: Rock paper scissors library
*/

using RPSContracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace RPSLibrary
{
    // Define an implementation for the Game service contract
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Game : IGame
    {
        // Member variables
        private List<string> queue;
        private string player1Image;
        private string player2Image;
        private string winnerMessage;
        private int winsAsKing;
        private bool canLogout;
        private Dictionary<string, ICallback> callbacks;

        // Constructor
        public Game()
        {
            // Initialize member variables
            queue = new List<string>();
            player1Image = "";
            player2Image = "";
            winsAsKing = 0;
            canLogout = true;
            callbacks = new Dictionary<string, ICallback>();
        }

        // Add callback, add to queue and update GUI
        public bool Login(string username)
        {
            // Username must be unique
            if (callbacks.ContainsKey(username.ToUpper()))
                return false;
            else
            {
                ICallback cb = OperationContext.Current.GetCallbackChannel<ICallback>();
                callbacks.Add(username.ToUpper(), cb);
                queue.Add(username);
                Console.WriteLine($"{username} has logged in.");
                UpdateAllClients();

                return true;
            }
        }

        // Remove callback, remove from queue and update GUI
        public void Logout(string username)
        {
            if (callbacks.ContainsKey(username.ToUpper()))
            {
                if (queue[0] == username)
                {
                    winsAsKing = 0;
                    player1Image = "";
                }
                else if (queue[1] == username)
                    player2Image = "";

                callbacks.Remove(username.ToUpper());
                queue.Remove(username);
                Console.WriteLine($"{username} has logged out.");
                UpdateAllClients();
            }
        }

        // Change a player image and update GUI if they're both done
        public void PlayerImageChange(int player, string image)
        {
            if (player == 1)
            {
                player1Image = image;
                Console.WriteLine($"(King) {queue[player - 1]} has chosen.");
            }
            else if (player == 2)
            {
                player2Image = image;
                Console.WriteLine($"(Challenger) {queue[player - 1]} has chosen.");
            }

            // Game over
            if (player1Image != "" && player2Image != "")
            {
                if ((player1Image == "Rock" && player2Image == "Scissors") ||
                    (player1Image == "Paper" && player2Image == "Rock") ||
                    (player1Image == "Scissors" && player2Image == "Paper"))
                {
                    winnerMessage = $"(King) {queue[0]} Wins";
                    winsAsKing++;
                    Console.WriteLine($"(King) {queue[0]} has won.");
                }
                else if ((player1Image == "Rock" && player2Image == "Paper") ||
                    (player1Image == "Paper" && player2Image == "Scissors") ||
                    (player1Image == "Scissors" && player2Image == "Rock"))
                {
                    winnerMessage = $"(Challenger) {queue[1]} Wins";
                    winsAsKing = 0;
                    Console.WriteLine($"(Challenger) {queue[1]} has won.");
                }
                else
                {
                    winnerMessage = "Draw";
                    Console.WriteLine($"It was a draw.");
                }

                canLogout = false;
                UpdateAllClients();
                canLogout = true;

                System.Threading.Thread.Sleep(5000);

                if (winnerMessage == $"(King) {queue[0]} Wins")
                {
                    queue.Add(queue[1]);
                    queue.RemoveAt(1);
                }
                else if (winnerMessage == $"(Challenger) {queue[1]} Wins")
                {
                    queue.Add(queue[0]);
                    queue.RemoveAt(0);
                }

                player1Image = "";
                player2Image = "";
                winnerMessage = "";
                UpdateAllClients();
            }
        }

        // Update all clients
        private void UpdateAllClients()
        {
            CallbackInfo info = new CallbackInfo(queue, player1Image, player2Image, winnerMessage, winsAsKing, canLogout);

            foreach (ICallback cb in callbacks.Values)
                cb.UpdateGUI(info);
        }
    }
}
