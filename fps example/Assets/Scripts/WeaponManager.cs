﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static bool isChangeWeapon = false;
    [SerializeField] private string currentWeaponType;
    public static Transform currentWeapon;
    public static Animator currentWeaponAnim;

    [SerializeField] private float changeWeaponDelayTime;
    [SerializeField] private float changeWeaponEndDelayTime;
    [SerializeField] private Gun[] guns;
    [SerializeField] private CloseWeapon[] hands;
    [SerializeField] private CloseWeapon[] axes;
    [SerializeField] private CloseWeapon[] pickaxes;

    private Dictionary<string, Gun> gunDictionary = new Dictionary<string, Gun>();
    private Dictionary<string, CloseWeapon> handDictionary = new Dictionary<string, CloseWeapon>();
    private Dictionary<string, CloseWeapon> axeDictionary = new Dictionary<string, CloseWeapon>();
    private Dictionary<string, CloseWeapon> pickaxeDictionary = new Dictionary<string, CloseWeapon>();

    [SerializeField] private GunController gunController;
    [SerializeField] private HandController handController;
    [SerializeField] private AxeController axeController;
    [SerializeField] private PickaxeController pickaxeController;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            gunDictionary.Add(guns[i].gunName, guns[i]);
        }
        for (int i = 0; i < hands.Length; i++)
        {
            handDictionary.Add(hands[i].closeWeaponName, hands[i]);
        }
        for (int i = 0; i < axes.Length; i++)
        {
            axeDictionary.Add(axes[i].closeWeaponName, axes[i]);
        }
        for (int i = 0; i < pickaxes.Length; i++)
        {
            pickaxeDictionary.Add(pickaxes[i].closeWeaponName, pickaxes[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isChangeWeapon)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                StartCoroutine(ChangeWeaponCoroutine("HAND", "BareHand"));
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                StartCoroutine(ChangeWeaponCoroutine("GUN", "SubMachineGun1"));
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                StartCoroutine(ChangeWeaponCoroutine("AXE", "Axe"));
            else if (Input.GetKeyDown(KeyCode.Alpha4))
                StartCoroutine(ChangeWeaponCoroutine("PICKAXE", "Pickaxe"));
        }
    }

    public IEnumerator ChangeWeaponCoroutine(string _type, string _name)
    {
        isChangeWeapon = true;
        currentWeaponAnim.SetTrigger("weapon_out");
        yield return new WaitForSeconds(changeWeaponDelayTime);

        CancelPreWeaponAction();
        WeaponChange(_type, _name);
        yield return new WaitForSeconds(changeWeaponEndDelayTime);

        currentWeaponType = _type;
        isChangeWeapon = false;
    }

    private void CancelPreWeaponAction()
    {
        switch(currentWeaponType)
        {
            case "GUN":
                gunController.CancelFineSight();
                gunController.CancelReload();
                GunController.isActivate = false;
                break;
            case "HAND":
                HandController.isActivate = false;
                break;
            case "AXE":
                AxeController.isActivate = false;
                break;
            case "PICKAXE":
                PickaxeController.isActivate = false;
                break;
        }
    }

    private void WeaponChange(string _type, string _name)
    {
        if (_type == "GUN") 
            gunController.GunChange(gunDictionary[_name]);
        else if (_type == "HAND")
            handController.CloseWeaponChange(handDictionary[_name]);
        else if (_type == "AXE")
            axeController.CloseWeaponChange(axeDictionary[_name]);
        else if (_type == "PICKAXE")
            pickaxeController.CloseWeaponChange(pickaxeDictionary[_name]);

    }

    public IEnumerator WeaponInCoroutine()
    {
        isChangeWeapon = true;
        currentWeaponAnim.SetTrigger("weapon_out");
        yield return new WaitForSeconds(changeWeaponDelayTime);
        currentWeapon.gameObject.SetActive(false);
    }
    public void WeaponOut()
    {
        isChangeWeapon = false;
        currentWeapon.gameObject.SetActive(true);
    }
}
