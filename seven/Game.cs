using com.example.seven.models;

namespace com.example.seven{
    class Game{
        public readonly List<Player> players;

        private Borad borad;

        private int turn;

        private int clearedPlayers = 0;

        public int Turn
        {
            get { return turn; }
            private set { turn = value; }
        }


        public Game(int playersNum)
        {
            ValidatedPlayerNum(playersNum);
            this.players = InitializePlayer(playersNum);
            this.turn = 1;
            this.borad = new Borad();
        }

        public bool IsCleared()
        {
            return this.clearedPlayers == players.Count;
        }

        public void Play()
        {
            foreach (Player player in this.players)
            {
                // カードを選んで出す
                if (player.isHuman)
                {
                    HashSet<Card>  settableCards = player.holdingSettableCad(this.borad);
                    Console.WriteLine("Please choise your card:");
                    Console.WriteLine(String.Join(",", settableCards));
                    string? cardStr = Console.ReadLine();
                    if (cardStr == null || cardStr.Length == 0){
                        this.borad = player.Pass(this.borad);
                    }else{
                        Card card = Card.crateCardFromString(cardStr);
                        this.borad = player.SetCardManually(this.borad);
                    }
                } else {
                    this.borad = player.SetCardAutomatically(this.borad);
                }
                if (player.cards.Count == 0 && player.gameStatus == GameStatus.PLAYING)
                {
                    switch (this.clearedPlayers)
                    {
                        case ((int)GameStatus.PLAYING):
                            player.gameStatus = GameStatus.CLEARED_FIRST;
                            this.clearedPlayers++;
                            break;
                        case ((int)GameStatus.CLEARED_FIRST):
                            player.gameStatus = GameStatus.CLEARED_SECOND;
                            this.clearedPlayers++;
                            break;
                        case ((int)GameStatus.CLEARED_SECOND):
                            player.gameStatus = GameStatus.CLEARED_THIRD;
                            this.clearedPlayers++;
                            break;
                        case ((int)GameStatus.CLEARED_THIRD):
                            player.gameStatus = GameStatus.CLEARED_FOURTH;
                            this.clearedPlayers++;
                            break;
                        default:
                            throw new Exception("意図しないクリア状態です");
                    }
                    Console.WriteLine($"{player.name}がクリアしました");
                }
            }
            turn++;
        }

        private static void ValidatedPlayerNum(int playersNum)
        {
            if (playersNum < 2 || playersNum > 4)
            {
                throw new ArgumentException("プレイヤー人数は2~4名です。");
            }
        }

        private static List<List<Card>> HandsOutCards(int playersNum)
        {
            List<Card> cards = Card.CreateShuffledAllCards();
            int playerCardsNum = cards.Count / playersNum;
            return cards.Chunk(playerCardsNum).Select(cards => new List<Card>(cards)).ToList();
        }

        private static List<Player> InitializePlayer(int playersNum)
        {
            List<List<Card>> handsOutCards = HandsOutCards(playersNum);
            List<Player> players = new List<Player>();
            int player_no = 1;
            foreach (List<Card> cards in handsOutCards)
            {
                players.Add(new Player($"プレイヤー{player_no}", cards, player_no == 1));
                player_no++;
            }
            return players;
        }
    }
}