using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{

    private MeshRenderer mMeshRenderer;

    public float fScrollingSpeed;

    private Vector2 v2TextureOffset;

    // Use this for initialization
    void Start()
    {
        mMeshRenderer = GetComponent<MeshRenderer>();
        v2TextureOffset.x = 0;
        v2TextureOffset = mMeshRenderer.material.GetTextureOffset("_MainTex");
    }

    // Update is called once per frame
    void Update()
    {
        float repeat = Mathf.Repeat(Time.time * fScrollingSpeed, 1);
        v2TextureOffset.x = repeat;
        mMeshRenderer.material.SetTextureOffset("_MainTex", v2TextureOffset);
    }

    void OnDestroy()
    {
        v2TextureOffset.x = 0;
        mMeshRenderer.material.SetTextureOffset("_MainTex", v2TextureOffset);
    }

}
