using UnityEngine;

public class WalkState : IState
{
    float time;
    float randomTimeWalk;
    int randomNextState;
    public void OnEnter(Bot bot)
    {
        bot.Move(MapManager.Instance.GetRandomPositionInMap());
        time = 0;
        randomTimeWalk = Random.Range(4, 6);
        randomNextState = Random.Range(1, 3);

    }

    public void OnExercute(Bot bot)
    {
        time += Time.deltaTime;
        if (time >= randomTimeWalk || bot.IsFinishMove() == true)
        {
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot bot)
    {
    }
}
