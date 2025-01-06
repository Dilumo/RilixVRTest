// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.7568628,fgcg:0.6941177,fgcb:0.6156863,fgca:1,fgde:0.01,fgrn:0,fgrf:2000,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3055,x:32719,y:32712,varname:node_3055,prsc:2|emission-5748-OUT;n:type:ShaderForge.SFN_Tex2d,id:5341,x:32118,y:32494,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_5341,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7010,x:32114,y:32903,ptovrint:False,ptlb:Spectres,ptin:_Spectres,varname:node_7010,prsc:2,ntxv:0,isnm:False|UVIN-5866-OUT;n:type:ShaderForge.SFN_TexCoord,id:3090,x:31451,y:32596,varname:node_3090,prsc:2,uv:0;n:type:ShaderForge.SFN_Time,id:2652,x:31394,y:32802,varname:node_2652,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1851,x:31589,y:32859,varname:node_1851,prsc:2|A-2652-TSL,B-6518-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6518,x:31394,y:32965,ptovrint:False,ptlb:SpeedV,ptin:_SpeedV,varname:node_6518,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_Append,id:5866,x:31905,y:32624,varname:node_5866,prsc:2|A-5974-OUT,B-772-OUT;n:type:ShaderForge.SFN_Multiply,id:5748,x:32453,y:32750,varname:node_5748,prsc:2|A-2362-OUT,B-7010-RGB;n:type:ShaderForge.SFN_Add,id:772,x:31735,y:32710,varname:node_772,prsc:2|A-3090-V,B-1851-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8259,x:31391,y:32462,ptovrint:False,ptlb:SpeedU,ptin:_SpeedU,varname:node_8259,prsc:2,glob:False,v1:0.5;n:type:ShaderForge.SFN_Time,id:2218,x:31383,y:32265,varname:node_2218,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9335,x:31567,y:32289,varname:node_9335,prsc:2|A-2218-TSL,B-8259-OUT;n:type:ShaderForge.SFN_Add,id:5974,x:31723,y:32489,varname:node_5974,prsc:2|A-9335-OUT,B-3090-U;n:type:ShaderForge.SFN_Multiply,id:2362,x:32330,y:32543,varname:node_2362,prsc:2|A-5341-RGB,B-3625-OUT;n:type:ShaderForge.SFN_Vector1,id:3625,x:32118,y:32695,varname:node_3625,prsc:2,v1:0.25;proporder:5341-7010-6518-8259;pass:END;sub:END;*/

Shader "Custom/WormTunnelComplete" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Spectres ("Spectres", 2D) = "white" {}
        _SpeedV ("SpeedV", Float ) = 1
        _SpeedU ("SpeedU", Float ) = 0.5
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _Spectres; uniform float4 _Spectres_ST;
            uniform float _SpeedV;
            uniform float _SpeedU;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 node_2218 = _Time + _TimeEditor;
                float4 node_2652 = _Time + _TimeEditor;
                float2 node_5866 = float2(((node_2218.r*_SpeedU)+i.uv0.r),(i.uv0.g+(node_2652.r*_SpeedV)));
                float4 _Spectres_var = tex2D(_Spectres,TRANSFORM_TEX(node_5866, _Spectres));
                float3 emissive = ((_MainTex_var.rgb*0.25)*_Spectres_var.rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
