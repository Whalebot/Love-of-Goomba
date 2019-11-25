using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AIHealthBar : MonoBehaviour
{
    [SerializeField] Image healthBar;
    Status status;
    [SerializeField] int maxHealth;
    [SerializeField] TextMeshProUGUI text;
    private void Start()
    {
        status = GetComponentInParent<Status>();
        maxHealth = status.Health;
       // status.OnHealthChange += UpdateHealth;
    }

    private void Update()
    {
        if (status.health > 0)
            healthBar.fillAmount =  (float)status.health/(float)maxHealth;
        else healthBar.fillAmount = 0;

        if (text != null) {
            text.text = status.health + "/" + maxHealth;
        }

    }

    void UpdateHealth(int currentHealth) {
        if(currentHealth > 0)
        healthBar.fillAmount = maxHealth / currentHealth;
    }
}
