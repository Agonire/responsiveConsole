using System;

namespace program.Commands
{
    public class TextCommand : ICommand
    {
        public string Text { get; set; }

        public TextCommand(string text)
        {
            Text = text;
        }

        public void Execute()
        {
            Console.WriteLine(Text);
        }
    }
}