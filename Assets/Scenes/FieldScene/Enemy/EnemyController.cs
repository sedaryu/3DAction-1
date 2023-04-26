using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class EnemyController : MobController
{
    //�X�e�[�^�X
    private EnemyStatus status;
    //�ǐՂ���v���C���[
    private Transform player;
    //HitPoint�\��UI
    private GameObject hitPointCircle;

    void Awake()
    {
        status = GetComponent<EnemyStatus>();
        hitPointCircle = transform.Find("HitPointCircle").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>(); //�v���C���[�̈ʒu���擾
    }

    // Update is called once per frame
    void Update()
    {
        if (status.IsMovable) status.Agent.destination = player.position; //�v���C���[��ǐ�
        status.Animator.SetFloat("MoveSpeed", status.Agent.velocity.magnitude); //�A�j���[�^�[�Ɉړ��X�s�[�h�𔽉f
        //if (JudgeGrounded()) agent.destination = player.position; //�������j����̏ꍇ
    }

    public void Hit(Vector3 vector, float attack)
    {
        status.GoToDamageStateIfPossible(); //�L�����̏�Ԃ�Damage�ɑJ��
        status.Animator.SetTrigger("Damage");
        transform.rotation = Quaternion.LookRotation(-vector.normalized);
        StartCoroutine(FreezeMotion(vector, attack));
    }

    private IEnumerator FreezeMotion(Vector3 vector, float attack)
    {
        hitPointCircle.transform.Translate(vector * status.Param.Weight * 0.25f, Space.World);
        yield return new WaitForSeconds(0.03f);
        hitPointCircle.transform.Translate(vector * status.Param.Weight, Space.World);
        yield return new WaitForSeconds(0.03f);
        hitPointCircle.transform.localPosition = new Vector3(0, 0.001f, 0);
        JudgeCollapsed(vector);
        Knockback(vector);
        Damage(attack);
        status.GoToNormalStateIfPossible(); //�L�����̏�Ԃ�Normal�ɑJ��
    }

    //�m�b�N�o�b�N����
    public void Knockback(Vector3 vector)
    {
        transform.Translate(vector * status.Param.Weight, Space.World); //��ѓ���̕����Ƀm�b�N�o�b�N
    }

    //�_���[�W����
    protected override void Damage(float attack)
    {
        status.Damage(attack);
    }

    private void JudgeCollapsed(Vector3 vector)
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
            if (Physics.Raycast(rays[i], out RaycastHit hit, (vector * status.Param.Weight).magnitude - avoidSpace.magnitude, 1 << 7 | 1 << 8))
            {
                if (hit.transform.gameObject.CompareTag("Obstacle"))
                {
                    Debug.Log($"ObstacleCollapsel!!! : {hit.transform.gameObject.name}");
                }

                if (hit.transform.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log($"EnemyCollapse!!! : {hit.transform.gameObject.name}");
                }

                break;
            }
        }
    }

    protected bool JudgeGrounded() //�ڒn���菈�����s��
    {
        Ray[] rays = new Ray[4]
        { 
            new Ray(this.transform.position - new Vector3(transform.localScale.x * 0.5f, 0, 0), Vector3.down),
            new Ray(this.transform.position + new Vector3(transform.localScale.x * 0.5f, 0, 0), Vector3.down),
            new Ray(this.transform.position - new Vector3(0, 0, transform.localScale.z * 0.5f), Vector3.down),
            new Ray(this.transform.position + new Vector3(0, 0, transform.localScale.z * 0.5f), Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            Debug.DrawRay(rays[i].origin, Vector3.down * 0.3f, Color.red);
            if (!Physics.Raycast(rays[i], 0.3f, 1 << 6))
            {
                FallFromPlane();
                return false;
            }
        }
        return true;
    }

    //��������
    private void FallFromPlane()
    {
        status.Agent.enabled = false; //NavMeshAgent������
        //Rigidbody��L����
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
        StartCoroutine(FallingDeath()); //���ŏ��������s
    }

    //�������莞�Ԍ����
    private IEnumerator FallingDeath()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
