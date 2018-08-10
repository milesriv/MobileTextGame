using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TextGame
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        //***btnExit Click event. Closes App***
        private void btnExitClicked(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        //***btnStart Click event. Sets current MainPage to Game.xaml and starts the game***
        private void btnStartClicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new Game();
        }
	}
}
