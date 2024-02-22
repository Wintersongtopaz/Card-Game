using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Board hand;
    BoardItem dragTarget;
    Player player;

    Board enemyBoard;
    Creature targetCreature;

    void Awake()
    {
        hand = GetComponent<Board>();
        player = GetComponent<Player>();
        enemyBoard = FindObjectOfType<Enemy>().GetComponent<Board>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player.StartTurn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            player.EndTurn();
            return;
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        BoardItem focusTarget = hand.GetNearestBoardItem(mousePosition);
        hand.focusTarget = focusTarget;

        targetCreature = enemyBoard.GetNearestBoardItem(mousePosition) as Creature;

        if (Input.GetMouseButtonDown(0)) dragTarget = hand.GetNearestBoardItem(mousePosition);
        if (Input.GetMouseButtonUp(0))
        {
            if (!hand.InBoardArea(mousePosition) && targetCreature) player.TryPlayCard(dragTarget as Card, targetCreature);
            dragTarget = null;
        }

        // Set drag target never if null
        hand.SetDragTarget(dragTarget, mousePosition);
    }
}
