using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    [SerializeField] private Slider _slider;

    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _slider.maxValue = _player.GetComponent<PlayerMain>().GetMaxHealth();
        _slider.value = _player.GetComponent<PlayerMain>().health;

        Debug.Log("Start hp set");

        _player.GetComponent<PlayerMain>().onHealthChangedCallback += SetHealth;
    }

    public void SetHealth(int health)
    {
        Debug.Log($"hp set {health}");

        if (health > 0)
            _slider.value = health;
        else
            _slider.value = 0;
    }
}
