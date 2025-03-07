using UnityEngine;
using TMPro;

public class RoundUI : MonoBehaviour
{
    [Header ("Text Elements")]
    public TextMeshProUGUI roundCounter;
    public TextMeshProUGUI healthCounter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateRoundCounter(int round)
    {
        roundCounter.text = "Round " + round;
    }

    public void updateHealthCounter(int health)
    {
        healthCounter.text = "Health " + health;
    }
    
}
