using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/Hoe")]
public class HoeTile : ToolAction
{
    [SerializeField] List<TileBase> canPlow;

    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Item item)
    {
        TileBase tileToPlow = tileMapReadController.GetTileBase(gridPosition);
        Debug.Log("HoeTile.OnApplyToTileMap: tileToPlow=" + tileToPlow);

        if (canPlow.Contains(tileToPlow) == false)
        {
            return false;
        }

        tileMapReadController.cropsManager.Plow(gridPosition);
        
        return true;
    }
}
