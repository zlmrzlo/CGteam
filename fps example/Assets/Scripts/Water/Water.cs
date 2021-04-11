using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Water : MonoBehaviour
{
    [SerializeField] private float waterDrag;
    private float originDrag;

    [SerializeField] private Color waterColor;
    [SerializeField] private float waterFogDensity;
    [SerializeField] private Color waterNightColor;
    [SerializeField] private float waterNightFogDensity;

    private Color originColor;
    private float originFogDensity;
    [SerializeField] private Color originNightColor;
    [SerializeField] private float originNightFogDensity;

    [SerializeField] private string soundWaterOut;
    [SerializeField] private string soundWaterIn;
    [SerializeField] private string soundBreathe;
    [SerializeField] private float breatheTime;
    private float currentBreatheTime;

    [SerializeField] private float totalOxygen;
    private float currentOxygen;
    private float temp;

    [SerializeField] private GameObject baseUI;
    [SerializeField] private Text textTotalOxygen;
    [SerializeField] private Text textCurrentOxygen;
    [SerializeField] private Image imageGauge;
    private StatusController playerStat;

    // Start is called before the first frame update
    void Start()
    {
        originColor = RenderSettings.fogColor;
        originFogDensity = RenderSettings.fogDensity;
        originDrag = 0;
        playerStat = FindObjectOfType<StatusController>();
        currentOxygen = totalOxygen;
        textTotalOxygen.text = totalOxygen.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.isWater)
        {
            currentBreatheTime += Time.deltaTime;
            if(currentBreatheTime>=breatheTime)
            {
                SoundManager.instance.PlaySE(soundBreathe);
                currentBreatheTime = 0;
            }
        }
        DecreaseOxygen();
    }

    private void DecreaseOxygen()
    {
        if(GameManager.isWater)
        {
            currentOxygen -= Time.deltaTime;
            textCurrentOxygen.text = Mathf.RoundToInt(currentOxygen).ToString();
            imageGauge.fillAmount = currentOxygen / totalOxygen;
        }
        if(currentOxygen<=0)
        {
            temp += Time.deltaTime;
            if (temp >= 1) ;
            {
                playerStat.DecreaseHP(1);
                temp = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
            GetWater(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
            GetOutWater(other);
    }

    private void GetWater(Collider _Player)
    {
        SoundManager.instance.PlaySE(soundWaterIn);
        baseUI.SetActive(true);
        GameManager.isWater = true;
        _Player.transform.GetComponent<Rigidbody>().drag = waterDrag;
        if(!GameManager.isNight)
        {
            RenderSettings.fogColor = waterColor;
            RenderSettings.fogDensity = waterFogDensity;
        }
        else
        {
            RenderSettings.fogColor = waterNightColor;
            RenderSettings.fogDensity = waterNightFogDensity;
        }
    }

    private void GetOutWater(Collider _Player)
    {
        if (GameManager.isWater)
        {
            baseUI.SetActive(false);
            currentOxygen = totalOxygen;
            SoundManager.instance.PlaySE(soundWaterOut);
            _Player.transform.GetComponent<Rigidbody>().drag = originDrag;
            if(!GameManager.isNight)
            {
                RenderSettings.fogColor = originColor;
                RenderSettings.fogDensity = originFogDensity;
            }
            else
            {
                RenderSettings.fogColor = originNightColor;
                RenderSettings.fogDensity = originNightFogDensity;
            }
        }

    }
}
