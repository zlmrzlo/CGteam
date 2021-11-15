﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEffect
{
    public string itemName; // 아이템의 이름. (키값)
    [Tooltip("HP, SP, DP, HUNGRY, THIRSTY , SATISFY만 가능합니다")]
    public string[] part; // 부위.
    public int[] num; // 수치.
}

public class ItemEffectDatabase : MonoBehaviour
{
    [SerializeField] private ItemEffect[] itemEffects;

    //필요한 컴포넌트
    [SerializeField] private StatusController thePlayerStatus;
    //[SerializeField] private WeaponManager theWeaponManager;
    [SerializeField] private SlotToolTip theSlotToolTip;

    private const string HP = "HP", MP = "MP";

    public void ShowToolTop(Item _item, Vector3 _pos)
    {
        theSlotToolTip.ShowToolTip(_item, _pos);
    }

    public void HideToolTip()
    {
        theSlotToolTip.HideToolTip();
    }


    public void UseItem(Item _item)
    {
        if (_item.itemType == Item.ItemType.Equipment)
        {
            //StartCoroutine(theWeaponManager.ChangeWeaponCoroutine(_item.weaponType, _item.itemName));
        }
        else if (_item.itemType == Item.ItemType.Used)
        {
            for (int x = 0; x < itemEffects.Length; x++)
            {
                if (itemEffects[x].itemName == _item.itemName)
                {
                    for (int y = 0; y < itemEffects[x].part.Length; y++)
                    {
                        switch (itemEffects[x].part[y])
                        {
                            case HP:
                                thePlayerStatus.IncreaseHP(itemEffects[x].num[y]);
                                break;/*
                            case MP:
                                thePlayerStatus.IncreaseMP(itemEffects[x].num[y]);
                                break;*/
                            default:
                                Debug.Log("잘못된 Status 부위. HP, MP만 가능합니다");
                                break;
                        }
                        Debug.Log(_item.itemName + " 을 사용했습니다");
                    }
                    return;
                }
            }
            Debug.Log("ItemEffectDatabase에 일치하는 itemName 없습니다");
        }
    }
}
