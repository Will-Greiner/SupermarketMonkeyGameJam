using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public string gameState = "Menu";
    public TMP_Text[] resourceCounts;    
    public static GameManager Instance;
    public int day = 1;
    public int mango = 0;
    public int pineapple = 0;
    public int banana = 0;
    public int coconut = 0;
    public int soup = 0;
    public int watermelon = 0;
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
        UpdateText();
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
        if(mango >=3 && watermelon >= 2)
        {
            purchasableItems[0] = true;
            if(mango >=5) {purchasableItems[1] = true;}
            if(watermelon >= 4){purchasableItems[2] = true;}
        }
        if(pineapple >= 4 && soup >= 1)
        {
            purchasableItems[3] = true;
        }
        if(pineapple >=1 && soup >= 3)
        {
            purchasableItems[4] = true;
        }
        if(pineapple >=5 && soup >= 4)
        {
            purchasableItems[5] = true;
        }
        if(banana >=2 && coconut >= 2)
        {
            purchasableItems[6] = true;
            if(banana >= 3 && coconut >= 4){purchasableItems[7] = true;}
            if(coconut >= 7){ purchasableItems[8] = true;}
        }
    }

}
