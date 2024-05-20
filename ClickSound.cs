using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public AudioSource clickSound;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            clickSound.enabled = true;
        }
        else
        {
            clickSound.enabled = false;
        }
    }
}
