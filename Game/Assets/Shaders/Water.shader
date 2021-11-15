Shader "Custom/Water"
{
    Properties
    {
        _CubeMap("CubeMap", cube) = "" {}
        _BumpMap("Water Bump", 2D) = "bump" {}
        _BumpMap2("Water Bump2", 2D) = "bump" {}
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert
        #pragma target 3.0

        samplerCUBE _CubeMap;
        sampler2D _BumpMap, _BumpMap2;

        void vert(inout appdata_full v)
        {
            v.vertex.y += sin((abs(v.texcoord.x * 2 - 1) * 3) + sin(_Time.y * 1)) * 0.1;
        }

        struct Input
        {
            float2 uv_BumpMap;
            float2 uv_BumpMap2;
            float3 worldRefl;
            INTERNAL_DATA
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float3 fNormal1 = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap + float2(_Time.y * 0.07, 0.0f)));
            float3 fNormal2 = UnpackNormal(tex2D(_BumpMap2, IN.uv_BumpMap2 - float2(_Time.y * 0.05, 0.0f)));

            o.Normal = fNormal1 + fNormal2;
            o.Albedo = texCUBE(_CubeMap, WorldReflectionVector(IN, o.Normal));
        }
        ENDCG
    }
    FallBack "Diffuse"
}
