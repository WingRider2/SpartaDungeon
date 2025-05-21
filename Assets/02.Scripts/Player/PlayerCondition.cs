using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;
    Condition health { get { return uiCondition.health; } }
    //Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerHealthDecay;

    public event Action onTakeDamage;

    private void Update()
    {
        if (health.curValue <= 0f)
        {
            Die();
        }
    }
    public void hit(int amount)
    {
        health.Subtract();
    }
    public void Heal(int amount)
    {
        health.Add(amount);
    }

    public void Die()
    {
        Debug.Log("플레이어가 죽었다.");
    }
    public bool UseStamina(int amount)
    {
        if (stamina.curValue - amount < 0)
        {
            return false;
        }
        stamina.Subtract();
        return true;
    }
    public void TakePhysicalDamage(int damage)
    {
        health.Subtract();
        onTakeDamage?.Invoke();
    }
}
