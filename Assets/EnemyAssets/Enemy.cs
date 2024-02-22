using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//manages creating and destroying creatures
public class Enemy : MonoBehaviour
{
    public GameEvent Defeated;
    public Board board;
    public GameObject creaturePrefab;
    public List<Creature_SO> creatureTypes = new List<Creature_SO>();
    //a queue of creatures for the player to defeat in oder to win the match
    Queue<Creature_SO> creatureQueue = new Queue<Creature_SO>();
    List<Creature> creatures = new List<Creature>();

    [Header("Turn Sequence")]
    public float startDelay = 2f;
    public float attackDelay = 1f;
    public float spawnDelay = 1f;

    public void StartTurn()
    {
        if (TurnStateMachine.TurnState == TurnStateMachine.State.GameOver) return;
        TurnStateMachine.TurnState = TurnStateMachine.State.EnemyTurn;
        StartCoroutine(TurnSequence());
    }

    public void EndTurn()
    {
        if (TurnStateMachine.TurnState == TurnStateMachine.State.GameOver) return;
        FindObjectOfType<Player>().StartTurn();
    }

    void Awake()
    {
        board = GetComponent<Board>();
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < creatureTypes.Count; i++) creatureQueue.Enqueue(creatureTypes[i]);
        SpawnCreature(creatureQueue.Dequeue());
        SpawnCreature(creatureQueue.Dequeue());
        SpawnCreature(creatureQueue.Dequeue());

        StartCoroutine(TurnSequence());
    }
    //spawn creature create a creature and assigns its creature type
    void SpawnCreature(Creature_SO creatureType)
    {
        Creature newCreature = board.NewBoardItem(creaturePrefab).GetComponent<Creature>();
        newCreature.CreatureType = creatureType;
        creatures.Add(newCreature);
    }
    //destroy creature removes a creature from the board
    void DestroyCreature(Creature creature)
    {
        creatures.Remove(creature);
        board.DestroyBoardItem(creature);
    }
    //a coroutine controlling the sequence of events during the enemy's turn
    IEnumerator TurnSequence()
    {
        yield return new WaitForSeconds(startDelay);
        Player player = FindObjectOfType<Player>();

        for (int i = 0; i < creatures.Count; i++)
        {
            creatures[i].offset = Vector3.down;
            player.Health -= creatures[i].Attack;
            CameraShake.Shake();
            yield return new WaitForSeconds(attackDelay);
            creatures[i].offset = Vector3.zero;
        }

        yield return new WaitForSeconds(spawnDelay);

        if (creatureQueue.Count > 0) SpawnCreature(creatureQueue.Dequeue());

        EndTurn();

    }
    //Remove any creatures with 0 health from the board
    public void PostCardPlayed()
    {
        List<Creature> deadCreatures = new List<Creature>();

        foreach (Creature c in creatures) if (c.Health <= 0) deadCreatures.Add(c);
        foreach (Creature c in deadCreatures) DestroyCreature(c);

        if (creatures.Count == 0 && creatureQueue.Count == 0) Defeated.RaiseEvent();
    }

}
