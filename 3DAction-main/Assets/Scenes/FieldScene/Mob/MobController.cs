using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobController : MonoBehaviour
{
    //�L�����N�^�[�̈ړ���CharacterController�ōs��
    private CharacterController characterController;

    protected void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�L�����N�^�[�̈ړ����Ǘ����郁�\�b�h
    protected void Move(Vector3 moving)
    {
        characterController.Move(moving);
    }
}
