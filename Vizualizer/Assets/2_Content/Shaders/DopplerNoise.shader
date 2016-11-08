Shader "Custom/DopplerNoise"
{
	Properties{
		_PropertyOne("BandingResolution",  Range(0.005, 0.1)) = .1
		_PropertyTwo("Tilt", Range(-180, 180)) = 0
		_PropertyThree("Brightness", Range(0.2, 1.2)) = 0.6
		_PropertyFour("Messy", Range(0.00001, 1)) = 1
		_PropertyFive("Multiplier", Range(0.0000001, 0.00001)) = 0.00001
	}

		SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			float _PropertyOne;
			float _PropertyTwo;
			float _PropertyThree;
			float _PropertyFour;
			float _PropertyFive;

			struct v2f
			{
				float4 position : SV_POSITION;
			};

			v2f vert(float4 v:POSITION) : SV_POSITION
			{
				v2f o;
				o.position = mul(UNITY_MATRIX_MVP, v);
				return o;
			}

			float hash(float2 p)
			{
				return frac(sin(dot(p, float2(_PropertyTwo, 78.233))) * 40102.1024 * _PropertyFour);
;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float2 position = i.position.xy;
				float2 pos = (position * _PropertyOne + fmod(_Time.y, 4) * 10000 + 1000000);

				pos = pos * _PropertyFive;

				float h = hash(pos);

				float3 col = float3(h, h, h) * _PropertyThree;
				return float4(col, 1.0);
			}

			ENDCG
		}
	}
}
