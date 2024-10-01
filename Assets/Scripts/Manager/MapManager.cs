using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private Transform mapCenterPosition;
    [SerializeField] private float radius;

    [SerializeField] private List<Table_Set> table_Sets;
    public List<Table_Set> Table_Sets => table_Sets;

    public Vector3 GetRandomPositionInMap()
    {
        Vector3 randomPosition = Random.insideUnitSphere * radius + mapCenterPosition.position;
        NavMeshHit navHit;
        bool hasHit = NavMesh.SamplePosition(randomPosition, out navHit, 1.0f, NavMesh.AllAreas);
        if (hasHit == true)
        {
            return navHit.position;
        }
        return GetRandomPositionInMap();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(mapCenterPosition.position, radius);
    }
}
