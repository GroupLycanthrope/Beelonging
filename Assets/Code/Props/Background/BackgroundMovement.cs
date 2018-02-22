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
    }

    // Update is called once per frame
    void Update()
    {

        v2TextureOffset = mMeshRenderer.sharedMaterial.GetTextureOffset("_MainTex");
        v2TextureOffset.x += fScrollingSpeed * Time.deltaTime;
        mMeshRenderer.sharedMaterial.SetTextureOffset("_MainTex", v2TextureOffset);
    }

    void OnDestroy()
    {
        v2TextureOffset.x = 0;
        mMeshRenderer.sharedMaterial.SetTextureOffset("_MainTex", v2TextureOffset);
    }
}
