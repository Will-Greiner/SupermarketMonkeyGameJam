using UnityEngine;
using UnityEngine.UI;

public class SlowTower : MonoBehaviour
{
    public float slowrate;
    public GameObject emptySpace;
    public float slowIncrease;
    public float rangeIncrease;
    public float fireRateDecrease;
    public int tier = 1;
    public Button upgradeButton;
    public Button deleteButton;
    public Button upgradeInvalidButton;
    public Transform cameraRef;
    public GameManager gm;
    void Start()
    {
        gm = GameManager.Instance;
        slowrate = 3f;
        upgradeButton.onClick.AddListener(UpgradeListener);
        deleteButton.onClick.AddListener(DeleteListener);
    }
    void UpgradeListener()
    {
        if(tier == 1 && gm.purchasableItems[7])
        {
            gm.mango = gm.mango - 5;
            tier++;
            //setup range changer
            slowrate = slowrate-slowIncrease;
        }
        if(tier == 2 && gm.purchasableItems[8])
        {
            gm.watermelon = gm.watermelon - 4;
            tier++;
            //setup range changer
            slowrate = slowrate-slowIncrease;
        }
    }
    void DeleteListener(){
        Instantiate(emptySpace, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gm.purchasableItems[6] && tier == 1)
            {
                upgradeButton.gameObject.SetActive(true);
                upgradeInvalidButton.gameObject.SetActive(false);
            }
            else if (gm.purchasableItems[7] && tier == 2)
            {
                upgradeButton.gameObject.SetActive(true);
                upgradeInvalidButton.gameObject.SetActive(false);
            }
            else
            {
                upgradeButton.gameObject.SetActive(false);
                upgradeInvalidButton.gameObject.SetActive(true);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            upgradeButton.gameObject.SetActive(false);
            upgradeInvalidButton.gameObject.SetActive(false);
        }
    }
}
