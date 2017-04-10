// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33897,y:32683,varname:node_3138,prsc:2|emission-7241-RGB,tess-9815-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32270,y:32505,ptovrint:False,ptlb:smoke color,ptin:_smokecolor,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_LightVector,id:3273,x:32202,y:32701,varname:node_3273,prsc:2;n:type:ShaderForge.SFN_Color,id:6438,x:32609,y:33089,ptovrint:False,ptlb:shadowColor,ptin:_shadowColor,varname:_smokecolor_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.0989403,c2:0.2543132,c3:0.4485294,c4:1;n:type:ShaderForge.SFN_LightAttenuation,id:5850,x:32712,y:33255,varname:node_5850,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:3167,x:32202,y:32927,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:9043,x:32507,y:32869,varname:node_9043,prsc:2,dt:0|A-3273-OUT,B-3167-OUT;n:type:ShaderForge.SFN_Clamp01,id:9253,x:32691,y:32869,varname:node_9253,prsc:2|IN-9043-OUT;n:type:ShaderForge.SFN_Multiply,id:9319,x:32836,y:33031,varname:node_9319,prsc:2|A-9253-OUT,B-6438-RGB,C-5850-OUT;n:type:ShaderForge.SFN_Add,id:5586,x:33001,y:33045,varname:node_5586,prsc:2|A-9319-OUT,B-1942-RGB;n:type:ShaderForge.SFN_AmbientLight,id:1942,x:32909,y:33247,varname:node_1942,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2965,x:32933,y:32869,varname:node_2965,prsc:2|A-7241-RGB,B-5586-OUT;n:type:ShaderForge.SFN_TexCoord,id:9227,x:33153,y:31487,varname:node_9227,prsc:2,uv:0;n:type:ShaderForge.SFN_RemapRange,id:9171,x:33442,y:31561,varname:node_9171,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-9227-UVOUT;n:type:ShaderForge.SFN_Length,id:4201,x:33655,y:31703,varname:node_4201,prsc:2|IN-9171-OUT;n:type:ShaderForge.SFN_Floor,id:5709,x:33929,y:31794,varname:node_5709,prsc:2|IN-4201-OUT;n:type:ShaderForge.SFN_OneMinus,id:8664,x:34076,y:31843,varname:node_8664,prsc:2|IN-5709-OUT;n:type:ShaderForge.SFN_Slider,id:9815,x:33520,y:33142,ptovrint:False,ptlb:node_9815,ptin:_node_9815,varname:node_9815,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:10,max:10;n:type:ShaderForge.SFN_Color,id:4192,x:32356,y:32273,ptovrint:False,ptlb:node_4192,ptin:_node_4192,varname:node_4192,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Lerp,id:9095,x:33223,y:32417,varname:node_9095,prsc:2|A-4192-RGB,B-7241-RGB,T-9234-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:9234,x:32997,y:32495,ptovrint:False,ptlb:boost,ptin:_boost,varname:node_9234,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-7950-OUT,B-9208-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9208,x:32744,y:32693,ptovrint:False,ptlb:node_9208,ptin:_node_9208,varname:node_9208,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Vector1,id:7950,x:32591,y:32587,varname:node_7950,prsc:2,v1:0;proporder:7241-6438-9815-4192-9234-9208;pass:END;sub:END;*/

Shader "Shader Forge/smoke" {
    Properties {
        _smokecolor ("smoke color", Color) = (0.07843138,0.3921569,0.7843137,1)
        _shadowColor ("shadowColor", Color) = (0.0989403,0.2543132,0.4485294,1)
        _node_9815 ("node_9815", Range(0, 10)) = 10
        _node_4192 ("node_4192", Color) = (0.5,0.5,0.5,1)
        [MaterialToggle] _boost ("boost", Float ) = 0
        _node_9208 ("node_9208", Float ) = 1
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
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "Tessellation.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 5.0
            uniform float4 _smokecolor;
            uniform float _node_9815;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    return o;
                }
                float Tessellation(TessVertex v){
                    return _node_9815;
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float3 emissive = _smokecolor.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
