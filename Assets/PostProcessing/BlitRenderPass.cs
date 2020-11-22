using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlitRenderPass : ScriptableRenderPass
{
    public RenderTargetIdentifier source;

    private Material material;
    private RenderTargetHandle tempRenderTargetHandler;

    public BlitRenderPass(Material material)
    {
        this.material = material;

        tempRenderTargetHandler.Init("randomName");
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer commandBuffer = CommandBufferPool.Get();
        commandBuffer.GetTemporaryRT(tempRenderTargetHandler.id, renderingData.cameraData.cameraTargetDescriptor);

        Blit(commandBuffer, source, tempRenderTargetHandler.Identifier(), material);
        Blit(commandBuffer, tempRenderTargetHandler.Identifier(), source);

        context.ExecuteCommandBuffer(commandBuffer);
        CommandBufferPool.Release(commandBuffer);
    }
}
