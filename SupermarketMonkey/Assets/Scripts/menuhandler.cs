using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class menuhandler : MonoBehaviour
{
    public GameManager gm;
    public string gameState;
    public GameObject spawner;
    public Button play;
    public Button credits;
    public Button back;
    public Button costs;
    public Button startWave;
    public Button costsBack;
    public GameObject mainMenu;
    public GameObject creditsMenu;
    public GameObject towerBuildingMenu;
    public GameObject costsMenu;
    public GameObject towerGameplayMenu;

    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        play.onClick.AddListener(PlayListener);
        credits.onClick.AddListener(CreditsListener);
        back.onClick.AddListener(BackListener);
        costs.onClick.AddListener(CostsListener);
        startWave.onClick.AddListener(StartListener);
        costsBack.onClick.AddListener(CostsBackListener);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void PlayListener()
    {
        mainMenu.SetActive(false);
        towerBuildingMenu.SetActive(true);
        gm.gameState = "Building";
    }
    void CreditsListener()
    {
        gm.gameState = "Menu";
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }
    void BackListener()
    {
        gm.gameState = "Menu";
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    void CostsListener()
    {
        gm.gameState = "Building";
        costsMenu.SetActive(true);
    }
    void StartListener()
    {
        gm.gameState = "TD";
        towerBuildingMenu.SetActive(false);
        costsMenu.SetActive(false);
        towerGameplayMenu.SetActive(true);
        spawner.GetComponentInChildren<ChimpSpawner>().spawnChimps = true;
    }
    void CostsBackListener()
    {
        gm.gameState = "Menu";
        costsMenu.SetActive(false);
    }
}
