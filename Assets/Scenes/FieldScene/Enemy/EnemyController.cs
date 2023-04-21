using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MobController
{
    //�ǐՂ���v���C���[
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>(); //�v���C���[�̈ʒu���擾
        agent.speed = Random.Range(0.5f, 2.0f); //param.Speed; //�p�����[�^�[����X�s�[�h���擾
    }

    // Update is called once per frame
    void Update()
    {
        if (status.IsMovable) agent.destination = player.position;
        //if (JudgeGrounded()) agent.destination = player.position; //�������j����̏ꍇ
    }

    //�U���q�b�g���̏���
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile")) //��ѓ�����������ꍇ
        {
            Knockback(other.transform.forward.normalized * 1.5f); //�m�b�N�o�b�N�����s
            Destroy(other.gameObject); //��ѓ��������
            StartCoroutine(FrameOfDamageState());
        }

        if (other.CompareTag("Obstacle") && status.IsDamageble) //��Q���ɂ߂荞�񂾏ꍇ
        {
            Debug.Log("BANG!!");
        }
    }

    //�m�b�N�o�b�N����
    public void Knockback(Vector3 vector)
    { 
        transform.Translate(vector, Space.World); //��ѓ���̕����Ƀm�b�N�o�b�N
    }

    private IEnumerator FrameOfDamageState() //3�t���[�������L�����̏�Ԃ�Damage�ɂȂ�
    { 
        status.GoToDamageStateIfPossible();
        for (int i = 0; i < 3; i++)
        {
            yield return null;
        }
        status.GoToNormalStateIfPossible();
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
