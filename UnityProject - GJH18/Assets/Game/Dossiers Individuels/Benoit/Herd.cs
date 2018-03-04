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

    public List<HerdMember> getMembers()
    {
        return herdMembers;
    }

    public void Eat(PlayerInfo eater)
    {
        //Eat sheep Function to be call

        for (int i = 0; i < herdMembers.Count; i++)
        {
            (herdMembers[i].gameObject).Destroy();
            Rounds.Instance.AddSheepEaten(1, eater);
        }
         
        HerdList.Instance.RemoveHerd(this);
    }
}
