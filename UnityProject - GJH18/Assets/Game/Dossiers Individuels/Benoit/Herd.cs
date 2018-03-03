using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herd : MonoBehaviour {
    public static float maxDistance = 1.0f;

    [SerializeField]
    private List<HerdMember> herdMembers = new List<HerdMember>();

    public Herd(HerdMember firstMember)
    {
        herdMembers.Add(firstMember);
        firstMember.herd = this;
    }
    public Herd(List<HerdMember> members)
    {
        herdMembers = members;
        for (int i =0; i < members.Count; i++)
            members[i].herd = this;
    }

    public void Start ()
    {
		
	}

    public void Update ()
    {
        if (herdMembers.Count == 0)
            Destroy(this);

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
        new Herd(members);
    }

    public void UpdateHerd(List<HerdMember> members)
    {
        herdMembers = members;
        for (int i = 0; i < members.Count; i++)
            members[i].herd = this;
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
}
