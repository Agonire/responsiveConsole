using System;
using System.Text;
using System.Text.RegularExpressions;
using Program.CustomExceptions;
using Program.Parsers;

namespace Program
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Running");
            
            while (true)
            {
                try
                {
                    const string format = @"^[P][A-Z]{1}:[\x20-\x7E]*:[E]$";
                
                    var inputData = ResponsiveConsole.ReadKeys(new Regex(format));
                    Console.WriteLine("---Input--- " + inputData);

                    if (!Validator.ValidateRawInput(inputData, format))
                        throw new InvalidPackageException();

                    var packet = Parse.Packet(inputData);

                    var command = CommandService.CreateCommand(packet);
                    Console.WriteLine("ACK");
                    command.Execute();
                }
                catch(InvalidPackageException)
                {
                    Console.WriteLine("Invalid package");
                }
                catch (InvalidPayloadException)
                {
                    Console.WriteLine("NACK");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something bad happened. Probably parsing errors.");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }
    }
}