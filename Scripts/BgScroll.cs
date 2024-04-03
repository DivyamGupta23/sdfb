using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScroll : MonoBehaviour
{
    public float scrollSpeed = 1;

    public Renderer this_renderer;
    private Vector2 savedOffset;

    void FixedUpdate()
    {
        float x = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset = new Vector2(x, -x);
        this_renderer.material.mainTextureOffset = offset;
    }
}
