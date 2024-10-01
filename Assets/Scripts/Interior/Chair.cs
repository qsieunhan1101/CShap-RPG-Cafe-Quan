using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    [SerializeField] private Transform positionSitDown;



    public Character onerChair;
    public Transform PositionSitDown => positionSitDown;
    private void Start()
    {
        onerChair = null;
    }

}
