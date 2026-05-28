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
    public Transform cameraRef;
    void Start()
    {
        slowrate = 3f;
        upgradeButton.onClick.AddListener(UpgradeListener);
        deleteButton.onClick.AddListener(DeleteListener);
    }
    void UpgradeListener()
    {
        if(tier < 3)
        {
            tier++;
            //setup range changer
            slowrate = slowrate-slowIncrease;
        }
    }
    void DeleteListener(){
        Instantiate(emptySpace, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
