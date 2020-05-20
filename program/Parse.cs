using System;
using Program.CustomExceptions;
using Program.Data;

namespace Program.Parsers
{
    public static class Parse
    {
        public static SoundQualities SoundQualities(string parameters)
        {
            if (string.IsNullOrEmpty(parameters))
                return null;
            
            var parts = parameters.Split(",");
            return new SoundQualities(
                Convert.ToInt32(parts[0]),
                Convert.ToInt32(parts[1]));
        }

        public static string Text(string parameters)
        {
            return parameters;
        }

        public static Packet Packet(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;
            
            var parts = input.Split(":");
            return new Packet(parts[0], parts[1]);
        }
    }
}