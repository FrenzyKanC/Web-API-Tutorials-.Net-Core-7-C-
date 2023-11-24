namespace Web_API_Tutorials_.Net_Core_7_C_.MyLogging
{
    public class LogToServerMemory : IMyLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Logtoservermemory");
            // insert code logic here :)
        }
    }
}
