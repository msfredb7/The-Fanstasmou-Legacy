using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerdList : CCC.DesignPattern.PublicSingleton<HerdList>
{
    public List<Herd> herds = new List<Herd>();

    public int GetSheepCount()
    {
        int count = 0;
        for (int i = 0; i < herds.Count; i++)
            count += herds[i].MemberCount();
        return count;
    }

    public List<HerdMember> GetAllSheeps()
    {
        List<HerdMember> sheeps = new List<HerdMember>();

        for (int i = 0; i < herds.Count; i++)
            sheeps.AddRange(herds[i].getMembers());

        return sheeps;    
    }
        
        

    public Herd NewHerd(HerdMember member)
    {
        Herd newHerd = new Herd(member);
        herds.Add(newHerd);
        return newHerd;
    }

    public Herd NewHerd(List<HerdMember> members)
    {
        Herd newHerd = new Herd(members);
        herds.Add(newHerd);
        return newHerd;
    }

    public void RemoveHerd(Herd herd)
    {
        herds.Remove(herd);
        herd = null;
    }

	public void Update () {
        for (int i = 0; i < herds.Count; i++)
            herds[i].Update();
	}
}
