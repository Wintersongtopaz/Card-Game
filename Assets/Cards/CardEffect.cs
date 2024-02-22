using UnityEngine;

[System.Serializable]
//a class representing a single effect of a card type
public class CardEffect
{
    //Strength: the strength of the effect
    [SerializeField] public int strength;
    [SerializeField] public TargetType targetType;
    [SerializeField] public EffectType effectType;
    //VFX
    [Header("VisualEffects")]
    public GameObject FXPrefab;
    public Vector2 cameraShake;

    //the target type of an effect. who recieves the effect
    public enum TargetType
    {
        Player, Creature, AllCreatures
    }
    //the type of an effect. descripes what heppens when resolved
    public enum EffectType
    {
        ChangeHealth, ChangeAttack, ChangeMana, DrawCard
    }
//Resolve Effect(): an overloaded method used to resolve effects for different target types
    //call if effect targets the player
    public void ResolveEffect(Player player)
    {
        switch (effectType)
        {
            case EffectType.ChangeHealth: player.Health += strength; break;
            case EffectType.ChangeMana: player.Mana += strength; break;
            case EffectType.DrawCard: for (int i = 0; i < strength; i++) player.DrawCard(); break;
        }
    }
    //call if effect targets a single creature
    public void ResolveEffect(Creature targetCreature)
    {
        //VFX
        if (FXPrefab) MonoBehaviour.Instantiate(FXPrefab, targetCreature.transform.position, Quaternion.identity);
        CameraShake.Shake(cameraShake.x, cameraShake.y);

        switch (effectType)
        {
            case EffectType.ChangeHealth: targetCreature.Health += strength; break;
            case EffectType.ChangeAttack: targetCreature.Attack += strength; break;
        }
    }
    //call if effect targets multiple creatures
    public void ResolveEffect(Creature[] creatures)
    {
        foreach(Creature targetCreature in creatures)
        {
            ResolveEffect(targetCreature);
        }
    }
}
