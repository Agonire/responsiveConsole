namespace Program.Data
{
    public class Packet
    {
        public string Command { get; }
        public string Parameters { get; }
        public Packet(string command, string parameters)
        {
            Command = command;
            Parameters = parameters;
        }
    }
}