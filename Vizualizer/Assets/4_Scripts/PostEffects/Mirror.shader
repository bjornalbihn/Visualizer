Shader "Custom/GrayScale"
{
    Properties
    {
        _MainTex ("", 2D) = "white" {}
        _XMirror ("X", Range(0,1)) = .5
        _YMirror ("Y", Range(0,1)) = .5
    }

    SubShader
    {

        ZTest Always Cull Off ZWrite Off Fog { Mode Off } //Rendering settings

        Pass
        {
              CGPROGRAM
              #pragma vertex vert
              #pragma fragment frag
              #include "UnityCG.cginc"
              //we include "UnityCG.cginc" to use the appdata_img struct

              struct v2f
              {
                   float4 pos : POSITION;
                   half2 uv : TEXCOORD0;
              };

              //Our Vertex Shader
              v2f vert (appdata_img v)
              {
                   v2f o;
                   o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
                   o.uv = MultiplyUV (UNITY_MATRIX_TEXTURE0, v.texcoord.xy);
                   return o;
              }

              sampler2D _MainTex;
              float _XMirror;
              float _YMirror;

              //Our Fragment Shader
              fixed4 frag (v2f i) : COLOR
              {

                   i.uv.x = abs(i.uv.x-_XMirror)+_XMirror;
                   i.uv.y = abs(i.uv.y-_YMirror)+_YMirror;
                   i.uv.y = 1-i.uv.y;
                   //i.uv.y = abs(i.uv.y-_YMirror)+_YMirror;

                   fixed4 col = tex2D(_MainTex, i.uv); //Get the orginal rendered color

                   //Make changes on the color
                  // float avg = (orgCol.r + orgCol.g + orgCol.b)/3;
                   //fixed4 col = fixed4(avg, avg, avg, 1);

                   return col;
              }
              ENDCG
        }
    }
    FallBack "Diffuse"
}
