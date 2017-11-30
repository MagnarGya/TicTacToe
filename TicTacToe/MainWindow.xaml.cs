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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game game;
        Button[] buttons;
        int playermax;
        int currentplayer;

        public MainWindow()
        {
            InitializeComponent();
            game = new Game(3);
            playermax = 2;
            currentplayer = 0;
            NewGame();
        }

        public void NewGame()
        {
            buttons = new Button[game.Size * game.Size];
            Grid grid = (Grid)this.FindName("Game");
            grid.Children.Clear();
            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();
            for(int i = 0; i < game.Size; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                RowDefinition row = new RowDefinition();
                grid.ColumnDefinitions.Add(col);
                grid.RowDefinitions.Add(row);
            }
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = new Button();
                buttons[i].Content = "-";
                buttons[i].Click += new RoutedEventHandler(this.ButtonClicked);
                Grid.SetRow(buttons[i], (i / game.Size));
                Grid.SetColumn(buttons[i], (i % game.Size));
                grid.Children.Add(buttons[i]);
            }
        }


        void ButtonClicked(Object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.Content = currentplayer;
            currentplayer = (currentplayer + 1) % playermax;
            checkGame();
        }
        void checkGame()
        {

        }

        private void Button_NewGame(object sender, RoutedEventArgs e)
        {
            NewGame();
        }
    }
}

    public class Game
{
    private int m_size;
    public int Size { get { return m_size; } }

    public Game(int _size)
    {
        m_size = _size;
    }
}
