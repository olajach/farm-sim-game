using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemDragAndDropController : MonoBehaviour
{

    [SerializeField] ItemSlot itemSlot;

    private void Start()
    {
        itemSlot = new ItemSlot();
    }
    
   internal void OnClick(ItemSlot itemSlot)
   {
        if(this.itemSlot.item == null)
        {
            this.itemSlot.Copy(itemSlot);
            itemSlot.Clear();
        }
        else
        {
            Item item = itemSlot.item;
            int count = itemSlot.count;

            itemSlot.Copy(this.itemSlot);
            this.itemSlot.Set(item, count);
        }
}
}