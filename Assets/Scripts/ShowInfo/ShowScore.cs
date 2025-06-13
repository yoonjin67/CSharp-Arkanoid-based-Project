using UnityEngine;
using TMPro;

public class ShowScore : MonoBehaviour
{
    public TextMeshProUGUI infoLabel; // UI label to show Point and Bonus
    public SharedIntVariable highscore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        infoLabel.text = $"Score: {highscore.value}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
