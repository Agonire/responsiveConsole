namespace Program.Data
{
    public class SoundQualities
    {
        public SoundQualities(int frequency, int milliSeconds)
        {
            Frequency = frequency;
            MilliSeconds = milliSeconds;
        }

        public int Frequency { get; set; }
        public int MilliSeconds { get; set; }
    }
}