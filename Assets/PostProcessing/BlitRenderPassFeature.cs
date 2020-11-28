using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BlitRenderPassFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class Settings
    {
        public Material material = null;
        public bool renderSceneView = false;
    }

    public Settings settings = new Settings();
    private BlitRenderPass renderPass;

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if(renderingData.cameraData.isSceneViewCamera && !settings.renderSceneView)
        {
            return;
        }
        renderPass.source = renderer.cameraColorTarget;
        renderer.EnqueuePass(renderPass);
    }

    public override void Create()
    {
        renderPass = new BlitRenderPass(settings.material);

        renderPass.renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
    }
}
