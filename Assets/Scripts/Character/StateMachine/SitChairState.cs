using UnityEngine;

public class SitChairState : IState
{
    float time;
    float randomTimeSitChair;
    public void OnEnter(Bot bot)
    {
        time = 0;
        randomTimeSitChair = Random.Range(5, 8);
        //lay ghe
        bot.SetChair(bot.GetChair());
        //di chuyen den ghe va ngoi
        bot.StartCoroutine(bot.SitDown());
    }

    public void OnExercute(Bot bot)
    {
        if (bot.IsFinishMove() == true)
        {
            time += Time.deltaTime;
            if (time >= randomTimeSitChair)
            {
                bot.ChangeState(new WalkState());
            }
        }
    }

    public void OnExit(Bot bot)
    {
        bot.ResetChair();
    }
}
