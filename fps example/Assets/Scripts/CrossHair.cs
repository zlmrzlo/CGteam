using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private float gunAccuracy;
    [SerializeField] private GameObject crosshairHUD;
    [SerializeField] private GunController gunController;

    public void WalkingAnimation(bool _flag)
    {
        if(!GameManager.isWater)
        {
            WeaponManager.currentWeaponAnim.SetBool("walk", _flag);
            animator.SetBool("walking", _flag);
        }
    }
    public void RunningAnimation(bool _flag)
    {
        if (!GameManager.isWater)
        {
            WeaponManager.currentWeaponAnim.SetBool("run", _flag);
            animator.SetBool("running", _flag);
        }
    }
    public void JumpingAnimation(bool _flag)
    {
        if (!GameManager.isWater)
        {
            animator.SetBool("running", _flag);
        }
    }
    public void CrouchingAnimation(bool _flag)
    {
        if (!GameManager.isWater)
        {
            animator.SetBool("crouching", _flag);
        }
    }
    public void FineSightAnimation(bool _flag)
    {
        if (!GameManager.isWater)
        {
            animator.SetBool("finesight", _flag);
        }
    }
    public void FireAnimation()
    {
        if (!GameManager.isWater)
        {
            if (animator.GetBool("walking"))
                animator.SetTrigger("walkfire");
            else if (animator.GetBool("crouching"))
                animator.SetTrigger("crouchfire");
            else
                animator.SetTrigger("idlefire");
        }
        
    }
    public float GetAccuracy()
    {
        if (animator.GetBool("walking"))
            gunAccuracy = 0.06f;
        else if (animator.GetBool("crouching"))
            gunAccuracy = 0.015f;
        else if (gunController.GetFineSightMode())
            gunAccuracy = 0.001f;
        else
            gunAccuracy = 0.035f;

        return gunAccuracy;
    }
}
