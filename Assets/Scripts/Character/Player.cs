using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : Character
{
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private bool isStand = true;

    private Coroutine sitDownCouroutine;



    [Header("Move By JoyStick")]
    [SerializeField] private bool isUsingJoystick = false;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 5;
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private Button btnSit;
    [SerializeField] private Chair chairCollision;

    [SerializeField] private bool isMoving = false;


    private void Start()
    {
        isStand = true;
        isSitDown = false;

        if (isUsingJoystick == true)
        {
            rb.isKinematic = false;
        }


        btnSit.onClick.AddListener(OnClickSit);

    }
    private void Update()
    {
        if (isUsingJoystick == false)
        {

            if (Input.GetMouseButtonDown(0) && isUsingJoystick == false)
            {
                GetPositionToMove();

            }
            if (Input.GetMouseButtonDown(1) && isUsingJoystick == false)
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
    }

    private void FixedUpdate()
    {
        if (isUsingJoystick == true)
        {
            MoveByJoyStick();
            if (chairCollision == null)
            {
                btnSit.gameObject.SetActive(false);
            }
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
            if (sitDownCouroutine != null)
            {
                StopCoroutine(sitDownCouroutine);
            }

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

                sitDownCouroutine = StartCoroutine(SitDown());

            }
        }
    }
    private Vector3 CheckValidPositionNavMesh(Vector3 pos, float radius)
    {
        NavMeshHit navHit;
        bool checkValidPos = NavMesh.SamplePosition(pos, out navHit, radius, NavMesh.AllAreas);
        if (checkValidPos == false)
        {
            return CheckValidPositionNavMesh(pos, radius + 2);
        }
        return navHit.position;
    }


    private void MoveByJoyStick()
    {
        if (isMoving == false)
        {
            if (isSitDown == false)
            {
                ChangeAnim(Constant.Anim_Idle);

            }
        }
        else
        {
            ChangeAnim(Constant.Anim_Walk);
            rb.velocity = new Vector3(joystick.Horizontal * speed, rb.velocity.y, joystick.Vertical * speed);
            isSitDown = false;
            ResetChair();
            if (sitDownCouroutine != null)
            {
                StopCoroutine(sitDownCouroutine);
            }
        }
        PlayerJoyStickRotation();
    }
    private void PlayerJoyStickRotation()
    {

        if (Input.touchCount > 0)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(joystick.GetComponent<RectTransform>(), Input.GetTouch(0).position))
            {
                moveDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
                isMoving = true;
                transform.rotation = Quaternion.LookRotation(moveDirection);
            }
        }
        else
        {
            isMoving = false;
        }

    }
    private void OnClickSit()
    {
        if (chairCollision.onerChair != null)
        {
            return;
        }
        SetChair(chairCollision);
        sitDownCouroutine = StartCoroutine(SitDown());
        btnSit.gameObject.SetActive(false);
        isSitDown = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isUsingJoystick == true)
        {
            if (other.CompareTag(Constant.Tag_Chair))
            {
                btnSit.gameObject.SetActive(true);
                chairCollision = other.GetComponent<Chair>();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (isUsingJoystick == true)
        {
            if (other.CompareTag(Constant.Tag_Chair))
            {
                chairCollision = null;
            }
        }
    }

}
