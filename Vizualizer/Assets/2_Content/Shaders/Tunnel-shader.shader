// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "bteitler/RaymarchFramework/SSmokes2" {
    Properties {
		_SphereSize("SphereSize", Float) = 50
		_NoiseSize("NoiseSize", Vector) = (1,1,1,1)
		_SinPower("SinPower", Range(0, 2)) = 1
		_Movement("Movement", Vector) = (0,0,0,0)
		_Noise("Noise", Range(0, 2)) = 1

		[Header(March)]
		_MarchIterations("March Iterations", Range(0, 100)) = 50

		[Header(Clouds)]
		_Iterations("Iterations", Range(0, 500)) = 325
		_ViewDistance("View Distance", Range(0, 5)) = 3.36
		_CloudDensity("Cloud Density", Vector) = (0.18, 0.8,0,0)
		_Color("Color", Color) = (0.5529, 0.47568, 1, 1)
		_ColorPower("ColorPower", Float) = 1
		_Background("Background", Color) = (0.5529, 0.47568, 1, 1)
   	}
    	
    SubShader {
        Pass {
            CGPROGRAM
            
            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag
            
            #pragma target 3.0
            
			sampler2D _NoiseOffsets;
			// Local properties
			float _SphereSize;
			float4 _NoiseSize;
			float _SinPower;
			float _Noise;
			float4 _Movement;

			float4 _Color;
			float _ColorPower;
			float4 _Background;
			int _MarchIterations;
			int _Iterations;
			float _ViewDistance;
			float2 _CloudDensity;
            
			// Shamelessly stolen from https://www.shadertoy.com/view/4sfGzS
			float noise(float3 x) { x *= 4.0; float3 p = floor(x); float3 f = frac(x+(lerp(0, _SinTime, _Noise))); f = f*f*(3.0 - 2.0*f); float2 uv = (p.xy + float2(37.0, 17.0)*p.z) + f.xy; float2 rg = tex2D(_NoiseOffsets, (uv + 0.5) / 256.0).yx; return lerp(rg.x, rg.y, f.z); }
			float fbm(float3 pos, int octaves) 
			{ 
				pos *=  _NoiseSize.xyz * _NoiseSize.w;
				pos += _Movement.xyz * (_Time + (_SinTime *_SinPower)); 

				float f = 0.; 
				for (int i = 0; i < octaves; i++) 
					{ 
					f += noise(pos) / pow(2, i + 1); 
					pos *= 2.01; 
					} 
					f /= 1 - 1 / pow(2, octaves + 1); 
				return f; 
			}

			float distFunc(float3 pos)
			{
			  	//float2 q = float2(length(pos.xz)-5,pos.y);
 				// return length(q)-2;

 				float a = length (float3(pos.x,0,pos.z)) - _SphereSize;
 				float b = length (float3(pos.x,0,pos.z)) - _SphereSize*2;

				return max(-a,b);
			}

			float march(in float3 ray, inout float3 pos)
			{
				float hit = 0;
				for (int i = 0; i < _MarchIterations; i++)
				{
					float d = distFunc(pos);
					///if (d < 0.005)
					//{
					//	hit = 1;
					//	break;
					//}
					pos.xyz += ray * d;
				}
				return hit;
			}
            
            struct v2f 
            {
                float4 position : SV_POSITION;
                //float2 uv : TEXCOORD0; // stores uv
                float3 worldSpacePosition : TEXCOORD0;
                float3 worldSpaceView : TEXCOORD1; 
            };
            
            v2f vert(appdata_full i) {

                v2f o;
                o.position = UnityObjectToClipPos (i.vertex);
                
                float4 vertexWorld = mul(unity_ObjectToWorld, i.vertex);
                
                o.worldSpacePosition = vertexWorld.xyz;
                o.worldSpaceView = vertexWorld.xyz - _WorldSpaceCameraPos;
                return o;
            }


            fixed4 frag(v2f i) : SV_Target 
            {	
				float3 ro = _WorldSpaceCameraPos;
				float3 rd = normalize(i.worldSpaceView);
			
				float3 p = ro;
				float cloudDensity = 0;
				float maxDensity = 0;
				float hit = march(rd, ro);

				for (float i = 0; i < _Iterations; i++)
				{
					float sphere = smoothstep(0, -1, distFunc(p));
					float f = i / _Iterations ;
					float alpha = smoothstep(0, 20, i) * (1 - f) * (1 - f);
					float clouds = smoothstep(_CloudDensity.x, _CloudDensity.y, fbm(p, 5)) * sphere;
					maxDensity = max(maxDensity, clouds + 0.1);
					cloudDensity += clouds * alpha * smoothstep(0.7, 0.4, maxDensity);
					p = ro + rd * f * _ViewDistance;
				}


				float cloudFactor = 1 - (cloudDensity / _Iterations) * 20 * _Color.a;
				float value = smoothstep(0.2, 1, cloudFactor);

				float3 color = lerp(_Color.rgb * _ColorPower, _Background.rgb, value);
				//fixed3 color = tex2D(_Gradient, float2(value, 0.5)) * _ColorPower;


				return fixed4(color * cloudFactor, 1);
			

            }

            ENDCG
        }
    }
}