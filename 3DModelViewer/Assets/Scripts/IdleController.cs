﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IdleController : MonoBehaviour
{
    // gameObject for model
    private GameObject go;

    // String model names
    private string[] modelNames = { "model/21370.11", "model/21393.11", "model/Axe Head", "model/LateGlazeBowl Touchup", "model/pot 2 blender", "model/Pot1Blender_", "model/Pot2Blender_" };

    // get models
    private List<GameObject> models;

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

        // pick random model
        int rndModel = Random.Range(0, models.Count);
        models[rndModel].SetActive(true);

        // set color of model rgba
        //models[rndModel].GetComponent<Renderer>().material.color = new Color32(190, 188, 169, 255);
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around its local X axis at 1 degree per second
        go.transform.Rotate(Vector3.up * (Time.deltaTime * 20));

        // Click the screen to start dext scene
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("ModeViewerScene");
        }
    }
}
