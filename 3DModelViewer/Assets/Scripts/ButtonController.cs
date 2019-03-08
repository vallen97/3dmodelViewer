using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    // TODO: Auto Select the first button and disbale the button
    // TODO: When another model is selected set orientation back to 0,0

    // variables
    // buttons
    private Button btnModel1;
    private Button btnModel2;
    private Button btnModel3;
    private Button btnModelDesc;

    // text
    private Text txtModelDesc;

    // string
    private string activeModel = "NO ACTIVE MODEL";

    // string array
    // TODO: Fill with model information 
    private string[] modelInfo = {"What is Lorem Ipsum? Lorem Ipsum is simply dummy text of the printing and typesetting industry.Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
            "Where does it come from?Contrary to popular belief, Lorem Ipsum is not simply random text.It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old.Richard McClintock, a Latin professor at Hampden - Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source.Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum(The Extremes of Good and Evil) by Cicero, written in 45 BC.This book is a treatise on the theory of ethics, very popular during the Renaissance.The first line of Lorem Ipsum, Lorem ipsum dolor sit amet., comes from a line in section 1.10.32.",
            "Where can I get some? There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc."};

    // String 
    private string[] modelNames = { "model/vase", "model/tulipany", "model/bowl" };

    // list
    private List<GameObject> models;

    // gameObject for model
    private GameObject go; 

    // Start is called before the first frame update
    void Start()
    {
        go = GameObject.Find("model");

        // instanciate list
        models = new List<GameObject>();

        // loop to tind and hide all models
        for (int i = 0; i < modelNames.Length; i++)
        {
            models.Add(GameObject.Find(modelNames[i]));
            models[i].SetActive(false);
        }

        // Find button
        btnModel1 = GameObject.Find("btnModel1").GetComponent<Button>();
        btnModel2 = GameObject.Find("btnModel2").GetComponent<Button>();
        btnModel3 = GameObject.Find("btnModel3").GetComponent<Button>();

        btnModelDesc = GameObject.Find("btnModelDesc").GetComponent<Button>();
        txtModelDesc = GameObject.Find("txtModelDesc").GetComponent<Text>();

        // add listners
        btnModel1.onClick.AddListener(() => ModelChange(1));
        btnModel2.onClick.AddListener(() => ModelChange(2));
        btnModel3.onClick.AddListener(() => ModelChange(3));

        btnModelDesc.onClick.AddListener(DisplayModelDesc);

        // hide text
        txtModelDesc.gameObject.SetActive(false);

        // Set model 1 to show when project shows
        ModelChange(1);
    }

    private void ModelChange(int modelNumber)
    {
        // set name for active model for setting text
        activeModel = "Model Button: " + modelNumber;

        // find which button was pressed
        if (modelNumber == 1)
        {
            StartCoroutine("animateBtnTxtHide");

            // Disbale button and Enable other buttons
            btnModel1.interactable = false;
            btnModel2.interactable = true;
            btnModel3.interactable = true;

            // set rotation back to  0,0,0
            go.transform.rotation = Quaternion.identity;

            // show and hide models
            models[0].gameObject.SetActive(true);
            models[1].gameObject.SetActive(false);
            models[2].gameObject.SetActive(false);

        }
        else if (modelNumber == 2)
        {
            StartCoroutine("animateBtnTxtHide");

            // Disbale button and Enable other buttons
            btnModel1.interactable = true;
            btnModel2.interactable = false;
            btnModel3.interactable = true;

            // set rotation back to  0,0,0
            go.transform.rotation = Quaternion.identity;

            // show and hide models
            models[1].gameObject.SetActive(true);
            models[2].gameObject.SetActive(false);
            models[0].gameObject.SetActive(false);
        }
        else if (modelNumber == 3)
        {
            StartCoroutine("animateBtnTxtHide");
            // Disbale button and Enable other buttons
            btnModel1.interactable = true;
            btnModel2.interactable = true;
            btnModel3.interactable = false;

            // set rotation back to  0,0,0
            go.transform.rotation = Quaternion.identity;

            // show and hide models
            models[2].gameObject.SetActive(true);
            models[1].gameObject.SetActive(false);
            models[0].gameObject.SetActive(false);
        }

    }

    private void DisplayModelDesc()
    {
        // check if text is showing
        if (txtModelDesc.gameObject.activeSelf == false)
        {
            StartCoroutine("animateBtnTxtShow");
        }
        else
        {
            StartCoroutine("animateBtnTxtHide");
        }


        // Find which button pressed and asign model text
        if (activeModel == "Model Button: 1")
        {
            txtModelDesc.text = modelInfo[0];
        }
        else if (activeModel == "Model Button: 2")
        {
            txtModelDesc.text = modelInfo[1];
        }
        else if (activeModel == "Model Button: 3")
        {
            txtModelDesc.text = modelInfo[2];
        }

    }


    IEnumerator animateBtnTxtShow()
    {
        // show text
        txtModelDesc.gameObject.SetActive(true);
        // I set up povit points and this is used to animate the button and text 
        while (btnModelDesc.GetComponent<RectTransform>().anchoredPosition.x > 0)
        {
            // move left every .01 seconds
            btnModelDesc.transform.Translate(Vector3.left * (Time.deltaTime * 300));
            txtModelDesc.transform.Translate(Vector3.left * (Time.deltaTime * 300));
            yield return new WaitForSecondsRealtime(.01f);
        }

        //stop coroutine 
        StopCoroutine("animateBtnTxtShow");
    }

    IEnumerator animateBtnTxtHide()
    {
        // I set up povit points and this is used to animate the button and text 
        while (btnModelDesc.GetComponent<RectTransform>().anchoredPosition.x < 360)
        {
            // move right every .01 seconds
            btnModelDesc.transform.Translate(Vector3.right * (Time.deltaTime * 300));
            txtModelDesc.transform.Translate(Vector3.right * (Time.deltaTime * 300));
            yield return new WaitForSecondsRealtime(.01f);
        }

        // hide text
        txtModelDesc.gameObject.SetActive(false);

        //stop coroutine 
        StopCoroutine("animateBtnTxtHide");
    }
}
