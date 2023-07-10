namespace com.example.seven.models
{
    public enum Suit
    {
        CLOVERS = 1,
        SPADES = 2,
        HEARTS = 3,
        DIAMONDS = 4,
    }

    public static class SuitList
    {
        public static List<Suit> GetAllSuit()
        {
            return Enum.GetValues(typeof(Suit)).Cast<Suit>().ToList();
        }
    }
}