using UnityEngine;
using UnityEngine.InputSystem;

public class RecruitingTableButton : MonoBehaviour
{
    public GameObject Unit;
    private GameObject currentUnit;
    public Vector3 spawnPos;
    private RoundManager roundManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPos = Input.mousePosition;
        roundManager = GameObject.Find("Round Manager").GetComponent<RoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createUnit()
    {
        if(roundManager.UnitBeingPlaced == null && roundManager.isUnitBeingPlaced == false)
        {
            currentUnit = Instantiate(Unit, spawnPos, Unit.transform.rotation);
            roundManager.isUnitBeingPlaced = true;
        }
        
    }
}
