using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public int curValue;
    public int maxValue;
    public GameObject HeartPrefab;

    private Image[] img;
    private int pivot = 0;
    private void Awake()
    {
        for (int i = 0; i < maxValue; i++)
        {
            Instantiate(HeartPrefab, this.transform);
        }
        Image[] img = transform.GetComponentsInChildren<Image>();
    }

    private void Update()
    {

    }

    public void Add(int amount)
    {
        curValue = Math.Min(curValue + amount, maxValue);
        img[curValue].color = Color.red;
    }

    public void Subtract(int amount)
    {
        curValue = Math.Max(curValue - amount, 0);
        img[curValue].color = Color.black;
    }

}
