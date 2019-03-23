using UnityEngine;

public class ModelController : MonoBehaviour
{
    private GameObject go;
    private float lastPosX;
    private float lastPosY;

    private ButtonController bc;

    // Start is called before the first frame update
    void Start()
    {
        go = GameObject.Find("model");
        bc = GameObject.Find("ModelController").GetComponent<ButtonController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around its local X axis at 1 degree per second
        //go.transform.Rotate(Vector3.right * (Time.deltaTime * 5));
    }

    void OnMouseDown()
    {
        bc.CloseTextBox();
    }


    void OnMouseDrag()
    {
        float diffX;
        float diffY;

        diffX = lastPosX - Input.mousePosition.x;
        diffY = lastPosY - Input.mousePosition.y;

        go.transform.Rotate(Vector3.up, diffX);
        go.transform.Rotate(Vector3.right, diffY);


        lastPosX = Input.mousePosition.x;
        lastPosY = Input.mousePosition.y;
    }
}
