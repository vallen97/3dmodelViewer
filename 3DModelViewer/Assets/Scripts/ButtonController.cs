using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    // variables
    // image
    private Image imgTxtBackground;

    // sprite
    // change between the images
    [SerializeField]
    private Sprite sprShowDesc;
    [SerializeField]
    private Sprite sprHideDesc;


    // button
    private Button btnModelDesc;
    private Button btnTextF;
    private Button btnTextB;
    private Button btnCredits;

    // text
    private Text txtModelTitle;
    private Text txtModelLocat;
    private Text txtModelDesc;   // model description text
    private Text txtPageNum;     // page number

    // int
    private int activeModel = 100;  // changed when new models are selected 
    private int infoPos = 0;        // current page
    private int moveText = 10;      // speed when showing model description
    private int defaultZoom = 5;    // camera zoom currrently 5

    // float
    // NOTE: in seconds
    private float speed = .01625f; // how frequent to move text 

    // string 
    // model names model gameobject and model
    private string[] modelNames = { "model/21370.11", "model/21393.11", "model/Axe Head", "model/LateGlazeBowl Touchup", "model/pot 2 blender", "model/Pot1Blender_", "model/Pot2Blender_" };
    // credits
    private static string credits = "Frank Naranjo,\nVaughn Allen,\nMireya Rodriguez,\nBecca Sharp";

    // list
    private List<GameObject> models;    // list of allmodels
    private List<GameObject> buttons;   // create buttons for model buttons
    private List<Button> modelButtons;  // list of all model buttons
    private List<List<string[]>> modelDesc;   // list of all model descriptions
    private List<Sprite> buttonImages;  // sprites for button images

    // gameObject
    private GameObject go;              // model gameobject
    [SerializeField]
    private GameObject buttonPrefab;    // button prefab for models

    // sprite
    // for button models
    [SerializeField]
    private Sprite btn1;
    [SerializeField]
    private Sprite btn2;
    [SerializeField]
    private Sprite btn3;
    [SerializeField]
    private Sprite btn4;
    [SerializeField]
    private Sprite btn5;
    [SerializeField]
    private Sprite btn6;
    [SerializeField]
    private Sprite btn7;
    [SerializeField]
    private Sprite btn8;

    // variable to check if button is pressed and model description  wasn't open
    private bool isCredits = false;


    // Start is called before the first frame update
    void Start()
    {
        // find model gameobject
        go = GameObject.Find("model");

        // instanciate list
        models = new List<GameObject>();
        modelButtons = new List<Button>();
        modelDesc = new List<List<string[]>>();
        buttons = new List<GameObject>();
        buttonImages = new List<Sprite>();

        // add sprites to list
        buttonImages.Add(btn1);
        buttonImages.Add(btn2);
        buttonImages.Add(btn3);
        buttonImages.Add(btn4);
        buttonImages.Add(btn5);
        buttonImages.Add(btn6);
        buttonImages.Add(btn7);

        // find canvas
        Transform canv = GameObject.Find("Canvas").gameObject.transform;

        // set pivot point
        float pivotX = -1.075f;

        // make buttons
        for (int i = 0; i < 8; i++)
        {
            // make button
            buttons.Add((GameObject)Instantiate(buttonPrefab, transform));
            // set button name
            buttons[i].gameObject.name = "btnModel" + (i + 1);
            // set parent
            buttons[i].transform.SetParent(canv, false);
            // set scale
            buttons[i].transform.localScale = new Vector3(1, 1, 1);
            // set position when using pivot points
            buttons[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, -2000);
            // https://forum.unity.com/threads/setting-pos-z-in-recttransform-via-scripting.270230/
            // move button on z axis, so buttons aren't behind the model
            buttons[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            buttons[i].GetComponent<RectTransform>().localPosition =
                 new Vector3(buttons[i].GetComponent<RectTransform>().localPosition.x,
                             buttons[i].GetComponent<RectTransform>().localPosition.y,
                             -2000);
            // set pivot
            buttons[i].GetComponent<RectTransform>().pivot = new Vector2(pivotX, 1.5f);

            if (i < buttonImages.Count)
            {
                // set images
                buttons[i].GetComponent<Image>().sprite = buttonImages[i];
            }
            // move button to the right
            pivotX -= 1.5f;
        }

        // loop to find and hide all models
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
        btnCredits = GameObject.Find("btnCredits").GetComponent<Button>();

        // find text box
        txtModelTitle = GameObject.Find("txtModelTitle").GetComponent<Text>();
        txtModelLocat = GameObject.Find("txtModelLocat").GetComponent<Text>();
        txtModelDesc = GameObject.Find("txtModelDesc").GetComponent<Text>();
        txtPageNum = GameObject.Find("txtPageNum").GetComponent<Text>();

        // find image
        imgTxtBackground = GameObject.Find("imgTxtBackground").GetComponent<Image>();

        // add listners  
        // model button
        modelButtons[0].onClick.AddListener(() => ModelChange(0));
        modelButtons[1].onClick.AddListener(() => ModelChange(1));
        modelButtons[2].onClick.AddListener(() => ModelChange(2));
        modelButtons[3].onClick.AddListener(() => ModelChange(3));
        modelButtons[4].onClick.AddListener(() => ModelChange(4));
        modelButtons[5].onClick.AddListener(() => ModelChange(5));
        modelButtons[6].onClick.AddListener(() => ModelChange(6));
        //modelButtons[7].onClick.AddListener(() => ModelChange(7));

        // info button
        btnModelDesc.onClick.AddListener(() => DisplayModelDesc());

        // page button
        btnTextF.onClick.AddListener(() => ChangeText(1));
        btnTextB.onClick.AddListener(() => ChangeText(0));

        // credits button
        btnCredits.onClick.AddListener(() => DisplayCredits());

        // Set model descriptions into list
        // NOTE: adding a new string ie "new", "line", line will be on a the next page
        // more about formatting text using HTML: https://docs.unity3d.com/Manual/StyledText.html
        modelDesc.Add(new List<string[]> {
            new string[]
            {
                "<b>Small Jar</b>"
            },
            new string[]
            {
                "<i>Jemez Black-on-white</i>\n<i>AD 1600-1700</i>"
            },
            new string[]
            {
                "This jar was likely used as a canteen. It was excavated from the nearby ancient Jemez village of Unshagi. Two sparrows can be seen on each side of the vessel. There is also a hole drilled near the mouth which was probably used for a carrying cord."
            }
        });
        modelDesc.Add(new List<string[]> {
            new string[]
            {
                "<b>Stepped Bowl</b>"
            },
            new string[]
            {
                "<i>Jemez Black-on-white</i>\n<i>AD 1300-1750</i>"
            },
            new string[]
            {
                "This partially restored bowl was excavated from a nearby ancient Jemez village. Notice the repeating stairstep motifs all over this vessel. It was common in the early 1900s for archaeologists to “repair” or “recreate” missing portions of pottery.Can you identify where the early",
                "archaeologists added to this vessel?"
            }
        });
        modelDesc.Add(new List<string[]> {
            new string[]
            {
                "<b>Metal Axe</b>"
            },
            new string[]
            {
                "<i>AD 1700-1800</i>"
            },
            new string[]
            {
                "Before the arrival of the Spanish, most pueblo tools were made of wood, ceramic, stone, and bone. The introduction of metal tools changed the way people in the village of Giusewa lived. It takes half of the time to chop down a tree with a metal axe compared to a stone axe.7"
            }
        });
        modelDesc.Add(new List<string[]> {
            new string[]
            {
                "<b>Trade Bowl</b>"
            },
            new string[]
            {
                "<i>Rio Grande Glazeware</i>\n<i>AD 1515-1750</i>"
            },
            new string[]
            {
                "While most of the pottery found at the ancient village of Giusewa is Jemez Black-on-white, recent excavations have revealed a large amount of Rio Grande Glazeware. These colorful pieces of pottery were made in the ancestral villages of Zia and Tamaya, and traded into the Jemez Valley."
            }
        });
        modelDesc.Add(new List<string[]> {
            new string[]
            {
                "<b>Shipping Jar</b>"
            },
            new string[]
            {
                "<i>Andalusian Utilitywear</i>\n<i>AD 1580-1850</i>"
            },
            new string[]
            {
                "These large egg-shaped jars were produced in Spain, filled with goods, then transported to the North American Colonies. Jars of this shape typically held wine, olive oil, olives. Once the contents of the jars were depleted, they were reused as storage jars for a variety of products made in the New World."
            }
        });
        modelDesc.Add(new List<string[]> {
            new string[]
            {
                "<b>Cooking Pot</b>"
            },
            new string[]
            {
                "<i>Jemez Utilitywear</i>\n<i>AD 1300-1850</i>"
            },
            new string[]
            {
                "More than half of the pottery excavated from Giusewa Pueblo were fragments of grey and black Utilitywear. These vessels were used for cooking and storing food.It was an effective form of pottery, so it changed very little over the course of several hundred years."
            }
        });
        modelDesc.Add(new List<string[]> {
            new string[]
            {
                "<b>Miniature Canteen</b>"
            },
            new string[]
            {
                "<i>Jemez Black-on-white</i>\n<i>AD 1600-1700</i>"
            },
            new string[]
            {
                "This miniature canteen was found on the floor of a room here in the village of Giusewa. It was excavated in 2019 and appears to still have materials inside. These will be tested to reveal the canteen’s contents. Notice the detailed quartered-designs on the face of the vessel."
            }
        });

        /*
        modelDesc.Add(new string[]{
            "<b>Small Jar</b>\n<i>Jemez Black-on-white</i>\nAD 1300-1750\n\nThis jar was likely used as a canteen. It was excavated from the nearby ancient Jemez village of Unshagi. Two sparrows can be seen on each side of the vessel. There is also a hole drilled near the mouth which was probably used for a carrying cord."
        });

        modelDesc.Add(new string[] {
            "<b>Stepped Bowl</b>\n<i>Jemez Black-on-white</i>\nAD 1300-1750\n\nThis partially restored bowl was excavated from a nearby ancient Jemez village. Notice the repeating stairstep motifs all over this vessel. It was common in the early 1900s for archaeologists to “repair” or “recreate” missing portions of pottery.Can you identify where the early",
            "archaeologists added to this vessel?"
            });

        modelDesc.Add(new string[]{
            "<b>Metal Axe</b>\n<i>Steel</i>\nAD 1700-1800\n\nBefore the arrival of the Spanish, most pueblo tools were made of wood, ceramic, stone, and bone. The introduction of metal tools changed the way people in the village of Giusewa lived. It takes half of the time to chop down a tree with a metal axe compared to a stone axe.7"
        });

        modelDesc.Add(new string[] {
            "<b>Trade Bowl</b>\n<i>Rio Grande Glazeware</i>\nAD 1515-1750\n\nWhile most of the pottery found at the ancient village of Giusewa is Jemez Black-on-white, recent excavations have revealed a large amount of Rio Grande Glazeware. These colorful pieces of pottery were made in the ancestral villages of Zia and Tamaya, and traded into the Jemez Valley."
        });

        modelDesc.Add(new string[]{
            "<b>Shipping Jar</b>\n<i>Andalusian Utilitywear</i>\nAD 1580-1850\n\nThese large egg-shaped jars were produced in Spain, filled with goods, then transported to the North American Colonies. Jars of this shape typically held wine, olive oil, olives. Once the contents of the jars were depleted, they were reused as storage jars for a variety of products made in the New World."
        });

        modelDesc.Add(new string[]{
            "<b>Cooking Pot</b>\n<i>Jemez Utilitywear<i>\nAD 1300-1850\n\nMore than half of the pottery excavated from Giusewa Pueblo were fragments of grey and black Utilitywear. These vessels were used for cooking and storing food.It was an effective form of pottery, so it changed very little over the course of several hundred years."
        });

        modelDesc.Add(new string[]{
            "<b>Miniature Canteen</b>\n<i>Jemez Black-on-white</i>\nAD 1600-1700\n\nThis miniature canteen was found on the floor of a room here in the village of Giusewa. It was excavated in 2019 and appears to still have materials inside. These will be tested to reveal the canteen’s contents. Notice the detailed quartered-designs on the face of the vessel."
        });*/



        // hide UI elements
        txtModelTitle.gameObject.SetActive(false);
        txtModelLocat.gameObject.SetActive(false);
        txtModelDesc.gameObject.SetActive(false);
        imgTxtBackground.gameObject.SetActive(false);
        btnTextF.gameObject.SetActive(false);
        btnTextB.gameObject.SetActive(false);

        // set sprite imaage
        btnModelDesc.GetComponent<Image>().sprite = sprShowDesc;

        // Set model 1 to show when project loads
        ModelChange(0);
    }

    // changes model from onclickllistner from button
    private void ModelChange(int modelNumber)
    {
        // change soom back to default
        GameObject.Find("ModelController").GetComponent<ModelController>().camera.orthographicSize = defaultZoom;
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
        // set position to 0
        infoPos = 0;
        StartCoroutine("animateBtnTxtHide");
    }

    private void DisplayModelDesc()
    {
        if (txtModelDesc.gameObject.activeSelf == false)        // if text is not showing
        {
            StartCoroutine("animateBtnTxtShow");
        }
        else if (txtModelDesc.gameObject.activeSelf == true)    // if text is showing
        {
            if (txtModelLocat.text == credits)   // if text showing is credits
            {
                if (isCredits == true)
                {
                    StartCoroutine("animateBtnTxtHide"); // hide text
                }
                else
                {
                    txtModelTitle.text = modelDesc[activeModel][0][0];
                    txtModelLocat.text = modelDesc[activeModel][1][0];
                    txtModelDesc.text = modelDesc[activeModel][2][infoPos];        // change back to model description
                    hideButtonArrows(modelDesc[activeModel][2].Length);
                    btnModelDesc.GetComponent<Image>().sprite = sprHideDesc;    // change button to hide sprite
                }
            }
            else  // if text is not credits
            {
                StartCoroutine("animateBtnTxtHide"); // hide text
            }
        }

        // which button was pressed and asign model text
        txtModelTitle.text = modelDesc[activeModel][0][0];
        txtModelLocat.text = modelDesc[activeModel][1][0];
        txtModelDesc.text = modelDesc[activeModel][2][infoPos];
        // hide or unhide button arrows
        hideButtonArrows(modelDesc[activeModel][2].Length);
    }

    private void DisplayCredits()
    {
        if (txtModelDesc.gameObject.activeSelf == false)        // if text is not showing
        {
            isCredits = true;                       // model description is not open
            txtModelTitle.text = "Credits:";
            txtModelLocat.text = credits;
            txtModelDesc.text = " ";            // change text to credits 
            txtPageNum.text = "1/1";                // change text to 1/1
            StartCoroutine("animateBtnTxtShow");    // show text
            hideButtonArrows(1);                    // hide page button arrows
        }
        else if (txtModelDesc.gameObject.activeSelf == true)    // if text is showing
        {
            if (txtModelLocat.text != credits)   // if text is not credits
            {
                isCredits = false;
                txtModelTitle.text = "Credits:";
                txtModelLocat.text = credits;
                txtModelDesc.text = " ";            // change text to credits
                txtPageNum.text = "1/1";                // change text to 1/1
                btnModelDesc.GetComponent<Image>().sprite = sprShowDesc; // change sprite to info
                hideButtonArrows(1);                    // hide page button arrows
            }
            else if (isCredits == false)
            {
                btnModelDesc.GetComponent<Image>().sprite = sprHideDesc;    // change button to hide sprite
                // which button was pressed and asign model text
                txtModelTitle.text = modelDesc[activeModel][0][0];
                txtModelLocat.text = modelDesc[activeModel][1][0];
                txtModelDesc.text = modelDesc[activeModel][2][infoPos];
                txtPageNum.text = "" + (infoPos + 1) + "/" + modelDesc[activeModel][2].Length;
                // if current models description is 1 or one page don't show the page buttons
                // hide page button arrows
                hideButtonArrows(modelDesc[activeModel][2].Length);
            }
            else if (isCredits == true) // if text is credits
            {
                StartCoroutine("animateBtnTxtHide");    // hide text
            }
        }
    }

    // called from modelController
    public void CloseTextBox()
    {
        StartCoroutine("animateBtnTxtHide");    // hide description
    }

    // change page
    private void ChangeText(int dir)
    {
        // check which button was pressed
        if (dir == 1)
        {
            // increment position
            infoPos++;
            // make sure within array
            if (infoPos >= modelDesc[activeModel][2].Length)
            {
                infoPos = modelDesc[activeModel][2].Length - 1;
            }
        }
        else if (dir == 0)
        {
            infoPos--;
            if (infoPos < 0)
            {
                infoPos = 0;
            }
        }
        // set info to text
        txtModelTitle.text = modelDesc[activeModel][0][0];
        txtModelLocat.text = modelDesc[activeModel][1][0];
        txtModelDesc.text = modelDesc[activeModel][2][infoPos];
        // checkk info pos
        checkInfoPos();
    }

    private void checkInfoPos()
    {
        // set forward and backward buttons to show
        btnTextF.interactable = true;
        btnTextB.interactable = true;

        if (infoPos >= modelDesc[activeModel][2].Length - 1) // if page is to the most right
        {
            btnTextF.interactable = false;  // disable forward button
        }
        else if (infoPos <= 0)  // if button is most left
        {
            btnTextB.interactable = false;  // disable backwards button
        }

        // if there is only one paGe
        if (modelDesc[activeModel][2].Length == 1)
        {
            // both buttons are disable
            btnTextF.interactable = false;
            btnTextB.interactable = false;
        }
        // show current page of total
        txtPageNum.text = "" + (infoPos + 1) + "/" + modelDesc[activeModel][2].Length;
    }

    private void hideButtonArrows(int pageNumber)
    {
        // if current models description is 1 or one page don't show the page buttons
        if (pageNumber == 1)
        {
            btnTextF.gameObject.SetActive(false);
            btnTextB.gameObject.SetActive(false);
        }
        else
        {
            btnTextF.gameObject.SetActive(true);
            btnTextB.gameObject.SetActive(true);
        }
    }


    IEnumerator animateBtnTxtShow()
    {
        // set sprite to close
        btnModelDesc.GetComponent<Image>().sprite = sprHideDesc;

        // mode collider to the left
        this.GetComponent<BoxCollider>().center = new Vector3(-4, 0, 0);

        // show description
        txtModelTitle.gameObject.SetActive(true);
        txtModelLocat.gameObject.SetActive(true);
        txtModelDesc.gameObject.SetActive(true);
        imgTxtBackground.gameObject.SetActive(true);
        btnTextF.gameObject.SetActive(true);
        btnTextB.gameObject.SetActive(true);


        // I set up povit points and this is used to animate the button and text 
        while (btnModelDesc.GetComponent<RectTransform>().anchoredPosition.x > -395)
        {
            // move left every .01 seconds
            btnModelDesc.transform.Translate(Vector3.left * (Time.deltaTime * moveText));
            txtModelTitle.transform.Translate(Vector3.left * (Time.deltaTime * moveText));
            txtModelLocat.transform.Translate(Vector3.left * (Time.deltaTime * moveText));
            txtModelDesc.transform.Translate(Vector3.left * (Time.deltaTime * moveText));
            btnTextF.transform.Translate(Vector3.left * (Time.deltaTime * moveText));
            btnTextB.transform.Translate(Vector3.left * (Time.deltaTime * moveText));
            txtPageNum.transform.Translate(Vector3.left * (Time.deltaTime * moveText));
            imgTxtBackground.transform.Translate(Vector3.left * (Time.deltaTime * moveText));
            yield return new WaitForSecondsRealtime(speed);
        }

        //stop coroutine 
        StopCoroutine("animateBtnTxtShow");
    }

    IEnumerator animateBtnTxtHide()
    {
        // change button sprite to info
        btnModelDesc.GetComponent<Image>().sprite = sprShowDesc;

        // I set up povit points and this is used to animate the button and text 
        while (btnModelDesc.GetComponent<RectTransform>().anchoredPosition.x < 275)
        {
            // move right every .01 seconds
            btnModelDesc.transform.Translate(Vector3.right * (Time.deltaTime * moveText));
            txtModelTitle.transform.Translate(Vector3.right * (Time.deltaTime * moveText));
            txtModelLocat.transform.Translate(Vector3.right * (Time.deltaTime * moveText));
            txtModelDesc.transform.Translate(Vector3.right * (Time.deltaTime * moveText));
            btnTextF.transform.Translate(Vector3.right * (Time.deltaTime * moveText));
            btnTextB.transform.Translate(Vector3.right * (Time.deltaTime * moveText));
            txtPageNum.transform.Translate(Vector3.right * (Time.deltaTime * moveText));
            imgTxtBackground.transform.Translate(Vector3.right * (Time.deltaTime * moveText));
            yield return new WaitForSecondsRealtime(speed);
        }

        // hide description
        txtModelTitle.gameObject.SetActive(false);
        txtModelLocat.gameObject.SetActive(false);
        txtModelDesc.gameObject.SetActive(false);
        imgTxtBackground.gameObject.SetActive(false);
        btnTextF.gameObject.SetActive(false);
        btnTextB.gameObject.SetActive(false);

        // move collider back to normal
        this.GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);

        // check position on buttons
        checkInfoPos();

        //stop coroutine 
        StopCoroutine("animateBtnTxtHide");
    }
}
