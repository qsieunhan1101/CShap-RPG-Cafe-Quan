using UnityEngine;

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
            positionTarget = hit.point;
            Debug.Log(hit.point);
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
}
