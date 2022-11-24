using System;
using System.Collections.Generic;
using System.Text;

namespace CleverCSVReader
{
    public class HeaderDefinition
    {
        private IDictionary<string, List<Type>> _header;

        public HeaderDefinition(IDictionary<string, List<Type>> header)
        {
            _header = header;
        }

        public Type[] DataTypes { get; }
        public string[] HeaderNames { get; }


        public Type FindDominantGuess(Type[] guessedTypes)
        {
            var guessesCount = new Dictionary<Type, int>();

            var typesGuessed = new HashSet<Type>();
            typesGuessed.CopyTo(guessedTypes);

            if (typesGuessed.Count == 1)
                return guessedTypes[0];

            if (typesGuessed.Contains(typeof(int)) && typesGuessed.Contains(typeof(Double)))
            {
                return typeof(Double);
            }
            
            foreach(Type type in guessedTypes)
            {
                if (guessesCount.ContainsKey(type))
                {
                    guessesCount[type]++;
                } else
                {
                    guessesCount.Add(type, 1);
                }
            }

            if (guessesCount.Count == 1)
                return guessedTypes[0];

            int maxGuessCount = 0; Type MaxGuessType = guessedTypes[0];

            foreach (var guessCount in guessesCount)
            {
                if (guessCount.Value > maxGuessCount)
                {
                    maxGuessCount = guessCount.Value;
                    MaxGuessType = guessCount.Key;
                }

            }

            

            return MaxGuessType;
        }
    }
}
