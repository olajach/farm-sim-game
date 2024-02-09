using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolsCharacterController : MonoBehaviour
{
    CharacterController2D character;
    Rigidbody2D rgbd2d;
    ToolbarController toolbarController;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1f;
    [SerializeField] TileMapReadController tileMapReadController;
    [SerializeField] float maxDistance = 1.5f;
    

    Vector3Int selectedTilePosition;
    bool selectable;

    private void Awake()
    {
        character = GetComponent<CharacterController2D>();
        rgbd2d = GetComponent<Rigidbody2D>();
        toolbarController = GetComponent<ToolbarController>();
    }

    private void Update()
    {
        SelectTile();
        CanSelectCheck();
        if (Input.GetMouseButtonDown(0))
        {
            if(UseToolWorld() == true)
            {
                return;
            };
            UseToolGrid();
        }
    }

    private void SelectTile()
    {
        selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
    }

    void CanSelectCheck()
    {
        Vector2 characterPosition = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(characterPosition, cameraPosition) < maxDistance;

    }

    private bool UseToolWorld()
    {
        Vector2 position = rgbd2d.position + character.lastMotionVector * offsetDistance;
        Item item = toolbarController.GetItem;
        if (item == null)
        {
            return false;
        }
        if (item.onAction == null)
        {
            return false;
        }
        
        bool complete = item.onAction.OnApply(position);

        if (complete == true)
            {
                if (item.onItemUsed != null)
                {
                    item.onItemUsed.OnItemUsed(item, GameManager.instance.inventoryContainer);
                }
            }
       
        return complete;
    }

    private void UseToolGrid()
    {
        if (selectable == true)
        {
           Item item = toolbarController.GetItem;
           if (item == null)
           {
               return;
           }
            if (item.onTileMapAction == null)
            {
                return;
            }

            bool complete = item.onTileMapAction.OnApplyToTileMap(selectedTilePosition, tileMapReadController);

            if (complete == true)
            {
                if (item.onItemUsed != null)
                {      
                item.onItemUsed.OnItemUsed(item, GameManager.instance.inventoryContainer);
                }
            }

         }     
    }  
} 