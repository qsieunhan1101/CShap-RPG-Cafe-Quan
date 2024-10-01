using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table_Set : MonoBehaviour
{
    [SerializeField] private List<Chair> chairs;



    public Chair GetChair()
    {
        for (int i = 0; i< chairs.Count; i++)
        {
            if (chairs[i].onerChair == null)
            {
                return chairs[i];
            }
        }
        return null;
    }
}
