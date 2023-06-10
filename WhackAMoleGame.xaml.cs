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
using System.Windows.Threading;
using System.Media;

namespace Games
{
	/// <summary>
	/// Interaction logic for WhackAMoleGame.xaml
	/// </summary>
    public partial class WhackAMoleGame : Page
    {
        private int score;
		private int countDownTimer;
		private Quiz quizJson;
		private Random random = new Random();

        private SoundPlayer win = new SoundPlayer(@"C:\Users\Gebruiker\Documents\GitHub\temp\sounds\win.wav");


        private SoundPlayer fail = new SoundPlayer(@"C:\Users\Gebruiker\Documents\GitHub\temp\sounds\fail.wav");


        private SoundPlayer completeGame = new SoundPlayer(@"C:\Users\Gebruiker\Documents\GitHub\temp\sounds\gameComplete.wav");

        public WhackAMoleGame()
        {
            InitializeComponent();
            //Question.Content = "maafafaff";
            GetQuiz();
			//GetQuizAnswers();
			setupGame();
            quizTimer();
            showGame();

		}


        public void setupGame()
        {
            score = 0;
            quizJson = new Quiz();
            countDownTimer = 30;

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();

            //this.NavigationService.Navigate(mainWindow);
        }

		private async void GetQuiz()
		{

            try
			{
				using (HttpClient client = new HttpClient())
				{
					var response = await client.GetAsync("https://the-trivia-api.com/api/questions/");
					response.EnsureSuccessStatusCode();

					if (response.IsSuccessStatusCode)
					{
						var jsonArray = await response.Content.ReadAsAsync<List<Quiz>>();
						var quiz = jsonArray.FirstOrDefault(); 

						if (quiz != null)
						{
                            quizJson = quiz;
							GetQuizQuestion(quiz);
							GetQuizAnswers(quiz);

                            enableAllBtns();
                            showGame();
                        }
						else
						{
                            //Replace with end scenario and you have answered all the questions at the time come back later
							endText.Text = "You have answered all the questions at this time, try again later";
                            endGame();
                            playSound(completeGame);
                        }
					}
					else
					{
						Question.Text = $"Server error code {response.StatusCode}";
					}
				}

			}
			catch (AggregateException)
			{
			}
		}

		//method to get the quiz qustion
		private void GetQuizQuestion(Quiz quiz)
		{
			Question.Text = quiz.Question;
		}

		//method to get the quiz answers
		private void GetQuizAnswers(Quiz quiz)
		{
			// Retrieve the correct and incorrect answers
			List<string> correctAnswers = new List<string>();
			List<string> incorrectAnswers = new List<string>();

			correctAnswers.Add(quiz.CorrectAnswer);

			if (quiz.IncorrectAnswers != null)
			{
				incorrectAnswers.AddRange(quiz.IncorrectAnswers);
			}

			// Shuffle the answers and assign them to the choice labels
			List<string> allAnswers = new List<string>();
			allAnswers.AddRange(correctAnswers);
			allAnswers.AddRange(incorrectAnswers);
			Shuffle(allAnswers);

			// Assign the answers to the choice labels
			choice1.Content = allAnswers[0];
			choice2.Content = allAnswers[1];
			choice3.Content = allAnswers[2];
			choice4.Content = allAnswers[3];

		}

		//method to shuffle the answers randomly
		private void Shuffle<T>(List<T> list)
		{
			int n = list.Count;
			while (n > 1)
			{
				n--;
				int k = random.Next(n + 1);
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
		}

       

        private void incrementScore()
        {
                score += 1;
                Score.Content = "Score: " + score;
                GetQuiz();
            
        }

        private void endGame()
        {
			
			try
			{
				score = 0;
				Score.Content = "Score: " + score;
                //WhackAMoleEnd whackAMoleEnd = new WhackAMoleEnd();
                //NavigationService.Navigate(whackAMoleEnd); // Navigate to the new page
                //Window window = Window.GetWindow(this);
                //window.Content = whackAMoleEnd;
                showEnd();
            }
			catch (NullReferenceException ex)
			{
				// Handle the exception.
				MessageBox.Show("The NavigationService property is null.");
			}
		}

        private void choice1click(object sender, RoutedEventArgs e)
        {
			if (choice1.Content == quizJson.CorrectAnswer)
			{
                disableAllBtns();
            }
			else
			{

                playSound(fail);
                endGame();
			}


		}

        private void choice2click(object sender, RoutedEventArgs e)
        {
            if (choice2.Content == quizJson.CorrectAnswer)
            {
                disableAllBtns();
            }
            else
            {

                playSound(fail);
                endGame();
            }
        }

        private void choice3click(object sender, RoutedEventArgs e)
        {
            if (choice3.Content == quizJson.CorrectAnswer)
            {
                disableAllBtns();
            }
            else
            {

                playSound(fail);
                endGame();
            }
        }

        private void choice4click(object sender, RoutedEventArgs e)
        {
            if (choice4.Content == quizJson.CorrectAnswer)
            {
                disableAllBtns();
            }
            else
            {

                playSound(fail);
                endGame();
            }
        }

        private void disableAllBtns()
        {
            incrementScore();
            playSound(win);
            countDownTimer = 30;
            choice1.IsEnabled = false;
            choice2.IsEnabled = false;
            choice3.IsEnabled = false;
            choice4.IsEnabled = false;
        }

        private void enableAllBtns()
        {
            choice1.IsEnabled = true;
            choice2.IsEnabled = true;
            choice3.IsEnabled = true;
            choice4.IsEnabled = true;
        }

        private void RetryClick(object sender, RoutedEventArgs e)
        {
            showGame();
            GetQuiz();

            countDownTimer = 30;
        }

        private void toStoreClick(object sender, RoutedEventArgs e)
        {
            //Todo
        }

        private void quizTimer()
        {
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += dtTicker;
            dt.Start();
        }

        private void playSound(SoundPlayer name)
        {
           name.Load();
           name.Play();
        }

        private void dtTicker(object sender, EventArgs e)
        {
            countDownTimer--;
            Timer.Content ="Timer: " + countDownTimer.ToString();
            if(countDownTimer == 0)
            {
                endGame();
            }

        }
        //TImer. ENd came calls the hide
        //UiEnd hid all element and alterante

        private void showGame()
        {
            Score.Visibility = Visibility.Visible;
            Timer.Visibility = Visibility.Visible;
            Question.Visibility = Visibility.Visible;
            choice1.Visibility = Visibility.Visible;
            choice2.Visibility = Visibility.Visible;
            choice3.Visibility = Visibility.Visible;
            choice4.Visibility = Visibility.Visible;
            endText.Visibility = Visibility.Collapsed;
            retry.Visibility = Visibility.Collapsed;
            toStore.Visibility = Visibility.Collapsed;
        }

        private void showEnd()
        {
            endText.Text = "You got the answer wrong, The correct answer is " + quizJson.CorrectAnswer;
            Score.Visibility = Visibility.Collapsed;
            Timer.Visibility = Visibility.Collapsed;
            Question.Visibility = Visibility.Collapsed;
            choice1.Visibility = Visibility.Collapsed;
            choice2.Visibility = Visibility.Collapsed;
            choice3.Visibility = Visibility.Collapsed;
            choice4.Visibility = Visibility.Collapsed;
            endText.Visibility = Visibility.Visible;
            retry.Visibility = Visibility.Visible;
            toStore.Visibility = Visibility.Visible;
        }

    }

}


