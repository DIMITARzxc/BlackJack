using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlackJackwh
{

    public enum CardsScore
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
    }
    public enum CardsCostume
    {
        Spade,
        Diamond,
        Heart,
        Club
    }
    public class Card
    {
        public CardsCostume Costume { get; set; }
        public CardsScore Visage { get; set; }
        public int Value { get; set; }
    }

    public class Table
    {
        private List<Card> cards;

        public Table()
        {
            this.Initalize();
        }
        public void Shuffle()
        {
            Random rnd = new Random();
            int a = cards.Count;
            while (a > 1)
            {
                a--;
                int p = rnd.Next(a + 1);
                Card card = cards[p];
                cards[p] = cards[a];
                cards[a] = card;
            }
        }
        public void Initalize()
        {
            cards = new List<Card>();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    cards.Add(new Card() { Costume = (CardsCostume)i, Visage = (CardsScore)j });

                    if (j <= 8)
                        cards[cards.Count - 1].Value = j + 1;
                    else
                        cards[cards.Count - 1].Value = 10;
                }
            }
        }

        public Card Draw()
        {
            if (cards.Count <= 0)
            {
                this.Initalize();
                this.Shuffle();
            }

            Card cardToReturn = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 1);
            return cardToReturn;
        }
        public void PrintDeck()
        {
            
            int i = 1;
            foreach (Card card in cards)
            {
                Console.WriteLine("Card {0}: {1} of {2}. Value: {3}", i, card.Visage, card.Costume, card.Value);
                i++;
            }
        }
        public int RemainingCrads()
        {
            return cards.Count;
        }
    }

    class Program
    {

        static int money;
        static int sCoins;
        static int victory;
        static int jack;
        static Table table;
        static List<Card> myHand;
        static List<Card> computerHand;

        static void Main(string[] args)
        {
 
            Console.Title = "© Blackjack Game by Nedilko";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("♥♦♣♠SILENT CASINO♠♣♦♥");
            Console.ResetColor();
            Console.WriteLine("$Enter your deposit$");

            money = Convert.ToInt32(Console.ReadLine());
            sCoins = money * 2;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Your money {money}$ convert to silent coins x2\n Current silent coins:{sCoins}¤");
            Console.ResetColor();
            table = new Table();
            table.Shuffle();

            while (sCoins > 0)
            {
                DealerHand();
                Console.WriteLine("\nPress any key for the next hand...\n");
                ConsoleKeyInfo userInput = Console.ReadKey(true);
            }

            Console.WriteLine("Money is over, see you next time...");
            Console.ReadLine();
        }

        static void DealerHand()
        {
            if (table.RemainingCrads() < 20)
            {
                table.Initalize();
                table.Shuffle();
            }
            Console.WriteLine("Remaining Cards: {0}", table.RemainingCrads());
            Console.WriteLine($"Current Silent Coins: {sCoins}¤");
            Console.WriteLine("Bet some Silent Coins (1¤ - {0}¤)", sCoins);
            string input = Console.ReadLine().Trim().Replace(" ", "");
            int betCost;
            while (!Int32.TryParse(input, out betCost) || betCost < 1 || betCost > sCoins)
            {
                Console.WriteLine("Bad  input. Try again (1¤ - {0}¤)", sCoins);
                input = Console.ReadLine().Trim().Replace(" ", "");
            }
            Console.WriteLine();

            myHand = new List<Card>();
            myHand.Add(table.Draw());
            myHand.Add(table.Draw());

            foreach (Card card in myHand)
            {
                if (card.Visage == CardsScore.Ace)
                {
                    card.Value += 10;
                    break;
                }
            }

            Console.WriteLine("♣Player♥");
            Console.WriteLine("Card 1: {0} of {1}", myHand[0].Visage, myHand[0].Costume);
            Console.WriteLine("Card 2: {0} of {1}", myHand[1].Visage, myHand[1].Costume);
            Console.WriteLine("Total: {0}\n", myHand[0].Value + myHand[1].Value);

            computerHand = new List<Card>();
            computerHand.Add(table.Draw());
            computerHand.Add(table.Draw());

            foreach (Card card in computerHand)
            {
                if (card.Visage == CardsScore.Ace)
                {
                    card.Value += 10;
                    break;
                }
            }
            Console.WriteLine("™Computer™");
            Console.WriteLine("Card 1: {0} of {1}", computerHand[0].Visage, computerHand[1].Costume);
            Console.WriteLine("Card 2: [Hole Card]");
            Console.WriteLine("Total: {0}\n", computerHand[0].Value);

            bool insurance = false; ;

            if (computerHand[0].Visage == CardsScore.Ace)
            {
                Console.WriteLine("Belay? (yes / no)");
                string userInput = Console.ReadLine();

                while (userInput != "yes" && userInput != "no")
                {
                    Console.WriteLine("Could not understand.  (yes / no)");
                    userInput = Console.ReadLine();
                }

                if (userInput == "yes")
                {
                    insurance = true;
                    sCoins -= betCost / 2;
                    Console.WriteLine("\n!Belay Accepted!☻\n");
                }
                else
                {
                    insurance = false;
                    Console.WriteLine("\nBelay Decline☺\n");
                }
            }

            if (computerHand[0].Visage == CardsScore.Ace || computerHand[0].Value == 10)
            {
                Console.WriteLine("™Computer™ check his  blackjack...\n");
                Thread.Sleep(2000);
                if (computerHand[0].Value + computerHand[1].Value == 21)
                {
                    Console.WriteLine("™Computer™");
                    Console.WriteLine("Card 1: {0} of {1}", computerHand[0].Visage, computerHand[1].Costume);
                    Console.WriteLine("Card 2: {0} of {1}", computerHand[1].Visage, computerHand[1].Costume);
                    Console.WriteLine("Total: {0}\n", computerHand[0].Value + computerHand[1].Value);

                    Thread.Sleep(2000);

                    int amountLost = 0;

                    if (myHand[0].Value + myHand[1].Value == 21 && insurance)
                    {
                        amountLost = betCost / 2;
                        sCoins -= betCost / 2;
                    }
                    else if (myHand[0].Value + myHand[1].Value != 21 && !insurance)
                    {
                        amountLost = betCost + betCost / 2;
                        sCoins -= betCost + betCost / 2;
                    }

                    Console.WriteLine("You lost {0} Silent Coins", amountLost);

                    Thread.Sleep(1000);

                    return;
                }
                else
                {
                    Console.WriteLine("Computer does not have a blackjack, moving on...\n");
                }
            }

            if (myHand[0].Value + myHand[1].Value == 21)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Blackjack, You Won! ({0} Silent Coins)\n", betCost + betCost / 2);
                Console.ResetColor();
                sCoins += betCost + betCost / 2;

                return;
            }

            do
            {
                Console.WriteLine("Your choice? [S]tand or [H]it");
                ConsoleKeyInfo playerChoice = Console.ReadKey(true);
                while (playerChoice.Key != ConsoleKey.H && playerChoice.Key != ConsoleKey.S)
                {
                    Console.WriteLine("Bad input. Please  make your choice again: [S]tand or [H]it");
                    playerChoice = Console.ReadKey(true);
                }
                Console.WriteLine();

                switch (playerChoice.Key)
                {
                    case ConsoleKey.H:
                        myHand.Add(table.Draw());
                        Console.WriteLine("Hitted {0} of {1}", myHand[myHand.Count - 1].Visage, myHand[myHand.Count - 1].Costume);
                        int totalCardsValue = 0;
                        foreach (Card card in myHand)
                        {
                            totalCardsValue += card.Value;
                        }
                        Console.WriteLine("Total cards value now: {0}\n", totalCardsValue);
                        if (totalCardsValue > 21)
                        {
                            Console.Write("Lose! Good Lusk next time\n");
                            sCoins -= betCost;
                            Thread.Sleep(2000);
                            return;
                        }
                        else if (totalCardsValue == 21)
                        {

                            Console.WriteLine("Black Jack! Some whores come in...\n");
                            jack++;
                            if (jack >= 2)
                            {
                                Console.WriteLine("Two Black Jack's today, smell like robber");
                            }
                            Thread.Sleep(1500);
                            continue;
                        }
                        else
                        {
                            continue;
                        }


                    case ConsoleKey.S:

                        Console.WriteLine("Computer");
                        Console.WriteLine("Card 1: {0} of {1}", computerHand[0].Visage, computerHand[1].Costume);
                        Console.WriteLine("Card 2: {0} of {1}", computerHand[1].Visage, computerHand[1].Costume);

                        int dealerCardsValue = 0;
                        foreach (Card card in computerHand)
                        {
                            dealerCardsValue += card.Value;
                        }

                        while (dealerCardsValue < 17)
                        {
                            Thread.Sleep(1500);
                            computerHand.Add(table.Draw());
                            dealerCardsValue = 0;
                            foreach (Card card in computerHand)
                            {
                                dealerCardsValue += card.Value;
                            }
                            Console.WriteLine("Card {0}: {1} of {2}", computerHand.Count, computerHand[computerHand.Count - 1].Visage, computerHand[computerHand.Count - 1].Costume);
                        }
                        dealerCardsValue = 0;
                        foreach (Card card in computerHand)
                        {
                            dealerCardsValue += card.Value;
                        }
                        Console.WriteLine("Total: {0}\n", dealerCardsValue);

                        if (dealerCardsValue > 21)
                        {
                            Console.WriteLine("Computer lose! You win! ({0} Silent Coins)", betCost);
                            victory++;
                            if (victory > 2)
                            {
                                Console.WriteLine("You are so lucky today go win more SILENT COINS");
                            }
                            sCoins += betCost;
                            return;
                        }
                        else
                        {
                            int playerCardValue = 0;
                            foreach (Card card in myHand)
                            {
                                playerCardValue += card.Value;
                            }

                            if (dealerCardsValue > playerCardValue)
                            {
                                Console.WriteLine("Computer has {0} and player has {1}, computer wins!", dealerCardsValue, playerCardValue);
                                sCoins -= betCost;
                                return;
                            }
                            else
                            {
                                Console.WriteLine("Player has {0} and Computer has {1}, player wins!", playerCardValue, dealerCardsValue);
                                sCoins += betCost;
                                return;
                            }
                        }
                }

                Console.ReadLine();
            }
            while (true);
        }
    }
}
