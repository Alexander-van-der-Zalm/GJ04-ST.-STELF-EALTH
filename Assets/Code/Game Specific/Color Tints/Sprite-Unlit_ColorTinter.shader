Shader "Custom/Sprite-Unlit_ColorTinter" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		_PaletteTex ("PaletteTexture", 2D) = "white" {}
		_PaletteTexWidth("PaletteWidth",int) = 0
		_PaletteTexHeight("PaletteVersions/Height",int) = 0
	}
	SubShader 
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
		
		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Blend One OneMinusSrcAlpha
		
		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DUMMY PIXELSNAP_ON // WHADISDIS
			#include "UnityCG.cginc" // WHADISDIS
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR; // Vertex color is index
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				float4 color    : COLOR; // Vertex color is index
				half2 texcoord  : TEXCOORD0;
			};
			
			
			// Vertex shader
			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _PaletteTex;
			
			int _PaletteTexWidth;
			int _PaletteTexHeight;
			
			// fragment shader (without lighting/unity hax style)
			fixed4 frag(v2f IN) : SV_Target
			{
				// TODO: Replace color method
				// (V) - Grab the color
				// (X) - Test using the mainsprite with a colorPalette
				// (X) - Check if the r channel returns a color that is not 0,0,0,0
				// (X) - Replace color if previous is true
				// (X) - combine with the index number from the vertex for multi tint palette support
				// (X) - Make it into a function
				// (X) - Check how to use function in other shader scripts
				// (X) - Diffuse version
				// (X) - Normal,Spec version
				
				// original sprite color
				fixed4 c = tex2D(_MainTex, IN.texcoord); 
				//fixed i = (fixed)1/_PaletteTexWidth; // index1 for testing
				// fixed i = IN.color.r; // index1 with instance vertex driven index (variant
				//fixed j = round(c.r*255)/_PaletteTexWidth; // index2
				
				// palette color
				fixed4 r = tex2D(_PaletteTex, half2(c.r,0.5));
				
				if(r.r == 1 && r.g == 1 &&r.a == 0)
				{
					c.rgb *= c.a;// Prepare result
					return c;
				}
				else
				{
					r.rgb *= r.a;// Prepare result
					return r;
				}
				
				//if(test%2 == 0)
				//	return float4(1,0,0,1);
				//else
				//	return float4(0,1,0,1);
					
					// Palette = [5]
					// 2 pallettes
					
					// Dit is gevoelig
					
					
					// Memory efficient
					// 2 texture lookups in de fragment shader
					// 1 texture - [1,256] -> niet directe kleur - maar de index
					// 1 texture - [versie,maxkleuren] -> [2,5]
					// 2x tex2D (2x texture lookups)
					// totaal 3x tex2D
					
					// GPU efficient
					// 1 texture lookup in de fragment shader
					// texture2D[versions,supportedIndices]
					// maar heeft grotere textures COlor palette (support voor 256 indices) - texture[versies,256] is texture[u,v] 244 - mijnmooiekleur
					// pallet1[1,,,,128,,,,,,244,,,]256 breed
					// pallet2[,,,,,] 256 breed
					// pallet3[,,,,,]256 breed
					//=mijnmooiekleur
					// totaal 2x tex2D
					
					// Material nivo
					// lijstje van kleuren palettes
					// lijstje van grayvalues naar index
					// -> texture op shader nivo
					
					// User nivo
					// Bedoel je dit???
					// Reference sprite - 128 = 1,,,,, 244 = 10
					// Sprite (character) 5 verschillende tinten - head,clothing1,etc.
					// ??? - Hoe zit het dan met verschillende palette grotes
				
				//float ind = 2/(fixed)8.0f;
				//float2 index = (0.5,ind);
				//fixed4 colortTint = tex2D(_PaletteTex, index);
				//colortTint.rgb *= c.a;
				//return colortTint;
				//return float4(1,1,1,1);
				//c *= 100;
				//return c;
				
				float i = 4;
				float mx = 256;
				float b = IN.color.r*mx;
				if(b == i)
					return float4(1,1,1,1);
				else
				{		
					IN.color.rgb *= c.a;
					IN.color.a = c.a;
					
					return IN.color;
				}
				//if(c.r*256 == 1)
				//	return float4(0,0,0,1);
				//else
				//{
					//if(c.r == 1)
					//	return float4(1,1,1,1);
				//	float2 index = (c.r*256,1);
					
				//	fixed4 colortTint = tex2D(_PaletteTex, index); // RGB is color // Alpha is value to grab from the 
					//c.rgb *= c.a;
				//	colortTint.rgb *= c.a;
					
				//	return colortTint;
				//}
			}
		ENDCG
		}
	} 
	CustomEditor "ColorTinterMaterialEditor"
	Fallback "Sprites/Default"
}
