using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobController : MonoBehaviour
{
    //�p�����[�^�[
    [SerializeField] protected MobParam param;
    //�X�e�[�^�X
    protected MobStatus status;
    //�L�����N�^�[�̈ړ���NavMeshAgent�ōs��
    protected NavMeshAgent agent;
    //�A�j���[�^�[���i�[
    protected Animator animator;

    protected void Awake()
    {
        status = new MobStatus();
        agent = GetComponent<NavMeshAgent>(); //�G�[�W�F���g���擾
        animator = GetComponent<Animator>(); //�A�j���[�^�[���擾
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
