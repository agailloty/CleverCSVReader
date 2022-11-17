using CleverCSV;
namespace TestCleverCSV
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            TypeGuesser _guesser;
            string[] data = { "age", "20", "20.5", "22.55", "23/08/2022", "2022-08-12", "2022/06/11" };

            foreach (string s in data)
            {
                _guesser = new TypeGuesser(s);
                Console.WriteLine($"The guessed type of {s} is {_guesser.GuessValueType().Name}");
            }
        }
    }
}