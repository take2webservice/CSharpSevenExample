namespace com.example.seven.models
{
    public class Line
    {
        public readonly Suit suit;
        public readonly SortedSet<Card> cards;
        public Line(Suit suit)
        {
            this.suit = suit;
            this.cards = new SortedSet<Card>(){
                new Card(suit, Card.SEVEN),
            };
        }

        // FIXME: 戻り値がなんか気持ち悪い。
        public bool setCard(Card card)
        {
            if (!CanSetCard(card))
            {
                return false;
            }
            this.cards.Add(card);
            return true;
        }

        private bool CanSetCard(Card card)
        {
            if (card.suit != this.suit)
            {
                throw new ArgumentException("カードのスートが異なります");
            }
            if (card.number == this.cards.Max.number + 1)
            {
                return true;
            }
            if (card.number == this.cards.Min.number -1)
            {
                return true;
            }
            return false;
        }

        public HashSet<Card> GetSettableCards()
        {
            HashSet<Card> cards = new HashSet<Card>();
            if (this.cards.Max.number + 1 <= Card.KING)
            {
                cards.Add(new Card(this.suit, this.cards.Max.number + 1));
            }
            if (this.cards.Min.number - 1 >= Card.ACE)
            {
                cards.Add(new Card(this.suit, this.cards.Min.number - 1));
            }
            return cards;
        }

    }
}