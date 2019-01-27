Shader "Custom/TimeTransition"
{
 Properties
    {
        _MainTex ("Main texture", 2D) = "white" {}
        _NoiseTex ("Noise texture", 2D) = "grey" {}
		_OffsetX("Offset along X", Range(-1, 1)) = 1
        _SpeedX("Speed along X", Range(0, 5)) = 1
		_Adjustment("Adjusting", Range(0,1))=0
		_Disperse("Disperse out", Range(0,1))=0
		_Direction("Direction", Range(-1,1))=1


        _SpeedY("Speed along Y", Range(0, 5)) = 1
        _OffsetY("Offset along Y", Range(0, 15)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
		Cull Off
        LOD 200
		Pass
		{
        CGPROGRAM
   //     #pragma surface surf Standard fullforwardshadows
	    #pragma vertex vert
		#pragma fragment frag 
        #pragma target 3.0
		#include "UnityCG.cginc"

        sampler2D _MainTex;
        sampler2D _NoiseTex;
        float _SpeedX;
        float _SpeedY;
		float _OffsetX;
		float _OffsetY;
		float _Direction;

		float _Adjustment;
		float _Disperse;
        struct v2f {
            half4 pos : SV_POSITION;
            half2 uv : TEXCOORD0;
        };
 
        fixed4 _MainTex_ST;
 
		v2f vert(appdata_base v) {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
            return o;
        }

		half4 frag(v2f i) : COLOR {
                half2 uv = i.uv;
                uv.x = (uv.x + (_Time.y * _SpeedX)*_Direction)*_OffsetX;
                uv.y = (uv.y + _Time.y * _SpeedY)*_OffsetY;
                half noiseVal = tex2D(_NoiseTex, uv).r;
				half test = tex2D(_MainTex, uv).r;

				clip(step(test,_Disperse)-1);

				half4 Final = (half4)(1, 0, 0, 0);

                return Final;
        }
        ENDCG
		}
    }
    FallBack "Diffuse"
}
