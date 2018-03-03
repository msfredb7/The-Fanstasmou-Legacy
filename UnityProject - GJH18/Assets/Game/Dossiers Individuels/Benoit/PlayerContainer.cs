using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContainer : CCC.DesignPattern.PublicSingleton<PlayerContainer>
{

    List<WolfBehavior> wolves = new List<WolfBehavior>();
    List<BergerBehavior> bergers = new List<BergerBehavior>();

    public void NewWolves(WolfBehavior wolf) {
        wolves.Add(wolf);
    }

    public void NewBerger(BergerBehavior berger){
        bergers.Add(berger);
    }

    public List<WolfBehavior> GetWolves() {
        return wolves;
    }
    public List<BergerBehavior> GetBergers(){
        return bergers;
    }
}
