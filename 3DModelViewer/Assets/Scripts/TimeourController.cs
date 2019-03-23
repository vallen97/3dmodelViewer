using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeourController : MonoBehaviour
{
    // variables
    [SerializeField]
    private int waitUntilIdle = 45;

    // Start is called before the first frame update
    void Start()
    {
        // start Coroutine when scene starts
        StartCoroutine("Timeout");
    }

    // Update is called once per frame
    void Update()
    {
        // if right click is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // stop current coroutine
            StopCoroutine("Timeout");
            // start new coroutine
            StartCoroutine("Timeout");
        }
    }

    IEnumerator Timeout()
    {
        // wait 
        yield return new WaitForSecondsRealtime(waitUntilIdle);
        // Switch to idle scene
        SceneManager.LoadScene("IdleScene");
        //stop coroutine 
        StopCoroutine("Timeout");
    }
}
