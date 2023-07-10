using System.Linq;

namespace com.example.seven.models
{
    public struct Card: IComparable<Card>
    {
        public const int ACE = 1;
        public const int KING = 13;
        public const int SEVEN = 7;

        public readonly Suit suit;
        public readonly int number;

        public Card(Suit suit, int number)
        {
            if (IsValidNumber(number))
            {
                throw new ArgumentException("numberは1~13の間の整数を入力してください。");
            }
            this.suit = suit;
            this.number = number;
        }

        public int CompareTo(Card other)
        {
            int numberComparison = this.number.CompareTo(other.number);
            if (numberComparison != 0)
            {
                return numberComparison;
            }

            return this.suit.CompareTo(other.suit);
        }

        public override int GetHashCode()
        {
            return this.number.GetHashCode() ^ this.suit.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is Card other)
            {
                return this.suit == other.suit && this.number == other.number;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{this.suit}:{this.number}";
        }

        private static bool IsValidNumber(int number)
        {
            return number < ACE && number > KING;
        }

        public static Card crateCardFromString(string str)
        {
             string[] splited = str.Split(":");
             Card card = new Card(((Suit)Enum.Parse(typeof(Suit), splited[0])), int.Parse(splited[1]));
            return card;
        }

        public static List<Card> CreateShuffledAllCards()
        {
            List<Card> cards = new List<Card>();
            foreach (Suit suit in SuitList.GetAllSuit())
            {
                foreach (int number in Enumerable.Range(ACE, KING))
                {
                    cards.Add(new Card(suit, number));
                }
            }
            cards.Remove(new Card(Suit.CLOVERS, SEVEN));
            cards.Remove(new Card(Suit.SPADES, SEVEN));
            cards.Remove(new Card(Suit.HEARTS, SEVEN));
            cards.Remove(new Card(Suit.DIAMONDS, SEVEN));
            return cards.OrderBy(e => Guid.NewGuid()).ToList<Card>();
        }
    }
}