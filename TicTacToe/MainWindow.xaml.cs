﻿using System;
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
        int gamesize;
        Button[] buttons;
        int playermax;
        int currentplayermax;
        int currentplayer;

        public MainWindow()
        {
            InitializeComponent();
            gamesize = 3;
            playermax = 2;
            currentplayer = 0;
            NewGame();
        }

        public void NewGame()
        {
            game = new Game(gamesize);
            currentplayermax = playermax;
            currentplayer = 0;
            buttons = new Button[game.Size * game.Size];
            Grid grid = this.GameGrid;
            grid.VerticalAlignment = VerticalAlignment.Stretch;
            grid.Children.Clear();
            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();
            for(int i = 0; i < game.Size; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                RowDefinition row = new RowDefinition();
                if (double.IsNaN(grid.Height))
                {
                    row.Height = new GridLength(285 / game.Size);
                }
                else
                {
                    row.Height = new GridLength(grid.Height / game.Size);
                }
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
            RenderButtons();
        }


        void ButtonClicked(Object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.Content = currentplayer;
            currentplayer = (currentplayer + 1) % currentplayermax;
            button.IsEnabled = false;
            checkGame();
        }

        void checkGame()
        {
            bool gameOver = false;
            int match = 0;

            //Checks Rows for winning condition
            for(int i = 0; i < game.Size-1; i++)
            {
                for(int j = 1; j < game.Size; j++)
                {
                    if (buttons[i * game.Size + j - 1].Content.Equals(buttons[i * game.Size + j].Content) && !buttons[i * game.Size + j].Content.Equals("-"))
                    {
                        match++;
                    }
                }
                if (match == game.Size - 1)
                {
                    gameOver = true;
                }
                match = 0;
            }

            //Checks Columns for winning condition
            for(int i = 0; i < game.Size; i++)
            {
                for(int j = 1; j < game.Size; j++)
                {
                    if (buttons[(j-1) * game.Size + i].Content.Equals(buttons[j * game.Size + i].Content) && !buttons[j * game.Size + i].Content.Equals("-"))
                    {
                        match++;
                    }
                }
                if (match == game.Size - 1)
                {
                    gameOver = true;
                }
                match = 0;
            }

            //Checks Cross for winning condition
            for(int i = 1; i < game.Size; i++)
            {
                if (buttons[game.Size * (i - 1) + (i - 1)].Content.Equals(buttons[game.Size * i + i].Content) && !buttons[game.Size * i + i].Content.Equals("-"))
                {
                    match++;
                }
            }
            if (match == game.Size - 1)
            {
                gameOver = true;
            }
            match = 0;
            for(int i = 2; i < game.Size+1; i++)
            {
                if(buttons[game.Size * (i-1) - (i-1)].Content.Equals(buttons[game.Size * i - i].Content) && !buttons[game.Size * i - i].Content.Equals("-"))
                {
                    match++;
                }
            }
            if (match == game.Size - 1)
            {
                gameOver = true;
            }

            if (gameOver)
            {
                GameOver();
            }
        }

        void GameOver()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].IsEnabled = false;
            }
            GameOver go = new GameOver((currentplayer + currentplayermax - 1) % currentplayermax);
            go.ShowDialog();
        }

        void Button_Settings(Object sender, RoutedEventArgs e)
        {
            Settings sd = new Settings(game.Size, currentplayermax, (int)this.Width, (int)this.Height, ChangeSettings);
            sd.ShowDialog();
        }

        void ChangeSettings(int _gamesize, int _playercount, int _width, int _height)
        {
            gamesize = _gamesize;
            playermax = _playercount;
            this.Width = _width;
            this.Height = _height;
            RenderButtons();
        }

        void RenderButtons()
        {
            foreach(RowDefinition row in this.GameGrid.RowDefinitions)
            {
                row.Height = new GridLength ((this.Height-65) / game.Size);
            }
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
