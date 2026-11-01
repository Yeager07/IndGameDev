using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Health : MonoBehaviour
{
    [SerializeField] public float hp = 100.0f;
    [SerializeField] public float hunger = 0.0f;
    [SerializeField] public float timer;
    public DataChangeValue changerValue;

    void Start()
    {
        StartCoroutine(HungerChangeCoroutine());
    }

    private IEnumerator HungerChangeCoroutine()
    {
        while(hunger != changerValue.maxHunger)
        {
            hunger = Mathf.Min(hunger + 2.0f, changerValue.maxHunger);
            yield return new WaitForSeconds(2.0f);
        }
    }

    public IEnumerator ToxicFoodDamageCoroutine()
    {
        while(timer != 10.0f)
        {
            timer += 1.0f;
            hp -= changerValue.toxicFoodDamage;
            yield return new WaitForSeconds(1.5f);
        }
        timer = 0.0f;
    }
}
