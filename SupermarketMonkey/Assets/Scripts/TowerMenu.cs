using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenu : MonoBehaviour
{
    [System.Serializable]
    public class TowerCost
    {
        public int mango;
        public int pineapple;
    }
    public TowerCost tower1Cost;
    public TowerCost tower2Cost;
    public TowerCost tower3Cost;

    public bool towerEmpty = true;
    public Button tower1;
    public Button tower2;
    public Button tower3;
    public GameObject emptySpace;
    public GameObject selectionUI;
    public GameObject prefabTower1;
    public GameObject prefabTower2;
    public GameObject prefabTower3;
    public Transform cameraRef;
    public GameObject[] validTowerOptions;
    public GameManager gm;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = GameManager.Instance;
        tower1.onClick.AddListener(TowerOneClick);
        tower2.onClick.AddListener(TowerTwoClick);
        tower3.onClick.AddListener(TowerThreeClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void TowerOneClick()
    {
        gm.pineapple = gm.pineapple - 4;
        gm.soup = gm.soup - 1;
        Instantiate(prefabTower1, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    void TowerTwoClick()
    {
        gm.banana = gm.banana - 2;
        gm.coconut = gm.coconut - 2;
        
        Instantiate(prefabTower2, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    void TowerThreeClick()
    {
        gm.mango = gm.mango-3;
        gm.watermelon = gm.watermelon+3;
        Instantiate(prefabTower3, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
