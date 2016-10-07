Shader "Custom/Stars"
{
	Properties
	{
		_Iterations("Iterations", Range(0, 100)) = 17
		_FormuParam("ForumParam", Float) = .53

		_Volsteps("Volume Steps", Range(0, 100)) = 20
		_StepSize("Step Size", Float) = .1

		_Zoom("Zoom", Float) = .8
		_Tile("Tile", Float) = .85
		_Speed("Speed", Float) = .01


		_Brightness("Brightness", Range(0, 1)) = 0.0015
		_DarkMatter("Dark Matter", Range(0, 2)) = 0.3
		_DistFading("DistFading", Range(0, 2)) = 0.73
		_Saturation("Saturation", Range(0, 2)) = 0.85


	}
	SubShader
	{
		Pass
		{
			Blend SrcAlpha Zero

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			// Global properties
			sampler2D _NoiseOffsets;
			float3 _CamPos;
			float3 _CamRight;
			float3 _CamUp;
			float3 _CamForward;
			float _AspectRatio;
			float _FieldOfView;

			// Local properties

			float _Iterations;
			float _FormuParam;

			float _Volsteps;
			float _StepSize;

			float _Zoom;
			float _Tile;
			float _Speed;


			float _Brightness;
			float _DarkMatter;
			float _DistFading;
			float _Saturation;


			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert(appdata_base  v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.texcoord;
				return o;
			}

			float4 stars(float3 pos, float3 dir)
			{
				float s= 0.1, fade=1.;
				float3 v= float3(0,0,0);
				for (int r=0; r<_Volsteps; r++) 
				{
					float3 p=pos+s*dir*.5;
					p = abs(float3(_Tile, _Tile, _Tile)-fmod(p,float3(_Tile, _Tile,_Tile)*2.)); // tiling fold
					float pa,a=pa=0.;
					for (int i=0; i<_Iterations; i++) 
					{ 
						p=abs(p)/dot(p,p)-_FormuParam; // the magic formula
						a+=abs(length(p)-pa); // absolute sum of average change
						pa=length(p);
					}
					float dm=max(0.,_DarkMatter-a*a*.001); //dark matter
					a*=a*a; // add contrast
					if (r>6) fade*=1.-dm; // dark matter, don't render near
					//v+=vec3(dm,dm*.5,0.);
					v+=fade;
					v+=float3(s,s*s,s*s*s*s)*a*_Brightness*fade; // coloring based on distance
					fade*=_DistFading; // distance fading
					s+=_StepSize;
				}
				v=lerp(float3(1,1,1) * length(v),v,_Saturation); //color adjust
				return float4(v*.01,1.);	
			}


			fixed4 frag(v2f i) : SV_Target
			{
				float2 uv = (i.uv - 0.5) * _FieldOfView;
				uv.x *= _AspectRatio;

				float3 ray = _CamUp * uv.y + _CamRight * uv.x + _CamForward;
				float3 pos = _CamPos;
				float3 p = pos;

				pos.z += _Time.y * _Speed *.025;

				return stars(pos, ray);
			}
			ENDCG
		}
	}
}



