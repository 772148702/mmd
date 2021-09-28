using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class euler : MonoBehaviour
{
    // Start is called before the first frame update
    public RenderTexture DyeRT;
    public RenderTexture DyeRT2;
    public RenderTexture VelocityRT;
    public RenderTexture VelocityRT2;
    public RenderTexture DivergenceRT;
    public RenderTexture PressureRT;
    public RenderTexture PressureRT2;

    public Material InitDyeMat;
    public Material InitVelocityMat;
    public Material AdvectionMat;
    public Material SplatMat;
    public Material DivergenceMat;
    public Material PressureMat;
    public Material SubtractMat;
    public Material DisplayMat;

    double MouseX = 0, MouseY = 0, MouseDX = 0, MouseDY = 0;
    int MouseDown = 0;
    void Start()
    {
        Graphics.Blit(null, DyeRT, InitDyeMat);
        Graphics.Blit(null, VelocityRT, InitVelocityMat);
        Graphics.Blit(null, PressureRT);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) MouseDown = 1;
        if (Input.GetMouseButtonUp(0)) MouseDown = 0;

        MouseDX = Input.mousePosition.x - MouseX;
        MouseDY = Input.mousePosition.y - MouseY;
        MouseX = Input.mousePosition.x;
        MouseY = Input.mousePosition.y;


    }
    void Process()
    {
        Graphics.Blit(VelocityRT, VelocityRT2);
        AdvectionMat.SetTexture("_VelocityTex", VelocityRT2);
        Graphics.Blit(VelocityRT2, VelocityRT,AdvectionMat);

        SplatMat.SetTexture("_VelocityTex", VelocityRT);
        SplatMat.SetFloat("PositionX",  (float)MouseX / Screen.width);
        SplatMat.SetFloat("PositionY",  (float)MouseY / Screen.height);
        SplatMat.SetFloat("PositionDx", (float)MouseDX / Screen.width);
        SplatMat.SetFloat("PositionDy", (float)MouseDY / Screen.height);
        SplatMat.SetInt("MouseDown", MouseDown);

        Graphics.Blit(null, VelocityRT2, SplatMat);
        Debug.Log(MouseX);
        Debug.Log(MouseDown);
  

        DivergenceMat.SetTexture("_VelocityTex", VelocityRT2);
        Graphics.Blit(VelocityRT2, DivergenceRT, DivergenceMat);
       
        PressureMat.SetTexture("_DivergenceTex", DivergenceRT);
        for (int i = 0; i < 20; ++i)
        {
            Graphics.Blit(PressureRT, PressureRT2);
            PressureMat.SetTexture("_PressureTex", PressureRT2);
            Graphics.Blit(DivergenceRT, PressureRT, PressureMat);
        }

        SubtractMat.SetTexture("_VelocityTex", VelocityRT2);
        SubtractMat.SetTexture("_PressureTex", PressureRT);
        Graphics.Blit(VelocityRT2, VelocityRT, SubtractMat);

        Graphics.Blit(DyeRT, DyeRT2);
        DisplayMat.SetTexture("_MainTex", DyeRT2);
        DisplayMat.SetTexture("_VelocityTex", VelocityRT);
        Graphics.Blit(DyeRT2, DyeRT, DisplayMat);

    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Process();
        Graphics.Blit(DyeRT, destination);
    }
}
