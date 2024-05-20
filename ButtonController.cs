using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    private ScoreManager scoreManager;

    public int speedCost = 2;
    public int AIcost = 2;
    public int destroyUp = 20;

    private UpgradeController upgradeController;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public void HomeSceneButton()
    {
        ScoreManager.scoreCount = 0;
        SceneManager.LoadScene(1);
    }

    public  static void AISceneButton()
    {

        SceneManager.LoadScene(3);
        
    }

    public void SpeedUpgradeButton()
    {
        upgradeController = FindAnyObjectByType<UpgradeController>();

        if (speedCost <= ScoreManager.scoreCount)
        {
            ScoreManager.scoreCount -= 2;
            Debug.Log("Hýz arttý");
            upgradeController.speed += 5f;
            Debug.Log(upgradeController.speed);
        }
        else if (speedCost > ScoreManager.scoreCount)
        {

        }

    }

    public void DestroyAIButton()
    {
        ScoreManager.scoreCount -= 20;
        SceneManager.LoadScene(5);
        
    }

    public void AIUpgradeButton()
    {
        upgradeController = FindAnyObjectByType<UpgradeController>();
        ScoreManager.scoreCount -= 2;
        upgradeController.AILevel += 1;
        Debug.Log("AI LEV:" + upgradeController.AILevel);
    }

}
