namespace Web_API_Tutorials_.Net_Core_7_C_.MyLogging
{
    public class LogToDB : IMyLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Logtodb");
            // insert code logic here :)
        }
    }
}
