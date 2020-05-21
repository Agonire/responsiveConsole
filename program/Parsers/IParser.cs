namespace program.Parsers
{
    public interface IParser<T>
    {
        public T Parse(string parameters);
    }
}