using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int level;

    public int currHealth;
    public int maxHealth;

    public int currMana;
    public int maxMana;

    public int currExp;
    public int maxExp;

    public int speed;

    public Slider healthBar;
    public Slider manaBar;
    public Slider expBar;

    public TextMeshProUGUI healthSliderDisplay;
    public TextMeshProUGUI manaSliderDisplay;
    public TextMeshProUGUI expSliderDisplay;
    public TextMeshProUGUI waveCountText;

    void Update() {
        ChangeSliderUI();

        if (currExp >= maxExp) {
            level += 1;
            //levelUpIcon.gameObject.SetActive(true);
            currExp = 0;
            maxExp += 50;
        }

        
    }
    


    public void ChangeSliderUI() {
        healthBar.value = currHealth;
        manaBar.value = currMana;
        expBar.value = currExp;

        healthBar.maxValue = maxHealth;
        manaBar.maxValue = maxMana;
        expBar.maxValue = maxExp;

        healthSliderDisplay.text = currHealth + " / " + maxHealth;
        manaSliderDisplay.text = currMana + " / " + maxMana;
        expSliderDisplay.text = currExp + " / " + maxExp;
        waveCountText.text = "level: " + level;
    }
}
