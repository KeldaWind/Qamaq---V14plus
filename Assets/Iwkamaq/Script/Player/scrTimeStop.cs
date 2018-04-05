using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrTimeStop : MonoBehaviour {

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) )
        {
            StartCoroutine(StopTime());
        }
    }

    public IEnumerator StopTime()
    {
        Debug.Log("stopTime");
        Time.timeScale = 0;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

}
