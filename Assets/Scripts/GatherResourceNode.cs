using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Gather Resource Node")]
public class GatherResourceNode : ToolAction
{
    [SerializeField] float sizeOfInteractableArea = 1f;
    public override bool OnApply(Vector2 worldPoint)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, sizeOfInteractableArea);

        foreach (Collider2D c in colliders)
        {
            ToolHit hit = c.GetComponent<ToolHit>();
            if (hit != null)
            {
                hit.Hit();
                return true;
            }
        }

        return false;
    
    }
}
