using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepComponent : MonoBehaviour
{

    public bool AIenabled = true;


    Vector2 m_Force, m_FWandering, m_FSepare, m_FAlign, m_FCohesion, m_FEvade, m_FSeek, m_FFlee;

    public float m_MaxSpeed;
    public bool log = false;
    [Range(0,1)]
    public float m_CohesionCentering = 1;

    [Header("Poids")]
    public float m_PoidWandering;
    public float m_PoidSeparation;
    public float m_PoidAlign;
    public float m_PoidCohesion;
    public float m_PoidFlee;
    public float m_PoidEvade;
    public float m_PoidSeek;



    [Header("Powers")]
    public float m_FleePower = 1;
    public float m_SeekPower = 1;
    public float m_EvadePower = 1;

    [Header("Wandering")]
    public float m_WanderRadius, m_WanderRefresh;

    [Header("Ranges"), Colored(1, .5f, .5f)] public float m_WanderRange;
    [Colored(.5f, 1, .5f)] public float m_SepareRange;
    [Colored(.5f, .5f, 1)] public float m_CohesionRange;
    [Colored(1, 1, 1)] public float m_AlignRange;
    [Colored(0.5f, 1, 1)] public float m_SeekRange;

    public float m_FleeRange;

    Rigidbody2D rb;

    private Transform tr;

    private GridSubscriber gs;
    private Voisin myself;

    private List<Vector2> vectorsToDisplay = new List<Vector2>();

    public bool FuirSourie;

    // Use this for initialization
    void Start()
    {
        gs = GetComponent<GridSubscriber>() as GridSubscriber;
        myself = GetComponent<Voisin>();

        rb = GetComponent<Rigidbody2D>() as Rigidbody2D;

        tr = GetComponent<Transform>() as Transform;

        //if(m_PoidWandering > 0)
        //  InvokeRepeating("WanderF", 1.0f, m_WanderRefresh);

        WanderF();

    }

    private void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //Debug.Log((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //m_Force += Flee((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)) * 3;
        //}


        if (Input.GetKeyDown("d") == true && m_PoidWandering != 0)
            rb.AddForce(new Vector2(15.0f, 0));
    }

    private void FixedUpdate()
    {
        if (AIenabled == false)
            return;

        m_Force = new Vector2(0, 0);

        List<VoisinInfo> lstVoisin = myself.otherVoisins;

        SeparationF(lstVoisin);
        CohesionF(lstVoisin);
        EvadeF();
        AttractF();
        
        //gs.GetNeighbors<SheepComponent>(m_CohesionRange));

        //AlignementF(lstVoisin);//gs.GetNeighbors<SheepComponent>(m_AlignRange));

        //m_Force += m_FWandering * m_PoidWandering;
        if (lstVoisin.Count > 0)
        {
            m_Force += m_FSepare * m_PoidSeparation;

            m_Force += m_FAlign * m_PoidAlign;

            m_Force += m_FCohesion * m_PoidCohesion;
        }
        else
        {
            m_Force += m_FWandering * m_PoidWandering;
        }

        m_Force += m_FFlee * m_PoidFlee;

        m_Force += m_FSeek * m_PoidSeek;


        if (FuirSourie == true)
            m_Force += Flee((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)) * m_PoidFlee;
        //m_Force += seek((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)) * m_PoidSeek;

        //if(tueur != null)
        //    m_Force += Evade(tueur) * m_PoidEvade;

        //Debug.Log(m_Force);

        //rb.AddRelativeForce(m_Force);
        rb.AddForce(m_Force);


        if (rb.velocity.magnitude > m_MaxSpeed)
        {
            rb.velocity = rb.velocity.normalized * m_MaxSpeed;
        }

        if(log)
        {
            Debug.Log(rb.velocity);
        }
    

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
    private void SeparationF(List<VoisinInfo> lstVoisin)
    {
        Vector2 ForceTot = new Vector2(0, 0);

        float influence = 0;
        if (m_SepareRange != 0)
            foreach (VoisinInfo voisin in lstVoisin)
            {
                influence = GetDistanceBasedInfluence(voisin.distance, m_SepareRange, 1);
                Vector2 ToV = tr.position - voisin.intance.tr.position;

                ForceTot += (ToV.normalized / ToV.magnitude) * influence;
            }

        m_FSepare = ForceTot;
    }

    //  met à jours la force de cohésion du groupe de chèvre vers le centre
    private void CohesionF(List<VoisinInfo> lstVoisin)
    {
        Vector2 ForceTot = new Vector2(0, 0);
        Vector2 destination = Vector2.zero;
        Vector2 CentreDeMasse = Vector2.zero;

        float influence = 0;
        float highestInfluence = 0;
        float totalInfluence = 0;

        Vector2 v = Vector2.zero;
        if (m_CohesionRange != 0)
            foreach (VoisinInfo voisin in lstVoisin)
            {
                influence = GetDistanceBasedInfluence(voisin.distance, m_CohesionRange, 0.01f);
                totalInfluence += influence;
                if (highestInfluence < influence)
                    highestInfluence = influence;

                CentreDeMasse += (Vector2)voisin.intance.tr.position * influence;
            }

        if (totalInfluence > 0)
        {
            CentreDeMasse /= totalInfluence;

            destination = Vector2.Lerp(tr.position, CentreDeMasse, Mathf.Lerp(highestInfluence, 1, m_CohesionCentering));


            var displacement = destination - (Vector2)tr.position;

            if (displacement.magnitude > 0.2f)
            {
                displacement.Normalize();
                ForceTot = displacement * m_MaxSpeed;
                ForceTot -= rb.velocity;
            }
        }

        if (log)
        {
            Debug.DrawLine(tr.position, CentreDeMasse, Color.red);
        }

        //

        m_FCohesion = ForceTot;
    }

    //  Met à jour la force de séparation avec tout les chèvre voisine
    //private void AlignementF(List<VoisinInfo> lstVoisin)
    //{
    //    Vector2 DirMoyenne = new Vector2(0, 0);

    //    float influence = 0;
    //    if (m_AlignRange != 0)
    //        foreach (VoisinInfo voisin in lstVoisin)
    //        {
    //            influence = Mathf.Clamp(1 - (voisin.distance / m_AlignRange), 0, 1) * Mathf.Clamp(voisin.intance.mySpeed / m_MaxSpeed, 0, 1);
    //            DirMoyenne += voisin.intance.myDirection * influence;
    //        }

    //    DirMoyenne.Normalize();

    //    m_FAlign = DirMoyenne;
    //}

    private static float GetDistanceBasedInfluence(float distance, float range, float power)
    {
        return Mathf.Pow(Mathf.Clamp(1 - (distance / range), 0, 1), power);
    }

    private Vector2 SmoothSeek(Vector2 target, float range = -1, float playerPower = 1, float offset = 0)
    {
        if (range == -1)
        range = m_SeekRange;

        if(offset != 0)
            target -= (target - (Vector2)tr.position).normalized * offset;
        


        return -GetRepulsePowerFrom(target, range, m_SeekPower) * playerPower;
    }

    private Vector2 SmoothSeek(Rigidbody2D target, float range = -1, float playerPower = 1, float offset = 0)
    {
        if (range == -1)
            range = m_SeekRange;
   
        Vector2 cible = target.position;

        if (offset != 0)
        {
            BergerBehavior Be = target.GetComponentInChildren<BergerBehavior>();
            Vector2 decalage = new Vector2(Be.transform.right.x * -offset, Be.transform.right.y * -offset);
            cible = cible + decalage;
        }



        return -GetRepulsePowerFrom(cible, range, m_SeekPower) * playerPower;
    }

    private Vector2 Seek(Vector2 target, float range = -1, float playerPower = -1)
    {
        return ((target - (Vector2)tr.position).normalized * m_MaxSpeed * playerPower) - rb.velocity;
    }

    private Vector2 Flee(Vector2 target, float range =-1, float playerPower = 1)
    {
        if (range == -1)
            range = m_FleeRange;

        return GetRepulsePowerFrom(target, range, m_FleePower) * playerPower;
    }

    private Vector2 Evade(Rigidbody2D poursuivant, float range = -1, float playerPower = 1)
    {
        if (range == -1)
            range = m_FleeRange;

        Vector2 ToPursuer = poursuivant.position - (Vector2)tr.position;

        //      !!!!!!!!!!!!!!!!!!!!!!!!!!!!!       Arranger la distance !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        float distMenace = range;//10.0f;
        if (ToPursuer.sqrMagnitude > distMenace * distMenace) return new Vector2();

        float LookAhead = ToPursuer.magnitude / (m_MaxSpeed + poursuivant.velocity.magnitude);

        Vector2 threat = (Vector2)poursuivant.transform.position;
        Vector2 estim = poursuivant.velocity * LookAhead;

        Vector2 dist = threat + estim;
        float newRange = range + estim.magnitude;

        return Flee(dist, newRange, playerPower);
    }


    private void EvadeF()
    {
        m_FFlee = Vector2.zero;

        List<Repulse> replusions = PlayerContainer.Instance.GetAllRepuslion();
        for(int i =0; i < replusions.Count; i++)
        {
            Repulse R = replusions[i];
            if ((R.position - (Vector2)tr.position).magnitude < R.range)
                m_FFlee += Flee(R.position, R.range, R.strength);
        }
    }

    private void AttractF()
    {
        m_FSeek = Vector2.zero;

        List<Attract> attractions = PlayerContainer.Instance.GetAllAttraction();
        for (int i = 0; i < attractions.Count; i++)
        {
            Attract A = attractions[i];
            if ((A.position - (Vector2)tr.position).magnitude < A.range)
                m_FSeek += SmoothSeek(A.owner, A.range, A.strength, 1f);
        }
    }



    private Vector2 GetRepulsePowerFrom(Vector2 target, float influenceRange, float influencePower)
    {
        float PanicDistance = m_FleeRange;
        Vector2 v = (Vector2)tr.position - target;
        float influence = Mathf.Clamp(1 - (v.magnitude / influenceRange), 0, 1);

        influence = Mathf.Pow(influence, influencePower);

        Vector2 ForceTot = v.normalized * m_MaxSpeed * influence;

        return ForceTot;
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

        for (int i = 0; i < vectorsToDisplay.Count; i++)
        {
            print("Vec: " + vectorsToDisplay[i]);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(tr.position, tr.position + (Vector3)vectorsToDisplay[i]);
        }
    }
}
