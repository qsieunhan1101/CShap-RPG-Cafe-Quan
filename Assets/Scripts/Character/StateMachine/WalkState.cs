using System.Collections;
using System.Collections.Generic;
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
        randomNextState = Random.Range(1, 3);
        randomTimeWalk = Random.Range(3,5);
        
    }

    public void OnExercute(Bot bot)
    {
        time += Time.deltaTime;
        if (time >= randomTimeWalk || bot.IsFinishMove() == true)
        {
            if (randomNextState == 1)
            {
                bot.ChangeState(new IdleState());
            }
            if (randomNextState == 2)
            {
                bot.ChangeState(new SitChairState());
            }
        }
    }

    public void OnExit(Bot bot)
    {
    }
}
