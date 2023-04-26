using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCreator : MonoBehaviour
{
    private GunParam param;

    private void Awake()
    {
        param = GetComponentInParent<PlayerShoot>().Param;
    }

    private void Start()
    {
        CreateMeshCollider();
    }

    //GunParamの値に対応したメッシュコリダーの作成
    public void CreateMeshCollider()
    {
        // メッシュの作成  
        Mesh mesh = new Mesh();

        // 頂点座標配列をメッシュにセット  
        mesh.SetVertices(new Vector3[]
        {
            new Vector3 (0, 0.0005f, 0), //0
            new Vector3 (param.Range * -0.5f, 0.0005f, param.Reach), //1
            new Vector3 (param.Range * 0.5f, 0.0005f, param.Reach), //2
            new Vector3 (0, 0, 0), //3
            new Vector3 (param.Range * -0.5f, 0, param.Reach), //4
            new Vector3 (param.Range * 0.5f, 0, param.Reach), //5
        });

        // インデックス配列をメッシュにセット  
        mesh.SetTriangles(new int[]
        {
            0, 1, 2, 0, 2, 5, 0, 5, 3, 0, 3, 4, 0, 4, 1, 1, 5, 2, 1, 4, 5, 3, 5, 4
        }, 0);

        // MeshFilterを通してメッシュをMeshRendererにセット  
        MeshFilter filter = GetComponent<MeshFilter>();
        filter.sharedMesh = mesh;
    }
}
