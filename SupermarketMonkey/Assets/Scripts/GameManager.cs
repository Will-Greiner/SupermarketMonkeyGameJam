using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;

public class GameManager : MonoBehaviour
{
    public float roundTime = 20f;
    private float timer;
    private bool isRunning;

    public ShoppingCartController cart;
    public Image timerPie;
    public bool playerCart;
    public string gameState = "Menu";
    public TMP_Text[] resourceCounts; 
    public TMP_Text countdown;   
    public static GameManager Instance;
    public int day = 1;
    public int mango = 0;
    public int pineapple = 0;
    public int banana = 0;
    public int coconut = 0;
    public int soup = 0;
    public int watermelon = 0;
    public GameObject continueButton;
    //first 3 are for single towers, second 3 for burst towers, third 3 for slows.
    //first is first tower, second is first upgrade, third is last upgrade.
    public List<bool> purchasableItems = new List<bool>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }
    public bool HasResources(int mangoCost, int pineappleCost, int bananaCost, int coconutCost, int soupCost, int watermelonCost)
    {
        return mango >= mangoCost &&
               pineapple >= pineappleCost &&
               banana >= bananaCost &&
               coconut >= coconutCost &&
               soup >= soupCost &&
               watermelon >= watermelonCost;
    }

    public void SpendResources(int mangoCost, int pineappleCost , int bananaCost, int coconutCost, int soupCost, int watermelonCost)
    {
        mango -= mangoCost;
        pineapple -= pineappleCost;
        banana -= bananaCost;
        coconut -= coconutCost;
        soup -= soupCost;
        watermelon -= watermelonCost;
    }
    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            purchasableItems.Add(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckCosts();
        UpdateText();

        if (isRunning)
        {
            timer -= Time.deltaTime;

            UpdateTimerUI();

            if (timer <= 0f)
            {
                timer = 0f;
                isRunning = false;
                OnTimerFinished();
            }
        }
    }
    public void UpdateText()
    {
        resourceCounts[0].text = mango.ToString();
        resourceCounts[1].text = pineapple.ToString();
        resourceCounts[2].text = banana.ToString();
        resourceCounts[3].text = coconut.ToString();
        resourceCounts[4].text = soup.ToString();
        resourceCounts[5].text = watermelon.ToString();
    }
    public void CheckCosts()
    {
        //single shot
        purchasableItems[0] = pineapple >= 4 && soup >= 1;
        purchasableItems[1] = pineapple >= 1 && soup >= 3;
        purchasableItems[2] = pineapple >= 5 && soup >= 4;

        //spread shot
        purchasableItems[3] = banana >= 2 && coconut >= 2;
        purchasableItems[4] = banana >= 3 && coconut >= 4;
        purchasableItems[5] = coconut >= 7;

        //slow shot
        purchasableItems[6] = mango >= 3 && watermelon >= 2;
        purchasableItems[7] = mango >= 5;
        purchasableItems[8] = watermelon >= 4;
    }

    public void StartTimer()
    {
        timer = roundTime;
        isRunning = true;
        UpdateTimerUI();
        cart.canMove = true;
        Debug.Log("Move dammit");
    }
    void UpdateTimerUI()
    {
        if (timerPie != null)
        {
            timerPie.fillAmount = timer / roundTime;
        }
        if (countdown != null)
        {
            int seconds = Mathf.FloorToInt(timer % 20f);
            countdown.text = string.Format(seconds.ToString());
        }
    }
    void OnTimerFinished()
    {
        for(int i = 0; i < 6; i++)
        {
            switch (i)
            {
                case 0:
                    mango = mango + cart.cartContents[0];
                    break;
                case 1:
                    pineapple = pineapple + cart.cartContents[1];
                    break;
                case 2:
                    banana = banana + cart.cartContents[2];
                    break;
                case 3:
                    coconut = coconut + cart.cartContents[3];
                    break;
                case 4:
                    soup = soup + cart.cartContents[4];
                    break;
                case 5:
                    watermelon = watermelon + cart.cartContents[5];
                    break;
            }
            cart.cartContents[i] = 0;
        }
        cart.canMove = false;
        timerPie.gameObject.SetActive(false);
        countdown.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(true);
        roundTime = 20f;
    }
}
