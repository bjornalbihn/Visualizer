Shader "RayMarch/Warper"
{
	Properties
	{
	    _Color1("Color 1", Color) = (0.2,0.1,0.4, 1)
	    _Color2("Color 2", Color) = (0.3,0.05,0.05, 1)
	    _Color3("Color 3", Color) = (0.9,0.9,0.9, 1)
	    _Color4("Color 4", Color) = (0.4,0.3,0.3, 1)
	    _Color5("Color 5", Color) = (0.0,0.2,0.4, 1)

		_Weird("Weird", Vector) = (4.4,4.4,0,0)
		_Size("Size", Range(0.05, 2)) = 1
		_SinPower("SinPower", Range(0, 20)) = 1
		_Movement("Movement", Vector) = (0,0,0,0)
		_Noise("Noise", Range(0, 2)) = 1
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
			float2 _Screen;

			// Local properties
			float _SinPower;
			float _Size;
			float _Noise;
			float4 _Movement;
			float4 _Weird;

			float4 _Color1;
			float4 _Color2;
			float4 _Color3;
			float4 _Color4;
			float4 _Color5;

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

			float noise( in float2 x )
			{
				return sin(1.5*x.x)*sin(1.5*x.y);
			}

			float fbm4( float2 p )
			{
				float2x2 m = float2x2( 0.80,  0.60, -0.60,  0.80 ) * _Weird.z;

			    float f = 0.0;
			    f += 0.5000*noise( p ); p = mul(m,p*2.02 + _SinPower * _SinTime.x);
			    f += 0.2500*noise( p ); p = mul(m,p*2.03 + _SinPower * _SinTime.x);
			    f += 0.1250*noise( p ); p = mul(m,p*2.01 + _SinPower * _SinTime.x);
			    f += 0.0625*noise( p );

			    return f/0.9375;
			}

			float fbm6( float2 p )
			{
				float2x2 m = float2x2( 0.80,  0.60, -0.60,  0.80 )* _Weird.w;

			    float f = 0.0;
			    f += 0.500000*(0.5+0.5*noise( p )); p = mul(m,p*2.02);
			    f += 0.250000*(0.5+0.5*noise( p )); p = mul(m,p*2.03);
			    f += 0.125000*(0.5+0.5*noise( p )); p = mul(m,p*2.01);
			    f += 0.062500*(0.5+0.5*noise( p )); p = mul(m,p*2.04);
			    f += 0.031250*(0.5+0.5*noise( p )); p = mul(m,p*2.01);
			    f += 0.015625*(0.5+0.5*noise( p ));
			    return f/0.96875;
			}


			float func( float2 q, out float4 ron )
			{
			    float ql = length( q );
			    q.x += (_Time * _Movement.x) + 0.05*sin(0.27*_Time * _Movement.z+ql*_Weird.x);
			    q.y += (_Time * _Movement.y) + 0.05*cos(0.23*_Time * _Movement.z+ql*_Weird.y);
			    q *= 0.5;

				float2 o = float2(0.0, 0.0);
			    o.x = 0.5 + 0.5*fbm4( float2(2.0*q ));
			    o.y = 0.5 + 0.5*fbm4( float2(2.0*q+float2(5.2, 5.2))  );

				float ol = length( o );
			    o.x +=  + 0.02*sin(0.12*_Time * _Movement.z+ol)/ol;
			    o.y +=  + 0.02*sin(0.14*_Time * _Movement.z+ol)/ol;

			    float2 n;
			    n.x = fbm6( float2(4.0*o+float2(9.2, 9.2))  );
			    n.y = fbm6( float2(4.0*o+float2(5.7, 5.7))  );

			    float2 p = 4.0*q + 4.0*n;

			    float f = 0.5 + 0.5*fbm4( p );

			    f = lerp( f, f*f*f*3.5, f*abs(n.x) );

			    float g = 0.5 + 0.5*sin(4.0*p.x)*sin(4.0*p.y);
			    f *= 1.0-0.5*pow( g, 8.0 );

				ron = float4( o, n );
				
			    return f;
			}

			fixed3 doMagic(float2 p)
			{
				float2 q = p*0.6;

			    float4 on = float4(0.0, 0.0, 0.0, 0.0);
			    float f = func(q, on);

				fixed3 col = fixed3(0.0, 0.0, 0.0);
			    col = lerp( _Color1.rgb, _Color2.rgb, f );
			    col = lerp( col, _Color3.rgb, dot(on.zw,on.zw) );
			    col = lerp( col, _Color4.rgb, 0.5*on.y*on.y );
			    col = lerp( col, _Color5.rgb, 0.5*smoothstep(1.2,1.3,abs(on.z)+abs(on.w)) );
			    col = clamp( col*f*2.0, 0.0, 1.0 );

				return 1.1 * col *col;
			}



			fixed4 frag(v2f i) : SV_Target
			{
			    float2 q = i.uv.xy / _Size;
			    float2 p = -1.0 + 2.0 * q;
			    p.x *= _ScreenParams.x/_ScreenParams.y;

			    
				return fixed4( doMagic( p ), 1.0 );
			}
			ENDCG
		}
	}
}