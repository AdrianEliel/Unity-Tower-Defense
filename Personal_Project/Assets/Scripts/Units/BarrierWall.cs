using UnityEngine;

public class BarrierWall : MonoBehaviour
{
    [Header("Script Refs")]
    public UnitData data;
    public RoundManager roundManager;

    [Header("Self Stats")]
    public Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roundManager = GameObject.Find("Round Manager").GetComponent<RoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + 90, transform.rotation.z);
        }
    }
}
