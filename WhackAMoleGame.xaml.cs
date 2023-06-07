using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using System.Net.Http.Formatting;


namespace Games
{
    /// <summary>
    /// Interaction logic for WhackAMoleGame.xaml
    /// </summary>
    public partial class WhackAMoleGame : Page
    {
        int score;
        string question;
        string answer;
        string option1;
        string option2;
        string option3;
        string option4;

        public WhackAMoleGame()
        {
            InitializeComponent();
            JsonApiCall();
        }


        public void setupGame()
        {
            score = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();

            //this.NavigationService.Navigate(mainWindow);
        }

        private async void JsonApiCall()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var address = new Uri("https://the-trivia-api.com/api/questions/");
                    var result = client.GetAsync(address).Result;
                    var quiz = result.Content.ReadAsAsync<Quiz>().Result;

                    //label1.Text = "Joke 1 " + joke.Jokes[0].Joke;

                    Question.Content = quiz.Question;

                    
                    //System.Console.WriteLine(quiz.Question);
                    //System.Console.WriteLine(joke.Jokes[0].Joke);

                    //System.Console.WriteLine("NAmeNAmeNAmeNAmeNAmeNAmeNAmeNAmeNAmeNAme");
                }
            }
            catch (AggregateException)
            {
            }
        }
    }
}
