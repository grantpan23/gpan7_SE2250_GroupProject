﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;

    
    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitHearts()
    {
        for (int i = 0; i < heartContainers.initialValue; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    public void UpdateHearts()
    {
        float tempHealth = playerCurrentHealth.RuntimeValue;
        for (int i = 0; i < heartContainers.initialValue; i++)
        {
            if (i<=tempHealth-1)
            {
                // full heart
                hearts[i].sprite = fullHeart;
            }
            else
            {
                // empty health
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
