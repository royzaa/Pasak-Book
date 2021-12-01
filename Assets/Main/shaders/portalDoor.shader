Shader "Unlit/portalDoor"
{
    Properties
    {
       
    [Enum(CullMode)] _CullMode("Cull Mode", Int) = 0
    }

    SubShader
    {
        ColorMask 0
        Zwrite off
        Cull [_CullMode]

        Stencil
        {
            Ref 1
            Pass replace
            Comp always

        }

        Pass
        {

        }
    }
}
