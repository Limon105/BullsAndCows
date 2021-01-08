using System;
using System.Collections.Generic;
using System.Text;

namespace BullsAndCows
{
    class Game
    {
        public ValuesForGame ValuesForGame { get; set; }

        public List<Attempt> Attempts { get; set; }

        public List<char> WinnerNumbersCows { get; set; }
        public List<char> WinnerNumbersBulls { get; set; }

        private List<char> NumbersCow { get; set; }

        private List<CrossAttempt> crossAttempts;

        readonly List<char> allNumbers = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public Game(ValuesForGame valuesForGame)
        {
            ValuesForGame = valuesForGame;
            Attempts = new List<Attempt>();
            WinnerNumbersCows = new List<char>();
            NumbersCow = new List<char>();
            crossAttempts = new List<CrossAttempt>();
        }

        public void AnalysisReceivedData(ref int attemptNumber)
        {
            bool winnerNumbersCowIsAlreadyFound = Convert.ToBoolean(WinnerNumbersCows.Count);
            for (int k = 0; k < Attempts.Count; k++)
            {
                Attempt attempt = Attempts[k];
                if (attempt.CountBulls + attempt.CountCows == 4)
                {
                    if (attempt.CountBulls == 3 && attempt.CountCows == 1)
                    {
                        Console.WriteLine($"Вы запутались в решении поскольку, невозможно чтобы было {attempt.CountBulls} быка и {attempt.CountCows}");
                        Attempts.Remove(attempt);
                        attemptNumber--;
                    }
                    else
                    {
                        if (attempt.CountBulls == 4)
                        {
                            Console.WriteLine($"Решение найдено - > {attempt.Number}");
                            break;
                        }
                        else
                        {
                            if (!winnerNumbersCowIsAlreadyFound)
                            {
                                Console.WriteLine($"Найдены все цифры которые используются в решении -> {attempt.Number}");
                                for (int i = 0; i < attempt.Number.Length; i++)
                                {
                                    WinnerNumbersCows.Add(attempt.Number[i]);
                                }
                                winnerNumbersCowIsAlreadyFound = true;
                                Delete();
                            }
                            FindBullsForWin();
                            break;
                        }
                    }
                }
                else
                {
                    if (attempt.CountBulls + attempt.CountCows == 0)
                    {
                        Console.WriteLine($"Найдено числа которые не используются в решении -> {attempt.Number}");
                        Delete(attempt);
                        Attempts.Remove(attempt);
                        attemptNumber--;
                        break;
                    }
                }
            }
            if (Attempts.Count != 0)
            {
                FindCowsForWin(Attempts[Attempts.Count - 1]);
            }
            
        }

        private void FindBullsForWin()
        {
            foreach (var num in WinnerNumbersCows)
            {
                foreach (var attempt in Attempts)
                {
                    if(attempt.Number.Contains(num) && attempt.CountBulls == 0)
                    {
                        DeleteAttPosition(num, attempt.Number.IndexOf(num));
                    }
                }
            }

            int k = 0;

        }

        private void FindCowsForWin(Attempt attempt)
        {
            for (int i = 0; i < Attempts.Count; i++)
            {
                if (Attempts[i] != attempt)
                {
                    crossAttempts.Add(new CrossAttempt(Attempts[i], Attempts[Attempts.Count - 1]));
                }
            }
            AnalysisCrossAttempts();
        }

        private void AnalysisCrossAttempts()
        {
            foreach (var item in crossAttempts)
            {
                if (item.SameValue.Count == 1 && item.First.SummCowsAndBulls >= 1 && item.Second.SummCowsAndBulls >= 1)
                {
                    if (item.First.CountBulls >= 1 && item.Second.CountBulls >= 1 && item.First.CountCows == 0 && item.Second.CountCows == 0)
                    {
                        NumbersCow.Add(item.SameValue[0]);
                        DeleteAllPositionExcept(item.SameValue[0], item.First.Number.IndexOf(item.SameValue[0]));
                    }

                    if (item.First.CountBulls >= 1 && item.Second.CountCows >= 1 && item.First.CountBulls == 0 && item.Second.CountCows == 0)
                    {
                        NumbersCow.Add(item.SameValue[0]);
                        DeleteAllPositionExcept(item.SameValue[0], item.First.Number.IndexOf(item.SameValue[0]));
                    }

                    if (item.First.CountCows >= 1 && item.Second.CountBulls >= 1 && item.First.CountCows == 0 && item.Second.CountCows == 0)
                    {
                        NumbersCow.Add(item.SameValue[0]);
                        DeleteAllPositionExcept(item.SameValue[0], item.Second.Number.IndexOf(item.SameValue[0]));
                    }  
                }

                if (item.SameValue.Count == 3 && item.First.SummCowsAndBulls >= 1 && item.Second.SummCowsAndBulls >= 1)
                {
                    if(item.First.SummCowsAndBulls == 1 && item.Second.SummCowsAndBulls == 1)
                    {
                        foreach (var num in item.SameValue)
                        {
                            Delete(num);
                        }
                    }
                }

                if (item.SameValue.Count == 3 && item.First.SummCowsAndBulls >= 3 && item.Second.SummCowsAndBulls >= 3)
                {
                    if (item.First.SummCowsAndBulls == 3 && item.Second.SummCowsAndBulls == 3)
                    {
                        foreach (var num in item.DifferentValue)
                        {
                            Delete(num);
                        }
                    }
                }
            }
        }


        private void Delete(Attempt attempt)
        {
            for (int i = 0; i < attempt.Number.Length; i++)
            {
                Delete(attempt.Number[i]);
            }
        }

        private void Delete(char number)
        {
            for (int i = 0; i < ValuesForGame.Values.Count; i++)
            {
                if (ValuesForGame.Values[i].Contains(number))
                {
                    ValuesForGame.Values.RemoveAt(i);
                    i--;
                }
            }
        }

        private void DeleteAllPositionExcept(char number, int position)
        {
            for (int i = 0; i < ValuesForGame.Values.Count; i++)
            {
                if (ValuesForGame.Values[i].Contains(number) && !(ValuesForGame.Values[i][position] == number))
                {
                    ValuesForGame.Values.RemoveAt(i);
                    i--;
                }
            }
        }

        private void DeleteAttPosition(char number, int position)
        {
            for (int i = 0; i < ValuesForGame.Values.Count; i++)
            {
                if (ValuesForGame.Values[i].Contains(number) && (ValuesForGame.Values[i][position] == number))
                {
                    ValuesForGame.Values.RemoveAt(i);
                    i--;
                }
            }
        }


        private void Delete()
        {
            foreach (var number in allNumbers)
            {
                if (!WinnerNumbersCows.Contains(number))
                {
                    Delete(number);
                }
            }
        }

        public string GetValue()
        {
            Random r = new Random();
            return ValuesForGame.Values[r.Next(0, ValuesForGame.Values.Count)];
        }
    }
}
