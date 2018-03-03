using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSubscriber: MonoBehaviour
{
    [HideInInspector]
    public Vector2Int currentCell = new Vector2Int(-1 ,-1);

    Vector2 position {
        get  {
                return (Vector2)gameObject.transform.position;
        }
    }

    public void Start()
    {
        Update();
    }

    public void Update()
    {
        Vector2Int newCell = UnitGrid.Instance.WorldToGrid(position);
        if (newCell != currentCell)
            UnitGrid.Instance.SuscribeUnit(this, newCell);
    }

    public List<GameObject> GetNeighbors<T>(float range) where T : UnityEngine.MonoBehaviour
    {
        List<GameObject> objects = UnitGrid.Instance.GetNeighbors<T>(gameObject.transform.position, range);
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i] == gameObject)
                objects.RemoveAt(i);
        }
        return objects;
    }
}

