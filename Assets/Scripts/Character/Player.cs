using UnityEngine;

public class Player : Character
{
    [SerializeField] private LayerMask floorLayer;


    [SerializeField] private bool isMove = false;
    [SerializeField] private bool isStand = true;


    private void Start()
    {
        isMove = false;
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
        if (Vector3.Distance(transform.position, positionTarget) <= 0.2f)
        {
            isStand = true;
            isMove = false;

            if (isSitDown == true)
            {
                SitDown();
                isStand = false;
            }
        }
        else
        {
            isStand = false;
            isMove = true;
            isSitDown = false;
        }
        if (isStand == true)
        {
            ChangeAnim("Idle");
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
        }
    }
    private void GetChairToSit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.CompareTag("Chair"))
            {
                SetChair(hit.transform);
                positionTarget = currentChair.GetComponent<Chair>().PositionSitDown.position;
                Move(positionTarget);
                
            }
        }
    }
}
