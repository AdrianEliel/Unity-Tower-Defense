using UnityEngine;

public abstract class GeneralUnit : MonoBehaviour
{
    [Header("Script Refs")]
    protected UnitData data;

    [Header("Targeting variables")]
    public bool isInRange;

    [Header("Children")]
    Transform childFound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            isInRange = true;
            transform.LookAt(other.transform.position);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            isInRange = false;
        }
    }

    protected Transform CustomFindChild(string key, Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.name == key)
            {
                childFound = child;
            }

            else
            {
                if (child.childCount > 0)
                {
                    if (childFound == null)
                    {
                        CustomFindChild(key, child);
                    }
                }
            }
        }

        return childFound;
    }

    public abstract void Shoot();
}
