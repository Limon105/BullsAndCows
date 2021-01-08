using System;

namespace BullsAndCows
{
    class Program
    {
        static void Main(string[] args)
        {
            ValuesForGame valuesForGame = new ValuesForGame();
            Game game = new Game(valuesForGame);
            ReceivingInputData(game);
        }

        static void EnterOneAttempt(string value, ref int bulls, ref int cows)
        {
            Console.WriteLine(value);
            Console.WriteLine("Введите количество Быков - > ");
            bulls = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите количество Коров - > ");
            cows = Convert.ToInt32(Console.ReadLine());
        }


        static void ReceivingInputData(Game game)
        {
            Console.WriteLine("Начало игры \"Быки и Коровы\"");
            int attemptNumber = 1;
            int bulls = 0;
            int cows = 0;
            do
            {
                Attempt attempt;
                string value = String.Empty;
                value = game.GetValue();
                EnterOneAttempt(value, ref bulls, ref cows);
                attempt = new Attempt(value, bulls, cows);
                game.Attempts.Add(attempt);
                attemptNumber++;
                game.AnalysisReceivedData(ref attemptNumber);
            } while (bulls != 4);
        }
    }
}
