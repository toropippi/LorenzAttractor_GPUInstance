using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    //計算するコンピュートシェーダー
    [SerializeField]
    ComputeShader cshader;

    // 弾をレンダリングするシェーダー
    [SerializeField]
    Shader renderingShader;
    Material renderingShader_Material;

    ComputeBuffer posBuffer;

    //[SerializeField]
    int particleNum = 1024 * 1024;

    int kernel_Lorenz;
    int kernel_BufInit;

    
    void Start()
    {
        renderingShader_Material = new Material(renderingShader);

        posBuffer = new ComputeBuffer(particleNum, sizeof(double) * 3);//x,y,z成分で3つ
        kernel_Lorenz = cshader.FindKernel("Lorenz");
        kernel_BufInit = cshader.FindKernel("BufInit");
        cshader.SetBuffer(kernel_Lorenz, "posBuffer", posBuffer);
        cshader.SetBuffer(kernel_BufInit, "posBuffer", posBuffer);
        
        //初期化もGPUで
        cshader.Dispatch(kernel_BufInit, particleNum / 64, 1, 1);

        // GPUバッファをマテリアルに設定
        renderingShader_Material.SetBuffer("posBuffer", posBuffer);
    }

    // Update is called once per frame
    void Update()
    {
        cshader.Dispatch(kernel_Lorenz, particleNum / 64, 1, 1);
    }

    void OnRenderObject()
    {
        // レンダリングを開始
        renderingShader_Material.SetPass(0);
        // n個のオブジェクトをレンダリング
        Graphics.DrawProceduralNow(MeshTopology.Points, particleNum);
    }



    private void OnDestroy()
    {
        //解放
        posBuffer.Release();
    }
}
