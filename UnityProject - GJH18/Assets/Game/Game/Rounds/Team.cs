using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team {

    public List<PlayerInfo> PlayersInfo;
    public int NBSheepRescued = 0;
    public int NbSheepEaten = 0;

    public Team()
    {
        PlayersInfo = new List<PlayerInfo>();
    }
}
