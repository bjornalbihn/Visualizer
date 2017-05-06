Shader "Hidden/GlitchShader" {
Properties
{
    _MainTex ("", 2D) = "white" {}

	_vertJerkOpt ("Vert Jerk", Range(0.0, 2.0)) = 1
	_vertMovementOpt ("Vert Movement", Range(0.0, 2.0)) = 1
	_bottomStaticOpt ("Static Noise", Range(0.0, 2.0)) = 1
	_scalinesOpt ("Scanlines", Range(0.0, 2.0)) = 1
	_rgbOffsetOpt ("RGB Offset", Range(0.0, 2.0)) = 1
	_horzFuzzOpt ("Horizontal Offset", Range(0.0, 2.0)) = 1
}

SubShader {
	Pass {
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }

		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma fragmentoption ARB_precision_hint_fastest 

		#pragma target 3.0

		#include "UnityCG.cginc"

		float _vertJerkOpt;
        float _vertMovementOpt;
        float _bottomStaticOpt ;
        float _scalinesOpt ;
        float _rgbOffsetOpt ;
        float _horzFuzzOpt;
              sampler2D _MainTex;

		struct v2f {
			float4 pos : POSITION;
			float2 uv : TEXCOORD0;
		};
		
		v2f vert( appdata_img v )
		{
			v2f o;

			o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			o.uv = v.texcoord.xy;
			
			return o;
		}

		float3 mod289(float3 x) {
          return x - floor(x * (1.0 / 289.0)) * 289.0;
        }

        float2 mod289(float2 x) {
          return x - floor(x * (1.0 / 289.0)) * 289.0;
        }

        float3 permute(float3 x) {
          return mod289(((x*34.0)+1.0)*x);
        }

        float snoise(float2 v)
        {
          const float4 C = float4(0.211324865405187,  // (3.0-sqrt(3.0))/6.0
                              0.366025403784439,  // 0.5*(sqrt(3.0)-1.0)
                             -0.577350269189626,  // -1.0 + 2.0 * C.x
                              0.024390243902439); // 1.0 / 41.0
        // First corner
          float2 i  = floor(v + dot(v, C.yy) );
          float2 x0 = v -   i + dot(i, C.xx);

        // Other corners
          float2 i1;
          i1 = (x0.x > x0.y) ? float2(1.0, 0.0) : float2(0.0, 1.0);
          float4 x12 = x0.xyxy + C.xxzz;
          x12.xy -= i1;

        // Permutations
          i = mod289(i); // Avoid truncation effects in permutation
          float3 p = permute( permute( i.y + float3(0.0, i1.y, 1.0 ))
        		+ i.x + float3(0.0, i1.x, 1.0 ));

          float3 m = max(0.5 - float3(dot(x0,x0), dot(x12.xy,x12.xy), dot(x12.zw,x12.zw)), 0.0);
          m = m*m ;
          m = m*m ;

        // Gradients: 41 points uniformly over a line, mapped onto a diamond.
        // The ring size 17*17 = 289 is close to a multiple of 41 (41*7 = 287)

          float3 x = 2.0 * frac(p * C.www) - 1.0;
          float3 h = abs(x) - 0.5;
          float3 ox = floor(x + 0.5);
          float3 a0 = x - ox;

        // Normalise gradients implicitly by scaling m
        // Approximation of: m *= inversesqrt( a0*a0 + h*h );
          m *= 1.79284291400159 - 0.85373472095314 * ( a0*a0 + h*h );

        // Compute final noise value at P
          float3 g;
          g.x  = a0.x  * x0.x  + h.x  * x0.y;
          g.yz = a0.yz * x12.xz + h.yz * x12.yw;
          return 130.0 * dot(m, g);
        }

        float staticV(float2 uv) {
            float staticHeight = snoise(float2(9.0,_Time.x*1.2+3.0))*0.3+5.0;
            float staticAmount = snoise(float2(1.0,_Time.x*1.2-6.0))*0.1+0.3;
            float staticStrength = snoise(float2(-9.75,_Time.x*0.6-3.0))*2.0+2.0;
        	return (1.0-step(snoise(float2(5.0*pow(_Time.x,2.0)+pow(uv.x*7.0,1.2),pow((fmod(_Time.x,100.0)+100.0)*uv.y*0.3+3.0,staticHeight))),staticAmount))*staticStrength;
        }

		
		half4 frag (v2f i) : COLOR
		{
			float2 uv =  i.uv;

        	float jerkOffset = (1.0-step(snoise(float2(_Time.x*1.3,5.0)),0.8))*0.05;

        	float fuzzOffset = snoise(float2(_Time.x*15.0,uv.y*80.0))*0.003;
        	float largeFuzzOffset = snoise(float2(_Time.x*1.0,uv.y*25.0))*0.004;

            float vertMovementOn = (1.0-step(snoise(float2(_Time.x*0.2,8.0)),0.4))*_vertMovementOpt;
            float vertJerk = (1.0-step(snoise(float2(_Time.x*1.5,5.0)),0.6))*_vertJerkOpt;
            float vertJerk2 = (1.0-step(snoise(float2(_Time.x*5.5,5.0)),0.2))*_vertJerkOpt;
            float yOffset = abs(sin(_Time.x)*4.0)*vertMovementOn+vertJerk*vertJerk2*0.3;
            float y = fmod(uv.y+yOffset,1.0);


        	float xOffset = (fuzzOffset + largeFuzzOffset) * _horzFuzzOpt;

            float staticVal = 0.0;

            for (float i = -1.0; i <= 1.0; i += 1.0) {
                float maxDist = 5.0/200.0;
                float dist = i/200.0;
            	staticVal += staticV(float2(uv.x,uv.y+dist))*(maxDist-abs(dist))*1.5;
            }

            staticVal *= _bottomStaticOpt;

        	float red 	=   tex2D(	_MainTex, 	float2(uv.x + xOffset -0.01*_rgbOffsetOpt,y)).r+staticVal;
        	float green = 	tex2D(	_MainTex, 	float2(uv.x + xOffset,	  y)).g+staticVal;
        	float blue 	=	tex2D(	_MainTex, 	float2(uv.x + xOffset +0.01*_rgbOffsetOpt,y)).b+staticVal;

        	float3 color = float3(red,green,blue);
        	float scanline = sin(uv.y*800.0)*0.04*_scalinesOpt;
        	color -= scanline;
			
			return fixed4(color,1);
		}
		ENDCG
	}
}

Fallback off

}
