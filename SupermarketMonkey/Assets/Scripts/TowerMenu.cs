using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenu : MonoBehaviour
{
    public bool towerEmpty = true;
    public Button tower1;
    public Button tower2;
    public Button tower3;
    public GameObject emptySpace;
    public GameObject selectionUI;
    public GameObject prefabTower1;
    public GameObject prefabTower2;
    public GameObject prefabTower3;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        Instantiate(prefabTower1, transform.position, Quaternion.identity);
        emptySpace.SetActive(false);
        selectionUI.SetActive(false);
        towerEmpty = false;
    }
    void TowerTwoClick()
    {
        Instantiate(prefabTower2, transform.position, Quaternion.identity);
        emptySpace.SetActive(false);
        selectionUI.SetActive(false);
        towerEmpty = false;
    }
    void TowerThreeClick()
    {
        Instantiate(prefabTower3, transform.position, Quaternion.identity);
        emptySpace.SetActive(false);
        selectionUI.SetActive(false);
        towerEmpty = false;
    }
}
