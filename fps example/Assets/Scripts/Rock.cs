using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private int hp;
    [SerializeField] private float destroyTime;
    [SerializeField] private SphereCollider col;

    [SerializeField] private GameObject rock;
    [SerializeField] private GameObject debris;
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private GameObject rockItemPrefab;
    [SerializeField] private int count;

    [SerializeField] private string strikeSound;
    [SerializeField] private string destroySound;
    // Start is called before the first frame update
    public void Mining()
    {
        SoundManager.instance.PlaySE(strikeSound);
        var clone = Instantiate(effectPrefab, col.bounds.center, Quaternion.identity);
        Destroy(clone, destroyTime);
        hp--;
        if (hp <= 0) Destruction();
    }

    private void Destruction()
    {
        SoundManager.instance.PlaySE(destroySound);
        col.enabled = false;
        for (int i = 0; i < count; i++)
        {
            Instantiate(rockItemPrefab, rock.transform.position, Quaternion.identity);
        }
        Destroy(rock);
        debris.SetActive(true);
        Destroy(debris, destroyTime);
    }
}
