/*	
	Author: Derek Ackworth
	Date: April 4th, 2019
	File: MainWindow.xaml.cs
*/

using RPSContracts;
using System;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RPSClient
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, UseSynchronizationContext = false)]
    public partial class MainWindow : Window, ICallback
    {
        // Member variables
        private IGame game = null;
        // Values 1, and 2 are players currently playing. 0 is not currently playing.
        private int currentPlayer = 0;

        // Constuctor
        public MainWindow()
        {
            InitializeComponent();
        }

        // On rock, paper, or scissors button click
        private void OnBtnRPSClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Change player's image
                if (currentPlayer == 1)
                {
                    imgPlayer1.Source = new BitmapImage(new Uri($"{(sender as Button).Content.ToString()}.png", UriKind.Relative));
                }
                else if (currentPlayer == 2)
                {
                    imgPlayer2.Source = new BitmapImage(new Uri($"{(sender as Button).Content.ToString()}.png", UriKind.Relative));
                }

                // Disable buttons
                btnRock.IsEnabled = false;
                btnPaper.IsEnabled = false;
                btnScissors.IsEnabled = false;

                // Change player's image in game
                game.PlayerImageChange(currentPlayer, (sender as Button).Content.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // On login/logout button click
        private void OnBtnLoginLogoutClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Login button
                if ((sender as Button).Content.ToString() == "Login" && tbUsername.Text != "")
                {
                    // Connect to the WCF service called "GameService" and activate game object
                    DuplexChannelFactory<IGame> channel = new DuplexChannelFactory<IGame>(this, "GameService");
                    game = channel.CreateChannel();

                    // Login if the user doesn't exist and change content
                    if (game.Login(tbUsername.Text))
                    {
                        txtUsername.Visibility = Visibility.Hidden;
                        tbUsername.Visibility = Visibility.Hidden;
                        tbUsername.IsEnabled = false;
                        btnLoginLogout.Content = "Logout";
                        txtQueue.Visibility = Visibility.Visible;
                        lstQueue.Visibility = Visibility.Visible;
                    }
                    // Show message box if the user already exists
                    else
                    {
                        game = null;
                        MessageBox.Show("ERROR: Username already in use. Please try again.");
                    }
                }
                // Logout button
                else if ((sender as Button).Content.ToString() == "Logout")
                {
                    // Logout and reset content
                    game.Logout(tbUsername.Text);
                    game = null;
                    tbUsername.Text = "";
                    txtPlayer1.Text = "";
                    txtPlayer1Wins.Text = "";
                    txtPlayer2.Text = "";
                    txtMessage.Text = "";
                    imgPlayer1.Source = null;
                    imgPlayer2.Source = null;
                    lstQueue.ItemsSource = null;
                    txtUsername.Visibility = Visibility.Visible;
                    tbUsername.Visibility = Visibility.Visible;
                    tbUsername.IsEnabled = true;
                    btnLoginLogout.Content = "Login";
                    txtQueue.Visibility = Visibility.Hidden;
                    lstQueue.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // On window close
        private void OnWindowClose(object sender, CancelEventArgs e)
        {
            try
            {
                // Logout
                if (game != null)
                    game.Logout(tbUsername.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // Implement ICallback contract
        private delegate void UpdateGUIDelegate(CallbackInfo info);

        // Update queue
        public void UpdateGUI(CallbackInfo info)
        {
            if (System.Threading.Thread.CurrentThread == Dispatcher.Thread)
            {
                // Update queue source
                lstQueue.ItemsSource = info.Queue.Skip(2);
                lstQueue.SelectedItem = tbUsername.Text;

                // Update players
                if (info.Queue.Count > 0)
                {
                    txtPlayer1.Text = $"(King) {info.Queue[0]}";
                    txtPlayer1Wins.Text = $"Wins as King: {info.WinsAsKing}";
                }
                else
                {
                    txtPlayer1.Text = "";
                    txtPlayer1Wins.Text = "";
                }

                if (info.Queue.Count > 1)
                    txtPlayer2.Text = $"(Challenger) {info.Queue[1]}";
                else
                    txtPlayer2.Text = "";

                // Update both player images
                if (info.Player1Image != "" && info.Player2Image != "")
                {
                    imgPlayer1.Source = new BitmapImage(new Uri($"{info.Player1Image}.png", UriKind.Relative));
                    imgPlayer2.Source = new BitmapImage(new Uri($"{info.Player2Image}.png", UriKind.Relative));
                }
                else if (info.Player1Image == "" && info.Player2Image == "")
                {
                    imgPlayer1.Source = null;
                    imgPlayer2.Source = null;
                }

                // Show winner if there is one
                txtMessage.Text = info.WinnerMessage;

                // Enable/Disable logout button
                btnLoginLogout.IsEnabled = info.CanLogout;

                // Update current player and change content
                if (txtPlayer1.Text == $"(King) {tbUsername.Text.ToString()}")
                {
                    currentPlayer = 1;
                    txtPlayer1.Foreground = Brushes.Red;
                    txtPlayer1.TextDecorations = TextDecorations.Underline;
                    txtPlayer1Wins.Foreground = Brushes.Red;
                    txtPlayer2.Foreground = Brushes.Black;
                    txtPlayer2.TextDecorations = null;

                    if (imgPlayer1.Source == null)
                    {
                        btnRock.IsEnabled = true;
                        btnPaper.IsEnabled = true;
                        btnScissors.IsEnabled = true;
                    }
                }
                else if (txtPlayer2.Text == $"(Challenger) {tbUsername.Text.ToString()}")
                {
                    currentPlayer = 2;
                    txtPlayer1.Foreground = Brushes.Black;
                    txtPlayer1.TextDecorations = null;
                    txtPlayer1Wins.Foreground = Brushes.Black;
                    txtPlayer2.Foreground = Brushes.Red;
                    txtPlayer2.TextDecorations = TextDecorations.Underline;

                    if (imgPlayer2.Source == null)
                    {
                        btnRock.IsEnabled = true;
                        btnPaper.IsEnabled = true;
                        btnScissors.IsEnabled = true;
                    }
                }
                else
                {
                    currentPlayer = 0;
                    txtPlayer1.Foreground = Brushes.Black;
                    txtPlayer1.TextDecorations = null;
                    txtPlayer1Wins.Foreground = Brushes.Black;
                    txtPlayer2.Foreground = Brushes.Black;
                    txtPlayer2.TextDecorations = null;
                    btnRock.IsEnabled = false;
                    btnPaper.IsEnabled = false;
                    btnScissors.IsEnabled = false;
                }
            }
            // Only the main (dispatcher) thread can change the GUI
            else
                Dispatcher.BeginInvoke(new UpdateGUIDelegate(UpdateGUI), info);
        }
    }
}
