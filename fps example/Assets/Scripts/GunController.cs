using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public static bool isActivate = false;
    [SerializeField] private Gun currentGun;
    private float currentFireRate;

    private bool isReload = false;

    public bool isFineSightMode = false;
    [SerializeField] private Vector3 originPos;

    private AudioSource audioSource;
    private RaycastHit hitInfo;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject hitEffectPrefab;
    private CrossHair crossHair;
    void Start()
    {
        originPos = Vector3.zero;
        audioSource = GetComponent<AudioSource>();
        crossHair = FindObjectOfType<CrossHair>();

        //WeaponManager.currentWeapon = currentGun.GetComponent<Transform>();
        //WeaponManager.currentWeaponAnim = currentGun.anim;
    }

    void Update()
    {
        if(isActivate)
        {
            GunFireRateCalc();
            TryFire();
            TryReload();
            TryFineSight();
        }
    }

    private void GunFireRateCalc()
    {
        if (currentFireRate > 0)
            currentFireRate -= Time.deltaTime;
    }

    private void TryFire()
    {
        if (Input.GetButton("Fire1") && currentFireRate <= 0 && !isReload)
            Fire();
        
    }
    private void Fire()
    {
        if(!isReload)
        {
            currentFireRate = currentGun.fireRate;
            if (currentGun.currentBulletCount > 0)
                Shoot();
            else
            {
                CancelFineSight();
                StartCoroutine(ReloadCoroutine());
            }
        }
    }

    private void Shoot()
    {
        crossHair.FireAnimation();
        currentGun.currentBulletCount--;
        currentFireRate = currentGun.fireRate;
        PlaySE(currentGun.fire_Sound);
        currentGun.muzzleFlash.Play();
        Hit();
        StopAllCoroutines();
        StartCoroutine(RetroActionCoroutine());
    }

    private void TryReload()
    {
        if(Input.GetKeyDown(KeyCode.R)&&!isReload&&currentGun.currentBulletCount<currentGun.reloadBulletCount)
        {
            CancelFineSight();
            StartCoroutine(ReloadCoroutine());
        }
    }
    
    public void CancelReload()
    {
        if(isReload)
        {
            StopAllCoroutines();
            isReload = false;
        }
    }

    IEnumerator ReloadCoroutine()
    {
        if (currentGun.carryBulletCount > 0)
        {
            isReload = true;
            currentGun.anim.SetTrigger("reload");
            currentGun.carryBulletCount += currentGun.currentBulletCount;
            currentGun.currentBulletCount = 0;
            yield return new WaitForSeconds(currentGun.reloadTime);
            if (currentGun.carryBulletCount > -currentGun.reloadBulletCount)
            {
                currentGun.currentBulletCount = currentGun.reloadBulletCount;
                currentGun.carryBulletCount -= currentGun.reloadBulletCount;
            }
            else
            {
                currentGun.currentBulletCount = currentGun.carryBulletCount;
                currentGun.carryBulletCount = 0;
            }

            isReload = false;
        }
        else
            Debug.Log("소유한 총알이 없습니다.");
    }

    private void Hit()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward +
            new Vector3(Random.Range(-crossHair.GetAccuracy()-currentGun.accuracy, crossHair.GetAccuracy() + currentGun.accuracy),
                        Random.Range(-crossHair.GetAccuracy() - currentGun.accuracy, crossHair.GetAccuracy() + currentGun.accuracy),
                        0)
            , out hitInfo, currentGun.range))
        {
            var clone = Instantiate(hitEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(clone, 2f);
        }
    }

    private void TryFineSight()
    {
        if (Input.GetButtonDown("Fire2") && !isReload) 
            FineSight();
    }

    public void CancelFineSight()
    {
        if (isFineSightMode)
            FineSight();
    }
    private void FineSight()
    {
        isFineSightMode = !isFineSightMode;
        currentGun.anim.SetBool("finesightmode", isFineSightMode);
        crossHair.FineSightAnimation(isFineSightMode);
        if (isFineSightMode)
        {
            StopAllCoroutines();
            StartCoroutine(FineSightActivationCoroutine());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(FineSightDeactivationCoroutine());
        }
        
    }

    IEnumerator FineSightActivationCoroutine()
    {
        while(currentGun.transform.localPosition!=currentGun.fineSightOriginPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSightOriginPos, 0.2f);
            yield return null;
        }
    }

    IEnumerator FineSightDeactivationCoroutine()
    {
        while (currentGun.transform.localPosition != originPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.2f);
            yield return null;
        }
    }

    private void PlaySE(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }

    IEnumerator RetroActionCoroutine()
    {
        Vector3 recoilBack = new Vector3(currentGun.retroActionForce, originPos.y, originPos.z);
        Vector3 retroActionRecoilBack = new Vector3(currentGun.retroActionFineSightForce, currentGun.fineSightOriginPos.y, currentGun.fineSightOriginPos.z);

        if (!isFineSightMode)
        {
            currentGun.transform.localPosition = originPos;
            while (currentGun.transform.localPosition.x <= currentGun.retroActionForce - 0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, recoilBack, 0.4f);
                yield return null;
            }
            while (currentGun.transform.localPosition != originPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.1f);
                yield return null;
            }
        }
        else
        {
            currentGun.transform.localPosition = currentGun.fineSightOriginPos;
            while (currentGun.transform.localPosition.x <= currentGun.retroActionFineSightForce - 0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, retroActionRecoilBack, 0.4f);
                yield return null;
            }
            while (currentGun.transform.localPosition != currentGun.fineSightOriginPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSightOriginPos, 0.1f);
                yield return null;
            }
        }
    }

    public Gun GetGun()
    {
        return currentGun;
    }
    public bool GetFineSightMode()
    {
        return isFineSightMode;
    }
    public void GunChange(Gun _gun)
    {
        if (WeaponManager.currentWeapon != null)
        {
            WeaponManager.currentWeapon.gameObject.SetActive(false);
        }
        currentGun = _gun;
        WeaponManager.currentWeapon = currentGun.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentGun.anim;

        currentGun.transform.localPosition = Vector3.zero;
        currentGun.gameObject.SetActive(true);
        isActivate = true;
    }
}
