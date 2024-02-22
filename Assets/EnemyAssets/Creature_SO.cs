using UnityEngine;

[CreateAssetMenu()]
//A Scriptable Object representing enemy creatures
public class Creature_SO : ScriptableObject
{
    public int attack;
    public int health;
    public Sprite image;
}
