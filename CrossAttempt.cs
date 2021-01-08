using System;
using System.Collections.Generic;
using System.Text;

namespace BullsAndCows
{
    class CrossAttempt
    {
        public Attempt First { get; set; }
        public Attempt Second { get; set; }
        public List<char> SameValue { get; set; }
        public List<char> DifferentValue { get; set; }

        public CrossAttempt(Attempt first, Attempt second)
        {
            First = first;
            Second = second;
            SameValue = new List<char>();
            DifferentValue = new List<char>();
            CrossSameValue();
            CrossDifferentValue();
        }

        public void CrossSameValue()
        {
            for (int i = 0; i < First.Number.Length; i++)
            {
                for (int j = 0; j < Second.Number.Length; j++)
                {
                    if (First.Number[i] == Second.Number[j] && !SameValue.Contains(First.Number[i]))
                    {
                        SameValue.Add(First.Number[i]);
                    }
                }
            }
        }

        public void CrossDifferentValue()
        {
            foreach (var firstNumber in First.Number)
            {
                if (!DifferentValue.Contains(firstNumber) && !SameValue.Contains(firstNumber))
                {
                    DifferentValue.Add(firstNumber);
                }
            }
            foreach (var secondNumber in Second.Number)
            {
                if (!DifferentValue.Contains(secondNumber) && !SameValue.Contains(secondNumber))
                {
                    DifferentValue.Add(secondNumber);
                }
            }
        }
    }
}
