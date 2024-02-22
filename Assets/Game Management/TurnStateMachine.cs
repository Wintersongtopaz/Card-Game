using UnityEngine.Events;

public class TurnStateMachine 
{
  public enum State
    {
        PlayerTurn, EnemyTurn, Idle, GameOver
    }
    private static State turnState;
    public static State TurnState
    {
        get => turnState;
        set
        {
            if (turnState != value)
            {
                turnState = value;
                NewState.Invoke(turnState);
            }
        }
    }
    public static UnityEvent<TurnStateMachine.State> NewState = new UnityEvent<State>();
}
