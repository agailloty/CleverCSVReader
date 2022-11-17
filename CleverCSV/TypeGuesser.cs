using System;
using System.Linq;

namespace CleverCSV
{
    /// <summary>
    /// A class that guesses the type of a primitive value in a string
    /// </summary>
    public class TypeGuesser
    {
        private string _value;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public TypeGuesser(string value)
        {
            _value = value;
        }

        /// <summary>
        /// Returns the type of .NET primitive. 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Type GuessValueType()
        {
            bool isSuccess = false;
            bool isBool;
            isSuccess = bool.TryParse(_value, out isBool);

            if (isSuccess)
                return typeof(bool);

            int isInt;
            isSuccess = int.TryParse(_value, out isInt);

            if (isSuccess)
                return typeof(int);

            double isDouble;
            isSuccess = double.TryParse(_value, out isDouble);
            if (isSuccess)
                return typeof(double);

            DateTime isDate;

            isSuccess = DateTime.TryParse(_value, out isDate);
            if (isSuccess)
                return typeof(DateTime);


            return typeof(string);
        }
    }
}
