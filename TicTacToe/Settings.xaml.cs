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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public delegate void givesettings(int g, int p, int w, int h);
        event givesettings settingsgiver; 

        public Settings(int gamesize, int playercount, int width, int height, givesettings method)
        {
            InitializeComponent();
            settingsgiver += method;
            GameSize.Text = gamesize.ToString();
            PlayerCount.Text = playercount.ToString();
            WidthNumber.Text = width.ToString();
            HeightNumber.Text = height.ToString();
        }

        void Button_Cancel(Object sender, RoutedEventArgs e)
        {
            Close();
        }

        void Button_Save(Object sender, RoutedEventArgs e)
        {
            foreach(Object text in Texts.Children)
            {
                TextBlock tx = (TextBlock)text;
                tx.Background = Brushes.White;
            }
            int error = 0;
            int gamesize = 0, playercount = 0, width = 0, height=0;
            try {
                gamesize = Convert.ToInt32(GameSize.Text);
            }catch{
                error += 1;
            }
            try {
                playercount = Convert.ToInt32(PlayerCount.Text);
            }catch{
                error += 2;
            }
            try{
                width = Convert.ToInt32(WidthNumber.Text);
            }catch{
                error += 4;
            }
            try{
                height = Convert.ToInt32(HeightNumber.Text);
            }catch{
                error += 8;
            }
            if(error==0){
                settingsgiver.Invoke(gamesize, playercount, width, height);
                Close();
            }else{
                String errormessage = "";
                int addHeight = 0;
                if(error%2==1){
                    Size.Background = Brushes.OrangeRed;
                    errormessage += "Game Size needs to be a number \n";
                    addHeight += 20;
                }
                if((error%4)-(error%2)==2){
                    Count.Background = Brushes.OrangeRed;
                    errormessage += "Player count needs to be a number \n";
                    addHeight += 20;
                }
                if((error%8)- (error % 4) - (error % 2) == 4||(error%16)- (error % 8) - (error % 4) - (error % 2) == 8){
                    Resolution.Background = Brushes.OrangeRed;
                    errormessage += "Resolution needs to be 2 numbers \n";
                    addHeight += 20;
                }
                Error.Text = errormessage.Substring(0, errormessage.Length - 2);
            }
        }
    }
}
