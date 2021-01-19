using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AnimePostProcess : ScriptableRendererFeature
{
    class AnimeRenderPass : ScriptableRenderPass
    {
        private RenderTargetIdentifier source;
        private RenderTargetHandle tempTexture;
        private Material material;
        
        public AnimeRenderPass(Material material) {
            this.material = material;
            tempTexture.Init("_MainTexture");
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get("AnimePostProcessing");

            RenderTextureDescriptor cameraTextureDescriptor = renderingData.cameraData.cameraTargetDescriptor;
            cameraTextureDescriptor.depthBufferBits = 0;
            cmd.GetTemporaryRT(tempTexture.id, cameraTextureDescriptor, FilterMode.Bilinear);

            Blit(cmd, source, tempTexture.Identifier(), material, 0);
            Blit(cmd, tempTexture.Identifier(), source);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(tempTexture.id);
        }

        public void SetSource(RenderTargetIdentifier source) {
            this.source = source;
        }
    }

    AnimeRenderPass m_ScriptablePass;

    public override void Create()
    {
        Material material = new Material(Shader.Find("Anime/AnimePostProcess"));
        m_ScriptablePass = new AnimeRenderPass(material);

        m_ScriptablePass.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        m_ScriptablePass.SetSource(renderer.cameraColorTarget);
        renderer.EnqueuePass(m_ScriptablePass);
    }
}


