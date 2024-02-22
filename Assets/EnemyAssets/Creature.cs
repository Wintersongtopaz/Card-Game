using UnityEngine;
using UnityEngine.UI;
using TMPro;

//A board Item which displays a creature_SO
public class Creature : BoardItem
{

    private Creature_SO creatureType;

    // health and attack
    private int health;
    private int attack;
    //UI components
    public TextMeshProUGUI healthDisplay;
    public TextMeshProUGUI attackDisplay;
    public Image creatureImage;

    public Creature_SO CreatureType
    {
        get => creatureType;
        set
        {
            creatureType = value;
            creatureImage.sprite = creatureType.image;
            Health = creatureType.health;
            Attack = creatureType.attack;

        }
    }
    // properties
    public int Health
    {
        get => health;
        set
        {
            health = value;
            healthDisplay.text = health.ToString();
        }
    }

    public int Attack
    {
        get => attack;
        set
        {
            attack = value;
            attackDisplay.text = attack.ToString();
        }
    }
}
