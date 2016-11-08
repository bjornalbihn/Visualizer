using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Camera))]
public class RenderMaterialFullscreen : MonoBehaviour
{
	[SerializeField]
	private Material material;

	[SerializeField, Range(10, 1920)]
	private int width = 1920;

	[SerializeField, Range(10, 1080)]
	private int height = 1080;

	private CommandBuffer _commandBuffer;
	private Camera _camera;
	private Material _lastMaterial;

	private void Awake()
	{
		_camera = this.GetComponent<Camera>();
		_lastMaterial = material;
		SetCommandBuffer();
	}

	private void RemoveCommandBuffer()
	{
		_camera.RemoveCommandBuffer(CameraEvent.BeforeForwardOpaque, _commandBuffer);
	}

	private void Update()
	{
		if (material != _lastMaterial)
		{
			RemoveCommandBuffer();
			SetCommandBuffer();
			_lastMaterial = material;
		}
	}

	private void SetCommandBuffer()
	{
		_commandBuffer = new CommandBuffer();
		int lowResRenderTarget = Shader.PropertyToID("_LowResRenderTarget");
		_commandBuffer.GetTemporaryRT(lowResRenderTarget, this.width, this.height, 0, FilterMode.Trilinear, RenderTextureFormat.ARGB32);

		// Blit the low-res texture into itself, to re-draw it with the current material
		_commandBuffer.Blit(lowResRenderTarget, lowResRenderTarget, this.material);

		// Blit the low-res texture into the camera's target render texture, effectively rendering it to the entire screen
		_commandBuffer.Blit(lowResRenderTarget, BuiltinRenderTextureType.CameraTarget);

		_commandBuffer.ReleaseTemporaryRT(lowResRenderTarget);

		// Tell the camera to execute our CommandBuffer before the forward opaque pass - that is, just before normal geometry starts rendering
		_camera.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, _commandBuffer);
	}
}