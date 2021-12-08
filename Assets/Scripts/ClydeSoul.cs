using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClydeSoul : GhostSoul 
{
    public override void OnEnable()
    {
        ghost.target = soul;
    }
    //Targets the soul if farther than 6 away, targets the corner nearest the soul if 6 or less
}
