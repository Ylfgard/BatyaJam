using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Slider sliderLeft;
    [SerializeField] private Slider sliderRight;

    private ArchdemonStats bossStats;
    private float _startHealth;
    private float _health;

    private void Awake()
    {
        bossStats = GameObject.FindGameObjectWithTag("Boss").GetComponent<ArchdemonStats>();

        bossStats.onBossActiveCallback += SetSliders;
        bossStats.onBossHealthChangedCallback += SetHealth;

        bossStats.gameObject.SetActive(false);
    }

    public void SetSliders()
    {
        if (bossStats.gameObject == null) return;
        StartCoroutine(SetSlidersCoroutine());
    }

    IEnumerator SetSlidersCoroutine()
    {
        yield return new WaitForEndOfFrame();

        _startHealth = bossStats.startHealth;
        _health = bossStats.health;

        sliderLeft.gameObject.SetActive(true);
        sliderRight.gameObject.SetActive(true);
        Debug.Log("Sliders active.");

        sliderLeft.maxValue = _startHealth;
        sliderRight.maxValue = _startHealth;
        sliderLeft.value = _health;
        sliderRight.value = _health;

        Debug.Log("Slider left: " + sliderLeft.value + "; Slider right: " + sliderRight.value);
    }

    public void SetHealth(int health)
    {
        if (health > 0)
        {
            sliderLeft.value = health;
            sliderRight.value = health;
            Debug.Log("Slider left: " + sliderLeft.value + "; Slider right: " + sliderRight.value);
        }
        else
        {
            sliderLeft.value = 0;
            sliderRight.value = 0;
            sliderLeft.gameObject.SetActive(false);
            sliderRight.gameObject.SetActive(false);
        }
    }
}
