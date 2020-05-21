using program.Data;

namespace program.Parsers
{
    public class PacketParser: IParser<Packet>
    {
        public Packet Parse(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;
            
            var parts = input.Split(":");
            return new Packet(parts[0], parts[1]);
        }
    }
}