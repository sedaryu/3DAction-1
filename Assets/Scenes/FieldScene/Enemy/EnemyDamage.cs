using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    //ステータス
    private EnemyStatus status;

    //エフェクト
    [SerializeField] private GameObject bloodEffect;

    void Awake()
    {
        status = GetComponent<EnemyStatus>();
    }

    public void Hit(Vector3 vector, float attack)
    {
        transform.rotation = Quaternion.LookRotation(-vector.normalized);
        //StartCoroutine(HitMotion(vector, attack));

        status.GoToInvincibleStateIfPossible(); //キャラの状態をDamageに遷移
        status.Animator.SetTrigger("Damage"); //被ダメージの際のアニメーションを実行
        JudgeCollapsed(vector, attack);
        
        Knockback(vector);
        status.Damage(attack);
        status.GoToNormalStateIfPossible(); //キャラの状態をNormalに遷移
    }

    private IEnumerator HitMotion(Vector3 vector, float attack)
    {
        status.GoToInvincibleStateIfPossible(); //キャラの状態をDamageに遷移
        status.Animator.SetTrigger("Damage"); //被ダメージの際のアニメーションを実行
        //hitPointCircle.transform.Translate(vector * status.Param.Weight * 0.25f, Space.World);
        yield return new WaitForSeconds(0.06f);
        //hitPointCircle.transform.Translate(vector * status.Param.Weight, Space.World);
        //yield return new WaitForSeconds(0.03f);
        //hitPointCircle.transform.localPosition = new Vector3(0, 0.001f, 0);
        JudgeCollapsed(vector, attack);
        Knockback(vector);
        status.Damage(attack);
        status.GoToNormalStateIfPossible(); //キャラの状態をNormalに遷移
    }

    //public IEnumerator ScratchHit(float attack)
    //{
    //    status.GoToDamageStateIfPossible();
    //    status.Animator.SetTrigger("Damage");
    //    yield return new WaitForSeconds(0.06f);
    //    Damage(attack);
    //    status.GoToNormalStateIfPossible(); //キャラの状態をNormalに遷移
    //}

    //ノックバック処理
    public void Knockback(Vector3 vector)
    {
        transform.Translate(vector * status.Param.Weight, Space.World); //飛び道具の方向にノックバック
    }

    private void JudgeCollapsed(Vector3 vector, float attack)
    {
        Vector3 avoidSpace = transform.forward * -0.1f * status.Agent.radius;
        Ray[] rays = new Ray[5]
        {
            new Ray(this.transform.position + transform.right * -status.Agent.radius + avoidSpace, vector.normalized),
            new Ray(this.transform.position + transform.right * -0.5f * status.Agent.radius + transform.forward * -0.86f * status.Agent.radius + avoidSpace, vector.normalized),
            new Ray(this.transform.position + transform.forward * -status.Agent.radius + avoidSpace, vector.normalized),
            new Ray(this.transform.position + transform.right * 0.5f * status.Agent.radius + transform.forward * -0.86f * status.Agent.radius + avoidSpace, vector.normalized),
            new Ray(this.transform.position + transform.right * status.Agent.radius + avoidSpace, vector.normalized),
        };

        for (int i = 0; i < 5; i++)
        {
            Debug.DrawRay(rays[i].origin, vector * status.Param.Weight, Color.red);
            Debug.DrawRay(rays[i].origin, avoidSpace, Color.green);
            if (Physics.Raycast(rays[i], out RaycastHit hit, (vector * status.Param.Weight).magnitude - avoidSpace.magnitude, 1 << 8))
            {
                if (hit.transform.gameObject.CompareTag("Obstacle"))
                {
                    status.Damage(attack * 2f);
                }
            }
        }
    }
}
