using Web_API_Tutorials_.Net_Core_7_C_;

namespace Web_API_Tutorials_.Net_Core_7_C_.MyLogging
{
    public class LogToFile : IMyLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Logtofile");
            // insert code logic here :)
        }
    }
}
