using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team {

    public List<PlayerInfo> PlayersInfo;
    public int NBSheepRescued;
    public int NbSheepEaten;

    public Team()
    {
        PlayersInfo = new List<PlayerInfo>();
    }
}
