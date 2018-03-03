using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public struct NeighborsRequest
{
    public float lifetime;
    public List<GameObject> neighbors;
    public float range;
    public System.Type type;
    public bool delayed;

    public NeighborsRequest(System.Type tp, float life, float rg){
        type = tp;
        lifetime = life;
        range = rg;
        delayed = false;
        neighbors = new List<GameObject>();
    }
}




public class GridSubscriber: MonoBehaviour
{
    private static int instanceCount = 0;
    private int instanceNumber;

    [HideInInspector]
    public Vector2Int currentCell = new Vector2Int(-1, -1);

    public int RequestPerSeconds = 5;

    private Vector2 position { get  { return (Vector2)gameObject.transform.position; } }
    private List<NeighborsRequest> neighborsRequest = new List<NeighborsRequest>();

    

    public void Start()
    {
        instanceNumber = instanceCount++;
        Update();
    }

    public void Update()
    {
        Vector2Int newCell = UnitGrid.Instance.WorldToGrid(position);
        if (newCell != currentCell)
            UnitGrid.Instance.SuscribeUnit(this, newCell);


        for (int i = 0; i < neighborsRequest.Count; i++)
        {
            var nRequest = (neighborsRequest[i]);
            nRequest.lifetime -= Time.deltaTime;
            neighborsRequest[i] = nRequest;
        }
    }


    public List<GameObject> GetNeighbors<T>(float range) where T : UnityEngine.MonoBehaviour
    {
        for (int i = 0; i < neighborsRequest.Count; i++)
        {
            if (neighborsRequest[i].type == typeof(T) && neighborsRequest[i].range == range)
            {
                if (neighborsRequest[i].lifetime > 0)
                    return neighborsRequest[i].neighbors;
                else
                    return ResetNRequest<MonoBehaviour>(i, range);   
            }
        }
        return NewNRequest<T>(range);
    }
       
    private List<GameObject> NewNRequest<T>(float range) where T : UnityEngine.MonoBehaviour
    {
        NeighborsRequest newNRequest = new NeighborsRequest(typeof(T), 1 / RequestPerSeconds, range);
        neighborsRequest.Add(newNRequest);
        return ResetNRequest<T>(neighborsRequest.IndexOf(newNRequest), range);
    }
    
    private List<GameObject> ResetNRequest<T>(int i, float range) where T : UnityEngine.MonoBehaviour
    {
        var nRequest = neighborsRequest[i];
        nRequest.neighbors = RefreshNeighbors<T>(range);
        nRequest.lifetime = 1 / RequestPerSeconds;
        if (!nRequest.delayed)
        {
            nRequest.lifetime *= (1 - ((instanceNumber % 10) / 10));
            nRequest.delayed = true;
        }
        neighborsRequest[i] = nRequest;
        return nRequest.neighbors;
    }

    private List<GameObject> RefreshNeighbors<T>(float range) where T : UnityEngine.MonoBehaviour
    {
        List<GameObject> neighbors = UnitGrid.Instance.GetObjectsInRange<T>(gameObject.transform.position, range);
        for (int i = 0; i < neighbors.Count; i++)
        {
            if (neighbors[i] == gameObject)
                neighbors.RemoveAt(i);
        }
        return neighbors;
    }
}





