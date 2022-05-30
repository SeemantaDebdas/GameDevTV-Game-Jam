using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField]
    Texture2D[] textures;

    LineRenderer lr;
    float fps = 30;

    int animationStep;

    float fpsCounter;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.3f;
        lr.endWidth = 0.3f;
    }

    private void Update()
    {
        fpsCounter += Time.deltaTime;
        if(fpsCounter >= 1f / fps)
        {
            animationStep++;
            if(animationStep == textures.Length)
                animationStep = 0;

            lr.material.SetTexture("_MainTex", textures[animationStep]);

            fpsCounter = 0f;
        }
    }
}
