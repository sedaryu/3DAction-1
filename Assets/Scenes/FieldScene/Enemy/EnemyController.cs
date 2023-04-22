using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MobController
{
    //�p�����[�^�[
    private EnemyParam param;
    //�ǐՂ���v���C���[
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        param = (EnemyParam)_param;
        player = GameObject.Find("Player").GetComponent<Transform>(); //�v���C���[�̈ʒu���擾
        agent.speed = Random.Range(0.5f, 2.0f); //param.Speed; //�p�����[�^�[����X�s�[�h���擾
    }

    // Update is called once per frame
    void Update()
    {
        status.GoToNormalStateIfPossible();
        if (status.IsMovable) agent.destination = player.position; //�v���C���[��ǐ�
        //if (JudgeGrounded()) agent.destination = player.position; //�������j����̏ꍇ
    }

    //�U���q�b�g���̏���
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile")) //��ѓ�����������ꍇ
        {
            ProjectileParam projectileParam = other.GetComponent<ProjectileController>().Param;
            Knockback(other.transform.forward.normalized * param.Weight * projectileParam.Knockback); //�m�b�N�o�b�N�����s
            Damage(projectileParam.Attack); //�_���[�W���������s
            Destroy(other.gameObject); //��ѓ��������
            status.GoToDamageStateIfPossible(); //�L�����̏�Ԃ�Damage�ɑJ��
        }
    }

    //�ǂ߂荞�ݎ��̏���
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle") && status.IsDamageble) //��Q���ɂ߂荞�񂾏ꍇ
        {
            Debug.Log("Damage!!");
        }
    }

    //�m�b�N�o�b�N����
    public void Knockback(Vector3 vector)
    { 
        transform.Translate(vector, Space.World); //��ѓ���̕����Ƀm�b�N�o�b�N
    }

    //�_���[�W����
    protected override void Damage(float attack)
    {
        param.HitPoint -= attack;
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
        agent.enabled = false; //NavMeshAgent������
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
