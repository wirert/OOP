using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Cards
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Card> cards = new List<Card>();
            string[] inputCards = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries);

            foreach (var cardInfo in inputCards)
            {
                try
                {
                    string face = cardInfo.Split()[0];
                    string suit = cardInfo.Split()[1];

                    cards.Add(new Card(face, suit));
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }

            Console.WriteLine(string.Join(" ", cards));
        }
    }

    public class Card
    {
        private ReadOnlyCollection<string> validFaces;
        private readonly Dictionary<string, string> validSuits;
        private string face;
        private string suit;

        private Card()
        {
            validFaces = new List<string> { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" }.AsReadOnly();
            validSuits = new Dictionary<string, string>
            { { "S", "\u2660" }, { "H", "\u2665"}, { "D", "\u2666"}, { "C", "\u2663" } };
        }

        public Card(string face, string suit) : this()
        {
            Face = face;
            Suit = suit;
        }

        public string Face
        {
            get => face;
            private set
            {
                if (!validFaces.Contains(value))
                {
                    throw new ArgumentException("Invalid card!");
                }

                face = value;
            }
        }

        public string Suit
        {
            get => suit;
            private set
            {
                if (!validSuits.ContainsKey(value))
                {
                    throw new ArgumentException("Invalid card!");
                }

                suit = value;
            }
        }

        public override string ToString() => $"[{Face}{validSuits[Suit]}]";
    }
}
