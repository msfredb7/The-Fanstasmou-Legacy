using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridRegion
{
    private List<GameObject> units = new List<GameObject>();

    public List<GameObject> GetUnits()
    {

        return units;
    }

    public void Suscribe(GameObject unit)
    {
        if (!units.Contains(unit))
            units.Add(unit);
    }
    public void Unsubscribe(GameObject unit)
    {
        if (units.Contains(unit))
            units.Remove(unit);
    }
}


public class UnitGrid : CCC.DesignPattern.PublicSingleton<UnitGrid> {

    const int horizontalCell = 32;
    const int VerticalCell = 18;

    const int worldWidth = 1920;
    const int worldHeight = 1080;

    private float CellHeight { get { return worldHeight / horizontalCell; } }
    private float CellWidth { get { return worldWidth / horizontalCell; } }

    private GridRegion[,] grid = new GridRegion[horizontalCell, VerticalCell];


    public void Start()
    {
        for (int i = 0; i < horizontalCell; i++)
            for (int j = 0; j < VerticalCell; j++)
                grid[i, j] = new GridRegion();
    }


    public Vector2Int WorldToGrid(Vector2 postion)
    {
        Vector2Int retvalue = new Vector2Int();
        retvalue.x = Mathf.FloorToInt(postion.x / CellWidth);
        retvalue.y = Mathf.FloorToInt(postion.y / CellHeight);
        return retvalue;
    }

    private Vector2 GridToWorld(Vector2 postion)
    {
        Vector2 retvalue;
        retvalue.x = postion.x * CellWidth;
        retvalue.y = postion.y * CellHeight;
        return retvalue;
    }


    public List<GameObject> GetNeighbors<T>(Vector2 postion, float range) where T: UnityEngine.MonoBehaviour
    {
        // List<GridRegion> neighboringCells = GetNeighborsCells(postion, range);

        List<GameObject> neighbors = new List<GameObject>();

        T[] sheeps = FindObjectsOfType<T>();

        for (int i = 0; i < sheeps.Length; i++)
            neighbors.Add(sheeps[i].gameObject);

       // for (int i = 0; i < neighboringCells.Count; i++)
        //    neighbors.AddRange(neighboringCells[i].GetUnits());

        Vector2 min = new Vector2(postion.x - range, postion.y - range);
        Vector2 max = new Vector2(postion.x + range, postion.y + range);

        for (int i = 0; i < neighbors.Count; i++)
            if (neighbors[i].transform.position.x < min.x ||
                neighbors[i].transform.position.x > max.x ||
                neighbors[i].transform.position.y < min.y ||
                neighbors[i].transform.position.y > max.y)
                neighbors.RemoveAt(i);
        

        float sqRange = range * range;
        for (int i = 0; i < neighbors.Count; i++)
            if(((Vector2)neighbors[i].transform.position - postion).SqrMagnitude() > range)
                neighbors.RemoveAt(i);

        for (int i = 0; i < neighbors.Count; i++)
            if (!(neighbors[i] is T))
                neighbors.RemoveAt(i);

        return neighbors;
    }

    public void SuscribeUnit(GridSubscriber subscriber, Vector2Int newCell)
    {
        Vector2Int oldCell = subscriber.currentCell;

        if (oldCell.x >= 0 && oldCell.y >= 0 && oldCell.x < horizontalCell && oldCell.y < VerticalCell)
            grid[oldCell.x, oldCell.y].Unsubscribe(subscriber.owner);
    
        if (newCell.x >= 0 && newCell.y >= 0 && newCell.x < horizontalCell && newCell.y < VerticalCell)
            grid[newCell.x, newCell.y].Suscribe(subscriber.owner);

        subscriber.currentCell = newCell;
    }

    private List<GridRegion> GetNeighborsCells(Vector2 postion, float range)
    {
        Vector2 min = WorldToGrid( new Vector2(postion.x - range, postion.y - range) );
        Vector2 max = WorldToGrid( new Vector2(postion.x + range, postion.y + range) );

        List<GridRegion> neighbors = new List<GridRegion>();
        for (int i = (int)min.x; i <= (int)max.x; i++ )
            for (int j = (int)min.y; j <= (int)max.y; j++)
                if (i >= 0 && j >= 0 && i < horizontalCell && j < VerticalCell)
                    neighbors.Add(grid[i, j]);

        return neighbors;
    }
}
