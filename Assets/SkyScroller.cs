using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Playables;

public class SkyScroller : MonoBehaviour
{
    private MeshRenderer mMeshRenderer;
    
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
        if (Spawner.GetProcentOfWave(Spawner.GetWaveAt()) > 20)
        {
            float repeat = Mathf.Repeat((Spawner.GetProcentOfWave(Spawner.GetWaveAt()) / 100) -0.17f, 1);
            v2TextureOffset.x = repeat;
            mMeshRenderer.material.SetTextureOffset("_MainTex", v2TextureOffset);
        }
    }

    void OnDestroy()
    {
        v2TextureOffset.x = 0;
        mMeshRenderer.material.SetTextureOffset("_MainTex", v2TextureOffset);
    }

}