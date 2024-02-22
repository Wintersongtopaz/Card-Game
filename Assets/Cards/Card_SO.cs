using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Card_SO : ScriptableObject
{
    // Card_SO: a scriptable object containing card type data for collectible card game cards.
    public string cardName;
    public string cardDescription;
    public int manaCost;
    public Sprite cardImage;
    public List<CardEffect> cardEffects;
}
