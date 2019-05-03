using UnityEngine;
using UnityEngine.UI;

public class ScreenResolutionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   // set screen resolution
        Screen.SetResolution(2736, 1824, true, 0);
    }
}
