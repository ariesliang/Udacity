Shader "Custom/PlaneShader" {
 
    Properties
    { 
		_Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)    // (R, G, B, A) 


		/*
		_MainTex ("Texture", 2D) = "white" {}
		_DisplaceTex ("Display Texture", 2D) = "white" {}
		_Magnitude ("Magnitude", Range(0.0, 1.0)) = 1
		*/
    }

    SubShader
	{  
		Tags
		{
			"LightMode" = "ForwardBase"
		}

		Pass {
			
			CGPROGRAM 
			
			// pragmas 
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			// user defined variables 
			uniform float4 _Color; // uniform keyword here will make this shader more generic 
			
			// unity defined variables 
			uniform float4 _LightColor0;
			// float4x4 _Object2World;
			// float4x4 _World2Object;
			// float4 _WorldSpaceLightPos0; 

			// base input structs 
			struct vertexInput {
				float4 vertex: POSITION; // (x, y, z, w)
				float3 uv: TEXCOORD0;
				float3 normal: NORMAL;
			};

			struct vertexOutput {
				float4 pos: SV_POSITION;  
				float3 uv: TEXCOORD0;
				float3 normal: NORMAL;
			};

			// vertex function 
			vertexOutput vert (vertexInput i) {
				vertexOutput o; 
				o.pos = UnityObjectToClipPos(i.vertex);
				// o.uv = i.uv;
				o.normal = mul(i.normal, (float3x3)unity_WorldToObject); // transform normal from object to world space
				return o;
			}
			
			float4 frag (vertexOutput i) : COLOR {
				return saturate(dot(i.normal, _WorldSpaceLightPos0)) * _Color;
			}
			/*
			//
			// fragment f
			sampler2D _MainTex; 
			sampler2D _DisplaceTex;
			float _Magnitude;

			unction 
			float4 frag (vertexOutput i) : COLOR { // just return a color to the pixel
				// float2 disp = tex2D(_DisplaceTex, i.uv).xy; 
				float2 distuv = float2(i.uv.x + _Time.x * 2, i.uv.y + _Time.x * 2); 
				float2 disp = tex2D(_DisplaceTex, distuv).xy; 
				// disp = ((disp * 2) - 1) * _Magnitude;
				float4 color = tex2D(_MainTex, i.uv + disp); 
				return color;
			}
			*/
			ENDCG
		} 
	} 
	// Fallback "Diffuse"
}
