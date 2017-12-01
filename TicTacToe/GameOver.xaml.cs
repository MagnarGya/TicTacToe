using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for GameOver.xaml
    /// </summary>
    public partial class GameOver : Window
    {
        public GameOver(int player)
        {
            InitializeComponent();
            TextBlock text = this.Congratulations;
            text.Text = " Congratulations, player " + player + "! \nYou won!";
        }
        public void ButtonOk(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
