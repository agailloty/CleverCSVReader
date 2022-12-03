using CleverCSVReader;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace CleverCSV
{
    /// <summary>
    /// A class that guesses the type of a primitive value in a string
    /// </summary>
    public class TypeGuesser
    {
        private IReader _reader;
        private string _value;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public TypeGuesser(string value)
        {
            _value = value;
        }

        public TypeGuesser(IReader reader)
        {
            _reader = reader;
        }

        private Dictionary<string, List<Type>> GuessRowTypes(int maxIter = 200)
        {
            var typeDict = new Dictionary<string, List<Type>>();
            int iter = 0;
            _reader.Read();
            _reader.ReadHeader();
            int headerCount = _reader.HeaderRecord.Count();
            string record = null;

            while (_reader.Read())
            {
                for (int i = 0; i < headerCount; i++)
                {
                    record = _reader.GetField(i);
                    if (typeDict.ContainsKey(_reader.HeaderRecord[i]))
                    {
                        typeDict[_reader.HeaderRecord[i]].Add(GuessValueType(record));
                    } 
                    else
                    {
                        var typeList = new List<Type>();
                        typeList.Add(GuessValueType(record));
                        typeDict.Add(_reader.HeaderRecord[i], typeList);
                    }
                    
                }

                iter++;
                if (iter >= maxIter)
                    break;
            }

            return typeDict;
            
        }

        public Dictionary<string, Type> ResolveFieldTypes()
        {
            var valueTypeMap = new Dictionary<string, Type>();
            var typeDict = GuessRowTypes();

            foreach (var type in typeDict)
            {
                valueTypeMap.Add(type.Key, FindDominantGuess(type.Value.ToArray()));
            }

            return valueTypeMap;
        }


        private Type FindDominantGuess(Type[] guessedTypes)
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

            foreach (Type type in guessedTypes)
            {
                if (guessesCount.ContainsKey(type))
                {
                    guessesCount[type]++;
                }
                else
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




        /// <summary>
        /// Returns the type of .NET primitive. 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Type GuessValueType()
        {
            return GuessValueType(_value);
        }

        public Type GuessValueType(string value)
        {

            if (value is null)
                return null;

            bool isSuccess = false;
            bool isBool;
            isSuccess = bool.TryParse(value, out isBool);

            if (isSuccess)
                return typeof(bool);

            int isInt;
            isSuccess = int.TryParse(value, out isInt);

            if (isSuccess)
                return typeof(int);

            double isDouble;
            isSuccess = double.TryParse(value, out isDouble);
            if (isSuccess)
                return typeof(double);

            DateTime isDate;

            isSuccess = DateTime.TryParse(value, out isDate);
            if (isSuccess)
                return typeof(DateTime);


            return typeof(string);
        }
    }
}
