// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33369,y:32715,varname:node_4013,prsc:2|emission-8959-OUT;n:type:ShaderForge.SFN_Tex2d,id:4632,x:32560,y:32734,ptovrint:False,ptlb:node_4632,ptin:_node_4632,varname:node_4632,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:372edc16de2a71548851fef17f2a2ad8,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Time,id:6870,x:32307,y:33021,varname:node_6870,prsc:2;n:type:ShaderForge.SFN_Sin,id:3176,x:32535,y:33015,varname:node_3176,prsc:2|IN-6870-TSL;n:type:ShaderForge.SFN_RemapRange,id:2366,x:32752,y:33039,varname:node_2366,prsc:2,frmn:-1,frmx:1,tomn:0.8,tomx:1|IN-3176-OUT;n:type:ShaderForge.SFN_Multiply,id:1678,x:32989,y:32902,varname:node_1678,prsc:2|A-4632-RGB,B-2366-OUT;n:type:ShaderForge.SFN_Color,id:8914,x:32696,y:32446,ptovrint:False,ptlb:dark,ptin:_dark,varname:node_8914,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.2352941,c2:0.3764706,c3:0.6039216,c4:1;n:type:ShaderForge.SFN_Lerp,id:8959,x:33012,y:32624,varname:node_8959,prsc:2|A-8914-RGB,B-1678-OUT,T-3176-OUT;proporder:4632-8914;pass:END;sub:END;*/

Shader "Shader Forge/eau" {
    Properties {
        _node_4632 ("node_4632", 2D) = "white" {}
        _dark ("dark", Color) = (0.2352941,0.3764706,0.6039216,1)
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
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node_4632; uniform float4 _node_4632_ST;
            uniform float4 _dark;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 _node_4632_var = tex2D(_node_4632,TRANSFORM_TEX(i.uv0, _node_4632));
                float4 node_6870 = _Time + _TimeEditor;
                float node_3176 = sin(node_6870.r);
                float3 emissive = lerp(_dark.rgb,(_node_4632_var.rgb*(node_3176*0.09999999+0.9)),node_3176);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
