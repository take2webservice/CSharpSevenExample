namespace com.example.seven.models
{
    public class Player
    {
        public readonly string name;
        public readonly SortedSet<Card> cards;

        public readonly bool isHuman;

        public GameStatus gameStatus;
        
        public Player(string name, List<Card> cards, bool isHuman)
        {
            this.name  = name;
            this.cards = new SortedSet<Card>(cards);
            this.isHuman = isHuman;
            gameStatus = GameStatus.PLAYING;
        }

        private bool IsCleared()
        {
            return this.gameStatus != GameStatus.PLAYING;
        }

        public HashSet<Card> holdingSettableCad(Borad borad)
        {
            HashSet<Card> settableCards = borad.GetSetableCards();
            settableCards.IntersectWith(this.cards);
            return settableCards;
        }

        public Borad SetCardManually(Borad borad)
        {
            if (this.IsCleared())
            {
                return borad;
            }
            HashSet<Card>  settableCards = holdingSettableCad(borad);
            Console.WriteLine("Please choise your card:");
            Console.WriteLine(String.Join(", ", settableCards));
            string? cardStr = Console.ReadLine();

            if (cardStr == null || cardStr.Length == 0)
            {
                return borad = Pass(borad);
            }
            Card selectedCard = Card.crateCardFromString(cardStr);

            if (settableCards.Count == 0)
            {
                return borad;
            }
            if (!settableCards.Contains(selectedCard))
            {
                throw new ArgumentException("場に出せないカードが指定されました");
            }
            this.cards.Remove(selectedCard);
            borad.SetCard(selectedCard);
            return borad;
        }
        public Borad SetCardAutomatically(Borad borad)
        {
            if (this.IsCleared())
            {
                return borad;
            }
            HashSet<Card> settableCards = this.holdingSettableCad(borad);
            if (settableCards.Count == 0)
            {
                return borad;
            }
            Card selectedCard = settableCards.FirstOrDefault();
            this.cards.Remove(selectedCard);
            borad.SetCard(selectedCard);
            return borad;
        }

        public Borad Pass(Borad borad)
        {
            HashSet<Card> settableCards = this.holdingSettableCad(borad);
            if (settableCards.Count == 0)
            {
                return borad;
            }
            throw new Exception("カードを出せる状態でパスの選択は禁止");
        }
    }
}