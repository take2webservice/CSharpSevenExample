namespace com.example.seven.models
{
    public class Borad
    {
        public readonly Line cloversCards;
        public readonly Line spadesCards;
        public readonly Line heartsCards;
        public readonly Line diamondsCards;

        public Borad()
        {
            this.cloversCards =  new Line(Suit.CLOVERS);
            this.spadesCards =  new Line(Suit.SPADES);
            this.heartsCards =  new Line(Suit.HEARTS);
            this.diamondsCards =  new Line(Suit.DIAMONDS);
        }

        public bool SetCard(Card card)
        {
            return card.suit switch
            {
                Suit.CLOVERS => cloversCards.setCard(card),
                Suit.SPADES => spadesCards.setCard(card),
                Suit.HEARTS => heartsCards.setCard(card),
                Suit.DIAMONDS => diamondsCards.setCard(card),
                _ => throw new ArgumentException("予期しないSuitのカードです"),
            };
        }

        public HashSet<Card> GetSetableCards()
        {
            HashSet<Card> cards =  new HashSet<Card>();
            cards.UnionWith(cloversCards.GetSettableCards());
            cards.UnionWith(spadesCards.GetSettableCards());
            cards.UnionWith(heartsCards.GetSettableCards());
            cards.UnionWith(diamondsCards.GetSettableCards());
            return cards;
        }

    }
}