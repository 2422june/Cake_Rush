#ifndef OUTLINE_INCLUDED
#define OUTLINE_INCLUDED

#include "UnityCG.cginc"

half3 _OutlineColor;
half _OutlineWidth, _OutlineFactor, _OutlineBasedVertexColorR;
half4 _OutlineDashParams;

//#if OUTLINE_DASH_NOT_USED
//half2 _OutlineDashCenter;
//half _OutlineDashRotate;
//#endif

struct v2f
{
	float4 pos : SV_POSITION;
	float4 scrpos : TEXCOORD0;
	UNITY_VERTEX_OUTPUT_STEREO
};
v2f vert (appdata_full v)
{
	v2f o;
	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

	// extrude direction
	float3 dir1 = normalize(v.vertex.xyz);
	float3 dir2 = v.normal;
	float3 dir = lerp(dir1, dir2, _OutlineFactor);
	dir = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, dir));

	// is outline based on R channel of vertex color ?
	float ow = _OutlineWidth * v.color.r * _OutlineBasedVertexColorR + (1.0 - _OutlineBasedVertexColorR);

	// view space vertex position
	float3 vp = UnityObjectToViewPos(v.vertex);
	o.pos = UnityViewToClipPos(vp + dir * -vp.z * ow * 0.001);
	o.scrpos = ComputeScreenPos(o.pos);
	return o;
}
//float2 PolarCoordinates (float2 uv, float2 center, float radialScale, float lengthScale)
//{
//	float2 delta = uv - center;
//	float radius = length(delta) * 2 * radialScale;
//	float angle = atan2(delta.x, delta.y) * 1.0 / 6.28 * lengthScale;
//	return float2(radius, angle);
//}
//float3 Checkerboard (float2 uv, float2 freq)
//{
//	uv = (uv + 0.5) * freq;
//	float4 derivatives = float4(ddx(uv), ddy(uv));
//	float2 len = sqrt(float2(dot(derivatives.xz, derivatives.xz), dot(derivatives.yw, derivatives.yw)));
//	float2 dist = 4.0 * abs(frac(uv + 0.25) - 0.5) - 1.0;
//	float fl = sqrt(clamp(1.1 - max(len.x, len.y), 0.0, 1.0));
//	float2 valpha = clamp(dist * (0.35 / len.xy), -1.0, 1.0);
//	float alpha = saturate(0.5 + 0.5 * valpha.x * valpha.y * fl);
//	return lerp(0.0, 1.0, alpha.xxx);
//}
//float2 RotateRadians (float2 uv, float2 center, float rotation)
//{
//	// rotation matrix
//	uv -= center;
//	float s = sin(rotation);
//	float c = cos(rotation);
//
//	// center rotation matrix
//	float2x2 m = float2x2(c, -s, s, c);
//	m *= 0.5;
//	m += 0.5;
//	m = m * 2.0 - 1.0;
//
//	// multiply uv by the rotation matrix
//	uv = mul(uv, m);
//	uv += center;
//	return uv;
//}
float4 frag (v2f input) : SV_TARGET
{
//#if OUTLINE_DASH_NOT_USED
//	float4 scrpos = float4(input.scrpos.xy / input.scrpos.w, 0, 0);
//	float2 rr = RotateRadians(scrpos.xy, 0.5, _OutlineDashRotate);
//	float2 pcuv = PolarCoordinates(rr, _OutlineDashCenter, 1.0, 1.0);
//	float3 a = Checkerboard(pcuv, float2(0, 32));
//	clip(0.5 - a);
//#endif

#if OUTLINE_DASH
	float2 pos = input.pos.xy + _Time.y * _OutlineDashParams.xy;
	float skip = sin(_OutlineDashParams.z * abs(distance(0, pos))) + _OutlineDashParams.w;
	clip(skip);
#endif
	return float4(_OutlineColor, 1.0);
}

#endif