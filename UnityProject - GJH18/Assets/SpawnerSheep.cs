using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSheep : MonoBehaviour {

    public GameObject m_BordHautDroit;
    public GameObject m_BordBasGauche;

    public GameObject m_SheepPrefab;
    public Transform m_UnitContain;

    public int m_NbSheepStart;

    float m_xMin, m_xMax, m_yMin, m_yMax;

	// Use this for initialization
	void Start () {
        m_xMin = m_BordBasGauche.transform.position.x;
        m_xMax = m_BordHautDroit.transform.position.x;
        m_yMin = m_BordBasGauche.transform.position.y;
        m_yMax = m_BordHautDroit.transform.position.y;

        SpawnPref(m_NbSheepStart);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("s"))
            SpawnPref(1);

        if (Input.GetKeyDown("c"))
            SpawnPrefAtLocation((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
	}

    private void SpawnPref(int NbToSpawn)
    {
        Vector3 pos;

        for (int i = 0; i < NbToSpawn; i++)
        {
            pos = new Vector3(Random.Range(m_xMin, m_xMax), Random.Range(m_yMin, m_yMax));
            Instantiate(m_SheepPrefab, pos, Quaternion.identity, m_UnitContain);
        }
    }

    private void SpawnPrefAtLocation(Vector2 loc)
    {
        Instantiate(m_SheepPrefab, loc, Quaternion.identity, m_UnitContain);
    }
}
