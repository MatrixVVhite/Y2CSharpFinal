using System.Threading.Channels;

namespace MovementAndInteraction
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");


            MovementHandler handler = new MovementHandler();
            Console.WriteLine(  handler.ToString());






        }
        
        
    }
}
