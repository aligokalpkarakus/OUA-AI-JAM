using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    UpgradeController controller;

    public GameObject easyGame;
    public GameObject normalGame;
    public GameObject hardGame;

    private void Start()
    {
        controller = GetComponent<UpgradeController>();

        if (controller.AILevel == 1)
        {
            easyGame.SetActive(true);
            Debug.Log("Easy");
        }
        else if (controller.AILevel == 2)
        {
            normalGame.SetActive(true);
            Debug.Log("Normal");
        }
        else if (controller.AILevel == 3)
        {
            hardGame.SetActive(true);
            Debug.Log("Hard");
        }
        else
        {
            Debug.Log("Error");
        }

    }
}
