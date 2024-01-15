using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace GameName.CustomRenderPassess
{
public class RippleEffectRenderPassFeature : ScriptableRendererFeature
{
    class RippleEffectRenderPass : ScriptableRenderPass
    {
        public RenderTargetIdentifier source;
        public RenderTargetHandle destination;
        public Material RippleMaterial;
        public int blitShaderPassIndex;
        public FilterMode filterMode;

        private RenderTargetHandle m_TemporaryColorTexture;

        public RippleEffectRenderPass(RenderPassEvent renderPassEvent, FilterMode filterMode, int blitShaderPassIndex, Material mat)
        {
            this.renderPassEvent = renderPassEvent;
            this.filterMode = filterMode;
            this.blitShaderPassIndex = blitShaderPassIndex;
            this.RippleMaterial = mat;
            m_TemporaryColorTexture.Init("_TemporaryColorTextureZAA"); // You can name this anything you want
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get();

            if (Application.isPlaying && RipplePostProcessor.CurrentAmount > RipplePostProcessor.RippleSettings.LOWEST_AMOUNT_VALUE)
            {
                RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
                opaqueDesc.depthBufferBits = 0;

                // Can't read and write to same color target, use a TemporaryRT
                if (destination == RenderTargetHandle.CameraTarget)
                {
                    cmd.GetTemporaryRT(m_TemporaryColorTexture.id, opaqueDesc, filterMode);
                    Blit(cmd, source, m_TemporaryColorTexture.Identifier(), RippleMaterial, blitShaderPassIndex);
                    Blit(cmd, m_TemporaryColorTexture.Identifier(), source);
                }

            }
            // execution
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }

    RippleEffectRenderPass m_ScriptablePass;

    public FilterMode filterMode = FilterMode.Bilinear;
    public RenderPassEvent TheRenderPassEvent = RenderPassEvent.AfterRenderingTransparents;
    public int blitShaderPassIndex = -1;
    public Material RippleMaterial;
    public override void Create()
    {
        m_ScriptablePass = new RippleEffectRenderPass(TheRenderPassEvent, filterMode, blitShaderPassIndex, RippleMaterial);
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        m_ScriptablePass.source = renderer.cameraColorTarget;
        m_ScriptablePass.destination = RenderTargetHandle.CameraTarget;
        renderer.EnqueuePass(m_ScriptablePass);
    }
}
}