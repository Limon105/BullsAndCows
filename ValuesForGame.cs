using System;
using System.Collections.Generic;
using System.Text;

namespace BullsAndCows
{
    public class ValuesForGame
    {
        public List<string> Values { get; set; }

        public ValuesForGame()
        {
            Values = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i != j)
                    {
                        for (int k = 0; k < 10; k++)
                        {
                            if (i != k && j != k)
                            {
                                for (int l = 0; l < 10; l++)
                                {
                                    if (i != l && j != l && k != l)
                                    {
                                        Values.Add($"{i}{j}{k}{l}");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void GetValuesOnScreen()
        {
            foreach (var item in Values)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
