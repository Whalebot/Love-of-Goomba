using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AIHealthBar : MonoBehaviour
{
    [SerializeField] bool GMBase;
    [SerializeField] Image healthBar;
    GameObject barBackground;
    Status status;
    [SerializeField] int maxHealth;
    [SerializeField] TextMeshProUGUI text;
    BillboardCanvas billboard;

    private void Start()
    {

        status = GetComponentInParent<Status>();
        maxHealth = status.Health;
        barBackground = healthBar.transform.parent.gameObject;
        billboard = GetComponent<BillboardCanvas>();
       // status.OnHealthChange += UpdateHealth;
    }

    private void Update()
    {
        if (status.health == maxHealth && !GMBase && billboard.owner == BillboardCanvas.Owner.Player)
        {
            barBackground.SetActive(false);
        }
        else barBackground.SetActive(true);

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
