using UnityEngine;
using UnityEngine.SceneManagement;

public class IdleTimer : MonoBehaviour
{
    //ADD TO YOU GAMECONTROLLER OR ANY CONTROLLER OF YOUR SCENE
    //Set the timer time 
    public float timerValue = 45f;
    //Set the counter 
    public float timerCount;

    public int initScene;

    // Update is called once per frame
    void Update()
    {
        timerCount += Time.deltaTime;

        //TODO: test if the input touch works in the device that will be put on

        ////If any input is registered reset the timercouter
        //if (Input.touches > 1)
        //{
        //    timerCount = 0f;
        //}

        ////if the counter is the timervalue set than load the scene. 
        //if (timerCount => timerValue)
        //{
        //    SceneManager.LoadScene(initScene);
        //}
    }
}
