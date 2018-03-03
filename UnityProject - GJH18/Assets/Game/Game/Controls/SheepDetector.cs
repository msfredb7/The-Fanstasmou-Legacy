using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SheepDetector : MonoBehaviour
{
    public List<SheepComponent> sheepsInRange;

    public UnityEvent onSheepInRange = new UnityEvent();

    public int maxSheepInRangeToEat = 1;

    private bool previouslyNoneInRange = true;

    void Start()
    {
        sheepsInRange = new List<SheepComponent>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<SheepComponent>() != null)
            sheepsInRange.Add(other.GetComponent<SheepComponent>());
        if(sheepsInRange.Count > 0 && previouslyNoneInRange)
        {
            onSheepInRange.Invoke();
            previouslyNoneInRange = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<SheepComponent>() != null)
            sheepsInRange.Remove(other.GetComponent<SheepComponent>());
        if (sheepsInRange.Count <= 0 && !previouslyNoneInRange)
            previouslyNoneInRange = true;
    }

    public SheepComponent FindClosestSheep()
    {
        SheepComponent result = null;
        for (int i = 0; i < sheepsInRange.Count; i++)
        {
            if (result == null)
                result = sheepsInRange[i];
            float distanceFromCurrentSheep = Vector3.Distance(sheepsInRange[i].transform.position,transform.position);
            float distanceFromCurrentResult = Vector3.Distance(result.transform.position, transform.position);
            if(distanceFromCurrentSheep < distanceFromCurrentResult)
                result = sheepsInRange[i];
        }
        return result;
    }
}
