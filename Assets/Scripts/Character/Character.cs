using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] protected Vector3 positionTarget;

    [SerializeField] protected Animator anim;
    [SerializeField] protected string currentAnimName;

    [SerializeField] protected Transform currentChair;

    [SerializeField] protected bool isSitDown = false;

    private void Start()
    {
        positionTarget = transform.position;
    }
    protected virtual void Move(Vector3 target)
    {
        agent.SetDestination(target);
        ChangeAnim("Walk");
    }

    protected virtual void SitDown()
    {
        //quay lung ve ghe
        SetRotationSitDown();
        //Chay anim SitDown
        ChangeAnim("SitDown");
        
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
        Vector3 dic = (currentChair.GetComponent<Chair>().PositionSitDown.position - currentChair.transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(dic);
    }
    protected void SetChair(Transform newChair)
    {
        currentChair = newChair;
    }
    protected void ResetChair()
    {
        currentChair = null;
    }
}
