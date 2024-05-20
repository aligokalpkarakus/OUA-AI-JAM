using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeController : MonoBehaviour
{
    public int AILevel;
    public float speed;
    private GameObject games;

    public int speedCost = 2;
    public int AIcost = 2;
    public int destroyUp = 20;

    public static UpgradeController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // Destroy this instance because another instance already exists
            Destroy(gameObject);
        }
        else
        {
            // Set the instance to this one and mark it to not be destroyed on load
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {     
        AILevel = 1;
        speed = 1.0f;
        
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            games = GameObject.FindGameObjectWithTag("Games");

            if (AILevel == 1)
            {
                games.transform.GetChild(0).gameObject.SetActive(true);

            }
            else if (AILevel == 2)
            {
                games.transform.GetChild(1).gameObject.SetActive(true);

            }
            else if (AILevel == 3)
            {
                games.transform.GetChild(2).gameObject.SetActive(true);

            }
            else
            {
                Debug.Log("AI LEVEL HATA");
            }
        }      
    }

    public void SpeedUpgradeButton()
    {
        if(speedCost <= ScoreManager.scoreCount)
        {
            ScoreManager.scoreCount -= 2;
            Debug.Log("H�z artt�");
            speed += 5f;
            Debug.Log(speed);
        }
        else if (speedCost > ScoreManager.scoreCount)
        {

        }        
    }

    public void DestroyAIButton()
    {
        ScoreManager.scoreCount -= 20;
        
    }

    public void AIUpgradeButton()
    {
        ScoreManager.scoreCount -= 2;
        AILevel += 1; 
        Debug.Log("AI LEV:" + AILevel);
    }
}
