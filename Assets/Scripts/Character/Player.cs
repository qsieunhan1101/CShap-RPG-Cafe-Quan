using UnityEngine;
using UnityEngine.AI;

public class Player : Character
{
    [SerializeField] private LayerMask floorLayer;


    [SerializeField] private bool isStand = true;


    private void Start()
    {
        isStand = true;
        isSitDown = false;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetPositionToMove();

        }
        if (Input.GetMouseButtonDown(1))
        {
            GetChairToSit();

        }
        if (Vector3.Distance(transform.position, positionTarget) <= 0.1f)
        {
            isStand = true;

            if (isSitDown == true)
            {
                isStand = false;
            }
        }
        else
        {
            isStand = false;
            isSitDown = false;
        }
        if (isStand == true)
        {
            ChangeAnim(Constant.Anim_Idle);
        }
        if (currentChair != null)
        {
            isSitDown = true;
        }
    }

    private void GetPositionToMove()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, floorLayer))
        {
            
            positionTarget = CheckValidPositionNavMesh(hit.point, 5);
            Move(positionTarget);
            ResetChair();
            StopCoroutine(SitDown());
            
        }
    }
    private void GetChairToSit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.CompareTag(Constant.Tag_Chair))
            {
                if (hit.transform.GetComponent<Chair>().onerChair != null)
                {
                    return;
                }
                if (currentChair == hit.transform)
                {
                    return;
                }
                SetChair(hit.transform.GetComponent<Chair>());

               
                StartCoroutine(SitDown());
                

            }
        }
    }
    private Vector3 CheckValidPositionNavMesh(Vector3 pos, float radius)
    {
        NavMeshHit navHit;
        bool checkValidPos = NavMesh.SamplePosition(pos,out navHit, radius, NavMesh.AllAreas);
        if (checkValidPos == false)
        {
            return CheckValidPositionNavMesh(pos, radius+2);
        }
        return navHit.position;
    }
}
