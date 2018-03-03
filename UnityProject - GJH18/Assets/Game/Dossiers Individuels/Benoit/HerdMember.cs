using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerdMember : MonoBehaviour {
    public static float maxDistance = 1.0f;

    private Voisin voisin;

    [HideInInspector]
    private Herd herd = null;

    public void Start()
    {
        voisin = GetComponent<Voisin>() as Voisin;
        herd = HerdList.Instance.NewHerd(this);
    }


    public List<HerdMember> GetcloseSheeps()
    {
        List<HerdMember> members = new List<HerdMember>();
        List<Voisin> lVoisins = voisin.GetVoisinsInRange(maxDistance);

        for(int i = 0; i < lVoisins.Count; i++)
        {
            HerdMember hMember = lVoisins[i].gameObject.GetComponent<HerdMember>() as HerdMember;
            if (hMember)
                members.Add(hMember);
        }

        return members;
    }


    public void Update()
    {
        Debug.DrawLine(transform.position, (Vector3)herd.GetMiddle(), Color.red);
    }

    public void SetHerd(Herd newHerd)
    {
        if (herd != null)
            herd.RemoveMember(this);
        herd = newHerd;
    }
    public Herd GetHerd()
    {
        return herd;
    }

}
