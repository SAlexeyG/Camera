using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    [SerializeField] private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        var renderTexture = new RenderTexture(512, 512, 16, RenderTextureFormat.ARGB32);
        camera.targetTexture = renderTexture;
        GetComponent<MeshRenderer>().material.SetTexture(1, renderTexture, UnityEngine.Rendering.RenderTextureSubElement.Color);
    }
}
