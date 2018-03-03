using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSubscriber
{
    public Vector2Int currentCell = new Vector2Int(-1 ,-1);
    public GameObject owner;
    Vector2 position {
        get  {
            if (owner)
                return (Vector2)owner.transform.position;
            else
                return new Vector3(0, 0, 0);
        }
    }

    public void Initiate(GameObject o)
    {
        owner = o;
        Update();
    }

    public void Update()
    {
        Vector2Int newCell = UnitGrid.Instance.WorldToGrid(position);
        if (newCell != currentCell)
            UnitGrid.Instance.SuscribeUnit(this, newCell);
    }

    public List<GameObject> GetNeighbors(float range)
    {
        return UnitGrid.Instance.GetNeighbors(owner.transform.position, range);
    }
}

