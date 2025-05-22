using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public int curValue;
    public int maxValue;
    public GameObject Prefab;

    public Image[] imgs;
    private int pivot = 0;
    private void Awake()
    {
        for (int i = 0; i < maxValue; i++)
        {
            Instantiate(Prefab, this.transform);
        }
        imgs = transform.GetComponentsInChildren<Image>(includeInactive: false);

    }

    public void Add(int amount)
    {
        curValue = Math.Min(curValue + amount, maxValue);
        imgs[curValue].color = Prefab.GetComponent<Image>().color;
    }

    public void Subtract()
    {
        if (curValue < 0) return;
        imgs[curValue].color = Color.black;
        curValue--;
    }

}
