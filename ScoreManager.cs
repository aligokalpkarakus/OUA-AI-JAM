using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI AILevelText;
    public TextMeshProUGUI speedText;
    public static int scoreCount = 100;

    private UpgradeController controller;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + scoreCount;
        
        controller = FindAnyObjectByType<UpgradeController>();

        AILevelText.text = "AI Level: " + controller.AILevel;
        speedText.text = "Speed: " + controller.speed;

    }
}
