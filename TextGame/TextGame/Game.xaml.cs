using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TextGame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Game : ContentPage
    {
        Int32 intScroll = 0;                //***Integer variable used extensively throughout the game in order to scroll through the text***
        List<String> Scenario = new List<String> { "Banana", "Apple", "Grapes" };     //***List of Scenarios randomly chosen 
        String strCurrentScenario = "";     //***String to determine which scenario is currently being played. Used for correct button behaviors***
        String strResultsSend = "";         //***String sent over to Results.xaml to display how the game ended in a ListView collection***

        Boolean blnAClicked = false;        //***Boolean used to determine which of the two buttons was clicked during the question portions***
        Boolean blnTapOK = true;            //***Boolean used to allow player to tap screen to scroll through text when allowed***

        public Game()
        {
            InitializeComponent();
            strCurrentScenario = "Intro";   //***Set to Intro for correct tap behavior***
            Intro();                        //***Call Intro function***
        }

        //***Function to randomly choose one of the initial three scenarios. Goes to StillLife() function if the three scenarios have been completed***
        private void ScenarioSelector()
        {
            //***Check if the three scenarios have been completed***
            if (Scenario.Count > 0)
            {
                Random random = new Random();

                int index = random.Next(Scenario.Count);    //***Randomly choose an index from the remaining scenarios list***
                strCurrentScenario = Scenario[index];       //***Set strCurrentScenario to name of randomly chosen scenario***
                Scenario.RemoveAt(index);                   //***Remove the scenario that was selected from the list so it isn't played again***

                //***Go to scenario function based on which was randomly selected***
                switch (strCurrentScenario)
                {
                    case "Banana":
                        intScroll = 0;
                        Banana();
                        break;

                    case "Apple":
                        intScroll = 0;
                        Apple();
                        break;

                    case "Grapes":
                        intScroll = 0;
                        Grapes();
                        break;
                }
            }

            //***If the three randomly chosen scenarios were completed, go to final StillLife() scenario***
            else
            {
                strCurrentScenario = "StillLife";
                intScroll = 0;
                StillLife();
            }
        }

        //***Intro flavor text to set up game***
        private void Intro()
        {
            //***Switch statement used to determine which text to display***
            //***Each screen tap increments intScroll by one to move through Intro text***
            switch (intScroll)
            {
                case 0:
                    lblText.Text = "You enter the dungeon" +
                        "\n\nTap to Continue...";

                    DisplayImage("Background.png");     //***Set imgDisplay image source and slowly fade from 0 opacity to 1 over 5 seconds***
                    HideButtons();                      //***Hide buttons and clear their text***
                    intScroll++;                        //***Increment intScroll by 1 to move through switch statement***
                    break;
                case 1:
                    lblText.Text = "You don't remember how you got here, but you still continue onward" +
                        "\n\nTap to Continue...";
                    intScroll++;
                    break;
                case 2:
                    lblText.Text = "You notice a figure getting clearer in the distance" +
                        "\n\nTap to Continue...";
                    intScroll++;
                    break;
                case 3:
                    lblText.Text = "You advance toward the figure" +
                        "\n\nTap to Continue...";
                    intScroll++;
                    break;
                case 4:
                    strResultsSend += "Got Through Intro";      //***Item that will be displayed in Results.xaml ListView collection***
                    ScenarioSelector();                         //***Call ScenarioSelector() function and randomly choose next scenario from list of remaining scenarios***
                    break;
            }
        }

        //***Banana Scenario. Filled with History Questions***
        private void Banana()
        {
            //***Switch statement used to determine which text to display***
            //***Each screen tap increments intScroll by one to move through scenario text***
            //***Answering question correctly will increment intScroll by one, wrong answer sends player to case 5000 to send them to Results.xaml early***
            switch (intScroll)
            {
                case 0:
                    lblText.Text = "The colors of the hallway change as you continue onward" +
                        "\n\nTap to Continue...";

                    DisplayImage("BackgroundBanana.png");       //***Set imgDisplay image source and slowly fade from 0 opacity to 1 over 5 seconds***
                    HideButtons();                              //***Hide buttons and clear their text***
                    intScroll++;                                //***Increment intScroll by 1 to move through switch statement***
                    break;

                case 1:
                    lblText.Text = "A figure steps into view" +
                        "\n\nTap to Continue...";
                    intScroll++;
                    break;

                case 2:
                    lblText.Text = "It's a banana" +
                        "\n\nTap to Continue...";

                    DisplayImage("FinalBanana.png");        //***Set imgDisplay image source and slowly fade from 0 opacity to 1 over 5 seconds***
                    intScroll++;
                    break;

                case 3:
                    lblText.Text = "The giant, angry banana with nice sneakers speaks" +
                        "\n\nTap to Continue...";
                    intScroll++;
                    break;

                case 4:
                    lblText.Text = "\"What's your problem?\"" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.Yellow;
                    intScroll++;
                    break;

                case 5:
                    lblText.Text = "You're not exactly sure what you've done to offend the talking banana" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.White;
                    intScroll++;
                    break;

                case 6:
                    lblText.Text = "\"Dude, what's with the attitude?\"" +
                        "\n\nTap to Continue...";
                    intScroll++;
                    break;

                case 7:
                    lblText.Text = "\"Your face bothers me. I'm not gonna let you pass unless you meet my conditions. Answer my questions, will ya?\"" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.Yellow;
                    intScroll++;
                    break;

                case 8:
                    lblText.Text = "You take this convenient segway and indulge the banana with his question" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.White;
                    intScroll++;
                    break;

                //***First Question***
                case 9:
                    lblText.Text = "Christopher Columbus' three ships were Nina, Pinta, and what?";
                    lblText.TextColor = Color.Yellow;

                    //***Populate two buttons with answer choices***
                    btnChoiceA.Text = "Santa Paola";
                    btnChoiceB.Text = "Santa Maria";
                    btnChoiceA.IsVisible = true;
                    btnChoiceB.IsVisible = true;

                    blnTapOK = false;
                    intScroll++;
                    break;

                case 10:
                    blnTapOK = true;

                    //***Wrong Choice***
                    if (blnAClicked)
                    {
                        lblText.Text = "The Banana wallops you upside the head. You just got beat up by a Banana. Your dignity is ruined" +
                        "\n\nTap to Continue...";

                        lblText.TextColor = Color.White;

                        strResultsSend += ":Bruised by the Banana";     //***Fail state item that will be displayed in Results.xaml ListView collection***
                        HideButtons();
                        intScroll = 5000;   //***Wrong choice was selected, send to Results.xaml***
                    }

                    //***Correct Choice***
                    else
                    {
                        lblText.Text = "\"Psh, that was an easy one. Try this next!\"" +
                        "\n\nTap to Continue...";

                        lblText.TextColor = Color.Yellow;
                        HideButtons();
                        intScroll++;
                    }

                    break;

                //***Second Question***
                case 11:
                    lblText.Text = "Who was born first? Socrates or Plato?";
                    lblText.TextColor = Color.Yellow;

                    //***Populate two buttons with answer choices***
                    btnChoiceA.Text = "Socrates";
                    btnChoiceB.Text = "Plato";
                    btnChoiceA.IsVisible = true;
                    btnChoiceB.IsVisible = true;

                    blnTapOK = false;
                    intScroll++;
                    break;

                case 12:
                    blnTapOK = true;

                    //***Correct Choice***
                    if (blnAClicked)
                    {
                        lblText.Text = "\"Lucky guess, you won't get my last question!\"" +
                        "\n\nTap to Continue...";

                        HideButtons();
                        intScroll++;
                    }

                    //***Wrong Choice***
                    else
                    {
                        lblText.Text = "The Banana wallops you upside the head. You just got beat up by a Banana. Your dignity is ruined" +
                        "\n\nTap to Continue...";

                        lblText.TextColor = Color.White;
                        strResultsSend += ":Bruised by the Banana";     //***Fail state item that will be displayed in Results.xaml ListView collection***
                        HideButtons();
                        intScroll = 5000;                               //***Wrong choice was selected, send to Results.xaml***
                    }
                    break;

                case 13:
                    lblText.Text = "You wait as the banana ponders his final question" +
                        "\n\nTap to Continue...";
                    lblText.TextColor = Color.White;
                    intScroll++;
                    break;

                //***Third Question***
                case 14:
                    lblText.Text = "Which President was in office during the first moon landing?";
                    lblText.TextColor = Color.Yellow;

                    //***Populate two buttons with answer choices***
                    btnChoiceA.Text = "LBJ";
                    btnChoiceB.Text = "Nixon";
                    btnChoiceA.IsVisible = true;
                    btnChoiceB.IsVisible = true;

                    blnTapOK = false;
                    intScroll++;
                    break;

                case 15:
                    blnTapOK = true;

                    //***Wrong Choice***
                    if (blnAClicked)
                    {
                        lblText.Text = "The Banana wallops you upside the head. You just got beat up by a Banana. Your dignity is ruined" +
                        "\n\nTap to Continue...";

                        lblText.TextColor = Color.White;
                        strResultsSend += ":Bruised by the Banana";     //***Fail state item that will be displayed in Results.xaml ListView collection***
                        HideButtons();
                        intScroll = 5000;                               //***Wrong choice was selected, send to Results.xaml***
                    }

                    //***Correct Choice***
                    else
                    {
                        lblText.Text = "\"Arggh! I thought I'd have you with that one\"" +
                        "\n\nTap to Continue...";

                        HideButtons();
                        intScroll++;
                    }
                    break;

                case 16:
                    lblText.Text = "\"I answered your questions, now let me pass\"" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.White;
                    intScroll++;
                    break;

                case 17:
                    lblText.Text = "\"Tch, fine. I can't believe I lost to someone like you\"" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.Yellow;
                    intScroll++;
                    break;

                case 18:
                    lblText.Text = "You leave the malcontent banana behind and continue on" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.White;
                    intScroll++;
                    break;

                case 19:
                    strResultsSend += ":Answered the Banana's questions";       //***Success item that will be displayed in Results.xaml ListView collection***
                    ScenarioSelector();                                         //***Call ScenarioSelector() function and randomly choose next scenario from list of remaining scenarios***
                    break;

                //***If user chooses wrong answer, send to Results.xaml with String collection***
                case 5000:
                    blnTapOK = false;
                    App.Current.MainPage = new TextGame.Results(strResultsSend);
                    break;
            }
        }

        //***Apple Scenario. Filled with Sports/Game Questions***
        private void Apple()
        {
            //***Switch statement used to determine which text to display***
            //***Each screen tap increments intScroll by one to move through scenario text***
            //***Answering question correctly will increment intScroll by one, wrong answer sends player to case 5000 to send them to Results.xaml early***
            switch (intScroll)
            {
                case 0:
                    lblText.Text = "The environment shifts as you walk down the hallway" +
                        "\n\nTap to Continue...";

                    DisplayImage("BackgroundApple.png");        //***Set imgDisplay image source and slowly fade from 0 opacity to 1 over 5 seconds***
                    HideButtons();                              //***Hide buttons and clear their text***
                    intScroll++;                                //***Increment intScroll by 1 to move through switch statement***
                    break;

                case 1:
                    lblText.Text = "A figure steps into view" +
                        "\n\nTap to Continue...";
                    intScroll++;
                    break;

                case 2:
                    lblText.Text = "It's an apple" +
                        "\n\nTap to Continue...";

                    DisplayImage("FinalApple.png");     //***Set imgDisplay image source and slowly fade from 0 opacity to 1 over 5 seconds***
                    intScroll++;
                    break;

                case 3:
                    lblText.Text = "The laid back Apple with awesome sneakers speaks" +
                        "\n\nTap to Continue...";
                    intScroll++;
                    break;

                case 4:
                    lblText.Text = "\"What's up dude?\"" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.Red;
                    intScroll++;
                    break;

                case 5:
                    lblText.Text = "You weren't expecting a chill Apple to cross your path" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.White;
                    intScroll++;
                    break;

                case 6:
                    lblText.Text = "\"Not much, mind if I pass?\"" +
                        "\n\nTap to Continue...";
                    intScroll++;
                    break;

                case 7:
                    lblText.Text = "\"Sorry dude. Got rules I've gotta follow. Answer my questions and I can let you by\"" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.Red;
                    intScroll++;
                    break;

                case 8:
                    lblText.Text = "You lean against the wall and wait for the Apple's questions" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.White;
                    intScroll++;
                    break;

                //***First Question***
                case 9:
                    lblText.Text = "How many sides does the home plate have in Baseball";
                    lblText.TextColor = Color.Red;

                    //***Populate two buttons with answer choices***
                    btnChoiceA.Text = "5";
                    btnChoiceB.Text = "6";
                    btnChoiceA.IsVisible = true;
                    btnChoiceB.IsVisible = true;

                    blnTapOK = false;
                    intScroll++;
                    break;

                case 10:
                    blnTapOK = true;

                    //***Correct Choice***
                    if (blnAClicked)
                    {
                        lblText.Text = "\"Not bad, see if you can get this next one dude\"" +
                        "\n\nTap to Continue...";

                        lblText.TextColor = Color.Red;
                        HideButtons();
                        intScroll++;
                    }

                    //***Wrong Choice***
                    else
                    {
                        lblText.Text = "The Apple just shakes his head in disappointment as he puts a concerned hand on your shoulder. You feel like a total jerk" +
                        "\n\nTap to Continue...";

                        lblText.TextColor = Color.White;

                        strResultsSend += ":Disappointed the Apple";        //***Fail state item that will be displayed in Results.xaml ListView collection***
                        HideButtons();
                        intScroll = 5000;                                   //***Wrong choice was selected, send to Results.xaml***
                    }

                    break;

                //***Second Question***
                case 11:
                    lblText.Text = "How many pieces does each player start with in a game of chess?";
                    lblText.TextColor = Color.Red;

                    //***Populate two buttons with answer choices***
                    btnChoiceA.Text = "16";
                    btnChoiceB.Text = "14";
                    btnChoiceA.IsVisible = true;
                    btnChoiceB.IsVisible = true;

                    blnTapOK = false;
                    intScroll++;
                    break;

                case 12:
                    blnTapOK = true;

                    //***Correct Choice***
                    if (blnAClicked)
                    {
                        lblText.Text = "\"Alright, easy enough. Answer this next one and I'll let you pass dude\"" +
                        "\n\nTap to Continue...";

                        HideButtons();
                        intScroll++;
                    }

                    //***Wrong Choice***
                    else
                    {
                        lblText.Text = "The Apple just shakes his head in disappointment as he puts a concerned hand on your shoulder. You feel like a total jerk" +
                        "\n\nTap to Continue...";

                        lblText.TextColor = Color.White;
                        strResultsSend += ":Disappointed the Apple";        //***Fail state item that will be displayed in Results.xaml ListView collection***
                        HideButtons();
                        intScroll = 5000;                                   //***Wrong choice was selected, send to Results.xaml***
                    }
                    break;

                case 13:
                    lblText.Text = "The Apple ponders for a few seconds before giving the final question" +
                        "\n\nTap to Continue...";
                    lblText.TextColor = Color.White;
                    intScroll++;
                    break;

                //***Third Question***
                case 14:
                    lblText.Text = "Who was Muhammad Ali’s opponent in \"The Rumble in the Jungle\" boxing event?";
                    lblText.TextColor = Color.Red;

                    //***Populate two buttons with answer choices***
                    btnChoiceA.Text = "Joe Frazier";
                    btnChoiceB.Text = "George Foreman";
                    btnChoiceA.IsVisible = true;
                    btnChoiceB.IsVisible = true;

                    blnTapOK = false;
                    intScroll++;
                    break;

                case 15:
                    blnTapOK = true;

                    //***Wrong Choice***
                    if (blnAClicked)
                    {
                        lblText.Text = "The Apple just shakes his head in disappointment as he puts a concerned hand on your shoulder. You feel like a total jerk" +
                        "\n\nTap to Continue...";

                        lblText.TextColor = Color.White;
                        strResultsSend += ":Disappointed the Apple";        //***Fail state item that will be displayed in Results.xaml ListView collection***
                        HideButtons();
                        intScroll = 5000;                                   //***Wrong choice was selected, send to Results.xaml***
                    }

                    //***Correct Choice***
                    else
                    {
                        lblText.Text = "\"I knew you could do it dude\"" +
                        "\n\nTap to Continue...";

                        HideButtons();
                        intScroll++;
                    }
                    break;

                case 16:
                    lblText.Text = "\"So is it alright if I pass?\"" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.White;
                    intScroll++;
                    break;

                case 17:
                    lblText.Text = "\"Yeah dude. Good luck up ahead dude\"" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.Red;
                    intScroll++;
                    break;

                case 18:
                    lblText.Text = "You give the Apple a high five as you pass by" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.White;
                    intScroll++;
                    break;

                case 19:
                    strResultsSend += ":Answered the Apple's questions";        //***Success item that will be displayed in Results.xaml ListView collection***
                    ScenarioSelector();                                         //***Call ScenarioSelector() function and randomly choose next scenario from list of remaining scenarios***
                    break;

                //***If user chooses wrong answer, send to Results.xaml with String collection***
                case 5000:
                    blnTapOK = false;
                    App.Current.MainPage = new TextGame.Results(strResultsSend);
                    break;
            }
        }

        //***Grapes Scenario. Filled with Science Questions***
        private void Grapes()
        {
            //***Switch statement used to determine which text to display***
            //***Each screen tap increments intScroll by one to move through scenario text***
            //***Answering question correctly will increment intScroll by one, wrong answer sends player to case 5000 to send them to Results.xaml early***
            switch (intScroll)
            {
                case 0:
                    lblText.Text = "The scenery morphs with each step" +
                        "\n\nTap to Continue...";

                    DisplayImage("BackgroundGrapes.png");       //***Set imgDisplay image source and slowly fade from 0 opacity to 1 over 5 seconds***
                    HideButtons();                              //***Hide buttons and clear their text***
                    intScroll++;                                //***Increment intScroll by 1 to move through switch statement***
                    break;

                case 1:
                    lblText.Text = "A figure looms ahead" +
                        "\n\nTap to Continue...";
                    intScroll++;
                    break;

                case 2:
                    lblText.Text = "It's a bunch of Grapes" +
                        "\n\nTap to Continue...";

                    DisplayImage("FinalGrapes.png");        //***Set imgDisplay image source and slowly fade from 0 opacity to 1 over 5 seconds***
                    intScroll++;
                    break;

                case 3:
                    lblText.Text = "The ridiculous bunch of grapes with rad sneakers speaks" +
                        "\n\nTap to Continue...";
                    intScroll++;
                    break;

                case 4:
                    lblText.Text = "\"Hey\"" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.Purple;
                    intScroll++;
                    break;

                case 5:
                    lblText.Text = "\"Hey. Can I pass?\"" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.White;
                    intScroll++;
                    break;

                case 6:
                    lblText.Text = "\"Nope, gotta answer some questions\"" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.Purple;
                    intScroll++;
                    break;

                case 7:
                    lblText.Text = "\"Of course I do. Never mind that I'm talking to a bunch of grapes\"" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.White;
                    intScroll++;
                    break;

                case 8:
                    lblText.Text = "The Grapes begin it's questioning" +
                        "\n\nTap to Continue...";

                    intScroll++;
                    break;

                //***First Question***
                case 9:
                    lblText.Text = "Which major galaxy is nearest to the Milky Way galaxy?";
                    lblText.TextColor = Color.Purple;

                    //***Populate two buttons with answer choices***
                    btnChoiceA.Text = "Andromeda";
                    btnChoiceB.Text = "Bode's";
                    btnChoiceA.IsVisible = true;
                    btnChoiceB.IsVisible = true;

                    blnTapOK = false;
                    intScroll++;
                    break;

                case 10:
                    blnTapOK = true;

                    //***Correct Choice***
                    if (blnAClicked)
                    {
                        lblText.Text = "\"That was an easy one. Try this one\"" +
                        "\n\nTap to Continue...";

                        lblText.TextColor = Color.Purple;
                        HideButtons();
                        intScroll++;
                    }

                    //***Wrong Choice***
                    else
                    {
                        lblText.Text = "The Grapes launches itself at you one grape at a time like a barrage of dodgeballs. You're left dazed and confused" +
                        "\n\nTap to Continue...";

                        lblText.TextColor = Color.White;

                        strResultsSend += ":Dazed and Confused by Grapes";        //***Fail state item that will be displayed in Results.xaml ListView collection***
                        HideButtons();
                        intScroll = 5000;                                   //***Wrong choice was selected, send to Results.xaml***
                    }

                    break;

                //***Second Question***
                case 11:
                    lblText.Text = "What is the universal donor Blood Type?";
                    lblText.TextColor = Color.Purple;

                    //***Populate two buttons with answer choices***
                    btnChoiceA.Text = "AB+";
                    btnChoiceB.Text = "O-";
                    btnChoiceA.IsVisible = true;
                    btnChoiceB.IsVisible = true;

                    blnTapOK = false;
                    intScroll++;
                    break;

                case 12:
                    blnTapOK = true;

                    //***Wrong Choice***
                    if (blnAClicked)
                    {
                        lblText.Text = "The Grapes launches itself at you one grape at a time like a barrage of dodgeballs. You're left dazed and confused" +
                        "\n\nTap to Continue...";

                        lblText.TextColor = Color.White;
                        strResultsSend += ":Dazed and Confused by Grapes";        //***Fail state item that will be displayed in Results.xaml ListView collection***
                        HideButtons();
                        intScroll = 5000;                                   //***Wrong choice was selected, send to Results.xaml***
                    }

                    //***Correct Choice***
                    else
                    {
                        lblText.Text = "\"Thought I had you with that one. Last one coming up\"" +
                        "\n\nTap to Continue...";

                        HideButtons();
                        intScroll++;
                    }
                    break;

                case 13:
                    lblText.Text = "The Grapes pace back and forth thinking of another question" +
                        "\n\nTap to Continue...";
                    lblText.TextColor = Color.White;
                    intScroll++;
                    break;

                //***Third Question***
                case 14:
                    lblText.Text = "What's the name of the metallic alloy composed of Copper and Zinc?";
                    lblText.TextColor = Color.Purple;

                    //***Populate two buttons with answer choices***
                    btnChoiceA.Text = "Bronze";
                    btnChoiceB.Text = "Brass";
                    btnChoiceA.IsVisible = true;
                    btnChoiceB.IsVisible = true;

                    blnTapOK = false;
                    intScroll++;
                    break;

                case 15:
                    blnTapOK = true;

                    //***Wrong Choice***
                    if (blnAClicked)
                    {
                        lblText.Text = "The Grapes launches itself at you one grape at a time like a barrage of dodgeballs. You're left dazed and confused" +
                        "\n\nTap to Continue...";

                        lblText.TextColor = Color.White;
                        strResultsSend += ":Dazed and Confused by Grapes";        //***Fail state item that will be displayed in Results.xaml ListView collection***
                        HideButtons();
                        intScroll = 5000;                                   //***Wrong choice was selected, send to Results.xaml***
                    }

                    //***Correct Choice***
                    else
                    {
                        lblText.Text = "\"Aw man, I thought I'd get you with that one\"" +
                        "\n\nTap to Continue...";

                        HideButtons();
                        intScroll++;
                    }
                    break;

                case 16:
                    lblText.Text = "\"Can I pass now?\"" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.White;
                    intScroll++;
                    break;

                case 17:
                    lblText.Text = "\"Yeah alright, fair's fair\"" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.Purple;
                    intScroll++;
                    break;

                case 18:
                    lblText.Text = "You pass by the seemingly sleepy Grapes and continue on" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.White;
                    intScroll++;
                    break;

                case 19:
                    strResultsSend += ":Answered the Grapes' questions";        //***Success item that will be displayed in Results.xaml ListView collection***
                    ScenarioSelector();                                         //***Call ScenarioSelector() function and randomly choose next scenario from list of remaining scenarios***
                    break;

                //***If user chooses wrong answer, send to Results.xaml with String collection***
                case 5000:
                    blnTapOK = false;
                    App.Current.MainPage = new TextGame.Results(strResultsSend);
                    break;
            }
        }

        //***StillLife Scenario. Filled with Art/Literature Questions***
        private void StillLife()
        {
            //***Switch statement used to determine which text to display***
            //***Each screen tap increments intScroll by one to move through scenario text***
            //***Answering question correctly will increment intScroll by one, wrong answer sends player to case 5000 to send them to Results.xaml early***
            switch (intScroll)
            {
                case 0:
                    lblText.Text = "You continue walking down this seemingly endless hallway" +
                        "\n\nTap to Continue...";

                    DisplayImage("BackgroundStillLife.png");        //***Set imgDisplay image source and slowly fade from 0 opacity to 1 over 5 seconds***
                    HideButtons();                                  //***Hide buttons and clear their text***
                    intScroll++;                                    //***Increment intScroll by 1 to move through switch statement***
                    break;
                case 1:
                    lblText.Text = "The colors on the wall morph once more" +
                        "\n\nTap to Continue...";
                    intScroll++;
                    break;
                case 2:
                    lblText.Text = "You wonder what anthropomorphic fruit you'll face next" +
                        "\n\nTap to Continue...";
                    intScroll++;
                    break;
                case 3:
                    lblText.Text = "Something comes into view ahead of you" +
                        "\n\nTap to Continue...";
                    intScroll++;
                    break;

                case 4:
                    lblText.Text = "There's a still life painting of the fruit you faced earlier because of course there is" +
                        "\n\nTap to Continue...";

                    DisplayImage("FinalStillLife.png");     //***Set imgDisplay image source and slowly fade from 0 opacity to 1 over 5 seconds***
                    intScroll++;
                    break;

                case 5:
                    lblText.Text = "\"You've done well making it this far, but you'll never make it passed my challenge!\"" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.LightGreen;
                    intScroll++;
                    break;

                case 6:
                    lblText.Text = "\"Don't you guys have anything better to do than ask people random trivia questions?\"" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.White;
                    intScroll++;
                    break;

                case 7:
                    lblText.Text = "\"Huh?\"" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.LightGreen;
                    intScroll++;
                    break;

                case 8:
                    lblText.Text = "\"Never mind, ask away\"" +
                        "\n\nTap to Continue...";

                    lblText.TextColor = Color.White;
                    intScroll++;
                    break;

                //***First Question***
                case 9:
                    lblText.Text = "What style of art was Salvador Dali famous for?";
                    lblText.TextColor = Color.LightGreen;

                    //***Populate two buttons with answer choices***
                    btnChoiceA.Text = "Impressionism";
                    btnChoiceB.Text = "Surrealism";
                    btnChoiceA.IsVisible = true;
                    btnChoiceB.IsVisible = true;

                    blnTapOK = false;
                    intScroll++;
                    break;

                case 10:
                    blnTapOK = true;

                    //***Wrong Choice***
                    if (blnAClicked)
                    {
                        lblText.Text = "The painting topples over and causes your top half to crash through the canvas. You look like a complete fool" +
                        "\n\nTap to Continue...";

                        lblText.TextColor = Color.White;
                        strResultsSend += ":Humiliated by Painting";        //***Fail state item that will be displayed in Results.xaml ListView collection***
                        HideButtons();
                        intScroll = 5000;                                   //***Wrong choice was selected, send to Results.xaml***
                    }

                    //***Correct Choice***
                    else
                    {
                        lblText.Text = "\"So it is. Prepare yourself for my next question!\"" +
                        "\n\nTap to Continue...";

                        lblText.TextColor = Color.LightGreen;
                        HideButtons();
                        intScroll++;
                    }
                    break;

                //***Second Question***
                case 11:
                    lblText.Text = "Who wrote \"Lord of the Flies\"?";
                    lblText.TextColor = Color.LightGreen;

                    //***Populate two buttons with answer choices***
                    btnChoiceA.Text = "William Golding";
                    btnChoiceB.Text = "JD Salinger";
                    btnChoiceA.IsVisible = true;
                    btnChoiceB.IsVisible = true;

                    blnTapOK = false;
                    intScroll++;
                    break;

                case 12:
                    blnTapOK = true;

                    //***Correct Choice***
                    if (blnAClicked)
                    {
                        lblText.Text = "\"Urgh, I was sure that one would get you!\"" +
                        "\n\nTap to Continue...";

                        lblText.TextColor = Color.LightGreen;
                        HideButtons();
                        intScroll++;
                    }

                    //***Wrong Choice***
                    else
                    {
                        lblText.Text = "The painting topples over and causes your top half to crash through the canvas. You look like a complete fool" +
                        "\n\nTap to Continue...";

                        lblText.TextColor = Color.White;

                        strResultsSend += ":Humiliated by Painting";        //***Fail state item that will be displayed in Results.xaml ListView collection***
                        HideButtons();
                        intScroll = 5000;                                   //***Wrong choice was selected, send to Results.xaml***
                    }
                    break;

                //***Third Question***
                case 13:
                    lblText.Text = "How many lines are in a sonnet?";
                    lblText.TextColor = Color.LightGreen;

                    //***Populate two buttons with answer choices***
                    btnChoiceA.Text = "12";
                    btnChoiceB.Text = "14";
                    btnChoiceA.IsVisible = true;
                    btnChoiceB.IsVisible = true;

                    blnTapOK = false;
                    intScroll++;
                    break;

                case 14:
                    blnTapOK = true;

                    //***Wrong Choice***
                    if (blnAClicked)
                    {
                        lblText.Text = "The painting topples over and causes your top half to crash through the canvas. You look like a complete fool" +
                        "\n\nTap to Continue...";

                        lblText.TextColor = Color.White;
                        strResultsSend += ":Humiliated by Painting";        //***Fail state item that will be displayed in Results.xaml ListView collection***
                        HideButtons();
                        intScroll = 5000;                                   //***Wrong choice was selected, send to Results.xaml***
                    }

                    //***Correct Choice***
                    else
                    {
                        lblText.Text = "\"No! I can't believe this has happened!\"" +
                        "\n\nTap to Continue...";

                        lblText.TextColor = Color.LightGreen;
                        HideButtons();
                        intScroll++;
                    }
                    break;

                case 15:
                    lblText.Text = "You watch as the painting overreacts and disappears" +
                        "\n\nTap to Continue...";

                    DisplayImage("BackgroundStillLife.png");
                    lblText.TextColor = Color.White;
                    intScroll++;
                    break;

                case 16:
                    lblText.Text = "Under any other circumstances this would be a completely bizarre scene, but at this point you're ready for this excursion to end" +
                        "\n\nTap to Continue...";

                    intScroll++;
                    break;

                case 17:
                    lblText.Text = "Your vision starts to fade" +
                        "\n\nTap to Continue...";

                    imgDisplay.FadeTo(0, 3000);     //***Fade out image to finish up game***
                    intScroll++;
                    break;

                case 18:
                    lblText.Text = "You wake up in your bed and realize it was all a dream" +
                        "\n\nTap to Continue...";

                    intScroll++;
                    break;

                case 19:
                    lblText.Text = "How cliche" +
                        "\n\nTap to Continue...";

                    intScroll++;
                    break;

                case 20:
                    lblText.Text = "You've learned absolutely nothing aside from random trivia facts you'll forget in half an hour" +
                        "\n\nTap to Continue...";

                    intScroll++;
                    break;

                case 21:
                    lblText.Text = "Congratulations!" +
                        "\n\nTap to Continue...";

                    strResultsSend += ":Completed the Game!";       //***Success item that will be displayed in Results.xaml ListView collection***
                    intScroll++;
                    break;

                //***Game has been successfully completed, send to Results.xaml with String collection***
                case 22:
                    blnTapOK = false;
                    App.Current.MainPage = new TextGame.Results(strResultsSend);
                    break;

                //***If user chooses wrong answer, send to Results.xaml with String collection***
                case 5000:
                    blnTapOK = false;
                    App.Current.MainPage = new TextGame.Results(strResultsSend);
                    break;
            }
        }

        //***Event fired when btnChoiceA is clicked***
        private void btnChoiceAClicked(object sender, EventArgs e)
        {
            blnAClicked = true;     //***Set blnAClicked to true in order to determine which button was clicked during trivia portions of scenarios***

            //***Switch statement that compares contents of strCurrentScenario in order to determine which scenario function to call***
            //***intScroll is then checked to determine which text, image, etc to display***
            switch (strCurrentScenario)
            {
                case "Intro":
                    Intro();
                    break;
                case "Banana":
                    Banana();
                    break;
                case "Apple":
                    Apple();
                    break;
                case "Grapes":
                    Grapes();
                    break;
                case "StillLife":
                    StillLife();
                    break;
            }
        }

        //***Event fired when btnChoiceB is clicked***
        private void btnChoiceBClicked(object sender, EventArgs e)
        {
            blnAClicked = false;        //***Set blnAClicked to false in order to determine which button was clicked during trivia portions of scenarios***

            //***Switch statement that compares contents of strCurrentScenario in order to determine which scenario function to call***
            //***intScroll is then checked to determine which text, image, etc to display***
            switch (strCurrentScenario)
            {
                case "Intro":
                    Intro();
                    break;
                case "Banana":
                    Banana();
                    break;
                case "Apple":
                    Apple();
                    break;
                case "Grapes":
                    Grapes();
                    break;
                case "StillLife":
                    StillLife();
                    break;
            }
        }

        //***Event is fired when screen is tapped. blnTapOK must be true in order for anything to happen. blnTapOK is set to false during trivia portions of scenarios***
        private void ScreenTapped(object sender, EventArgs e)
        {
            //***blnTapOK used to determine if a trivia portion is occuring. Nothing happens if set to false***
            if (blnTapOK)
            {
                //***Switch statement that compares contents of strCurrentScenario in order to determine which scenario function to call***
                //***intScroll is then checked to determine which text, image, etc to display***
                switch (strCurrentScenario)
                {
                    case "Intro":
                        Intro();
                        break;
                    case "Banana":
                        Banana();
                        break;
                    case "Apple":
                        Apple();
                        break;
                    case "Grapes":
                        Grapes();
                        break;
                    case "StillLife":
                        StillLife();
                        break;
                }
            }
        }

        //***Function to hide buttons and clear their text***
        private void HideButtons()
        {
            btnChoiceA.IsVisible = false;
            btnChoiceB.IsVisible = false;

            btnChoiceA.Text = "";
            btnChoiceB.Text = "";
        }

        //***Function to set imgDisplay source and fade the image in from 0 opacity to 1 over 5 seconds for added effect***
        private void DisplayImage(String strImage)
        {
            imgDisplay.Opacity = 0;
            imgDisplay.Source = strImage;
            imgDisplay.FadeTo(1, 5000);
        }
    }
}