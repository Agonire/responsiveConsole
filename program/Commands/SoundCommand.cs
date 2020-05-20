using System;
using System.Text.RegularExpressions;
using Program.CustomExceptions;
using Program.Data;

namespace Program.Commands
{
    public class SoundCommand : ICommand
    {
        public SoundQualities SoundQualities { get; set; }

        public SoundCommand(SoundQualities soundQualities)
        {
            SoundQualities = soundQualities;
        }

        public void Execute()
        {
            Console.WriteLine($"Sound of {SoundQualities.Frequency}Hz lasted for {SoundQualities.MilliSeconds}ms");
        }
    }
}