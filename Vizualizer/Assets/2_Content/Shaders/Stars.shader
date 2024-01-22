// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Stars"
{
	Properties
	{
		_Iterations("Iterations", Range(0, 100)) = 18
		_FormuParam("ForumParam", Float) = .53

		_Volsteps("Volume Steps", Range(0, 100)) = 1
		_StepSize("Step Size", Float) = .1

		_Tile("Tile", Float) = -0.2
		_Speed("Speed", Float) = .9
		_Flatten("Flatten", Range(0, 1)) = 1

		_Brightness("Brightness", Range(0, 1)) = 0.005
		_DistFading("DistFading", Range(0, 2)) = 0.88
		_Saturation("Saturation", Range(0, 2)) = 0.25


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

			float3 _CamPos = float3(0.5, 0.5, 0.5);

			float _Iterations;
			float _FormuParam;
			float _Volsteps;
			float _StepSize;
			float _Tile;
			float _Speed;
			float _Flatten;


			float _Brightness;
			float _DistFading;
			float _Saturation;


            struct v2f {
                float4 position : SV_POSITION;
                float3 worldSpacePosition : TEXCOORD0;
                float3 worldSpaceView : TEXCOORD1; 
            };
            
            v2f vert(appdata_full i) {

                v2f o;
				//o.position = float4(1,1,1,1);
                o.position = UnityObjectToClipPos (i.vertex);
                
                float4 vertexWorldFisheye = mul(unity_ObjectToWorld, i.vertex);
				float4 vertexWorldFlat = mul(float4(_CamPos.xyz, 0), i.vertex);
				float4 vertexWorld = lerp(vertexWorldFisheye, vertexWorldFlat, _Flatten);
				
                o.worldSpacePosition = vertexWorld.xyz;
                o.worldSpaceView = vertexWorld.xyz - _CamPos;
                return o;
            }

			float4 stars(float3 pos, float3 dir)
			{
				float s= 0.1, fade=1.;
				float3 v= float3(0,0,0);
				for (int r=0; r<_Volsteps; r++) 
				{
					float3 p=pos+s*dir*.5;
					p = abs(float3(_Tile, _Tile, _Tile)  -fmod(p,float3(_Tile, _Tile,_Tile)*2)); // tiling fold
					float pa,a=pa=0.;
					for (int i=0; i<_Iterations; i++) 
					{ 
						p=abs(p)/dot(p,p)-_FormuParam; // the magic formula
						a+=abs(length(p)-pa); // absolute sum of average change
						pa=length(p);
					}
					a*=a*a; // add contrast
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
				float3 ro = _CamPos;
				float3 rd = normalize(i.worldSpaceView);

				ro.x *= _Time.x * _Speed;

				return stars(ro, rd);
			}
			ENDCG
		}
	}
}



