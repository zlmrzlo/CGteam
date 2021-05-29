using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{

    // 체력
    [SerializeField] private int hp;
    public static float currentHp;

    // 스태미나
    [SerializeField] private int mp;
    public static float currentMp;

    // 필요한 이미지
    [SerializeField] private Image[] images_GaugeFront;
    [SerializeField] private Image[] images_GaugeBack;
    [SerializeField] private Text[] texts_Gauge;

    private const int HP = 0, MP = 1;
    private float[] lerpTimer;
    
    public float chipSpeed = 2f;

    // Use this for initialization
    void Start()
    {
        currentHp = hp;
        currentMp = mp;
        lerpTimer = new float[images_GaugeFront.Length];
    }

    // Update is called once per frame
    void Update()
    {
        currentHp = Mathf.Clamp(currentHp, 0, hp);
        currentMp = Mathf.Clamp(currentMp, 0, mp);
        GaugeUpdate();
        if (Input.GetKeyDown(KeyCode.Keypad4))
            DecreaseHP(Random.Range(5, 10));
        if (Input.GetKeyDown(KeyCode.Keypad6))
            IncreaseHP(Random.Range(5, 10));
        if (Input.GetKeyDown(KeyCode.Keypad7))
            DecreaseMP(Random.Range(5, 10));
        if (Input.GetKeyDown(KeyCode.Keypad9))
            IncreaseMP(Random.Range(5, 10));
    }

    private void GaugeUpdate()
    {
        float hpFront = images_GaugeFront[HP].fillAmount;
        float hpBack = images_GaugeBack[HP].fillAmount;
        float hpFraction = (float)currentHp / hp;
        if(hpBack>hpFraction)
        {
            images_GaugeFront[HP].fillAmount = hpFraction;
            Color c = new Color(1, 1, 0.3f);
            c.a = 0.5f;
            images_GaugeBack[HP].color = c;
            lerpTimer[HP] += Time.deltaTime;
            float percentComplete = lerpTimer[HP] / chipSpeed;
            percentComplete *= percentComplete;
            images_GaugeBack[HP].fillAmount = Mathf.Lerp(hpBack, hpFraction, percentComplete);
        }
        if(hpFront<hpFraction)
        {
            Color c = new Color(1, 0, 0);
            c.a = 0.5f;
            images_GaugeBack[HP].color = c;
            images_GaugeBack[HP].fillAmount = hpFraction;
            lerpTimer[HP] += Time.deltaTime;
            float percentComplete = lerpTimer[HP] / chipSpeed;
            percentComplete *= percentComplete;
            images_GaugeFront[HP].fillAmount = Mathf.Lerp(hpFront, hpFraction, percentComplete);
        }

        float mpFront = images_GaugeFront[MP].fillAmount;
        float mpBack = images_GaugeBack[MP].fillAmount;
        float mpFraction = (float)currentMp / mp;
        if (mpBack > mpFraction)
        {
            images_GaugeFront[MP].fillAmount = mpFraction;
            Color c = new Color(1, 1, 0.3f);
            c.a = 0.5f;
            images_GaugeBack[MP].color = c;
            lerpTimer[MP] += Time.deltaTime;
            float percentComplete = lerpTimer[MP] / chipSpeed;
            percentComplete *= percentComplete;
            images_GaugeBack[MP].fillAmount = Mathf.Lerp(mpBack, mpFraction, percentComplete);
        }
        if (mpFront < mpFraction)
        {
            Color c = new Color(0, 0, 1);
            c.a = 0.5f;
            images_GaugeBack[MP].color = c;
            images_GaugeBack[MP].fillAmount = mpFraction;
            lerpTimer[MP] += Time.deltaTime;
            float percentComplete = lerpTimer[MP] / chipSpeed;
            percentComplete *= percentComplete;
            images_GaugeFront[MP].fillAmount = Mathf.Lerp(mpFront, mpFraction, percentComplete);
        }

        //images_GaugeFront[HP].fillAmount = (float)currentHp / hp;
        //images_GaugeFront[MP].fillAmount = (float)currentMp / mp;
        texts_Gauge[HP].text = currentHp + " / " + hp;
        texts_Gauge[MP].text = currentMp + " / " + mp;
    }

    public void IncreaseHP(int _count)
    {
        if (currentHp + _count < hp)
            currentHp += _count;
        else
            currentHp = hp;
        lerpTimer[HP] = 0f;
    }

    public void DecreaseHP(int _count)
    {
        currentHp -= _count;

        if (currentHp <= 0)
            Debug.Log("캐릭터의 hp가 0이 되었습니다!!");
        lerpTimer[HP] = 0f;
    }

    public void IncreaseMP(int _count)
    {
        if (currentMp + _count < mp)
            currentMp += _count;
        else
            currentMp = mp;
        lerpTimer[MP] = 0f;
    }

    public void DecreaseMP(int _count)
    {

        if (currentMp < _count)
            Debug.Log("캐릭터의 mp가 부족합니다!!");
        else
            currentMp -= _count;
        lerpTimer[MP] = 0f;
    }

    public float GetCurrentMP()
    {
        return currentMp;
    }

    public float GetCurrentHP()
    {
        return currentHp;
    }
}
