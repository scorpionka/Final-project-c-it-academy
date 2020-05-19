using System;

namespace Mini_bot_sushi
{
    public class Events
    {
        public void Message(string message)
        {
            Console.WriteLine($"{message}\n");
        }
    }
}
