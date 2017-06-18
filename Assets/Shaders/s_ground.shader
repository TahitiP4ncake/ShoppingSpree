// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33206,y:32730,varname:node_3138,prsc:2|emission-1603-R;n:type:ShaderForge.SFN_Tex2d,id:1603,x:32710,y:32634,ptovrint:False,ptlb:groundTex,ptin:_groundTex,varname:node_1603,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0ff2deaa756a24b4fb94acc2d138bf91,ntxv:2,isnm:False|UVIN-7064-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:2920,x:32119,y:32519,varname:node_2920,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:7064,x:32477,y:32551,varname:node_7064,prsc:2,spu:-1,spv:0|UVIN-2920-UVOUT,DIST-3558-OUT;n:type:ShaderForge.SFN_Time,id:1548,x:32023,y:32795,varname:node_1548,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3558,x:32277,y:32779,varname:node_3558,prsc:2|A-1548-T,B-1719-OUT;n:type:ShaderForge.SFN_Slider,id:1719,x:32069,y:32968,ptovrint:False,ptlb:speed,ptin:_speed,varname:node_1719,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1282051,max:5;proporder:1603-1719;pass:END;sub:END;*/

Shader "Shader Forge/s_ground" {
    Properties {
        _groundTex ("groundTex", 2D) = "black" {}
        _speed ("speed", Range(0, 5)) = 0.1282051
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _groundTex; uniform float4 _groundTex_ST;
            uniform float _speed;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
////// Emissive:
                float4 node_1548 = _Time + _TimeEditor;
                float2 node_7064 = (i.uv0+(node_1548.g*_speed)*float2(-1,0));
                float4 _groundTex_var = tex2D(_groundTex,TRANSFORM_TEX(node_7064, _groundTex));
                float3 emissive = float3(_groundTex_var.r,_groundTex_var.r,_groundTex_var.r);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
