//http://wordpress.notargs.com/blog/blog/2015/01/27/unity%E3%82%B3%E3%83%B3%E3%83%94%E3%83%A5%E3%83%BC%E3%83%88%E3%82%B7%E3%82%A7%E3%83%BC%E3%83%80%E3%81%A8%E3%82%A4%E3%83%B3%E3%82%B9%E3%82%BF%E3%83%B3%E3%82%B7%E3%83%B3%E3%82%B0%E3%81%A71%E4%B8%87/
//こちらを全面的に参考にさせて頂きました。
Shader "Custom/SurfaceShader" {
	SubShader{
		ZWrite Off
		Blend One One//加算合成

		Pass {
			CGPROGRAM
			// シェーダーモデルは5.0を指定
			#pragma target 5.0

			#pragma vertex vert
			//#pragma geometry geom
			#pragma fragment frag
			#include "UnityCG.cginc"
	
	StructuredBuffer<double3> posBuffer;

	// 頂点シェーダからの出力
	struct VSOut {
		float4 pos : SV_POSITION;
	};

	// 頂点シェーダ
	VSOut vert(uint id : SV_VertexID)
	{
		// idを元に、弾の情報を取得
		VSOut output;
		output.pos = mul(UNITY_MATRIX_VP, float4(float3(posBuffer[id]), 1));
		return output;
	}


	// ピクセルシェーダー
	fixed4 frag(VSOut i) : COLOR
	{
		return float4(0.0154,0.05,0.3,1);
	}

	ENDCG
 }
	}
}