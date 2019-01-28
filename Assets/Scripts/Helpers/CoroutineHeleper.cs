using BitStrap;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoroutineHeleper : MonoBehaviour //todo: dodać do projetku SafeAction.cs
{
    private static bool _initialized = false;
    private Coroutine _coroutine;

    private Dictionary<int, LevelBackground> _dict = new Dictionary<int, LevelBackground>();

    private void Start()
    {
        if (!_initialized)
        {
            DontDestroyOnLoad(gameObject);
            _initialized = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartFadeIn(Image[] images, Action actionOnEnd)
    {
       _coroutine = StartCoroutine(FadeIn(images, actionOnEnd));
    }

    private IEnumerator FadeIn(Image[] images, Action actionOnEnd = null)
    {
        while(images[0].color.a > 0)
        {
            for (int i = 0; i < images.Length; i++)
            {
                Image image = images[i];
                float alpha = Time.deltaTime / 0.5f;
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - alpha);
            }
            yield return null;
        }
        StopCoroutine(FadeIn(null));
        _coroutine = null;
        if(actionOnEnd != null)
        {
            actionOnEnd();
        }
    }
}