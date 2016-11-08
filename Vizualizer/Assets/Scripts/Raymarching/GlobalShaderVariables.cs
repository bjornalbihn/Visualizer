using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class GlobalShaderVariables : MonoBehaviour
{
	[SerializeField]
	private Texture2D noiseOffsetTexture;

	private void Awake()
	{
		Shader.SetGlobalTexture("_NoiseOffsets", this.noiseOffsetTexture);
	}

	private void OnPreRender()
	{
		Shader.SetGlobalVector("_CamPos", this.transform.position);
		Shader.SetGlobalVector("_CamRight", this.transform.right);
		Shader.SetGlobalVector("_CamUp", this.transform.up);
		Shader.SetGlobalVector("_CamForward", this.transform.forward);

		Shader.SetGlobalVector("_Eye1Pos", this.transform.position);
		Shader.SetGlobalVector("_Eye1Right", this.transform.right);
		Shader.SetGlobalVector("_Eye1Up", this.transform.up);
		Shader.SetGlobalVector("_Eye1Forward", this.transform.forward);

		Shader.SetGlobalVector("_Eye2Pos", this.transform.position + Vector3.right * 23.5f);
		Shader.SetGlobalVector("_Eye2Right", this.transform.right);
		Shader.SetGlobalVector("_Eye2Up", this.transform.up);
		Shader.SetGlobalVector("_Eye2Forward", this.transform.forward);

		Shader.SetGlobalVector("_MyWorldPosition", this.transform.position);


		Shader.SetGlobalFloat("_AspectRatio", (float)Screen.width / (float)Screen.height);
		Shader.SetGlobalFloat("_FieldOfView", Mathf.Tan(Camera.main.fieldOfView * Mathf.Deg2Rad * 0.5f) * 2f);
	}
}