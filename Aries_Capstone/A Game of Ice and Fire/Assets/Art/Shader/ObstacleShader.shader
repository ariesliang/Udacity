Shader "Custom/ObstacleShader" {
 
    Properties
	{
		_EdgeColor("Edge Color", Color) = (1,1,1,1)
	}

	SubShader
	{
		Pass
		{ 
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION; 
				float3 normal : NORMAL;
			};

			struct v2f
			{ 
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float3 viewDir : TEXCOORD1;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex); 
				o.normal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = normalize(_WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, v.vertex).xyz);
				return o;
			}

			float4 _EdgeColor;

			fixed4 frag (v2f i) : SV_Target
			{
				float NdotV = (1 - dot(i.normal, i.viewDir)) * 5;
				return _EdgeColor * NdotV;
			}

			ENDCG
		}
	}
	Fallback "Diffuse"
}
