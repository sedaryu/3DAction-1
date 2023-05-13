using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCreator : MonoBehaviour
{
    private void Start()
    {
        GunParam param = GetComponentInParent<PlayerParameter>().GunParam;
        CreateMeshCollider(param.Range, param.Reach);
    }

    //GunParamの値に対応したメッシュコリダーの作成
    public void CreateMeshCollider(float range, float reach)
    {
        // メッシュの作成  
        Mesh mesh = new Mesh();

        // 頂点座標配列をメッシュにセット  
        mesh.SetVertices(new Vector3[]
        {
            new Vector3 (0, 0.01f, 0), //0
            new Vector3 (range * -0.5f, 0.01f, reach), //1
            new Vector3 (range * 0.5f, 0.01f, reach), //2
            new Vector3 (0, 0, 0), //3
            new Vector3 (range * -0.5f, 0, reach), //4
            new Vector3 (range * 0.5f, 0, reach), //5
        });

        // インデックス配列をメッシュにセット  
        mesh.SetTriangles(new int[]
        {
            0, 1, 2, 0, 2, 5, 0, 5, 3, 0, 3, 4, 0, 4, 1, 1, 5, 2, 1, 4, 5, 3, 5, 4
        }, 0);

        GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
