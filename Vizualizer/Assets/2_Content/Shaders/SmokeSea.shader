// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "bteitler/RaymarchFramework/RayMarchSkyboxSeascapeOptimizing" {
    Properties {
	        _UpVector ("Up Vector", Vector) = (0.0,1.0,0.0)
	        _RightVector ("Up Vector", Vector) = (1.0,0.0,0.0)
	        _ForwardVector ("Up Vector", Vector) = (0.0,0.0,1.0)
	        _MyWorldPosition ("My World Location", Vector) = (0.0,0.0,0.0)
	        _MyWorldCameraLook ("My World Camera Look", Vector) = (0.0,0.0,0.0)


	        		_Iterations("Iterations", Range(0, 200)) = 100
        // How long through space we should step
		_ViewDistance("View Distance", Range(0, 5)) = 2
        // Essentially the background color
		_SkyColor("Sky Color", Color) = (0.176, 0.478, 0.871, 1)
        // Cloud color
		_CloudColor("Cloud Color", Color) = (1, 1, 1, 1)
        // How dense our clouds should be
		_CloudDensity("Cloud Density", Range(0, 1)) = 0.5
   	}
    	
    SubShader {
        Pass {
            CGPROGRAM
            
            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag
            
            #pragma target 3.0
            
            float4 _UpVector;
            float4 _RightVector;
            float4 _ForwardVector;
            float3 _MyWorldPosition;
            float3 _MyWorldCameraLookVector;
            float3 _MyWorldCameraLook;

			sampler2D _NoiseOffsets;
            			int _Iterations;
			float3 _SkyColor;
			float4 _CloudColor;
			float _ViewDistance;
			float _CloudDensity;
            
            // Quality metric between 0 and 1 that is computed at the start of frag shader based on the angle
            // between the world look direction and the world ray for that pixel
            float fovQuality = 1.0;
            
            static const int NUM_STEPS = 5;
			static const float PI	 	= 3.1415;
			static const float EPSILON	= 1e-3;
			float EPSILON_NRM	= 0.01 / 1024;

			// sea
			static const int ITER_GEOMETRY_LOW  = 2;
			static const int ITER_GEOMETRY_HIGH = 3;
			
			static const int ITER_FRAGMENT_LOW  = 4;
			static const int ITER_FRAGMENT_HIGH = 5;
			
			static const float SEA_HEIGHT = 0.6;
			static const float SEA_CHOPPY = 4.0;
			static const float SEA_SPEED = 0.8;
			static const float SEA_FREQ = 0.16;
			static const float3 SEA_BASE = float3(0.1,0.19,0.22);
			static const float3 SEA_WATER_COLOR = float3(0.8,0.9,0.6);
			float SEA_TIME = 0;
			static const float2x2 octave_m = float2x2(1.6,1.2,-1.2,1.6);

            float fract(float x) {
            	return x - floor(x);
            }
            
            float2 fract(float2 x) {
            	return x - floor(x);
            }
            
            float hash( float2 p ) {
				float h = dot(p,float2(127.1,311.7));	
			    return fract(sin(h)*43758.5453123);
			}
			
			// TESTED
			float noise( in float2 p ) {
			    float2 i = floor( p );
			    float2 f = fract( p );	
				float2 u = f*f*(3.0-2.0*f);
				

				//return fract(p).x;
				
			    return -1.0+2.0*lerp( lerp( hash( i + float2(0.0,0.0) ), 
			                     hash( i + float2(1.0,0.0) ), u.x),
			                lerp( hash( i + float2(0.0,1.0) ), 
			                     hash( i + float2(1.0,1.0) ), u.x), u.y);
			}

			// lighting
			float diffuse(float3 n,float3 l,float p) {
			    return pow(dot(n,l) * 0.4 + 0.6,p);
			}
			float specular(float3 n,float3 l,float3 e,float s) {    
			    float nrm = (s + 8.0) / (3.1415 * 8.0);
			    return pow(max(dot(reflect(e,n),l),0.0),s) * nrm;
			}

			// sky
			float3 getSkyColor(float3 e) {
			    e.y = max(e.y,0.0);
			    float3 ret;
			    ret.x = pow(1.0-e.y,2.0);
			    ret.y = 1.0-e.y;
			    ret.z = 0.6+(1.0-e.y)*0.4;
			    return ret;
			}

			// TESTED
			// sea
			float sea_octave(float2 uv, float choppy) {
			    uv += noise(uv);  
			    float2 wv = 1.0-abs(sin(uv));
			    float2 swv = abs(cos(uv));    
			    wv = lerp(wv,swv,wv);
			    return pow(1.0-pow(wv.x * wv.y,0.65),choppy);
			}
			
			float map(float3 p) {
			    float freq = SEA_FREQ;
			    float amp = SEA_HEIGHT;
			    float choppy = SEA_CHOPPY;
			    float2 uv = p.xz; uv.x *= 0.75;

				// CHECKPOINT - TESTED TO HERE
				//return uv.x + uv.y;
               // return p.y;

			    float d = 0.0, h = 0.0;  
			   // p = float3(0.0, 0.0, 0.0);
			    //return 0.4f;  
			    // int iters = ITER_GEOMETRY_LOW + ceil(fovQuality * (ITER_GEOMETRY_HIGH - ITER_GEOMETRY_LOW));
			    for(int i = 0; i < ITER_GEOMETRY_HIGH; i++) { // ITER_GEOMETRY
			    	//d = 19.0; // remove  
			    	
			    	//if (i == 2) {
			    	//return 0.4f;
			    	            
			    	d = sea_octave((uv+SEA_TIME)*freq,choppy);
			    	d += sea_octave((uv-SEA_TIME)*freq,choppy);

			        h += d * amp;   
			                  
			    	uv = mul(octave_m, uv); 
			    	
			    	freq *= 1.9; 
			    	amp *= 0.22;
			        choppy = lerp(choppy,1.0,0.2);
			    }

			    return p.y - h;
			}

			float map_detailed(float3 p) {
			    float freq = SEA_FREQ;
			    float amp = SEA_HEIGHT;
			    float choppy = SEA_CHOPPY;
			    float2 uv = p.xz; 
			    uv.x *= 0.75;
			    
			    float d, h = 0.0;    
			    
			    int iters = ITER_FRAGMENT_LOW + ceil(fovQuality * (ITER_FRAGMENT_HIGH - ITER_FRAGMENT_LOW));
			    for(int i = 0; i < iters; i++) {        
			    	d = sea_octave((uv+SEA_TIME)*freq,choppy);
			    	d += sea_octave((uv-SEA_TIME)*freq,choppy);
			        h += d * amp;        
			    	uv = mul(octave_m, uv); 
			    	freq *= 1.9; 
			    	amp *= 0.22;
			        choppy = lerp(choppy,1.0,0.2);
			    }
			    return p.y - h;
			}
			
			float3 getSeaColor(float3 p, float3 n, float3 l, float3 eye, float3 dist) {  
			    float fresnel = 1.0 - max(dot(n,-eye),0.0);
			    fresnel = pow(fresnel,3.0) * 0.65;
			        
			    float3 reflected = getSkyColor(reflect(eye,n));    
			    float3 refractted = SEA_BASE + diffuse(n,l,80.0) * SEA_WATER_COLOR * 0.12; 
			    
			    float3 color_ = lerp(refractted,reflected,fresnel);
			    
			    float atten = max(1.0 - dot(dist,dist) * 0.001, 0.0);
			    color_ += SEA_WATER_COLOR * (p.y - SEA_HEIGHT) * 0.18 * atten;
			    
			    float c = specular(n,l,eye,60.0);
			    color_ += float3(c, c, c);
			    
			    return color_;
			}

			// tracing
			float3 getNormal(float3 p, float eps) {
			    float3 n;
			    n.y = map_detailed(p);    
			    n.x = map_detailed(float3(p.x+eps,p.y,p.z)) - n.y;
			    n.z = map_detailed(float3(p.x,p.y,p.z+eps)) - n.y;
			    n.y = eps;
			    return normalize(n);
			}

			float heightMapTracing(float3 ori, float3 dir, out float3 p) { 
				p = float3(0.0, 0.0, 0.0); 
				
			    float tm = 0.0;
			    float tx = 1000.0;    
			    float hx = map(ori + dir * tx);
			    if(hx > 0.0) return tx;   
			    float hm = map(ori + dir * tm);    
			    float tmid = 0.0;
			    
			    for(int i = 0; i < NUM_STEPS; i++) {
			        tmid = lerp(tm,tx, hm/(hm-hx));                   
			        p = ori + dir * tmid;                   
			    	float hmid = map(p);
					if(hmid < 0.0) {
			        	tx = tmid;
			            hx = hmid;
			        } else {
			            tm = tmid;
			            hm = hmid;
			        }
			    }
			    return tmid;
			}

            
            struct v2f 
            {
                float4 position : SV_POSITION;
                //float2 uv : TEXCOORD0; // stores uv
                float3 worldSpacePosition : TEXCOORD0;
                float3 worldSpaceView : TEXCOORD1; 
            };
            
            v2f vert(appdata_full i) {
            	SEA_TIME = 0; // TODO: swap above
            
                v2f o;
                o.position = UnityObjectToClipPos (i.vertex);
                
                float4 vertexWorld = mul(unity_ObjectToWorld, i.vertex);
                
                o.worldSpacePosition = vertexWorld.xyz;
                o.worldSpaceView = vertexWorld.xyz - _WorldSpaceCameraPos;
                return o;
            }

            			// Noise function by Inigo Quilez - https://www.shadertoy.com/view/4sfGzS
			float noise2(float3 x) { x *= 4.0; float3 p = floor(x); float3 f = frac(x); f = f*f*(3.0 - 2.0*f); float2 uv = (p.xy + float2(37.0, 17.0)*p.z) + f.xy; float2 rg = tex2D(_NoiseOffsets, (uv + 0.5) / 256.0).yx; return lerp(rg.x, rg.y, f.z); }
            
            // This function is the actual noise function we are going to be using.
            // The more octaves you give it, the more details we'll get in our noise.
			float fbm(float3 pos, int octaves) { float f = 0.; for (int i = 0; i < octaves; i++) { f += noise2(pos) / pow(2, i + 1); pos *= 2.01; } f /= 1 - 1 / pow(2, octaves + 1); return f; }

		

            fixed4 frag(v2f i) : SV_Target 
            {	
				float3 ro = _WorldSpaceCameraPos;
				float3 rd = normalize(i.worldSpaceView);
			
				// tracing
			    float3 p = float3(5.0,233.0,1.0);

			    ro.x += _Time.x * 5;
					        	        
				float density = 0;
                
				for (float i = 0; i < _Iterations; i++)
				{
                    // f gives a number between 0 and 1.
                    // We use that to fade our clouds in and out depending on how far and close from our camera we are.
					float f = i / _Iterations;
                    // And here we do just that:
					float alpha = smoothstep(0, _Iterations * 0.2, i) * (1 - f) * (1 - f);
                    // Note that smoothstep here doesn't do the same as Mathf.SmoothStep() in Unity C# - which is frustrating btw. Get a grip Unity!
                    // Smoothstep in shader languages interpolates between two values, given t, and returns a value between 0 and 1. 
                    // To get a bit of variety in our clouds we collect two different samples for each iteration.
					float denseClouds = smoothstep(_CloudDensity, 0.75, fbm(p, 5));
					float lightClouds = (smoothstep(-0.2, 1.2, fbm(p * 2, 2)) - 0.5) * 0.5;
                    // Note that I smoothstep again to tell which range of the noise we should consider clouds.
                    // Here we add our result to our density variable
					density += (lightClouds + denseClouds) * alpha;
                    // And then we move one step further away from the camera.
					p = ro + rd * f * _ViewDistance;
				}
                // And here i just melted all our variables together with random numbers until I had something that looked good.
                // You can try playing around with them too.
				float3 color = _SkyColor + (_CloudColor.rgb - 0.5) * (density / _Iterations) * 20 * _CloudColor.a;

				return fixed4(color, 1);
			

            }

            ENDCG
        }
    }
}