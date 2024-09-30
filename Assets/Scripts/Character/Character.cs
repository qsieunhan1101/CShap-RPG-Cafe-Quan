using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent agent;

    [SerializeField] protected Vector3 positionTarget;

    [SerializeField] protected Animator anim;
    [SerializeField] protected string currentAnimName = "Idle";

    [SerializeField] protected Chair currentChair = null;

    [SerializeField] protected bool isSitDown = false;



    private void Start()
    {
        positionTarget = transform.position;
    }
    public virtual void Move(Vector3 target)
    {
        agent.SetDestination(target);
        ChangeAnim(Constant.Anim_Walk);
    }

    public virtual IEnumerator SitDown()
    {
        positionTarget = currentChair.PositionSitDown.position;
        Move(positionTarget);
        Debug.Log("Cho di den vi tri ghe");
        yield return new WaitUntil(() => IsFinishMove() == true);
        //quay lung ve ghe
        SetRotationSitDown();
        //Chay anim SitDown
        ChangeAnim(Constant.Anim_SitDown);

        Debug.Log("Da ngoi ghe");

    }

    protected virtual void StandUp()
    {

    }

    protected void ChangeAnim(string newAnimName)
    {
        if (currentAnimName != newAnimName)
        {
            anim.ResetTrigger(currentAnimName);
            currentAnimName = newAnimName;
            anim.SetTrigger(newAnimName);
        }
    }
    protected void SetRotationSitDown()
    {
        Vector3 dic = (currentChair.PositionSitDown.position - currentChair.transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(dic);
    }
    public void SetChair(Chair newChair)
    {
        currentChair = newChair;
        currentChair.onerChair = this;
    }
    public void ResetChair()
    {
        if (currentChair != null)
        {
            currentChair.onerChair = null;
        }
        currentChair = null;
    }
    public bool IsFinishMove()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            return true;
        }
        return false;
    }

}
