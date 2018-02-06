using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{

    public Material mMaterial;

    public float fScrollingSpeed;

    private Vector2 v2TextureOffset;

    // Use this for initialization
    void Start()
    {
        v2TextureOffset.x = 0;
    }

    // Update is called once per frame
    void Update()
    {

        v2TextureOffset = mMaterial.GetTextureOffset("_MainTex");
        v2TextureOffset.x += fScrollingSpeed * Time.deltaTime;
        mMaterial.SetTextureOffset("_MainTex", v2TextureOffset);
    }

    void OnDestroy()
    {
        v2TextureOffset.x = 0;
        mMaterial.SetTextureOffset("_MainTex", v2TextureOffset);
    }
}
