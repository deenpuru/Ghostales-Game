using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RenderPipelineManager : MonoBehaviour
{
    [SerializeField] private RenderPipelineAsset _renderPipelineAsset;

    private void Awake()
    {
        GraphicsSettings.renderPipelineAsset = _renderPipelineAsset; 
    }
}
