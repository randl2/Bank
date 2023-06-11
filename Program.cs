using System.Text;

namespace Bank;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Logic logic = new Logic();
        logic.Start();
    }
}