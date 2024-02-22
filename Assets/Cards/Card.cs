using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : BoardItem
{
//Card: a mono behavior used to automatically display Card_SO data in game
    [Header("UI Elements")]
    public TextMeshProUGUI nameDisplay;
    public TextMeshProUGUI descriptionDisplay;
    public TextMeshProUGUI manaCost;
    public Image cardImage;

   [SerializeField] private Card_SO cardType;
    public Card_SO CardType
    {
        get => cardType;
        set
        {
            cardType = value;
            nameDisplay.text = cardType.cardName;
            descriptionDisplay.text = cardType.cardDescription;
            manaCost.text = cardType.manaCost.ToString();
            cardImage.sprite = cardType.cardImage;
        }
    }



  
}
