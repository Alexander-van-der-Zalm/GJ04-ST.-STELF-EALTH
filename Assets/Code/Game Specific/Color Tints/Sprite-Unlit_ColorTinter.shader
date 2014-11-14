Shader "Custom/Sprite-Unlit_ColorTinter" 
{
	Properties 
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		_PaletteTex ("PaletteTexture", 2D) = "white" {}
		_PaletteTexWidth("PaletteWidth",int) = 0
		_PaletteTexHeight("PaletteVersions/Height",int) = 0
		_PaletteTexHeight2("PaletteVersions/Height2	",int) = 0
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
				fixed4 color    : COLOR; // Vertex color is index
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
				// original sprite color
				fixed4 c = tex2D(_MainTex, IN.texcoord); 
				
				// palette color
				fixed4 palette = tex2D(_PaletteTex, half2(c.r,0.5));
				
				// If the original fragment(pixelToBe) is tranparent
				// or if it matches the code from the palette
				if(c.a == 0 || (palette.r == 1 && palette.b == 1 && palette.g == 0&& palette.a == 0))//c.a == 0 ||
				{ // Return original color
					c.rgb *= c.a;// Prepare result
					return c;
				}
				else // Return the replaced color
				{
					palette.rgb *= palette.a;// Prepare result
					return palette;
				}
				// TODO: Replace color method
				// (V) - Grab the color
				// (V) - Test using the mainsprite with a colorPalette
				// (V) - Check if the r channel returns a color that is not 0,0,0,0
				// (V) - Replace color if previous is true
				// (X) - combine with the index number from the vertex for multi tint palette support
				// (X) - Make it into a function
				// (X) - Check how to use function in other shader scripts
				// (X) - Diffuse version
				// (X) - Normal,Spec version
				
				// Original Sprite Untouched

				// GPU efficient
				// 1 texture lookup in de fragment shader
				// texture2D[versions,supportedIndices]
				// maar heeft grotere textures Color palette (support voor 256 indices) - texture[versies,256] is texture[u,v] 244 - mijnmooiekleur
				// pallet1r[1,,,,128,,,,,,244,,,]256 breed 
				// pallet1g[1,,,,128,,,,,,244,,,]
				// pallet1b[1,,,,128,,,,,,244,,,]
				// pallet1a[1,,,,128,,,,,,244,,,]
				// pallet2[,,,,,] 256 breed
				// pallet3[,,,,,]256 breed
				//=mijnmooiekleur
				// totaal 2x tex2D
				// + Works
				// + Doesnt use a lot of memory
				// + Short and efficient
				// - Just uses one channel for lookups
				
				// Memory efficient (HorseWash)
				// 2 texture lookups in de fragment shader
				// 1 texture - [1,256] -> niet directe kleur - maar de index
				// 1 texture - [versie,maxkleuren] -> [2,5]
				// 2x tex2D (2x texture lookups)
				// totaal 3x tex2D
				// - Doesnt save a significant amount of memory
				// - Still uses just one channel
				
				// Sprite changing methods
				
				// GPU & Memory efficient
				// Create processed sprite for every sprite used this way
				// - Store index in smallest red channel range
				// - Store 
				
					// Material nivo
					// lijstje van kleuren palettes
					// lijstje van grayvalues naar index
					// -> texture op shader nivo
					
					// User nivo
					// Bedoel je dit???
					// Reference sprite - 128 = 1,,,,, 244 = 10
					// Sprite (character) 5 verschillende tinten - head,clothing1,etc.
					// ??? - Hoe zit het dan met verschillende palette grotes		
			}
		ENDCG
		}
	} 
	CustomEditor "ColorTinterMaterialEditor"
	Fallback "Sprites/Default"
}
