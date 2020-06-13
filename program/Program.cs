using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using program.CustomExceptions;
using program.Parsers;
using program.Validators;

namespace program
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Running");

            const string packetFormat = @"^[P][A-Z]{1}:[\x20-\x7E]*:[E]$";
            var regex = new Regex(packetFormat);
            
            while (true)
            {
                try
                { 
                    var inputData = ResponsiveConsole.ReadKeys(regex);
                    Console.WriteLine("---Input--- " + inputData);

                    if (!regex.Match(inputData).Success)
                        throw new InvalidPackageException();
                    
                    var commandService = new CommandService();
                    
                    commandService.DefaultPayloadValidators.AddRange(new List<IPayloadValidator>() {
                        new PayloadValidator(CommandEnum.TextCommand, new Regex(@"")),
                        new PayloadValidator(CommandEnum.SoundCommand, new Regex(@"[0-9],[0-9]")),
                    });
                    commandService.DefaultCommandParsers.AddRange(new List<ICommandParser>()
                    {
                        new TextCommandParser(),
                        new SoundCommandParser(),
                    });
                    
                    var packet = new PacketParser().Parse(inputData);

                    var type = CommandService.IdentifyCommand(packet);

                    commandService.CreateCommand(packet);
                    
                    var command = commandService.CreateCommand(packet);
                    Console.WriteLine("ACK");
                    command.Execute();
                }
                catch (InvalidPayloadException)
                {
                    Console.WriteLine("NACK");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Parser error");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }
    }
}