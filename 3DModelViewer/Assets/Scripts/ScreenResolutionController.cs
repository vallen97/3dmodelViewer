using UnityEngine;
using UnityEngine.UI;

public class ScreenResolutionController : MonoBehaviour
{
    private Text txtInstruc;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(2736, 1824, true, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
