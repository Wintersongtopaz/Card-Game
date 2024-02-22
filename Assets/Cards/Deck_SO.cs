using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
//A Scriptable Object holdoing multiple card types and their count in the deck
public class Deck_SO : ScriptableObject
{
    const int MAX_CARDS = 40;
   public const int MAX_CARD_COUNT = 4;

    [System.Serializable]
    public class CardType
    {
        //A nested class representing a single card type in a deck
        [SerializeField] public Card_SO cardType;
        [SerializeField] public int count;
    }

    [SerializeField] public List<CardType> cards = new List<CardType>();
    //A public property used to obtain a list of Card_SO in the deck
    public List<Card_SO> CardList
    {
        get
        {
            List<Card_SO> temp = new List<Card_SO>();
            foreach(CardType cardType in cards)
            {
                for (int i = 0; i < cardType.count; i++) temp.Add(cardType.cardType);
            }
            return temp;
        }
    }
    //returns whether a card type is in the deck
    public bool ContainsCard(Card_SO cardType)
    {
        foreach(CardType c in cards)
        {
            if (c.cardType == cardType) return true;
        }
        return false;
    }
    //the total number of cards currently in the deck
    public int TotalCards
    {
        get
        {
            int sum = 0;
            for (int i = 0; i < cards.Count; i++) sum += cards[i].count;
            return sum;
        }
    }
  //Number of remaining cards in a deck
    public int CardsRemaining
    {
        get => Mathf.Clamp(MAX_CARDS - TotalCards, 0, MAX_CARDS);
    }
}
