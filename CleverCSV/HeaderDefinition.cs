using System;
using System.Collections.Generic;
using System.Text;

namespace CleverCSVReader
{
    internal class HeaderDefinition
    {
        private IDictionary<string, Type> _header;

        public HeaderDefinition(IDictionary<string, Type> header)
        {
            _header = header;
        }

        public Type[] DataTypes { get; }
        public string[] HeaderNames { get; }


        private Type FindDominantGuess(Type[] guessedTypes)
        {
            var count = new Dictionary<string, int>();
            
            foreach(Type type in guessedTypes)
            {
                if (count.ContainsKey(type.Name))
                {
                    count[type.Name]++;
                } else
                {
                    count.Add(type.Name, 1);
                }
            }

            if (count.Count == 1)
                return guessedTypes[0];


            /*foreach (var element in count)
            {
                if (element.Value >= )
            }
            */

            return guessedTypes[0];
        }
    }
}
