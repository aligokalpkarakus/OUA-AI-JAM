using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    public GameObject games;

    // Update is called once per frame
    void Update()
    {
        if(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }else if(remainingTime < 0)
        {         
            remainingTime = 0;
            timerText.color = Color.red;
            ResetScenes();
            SceneManager.LoadScene(2);
           
        }

        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("Time: {0:00}:{1:00}",minutes,seconds);
    }

    void ResetScenes()
    {
        for(int i = 0; i < 3; i++)
        {
            games.transform.GetChild(i).gameObject.SetActive(false);    
        }
    }

}
