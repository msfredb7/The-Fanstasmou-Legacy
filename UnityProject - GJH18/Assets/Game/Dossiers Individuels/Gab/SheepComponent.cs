using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepComponent : MonoBehaviour {

    Vector2 m_Force, m_FWandering, m_FSepare, m_FAlign, m_FCohesion;
    public float m_PoidWandering, m_PoidSeparation, m_PoidAlign, m_PoidCohesion;
    public float m_WanderRadius, m_WanderRange, m_MaxSpeed, m_WanderRefresh;

    Rigidbody2D rb;

    private Transform tr;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>() as Rigidbody2D;

        tr = GetComponent<Transform>() as Transform;

       InvokeRepeating("WanderF", 1.0f, m_WanderRefresh);

    }
	
	// Update is called once per frame
	void Update () {
        //m_Force = WanderF() * m_PoidWandering;

        if(Input.GetKeyDown("d"))
            rb.AddRelativeForce(new Vector2(10.0f, 10.0f));
    }

    private void FixedUpdate()
    {
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

    public void WanderF()
    {
        Vector2 cible = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

        cible.Normalize();

        cible *= m_WanderRadius;

        m_FWandering = cible;

        m_Force = m_FWandering * m_PoidWandering;
    }
}
