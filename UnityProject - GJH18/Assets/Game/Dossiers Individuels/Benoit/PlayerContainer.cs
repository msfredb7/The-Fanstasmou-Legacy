using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContainer : CCC.DesignPattern.PublicSingleton<PlayerContainer>
{

    List<WolfBehavior> wolves = new List<WolfBehavior>();
    List<BergerBehavior> bergers = new List<BergerBehavior>();

    public void AddWolves(WolfBehavior wolf) {
        wolves.Add(wolf);
    }

    public void AddBerger(BergerBehavior berger){
        bergers.Add(berger);
    }

    public List<WolfBehavior> GetWolves() {
        return wolves;
    }
    public List<BergerBehavior> GetBergers(){
        return bergers;
    }

    public List<Repulse> GetAllRepuslion()
    {
        List<Repulse> output = new List<Repulse>();
        for (int i = 0; i < wolves.Count; i++)
        {
            if (wolves[i].repulse.active == true)
                output.Add(wolves[i].repulse);
        }
        for (int i = 0; i < bergers.Count; i++)
        {
            if (bergers[i].repulse.active == true)
                output.Add(bergers[i].repulse);
        }
        return output;
    }

    public List<Attract> GetAllAttraction()
    {
        List<Attract> output = new List<Attract>();
        for (int i = 0; i < bergers.Count; i++)
        {
            if (bergers[i].attract.active == true)
                output.Add(bergers[i].attract);
           // if (bergers[i].barkAttract.active == true)
            //    output.Add(bergers[i].barkAttract);
        }
        return output;
    }

}
