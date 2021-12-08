using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkySoul : GhostSoul
{
    public override void OnEnable()
    {
        ghost.target = soul;
    }
    //targets the space farthest from the soul
}
