using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resize : MonoBehaviour
{
    [SerializeField] float Size;
    [SerializeField] bool isSysmetrical;

    void Awake()
    {
        ResizeSpriteToScreen(Size);
    }

    void ResizeSpriteToScreen(float size)
    {
        var sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = size <= 0 ? (float)(Camera.main.orthographicSize * 2.0) : (float)(Camera.main.orthographicSize * 2.0) / size;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        

        transform.localScale = isSysmetrical ? new Vector3(worldScreenWidth / width, worldScreenWidth / width, 1) :  new Vector3(worldScreenWidth / width, worldScreenHeight / height, 1);
    }
}
