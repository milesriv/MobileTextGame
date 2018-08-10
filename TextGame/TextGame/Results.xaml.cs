using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TextGame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Results : ContentPage
    {
        String[] strFields;     //***String array that will contain results passed in from Game.xaml. Used for livResults ListView***

        //***Constructor has String parameter to determine results that occurred while playing through Game.xaml***
        public Results(String strResults)
        {
            InitializeComponent();

            //***Check if error occured passing String from Game.xaml***
            if (strResults != null)
            {
                //***Split strResults into strFields array at each ':'***
                strFields = strResults.Split(':');

                //***Check if error occured passing String from Game.xaml***
                if (strFields.Length == 0)
                {
                    livResults.ItemsSource = new List<String>
                        {
                            "Error Processing Results"
                        };
                }

                //***Set livResults ListView Source to strFields. Each strFields index is within their own cell***
                else
                {
                    livResults.ItemsSource = strFields;
                }

            }

            //***If error occured passing String from Game.xaml***
            else
            {
                livResults.ItemsSource = new List<String>
                    {
                        "Error Processing Results"
                    };
            }
        }

        //***btnExit Click event. Closes app***
        private void btnExitClicked(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        //***btnReplay Click event. Starts up a new game***
        private void btnReplayClicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new Game();
        }

        //***lblGitHubLink Click event. Opens up web browser to GitHub page***
        private void lblGitHubLinkClicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://github.com/milesriv"));
        }
    }
}