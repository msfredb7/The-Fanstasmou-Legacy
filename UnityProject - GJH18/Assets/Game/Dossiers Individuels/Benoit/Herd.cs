using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Herd{
    public static float maxDistance = 1.5f;

    [SerializeField]
    private List<HerdMember> herdMembers = new List<HerdMember>();

    public Herd(HerdMember firstMember)
    {
        herdMembers.Add(firstMember);
        firstMember.SetHerd(this);
    }

    public Herd(List<HerdMember> members)
    {
        herdMembers = members;
        for (int i =0; i < members.Count; i++)
            members[i].SetHerd(this);
    }



    public void Update ()
    {
        List<HerdMember> memberLeftToCheck = herdMembers;
        while(memberLeftToCheck.Count > 0)
        {
            List<HerdMember> connectedSheeps = GetAllConnectedSheeps(herdMembers[0]);
            for (int j = 0; j < connectedSheeps.Count; j++)
            {
                if (memberLeftToCheck.Contains(connectedSheeps[j]))
                    memberLeftToCheck.Remove(connectedSheeps[j]);
            }

            if (memberLeftToCheck.Count != 0)
                NewHerd(connectedSheeps);
            else
                UpdateHerd(connectedSheeps);
        }
    }



    public void NewHerd(List<HerdMember> members)
    {
        Herd newHerd = HerdList.Instance.NewHerd(members);
    }

    public void UpdateHerd(List<HerdMember> members)
    {
        herdMembers = members;
        for (int i = 0; i < members.Count; i++)
            members[i].SetHerd(this);
    }

    public List<HerdMember> GetAllConnectedSheeps(HerdMember root)
    {
        List<HerdMember> detectedSheeps = new List<HerdMember>();
        detectedSheeps.Add(root);

        for(int i = 0; i < detectedSheeps.Count; i++)
        {
            List<HerdMember> closeSheeps = detectedSheeps[i].GetcloseSheeps();
            for (int j = 0; j < closeSheeps.Count; j++)
                if (!detectedSheeps.Contains(closeSheeps[j]))
                     detectedSheeps.Add(closeSheeps[j]);
        }
        return detectedSheeps;
    }

    public Vector2 GetMiddle()
    {
        Vector2 middle = Vector2.zero;
        for (int i = 0; i < herdMembers.Count; i++)
            middle += (Vector2)herdMembers[i].transform.position;

        return middle / herdMembers.Count;
    }

    public void RemoveMember(HerdMember member)
    {
        herdMembers.Remove(member);
        if(herdMembers.Count == 0)
            HerdList.Instance.RemoveHerd(this);
    }

    public int MemberCount()
    {
        return herdMembers.Count;
    }

    public void Eat()
    {
        //Eat sheep Function to be call

        for (int i = 0; i < herdMembers.Count; i++)
           (herdMembers[i].gameObject).Destroy();
        HerdList.Instance.RemoveHerd(this);
    }

    public void FixedUpdate()
    {
        //CalculateBehaviors();
    }

    /*
    public void CalculateBehaviors()
    {
        Vector2 middle = GetMiddle();

        m_FSeek = Vector2.zero;
        AttractF(middle);
    }


    private void AttractF(Vector2 middle)
    {
        m_FSeek = Vector2.zero;

        List<Attract> attractions = PlayerContainer.Instance.GetAllAttraction();
        for (int i = 0; i < attractions.Count; i++)
        {
            Attract A = attractions[i];
            if ((A.position - middle).magnitude < A.range)
                m_FSeek += HerdFollow(middle, A.position, A.range, A.strength);
        }
    }
    private Vector2 HerdFollow(Vector2 middle, Vector2 target, float range = 0, float playerPower = 0)
    {
        float targetDistance = 1;


        Vector2 v = middle - target;
        float influence = Mathf.Clamp(1 - (v.magnitude / range), 0, 1);

        influence = Mathf.Pow(influence, playerPower);

        Vector2 ForceTot = v.normalized * herdMembers[0].GetComponent<SheepComponent>().m_MaxSpeed * influence;

        return ForceTot;

       */
}
