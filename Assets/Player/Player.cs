using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public GameEvent Defeated;

    Board hand;
    public GameObject cardPrefab;
    Stack<Card_SO> deck = new Stack<Card_SO>();
    List<Card_SO> discard = new List<Card_SO>();
    public Deck_SO myDeck;
    public TextMeshProUGUI healthDisplay;
    public TextMeshProUGUI manaDisplay;
    private int health = 10;
    private int mana = 3;

    public bool MyTurn => TurnStateMachine.TurnState == TurnStateMachine.State.PlayerTurn;

    public void StartTurn()
    {
        if (TurnStateMachine.TurnState == TurnStateMachine.State.GameOver) return;
        TurnStateMachine.TurnState = TurnStateMachine.State.PlayerTurn;
        Mana = 3;
        DrawCard();
    }

    public void EndTurn()
    {
        if (TurnStateMachine.TurnState == TurnStateMachine.State.GameOver) return;
        FindObjectOfType<Enemy>().StartTurn();
    }

    public int Health
    {
        get => health;
        set
        {
            health = value;
            healthDisplay.text = health.ToString();
            if (Health <= 0) Defeated.RaiseEvent();
        }
    }

    public int Mana
    {
        get => mana;
        set
        {
            mana = value;
            manaDisplay.text = mana.ToString();
        }
    }

    void Awake()
    {
        hand = GetComponent<Board>();
        ShuffleDeck();
    }

    void Start()
    {
        Health = 10;
        Mana = 3;
        DrawCard();
        DrawCard();
        DrawCard();
        DrawCard();
        DrawCard();
    }



    //shuffle cards into deck in random order
    public void ShuffleDeck()
    {
        //copy to temp list
       
        List<Card_SO> temp = myDeck.CardList;
        while(temp.Count > 0)
        {
            int i = Random.Range(0, temp.Count);
            Card_SO card = temp[i];
            deck.Push(card);
            temp.RemoveAt(i);
        }
    }
    //draw top card from deck and add to hand
    public void DrawCard()
    {
        if (deck.Count <= 0)
        {
            Debug.Log("No Cards!");
        }
        else
        {
            Card_SO card = deck.Pop();
            hand.NewBoardItem(cardPrefab).GetComponent<Card>().CardType = card;
        }
    }
    //remove a card from hand and add to discard
    public void DiscardCard(Card card)
    {
        discard.Add(card.CardType);
        hand.DestroyBoardItem(card);
    }

    public void TryPlayCard(Card card, Creature targetCreature)
    {
        if (!MyTurn) return;
        if (!card) return;
        Card_SO cardType = card.CardType;
        if (Mana < cardType.manaCost) return;

        Mana -= cardType.manaCost;
        DiscardCard(card);
        //resolves a cards effects one at a time
        StartCoroutine(ResolveCardEffect(cardType, targetCreature));
    }

    public float startDelay = 0.1f; // Delay before starting
    public float effectDelay = 0.25f; //time between effects
    public float endDelay = 0.1f; // Delay before finished

    IEnumerator ResolveCardEffect(Card_SO cardType, Creature targetCreature)
    {
        yield return new WaitForSeconds(startDelay);

        foreach(CardEffect effect in cardType.cardEffects)
        {
            switch (effect.targetType)
            {
                case CardEffect.TargetType.Player: effect.ResolveEffect(this); break;
                case CardEffect.TargetType.Creature: effect.ResolveEffect(targetCreature); break;
                case CardEffect.TargetType.AllCreatures: effect.ResolveEffect(FindObjectOfType<Creature>()); break;
            }
            yield return new WaitForSeconds(effectDelay);
        }

        FindObjectOfType<Enemy>().PostCardPlayed();
        yield return new WaitForSeconds(endDelay);
    }
}
