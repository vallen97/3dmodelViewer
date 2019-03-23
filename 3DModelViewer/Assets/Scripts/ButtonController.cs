using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    // TODO:
    // Page numbers and arrows on info box 
    // multiple sets of info per data 
    // 8 thumb nails put along top -- Completed
    // change info box location  -- Completeed
    // new scene attract any model that rotates -- Completed
    // if info is open and model is clicked close info box -- Completed
    // ask Mireya to make a timeout. -- Completed

    // variables
    // button
    private Button btnModelDesc;
    private Button btnTextF;
    private Button btnTextB;

    // text
    private Text txtModelDesc;

    // int
    private int activeModel = 100;

    // string array
    private string[] modelInfo = {"What is Lorem Ipsum? Lorem Ipsum is simply dummy text of the printing and typesetting industry.Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
            "Where does it come from?Contrary to popular belief, Lorem Ipsum is not simply random text.It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old.Richard McClintock, a Latin professor at Hampden - Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source.Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum(The Extremes of Good and Evil) by Cicero, written in 45 BC.This book is a treatise on the theory of ethics, very popular during the Renaissance.The first line of Lorem Ipsum, Lorem ipsum dolor sit amet., comes from a line in section 1.10. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam sed lacus eget mauris varius fringilla. Ut id massa efficitur, accumsan quam eu, lobortis turpis. Nulla sollicitudin risus non felis euismod ultricies. Sed vitae libero at libero laoreet tincidunt. Nam eu ultricies arcu. Cras nec nunc risus. Donec nec sodales enim. Nunc imperdiet, quam quis feugiat semper, elit ligula imperdiet velit, vel tincidunt purus turpis vitae enim. In placerat rutrum diam nec ultrices. Nam eu placerat purus, at sollicitudin risus. Curabitur viverra augue sed diam interdum, interdum imperdiet ipsum interdum. Quisque sapien sem, eleifend at quam nec, auctor sodales lorem.32.",
            "Where can I get some? There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc."};

    // TODO: another string that contains portions of the string.
    private string[] TESTMODEL = {"Where does it come from?Contrary to popular belief, Lorem Ipsum is not simply random text.It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old.Richard McClintock, a Latin professor at Hampden - Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in",
                                 "classical literature, discovered the undoubtable source.Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum(The Extremes of Good and Evil) by Cicero, written in 45 BC.This book is a treatise on the theory of ethics, very popular during the Renaissance.The first line of Lorem Ipsum, Lorem ipsum dolor sit amet., comes from a line in section 1.10. Lorem ipsum dolor",
                                 "sit amet, consectetur adipiscing elit. Etiam sed lacus eget mauris varius fringilla. Ut id massa efficitur, accumsan quam eu, lobortis turpis. Nulla sollicitudin risus non felis euismod ultricies. Sed vitae libero at libero laoreet tincidunt. Nam eu ultricies arcu. Cras nec nunc risus. Donec nec sodales enim. Nunc imperdiet, quam quis feugiat semper, elit ligula imperdiet velit, vel tincidunt",
                                 "purus turpis vitae enim. In placerat rutrum diam nec ultrices. Nam eu placerat purus, at sollicitudin risus. Curabitur viverra augue sed diam interdum, interdum imperdiet ipsum interdum. Quisque sapien sem, eleifend at quam nec, auctor sodales lorem.32"};


    // String 
    private string[] modelNames = { "model/vase", "model/tulipany", "model/bowl" };

    // list
    private List<GameObject> models;
    private List<Button> modelButtons;

    // gameObject for model
    private GameObject go;

    // Start is called before the first frame update
    void Start()
    {
        go = GameObject.Find("model");

        // instanciate list
        models = new List<GameObject>();
        modelButtons = new List<Button>();

        // loop to tind and hide all models
        for (int i = 0; i < modelNames.Length; i++)
        {
            models.Add(GameObject.Find(modelNames[i]));
            models[i].SetActive(false);
        }

        // Find buttons
        for (int i = 1; i < 9; i++)
        {
            modelButtons.Add(GameObject.Find("btnModel" + i).GetComponent<Button>());
        }
        btnModelDesc = GameObject.Find("btnModelDesc").GetComponent<Button>();
        btnTextF = GameObject.Find("btnTextF").GetComponent<Button>();
        btnTextB = GameObject.Find("btnTextB").GetComponent<Button>();

        // find text box
        txtModelDesc = GameObject.Find("txtModelDesc").GetComponent<Text>();

        // add listners  
        // model button
        modelButtons[0].onClick.AddListener(() => ModelChange(0));
        modelButtons[1].onClick.AddListener(() => ModelChange(1));
        modelButtons[2].onClick.AddListener(() => ModelChange(2));
        // NOTE: Currently does not have any information
        //modelButtons[3].onClick.AddListener(() => ModelChange(3));
        //modelButtons[4].onClick.AddListener(() => ModelChange(4));
        //modelButtons[5].onClick.AddListener(() => ModelChange(5));
        //modelButtons[6].onClick.AddListener(() => ModelChange(6));
        //modelButtons[7].onClick.AddListener(() => ModelChange(7));

        // info button
        btnModelDesc.onClick.AddListener(DisplayModelDesc);

        // page button
        btnTextF.onClick.AddListener(() => ChangeText(1));
        btnTextB.onClick.AddListener(() => ChangeText(0));

        // hide text
        txtModelDesc.gameObject.SetActive(false);

        // Set model 1 to show when project shows
        ModelChange(1);
    }

    // changes model from onclickllistner from button
    private void ModelChange(int modelNumber)
    {
        // set name for active model for setting text
        activeModel = modelNumber;

        // set rotation back to  0,0,0
        go.transform.rotation = Quaternion.identity;

        // loop through models and modelButton lists
        for (int i = 0; i < models.Count; i++)
        {
            // set every button and model 
            modelButtons[i].interactable = true;
            models[i].gameObject.SetActive(false);

            // change only the current model and button selected
            if (i == modelNumber)
            {
                modelButtons[i].interactable = false;
                models[i].gameObject.SetActive(true);
            }
        }
        StartCoroutine("animateBtnTxtHide");
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

        // which button was pressed and asign model text
        txtModelDesc.text = modelInfo[activeModel];
    }

    public void CloseTextBox()
    {
        StartCoroutine("animateBtnTxtHide");
    }

    private void ChangeText(int dir)
    {
        if (dir == 1)
        {
            // TODO: Move Text forward and disable buttons if all the way to the right
        }
        else if (dir == 0)
        {
            // TODO: Move Text Back and disable button if to most left
        }
    }


    IEnumerator animateBtnTxtShow()
    {
        // show text
        txtModelDesc.gameObject.SetActive(true);
        // I set up povit points and this is used to animate the button and text 
        while (btnModelDesc.GetComponent<RectTransform>().anchoredPosition.x > -360)
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
