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

namespace Games
{
	/// <summary>
	/// Interaction logic for WhackAMoleGame.xaml
	/// </summary>
    public partial class WhackAMoleGame : Page
    {
        private int score;
		private int countDownTimer;
		private DispatcherTimer timer;
		private Quiz quizJson;
		private Random random = new Random();

		public WhackAMoleGame()
        {
            InitializeComponent();
            //Question.Content = "maafafaff";
            GetQuiz();
			//GetQuizAnswers();
			setupGame();
			startTimer();

		}


        public void setupGame()
        {
            score = 0;
            quizJson = new Quiz();
			countDownTimer = 10; // Set the countdown time in seconds
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
						}
						else
						{
							Question.Text = "No quiz found.";
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
				score += 0;
				Score.Content = "Score: " + score;
				WhackAMoleEnd whackAMoleEnd = new WhackAMoleEnd();
				NavigationService.Navigate(whackAMoleEnd); // Navigate to the new page
				Window window = Window.GetWindow(this);
				window.Content = whackAMoleEnd;
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
				incrementScore();
				startTimer();
				timer.Stop();
			}
			else
			{
				endGame();
			}


		}

        private void choice2click(object sender, RoutedEventArgs e)
        {
            if (choice2.Content == quizJson.CorrectAnswer)
            {
                incrementScore();
				startTimer();
				timer.Stop();
			}
            else
            {
                endGame();
            }
        }

        private void choice3click(object sender, RoutedEventArgs e)
        {
            if (choice3.Content == quizJson.CorrectAnswer)
            {
                incrementScore();
				startTimer();
				timer.Stop();
			}
            else
            {
                endGame();
            }
        }

        private void choice4click(object sender, RoutedEventArgs e)
        {
            if (choice4.Content == quizJson.CorrectAnswer)
            {
                incrementScore();
				startTimer();
				timer.Stop();
			}
            else
            {
                endGame();
            }
        }

		private void startTimer()
		{
			// Create a new instance of DispatcherTimer
			countDownTimer = 10;
			timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromSeconds(1); // Set the interval to 1 second
			timer.Tick += timer_Tick; // Set the event handler for the Tick event
			timer.Start(); // Start the timer
		}


		private void timer_Tick(object sender, EventArgs e)
		{
			// Update the countdown timer and display the remaining time
			countDownTimer--;
			TimeSpan timeRemaining = TimeSpan.FromSeconds(countDownTimer);
			Timer.Content = $"Timer: {timeRemaining.Minutes}:{timeRemaining.Seconds:D2}";

			if (countDownTimer <= 0)
			{
				// Stop the timer when countdown is complete
				timer.Stop();
				Timer.Content = "Countdown complete!";
				endGame();

			}
		}

	}
}


