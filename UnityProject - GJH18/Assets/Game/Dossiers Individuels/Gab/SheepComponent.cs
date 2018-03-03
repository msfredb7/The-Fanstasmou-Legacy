using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepComponent : MonoBehaviour {

    Vector2 m_Force, m_FWandering, m_FSepare, m_FAlign, m_FCohesion;

    public float m_PoidWandering, m_PoidSeparation, m_PoidAlign, m_PoidCohesion;
    public float m_WanderRadius, m_MaxSpeed, m_WanderRefresh;

    [Colored(1, .5f, .5f)] public float m_WanderRange;
    [Colored(.5f, 1, .5f)] public float m_SepareRange;
    [Colored(.5f, .5f, 1)] public float m_CohesionRange;
    [Colored(1, 1, 1)] public float m_AlignRange;

    Rigidbody2D rb;

    private Transform tr;

    private GridSubscriber gs;

    // Use this for initialization
    void Start () {

        gs = GetComponent<GridSubscriber>() as GridSubscriber;

        rb = GetComponent<Rigidbody2D>() as Rigidbody2D;

        tr = GetComponent<Transform>() as Transform;

        //if(m_PoidWandering > 0)
        //  InvokeRepeating("WanderF", 1.0f, m_WanderRefresh);

        WanderF();

    }

    private void Update()
    {

        if (Input.GetKeyDown("d") == true && m_PoidWandering != 0)
            rb.AddForce(new Vector2(15.0f, 0));
    }

    private void FixedUpdate()
    {
        m_Force = new Vector2(0, 0);

        List<GameObject> lstVoisin = gs.GetNeighbors<SheepComponent>(m_SepareRange);

        SeparationF(lstVoisin);

        CohesionF(lstVoisin);//gs.GetNeighbors<SheepComponent>(m_CohesionRange));

        AlignementF(lstVoisin);//gs.GetNeighbors<SheepComponent>(m_AlignRange));

        //m_Force += m_FWandering * m_PoidWandering;

        m_Force += m_FSepare * m_PoidSeparation;

        m_Force += m_FAlign * m_PoidAlign;

        m_Force += m_FCohesion * m_PoidCohesion;

        //if (m_PoidWandering > 0)
        m_Force += m_FWandering * m_PoidWandering;

        //Debug.Log(m_Force);

        //rb.AddRelativeForce(m_Force);
        rb.AddForce(m_Force);


        if (rb.velocity.magnitude > m_MaxSpeed)
        {
            rb.velocity = rb.velocity.normalized * m_MaxSpeed;
        }
        //if (rb.velocity.x > m_MaxSpeed)
        //    rb.velocity = Vector2.right * m_MaxSpeed;
        //else if (rb.velocity.x < -m_MaxSpeed)
        //    rb.velocity = Vector2.right * -m_MaxSpeed;

        //if (rb.velocity.y > m_MaxSpeed)
        //    rb.velocity = Vector2.left * m_MaxSpeed;
        //else if (rb.velocity.y < -m_MaxSpeed)
        //    rb.velocity = Vector2.left * -m_MaxSpeed;

    }

    //  met a jour la direction des sheep quand il n'ont pas de voisin, ce dirige aléatoirement
    private void WanderF()
    {
        Vector2 cible = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

        cible.Normalize();

        cible *= m_WanderRadius;

        m_FWandering = cible;

        Invoke("WanderF", Random.Range(2, 5));
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

    //  Met à jour la force de séparation avec tout les chèvre voisine
    private void AlignementF(List<GameObject> lstVoisin)
    {
        Vector2 DirMoyenne = new Vector2(0, 0);

        foreach (GameObject voisin in lstVoisin)
        {
            DirMoyenne += (voisin.GetComponent < Rigidbody2D >() as Rigidbody2D).velocity.normalized;
        }

        if(lstVoisin.Count > 0)
        {
            DirMoyenne /= lstVoisin.Count;

            DirMoyenne -= rb.velocity.normalized;
        }

        m_FAlign = DirMoyenne;
    }

    void OnDrawGizmosSelected()
    {
        Vector3 center = transform.position;
        Gizmos.color = new Color(1, 0, 0, 0.25f);
        Gizmos.DrawSphere(center, m_WanderRange);

        Gizmos.color = new Color(0, 1, 0, 0.25f);
        Gizmos.DrawSphere(center, m_SepareRange);

        Gizmos.color = new Color(0, 0, 1, 0.25f);
        Gizmos.DrawSphere(center, m_CohesionRange);

        Gizmos.color = new Color(1, 1, 1, 0.25f);
        Gizmos.DrawSphere(center, m_AlignRange);
    }
}
