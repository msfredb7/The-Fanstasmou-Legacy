using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerFoin : MonoBehaviour
{
    public GameObject m_PrefabFoin;

    public float m_StartDelay, m_TimeSpawnMin, m_TimeSpawnMax;

    [ReadOnly]
    public List<Transform> m_lstNode;

    private List<int> m_lstNodeFill;

    // Use this for initialization
    void Start()
    {

        m_lstNodeFill = new List<int>();

        m_lstNode.AddRange(FindComponentsInChildrenWithTag<Transform>(gameObject, "Node"));

        //SpawnFoinAtRandom();
        Invoke("SpawnFoinAtRandom", m_StartDelay);

        //InvokeRepeating(, 10.0f, Random.Range(0, 20));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
            SpawnFoinAtRandom();
    }

    private void SpawnFoinAtRandom()
    {
        if (m_lstNode.Count < 1)
        {
            Debug.LogError("Aucun noeud dans la liste de noeud de foin");
            return;
        }

        if (FindComponentsInChildrenWithTag<Transform>(gameObject, "Foin").LastIndex() > 2)
        {
            Debug.Log("Plus de 4 foin, stop du spawn de foin");
            return;
        }

        int index;

        do
        {
            index = Random.Range(0, (m_lstNode.Count - 1));

        } while (m_lstNodeFill.Contains(index) == true);// node index ==  occuper

        m_lstNodeFill.Add(index);

        Instantiate(m_PrefabFoin, m_lstNode[index].position, m_lstNode[index].rotation, transform);

        Invoke("SpawnFoinAtRandom", Random.Range(m_TimeSpawnMin, m_TimeSpawnMax));
    }

    //  reçoit le transforme du foin pour libéré le noeud dans lstNodeFill
    public void FoinRemove(Transform posNode)
    {
        if (m_lstNode.Contains(posNode) == true)
        {
            m_lstNodeFill.Remove(m_lstNode.IndexOf(posNode));
            Invoke("SpawnFoinAtRandom", Random.Range(m_TimeSpawnMin, m_TimeSpawnMax));
        }
    }

    public T[] FindComponentsInChildrenWithTag<T>(GameObject parent, string tag, bool forceActive = false) where T : Component
    {
        if (parent == null) { throw new System.ArgumentNullException(); }
        if (string.IsNullOrEmpty(tag) == true) { throw new System.ArgumentNullException(); }
        List<T> list = new List<T>(parent.GetComponentsInChildren<T>(forceActive));
        if (list.Count == 0) { return null; }

        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].CompareTag(tag) == false)
            {
                list.RemoveAt(i);
            }
        }
        return list.ToArray();
    }
}
