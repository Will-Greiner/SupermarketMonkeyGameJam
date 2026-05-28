using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public Button goButton;
    public Button continueButton;
    public Button continueTDButton;
    public GameObject mainMenu;
    public GameObject creditsMenu;
    public GameObject towerBuildingMenu;
    public GameObject costsMenu;
    public GameObject towerGameplayMenu;
    public GameObject shoppingResults;
    public GameObject shoppingMenu;
    public GameObject towerResultsMenu;

    public CameraFollow cameraPosition;
    public ShoppingCartController cart;
    public Image timerPie;
    public TMP_Text countdown;
    public GameObject background;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        play.onClick.AddListener(PlayListener);
        credits.onClick.AddListener(CreditsListener);
        back.onClick.AddListener(BackListener);
        costs.onClick.AddListener(CostsListener);
        startWave.onClick.AddListener(StartListener);
        costsBack.onClick.AddListener(CostsBackListener);
        goButton.onClick.AddListener(GoListener);
        continueButton.onClick.AddListener(ContinueListener);
        continueTDButton.onClick.AddListener(ContinuetoStoreListener);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void PlayListener()
    {
        mainMenu.SetActive(false);
        shoppingMenu.SetActive(true);
        gm.gameState = "Shopping";
        gm.playerCart = true;
        cameraPosition.onCart = true;
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
    void GoListener()
    { 
        goButton.gameObject.SetActive(false);
        gm.StartTimer();
    }
    void ContinueListener()
    {
        shoppingResults.SetActive(false);
        cart.currentCart = 0;
        cart.capacity.text = "0/" + cart.cartCapacity;
        goButton.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(false);
        gm.gameState = "Building";
        gm.playerCart = false;
        cameraPosition.onCart = false;
        timerPie.gameObject.SetActive(true);
        timerPie.fillAmount = 1;
        countdown.text = "20";
        countdown.gameObject.SetActive(true);
        towerBuildingMenu.SetActive(true);
        cart.ResetPosition();
        shoppingMenu.SetActive(false);
    }
    void ContinuetoStoreListener()
    {
        towerResultsMenu.SetActive(false);
        towerGameplayMenu.SetActive(false);
        gm.gameState = "Shopping";
        gm.playerCart = true;
        cameraPosition.onCart = true;
        shoppingMenu.SetActive(true);
        cart.ResetPosition();
    }
}
