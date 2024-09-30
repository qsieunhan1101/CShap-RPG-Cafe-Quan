using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    protected IState currentState;
    [SerializeField] string currentStateName;

    private void Start()
    {
        ChangeState(new IdleState());
    }
    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExercute(this);
        }
        
    }

    public void ChangeState(IState newState)
    {
        currentStateName = newState.ToString();
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void StopMove()
    {
        agent.SetDestination(this.transform.position);
        ChangeAnim(Constant.Anim_Idle);
    }

 
    public Chair GetChair()
    {
        Table_Set table = MapManager.Instance.Table_Sets[Random.Range(0, MapManager.Instance.Table_Sets.Count)];
        Chair newChair = table.GetChair();
        if (newChair != null)
        {
            return newChair;
        }
        return GetChair();
    }
}
