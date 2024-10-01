using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{

    private float time;
    private float randomTimeIdle;
    private int randomNextState;
    public void OnEnter(Bot bot)
    {
        bot.StopMove();
        time = 0;
        randomTimeIdle = Random.Range(4,6);
        randomNextState = Random.Range(1,3);
    }

    public void OnExercute(Bot bot)
    {
        time += Time.deltaTime;
        if (time >= randomTimeIdle)
        {
            if (randomNextState == 1)
            {
                bot.ChangeState(new WalkState());
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
