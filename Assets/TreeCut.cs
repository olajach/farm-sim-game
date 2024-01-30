using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCut : ToolHit
{
    override public void Hit()
    {
        Destroy(gameObject);
    }
}