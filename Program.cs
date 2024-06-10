using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuizMaster_Challenge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the QuizMaster");

            try
            {
                StartQuiz();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"An argument-related error occurred: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"An invalid operation error occurred: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"A format-related error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            finally
            {
                Console.WriteLine("Quiz completed.");
            }

        }

        static void StartQuiz()
        {
            // Initialize questions and answers
            string[] questions = new string[]
            {
            "What is the capital of Jordan?",
            "What is the capital of Syria?",
            "What is the capital of Lebanon?",
            "What is the capital of Egypt?",
            "What is the capital of Saudi Arabia?"
            };

            string[] answers = new string[]
            {
            "Amman",
            "Damascus",
            "Beirut",
            "Cairo",
            "Riyadh"
            };

            int score = 0;
            int timeLimit = 10; // 10 seconds

            for (int i = 0; i < questions.Length; i++)
            {
                //Console.WriteLine(questions[i]);
                Console.WriteLine("Ten Seconds to answer each question!");
                Console.WriteLine(questions[i]);
                string userAnswer = GetUserAnswerWithTimeout(timeLimit);

                if (userAnswer == null)
                {
                    Console.WriteLine("Time's up! Moving to the next question.");
                    continue;
                }

                if (string.IsNullOrWhiteSpace(userAnswer))
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    i--; // Repeat the question
                    continue;
                }

                if (userAnswer.Equals(answers[i], StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Correct!");
                    score++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Incorrect.");
                    Console.ResetColor();
                }
            }

            Console.WriteLine($"Your final score is: {score}/{questions.Length}");
        }

        static string GetUserAnswerWithTimeout(int timeLimit)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task<string> task = Task.Run(() => Console.ReadLine(), cts.Token);

            if (task.Wait(TimeSpan.FromSeconds(timeLimit)))
            {
                return task.Result;
            }
            else
            {
                cts.Cancel();
                return null;
            }
        }
    }

}

