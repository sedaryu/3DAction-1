using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class TransObstacle : MonoBehaviour
{
    //���C���΂���
    private Transform player;
    //�O�t���[�����C��Hit����Obstacle
    private MakeTransparent obstacle;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        Vector3 directionToPlayer = player.position - transform.position;
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, directionToPlayer.magnitude, 1 << 8))
        {
            obstacle = hit.transform.gameObject.GetComponent<MakeTransparent>();
            obstacle.Transparent();
        }
        else
        {
            if (obstacle != null)
            {
                obstacle.CancelTransparent();
                obstacle = null;
            }
        }
    }
}
