using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepComponent : MonoBehaviour {

    Vector2 m_Force, m_FWandering, m_FSepare, m_FAlign, m_FCohesion;
    public float m_PoidWandering, m_PoidSeparation, m_PoidAlign, m_PoidCohesion;
    public float m_WanderRadius, m_WanderRange, m_MaxSpeed, m_WanderRefresh, m_SepareRange, m_CohesionRange;

    Rigidbody2D rb;

    private Transform tr;

    private GridSubscriber gs;

    // Use this for initialization
    void Start () {

        gs = GetComponent<GridSubscriber>() as GridSubscriber;

        rb = GetComponent<Rigidbody2D>() as Rigidbody2D;

        tr = GetComponent<Transform>() as Transform;

        //InvokeRepeating("WanderF", 1.0f, m_WanderRefresh);
        

    }

    private void FixedUpdate()
    {
        m_Force = new Vector2(0, 0);

        //SeparationF(gs.GetNeighbors<SheepComponent>(m_SepareRange));

        CohesionF(gs.GetNeighbors<SheepComponent>(m_CohesionRange));

       // m_Force += m_FWandering * m_PoidWandering;

        //m_Force += m_FSepare * m_PoidSeparation;

        m_Force = m_FCohesion * m_PoidCohesion;

        //Debug.Log(m_Force);

        rb.AddRelativeForce(m_Force);

        if (rb.velocity.x > m_MaxSpeed)
            rb.velocity = Vector2.right * m_MaxSpeed;
        else if (rb.velocity.x < -m_MaxSpeed)
            rb.velocity = Vector2.right * -m_MaxSpeed;

        if (rb.velocity.y > m_MaxSpeed)
            rb.velocity = Vector2.left * m_MaxSpeed;
        else if (rb.velocity.y < -m_MaxSpeed)
            rb.velocity = Vector2.left * -m_MaxSpeed;

    }

    //  met a jour la direction des sheep quand il n'ont pas de voisin, ce dirige aléatoirement
    private void WanderF()
    {
        Vector2 cible = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

        cible.Normalize();

        cible *= m_WanderRadius;

        m_FWandering = cible;
    }

    //  Met à jour la force de séparation avec tout les chèvre voisine
    private void SeparationF(List<GameObject> lstVoisin)
    {
        Vector2 ForceTot = new Vector2(0,0);

        foreach (GameObject voisin in lstVoisin)
        {
            Vector2 ToV = tr.position - voisin.transform.position;

            ForceTot += ToV.normalized / ToV.magnitude;
        }

        m_FSepare = ForceTot;
    }

    //  met à jours la force de cohésion du groupe de chèvre vers le centre
    private void CohesionF(List<GameObject> lstVoisin)
    {
        Vector2 ForceTot = new Vector2(0, 0);
        Vector2 CentreDeMasse = new Vector2(0, 0);

        foreach (GameObject voisin in lstVoisin)
        {
            CentreDeMasse += (Vector2)voisin.transform.position;
        }

        if(lstVoisin.Count > 0)
        {
            CentreDeMasse /= lstVoisin.Count;

            //  seek au cas où

            ForceTot = (CentreDeMasse - (Vector2)tr.position).normalized * m_MaxSpeed;

            ForceTot -= rb.velocity;
        }

        m_FCohesion = ForceTot;

    }
}
