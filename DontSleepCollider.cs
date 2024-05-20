using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontSleepCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("UI"))
        {
            
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true); //game objectin transformunun birinci child'ý yani canvasý enabled yap.
                                                                                //Debug.Log("UI ACTIVATED");
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("UI")) //Hotpoint ile etkileþimden çýkarsa.
        {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false); //game objectin transformunun birinci child'ý yani canvasý disabled yap.
            //Debug.Log("UI DEACTIVATED");
        }
    }
}
