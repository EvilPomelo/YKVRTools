using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeControl : MonoBehaviour {
    public static FadeControl _instance;
    private Image image;
    public float fadeSpeed;
    private float fadeAlpha;
    private float fadeDir;
    // Use this for initialization
    void Awake () {
        if (_instance == null)
        {
            _instance = this;
        }
        image = GetComponent<Image>();
        fadeSpeed = 0.5f;
        fadeDir = -1;
        fadeAlpha = 1;
	}
	
	// Update is called once per frame
	void Update () {
        fadeAlpha += fadeDir * fadeSpeed * Time.deltaTime;
        fadeAlpha = Mathf.Clamp01(fadeAlpha);
        image.color = new Color(0, 0, 0, fadeAlpha);
    }

    public void FadeOut()
    {
        fadeDir = 1;
    }

    public void FadeIn()
    {
        fadeDir = -1;
    }



}
