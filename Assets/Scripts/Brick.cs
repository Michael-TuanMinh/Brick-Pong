using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brick : MonoBehaviour
{
    public float width;
    public float height;

    private void Awake()
    {
        width = GetComponentInChildren<Image>().sprite.rect.width;
        height = GetComponentInChildren<Image>().sprite.rect.height;
    }



}
