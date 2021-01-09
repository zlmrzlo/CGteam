﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWeapon : MonoBehaviour
{

    public string closeWeaponName; //  근접 무기 이름

    // 근접 무기 유형
    public bool isHand;
    public bool isAxe;
    public bool isPickaxe;

    public float range; // 공격 범위
    public int damage; // 공격력
    public float workSpeed; // 작업 속도
    public float attackDelay; // 공격 딜레이
    public float attackDelayA; // 공격 판정 시점
    public float attackDelayB; // 공격 완료 시점

    public Animator anim; // 애니메이션

}
