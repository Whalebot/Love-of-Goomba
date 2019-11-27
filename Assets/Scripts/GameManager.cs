using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public Status GMBase;
    public int GMMoney;
    public static int money;
    public int GMIncome;
    public static int income;
    public float incomeTimer;
    public float lastIncome;
    public int GMMana;
    public static int mana;
    public float manaDelay;
    float lastMana;
    public Image hpBar;
    public TextMeshProUGUI text;
    public static int killCount;

    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI incomeText;
    [SerializeField] TextMeshProUGUI manaText;

    [SerializeField] float timeCounter;
    [SerializeField] int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        killCount = 0;
        money = GMMoney;
        income = GMIncome;
        mana = GMMana;
        lastIncome = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter = Time.time;
        if (GMBase != null)
        {
            text.text = GMBase.health + "/" + maxHealth;
            hpBar.fillAmount = (float)GMBase.health/ (float)maxHealth;
        }
        if (Time.time > incomeTimer + lastIncome)
        {
            lastIncome = Time.time;
            GiveIncome();
        }
        if (mana < 100) {
            if(Time.time > manaDelay + lastMana){
                mana++;
                lastMana = Time.time;
            }
        }



        if (moneyText != null) moneyText.text = money + "";
        if (incomeText != null) incomeText.text = income + "";
        if (manaText != null) manaText.text = mana + "";


        if (Input.GetKeyDown("r")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown("1")) {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown("2"))
        {
            SceneManager.LoadScene(1);
        }
    }

    public void GiveIncome()
    {
        money += income;
    }
}
