using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobController : MonoBehaviour
{
    //キャラクターの移動はCharacterControllerで行う
    private CharacterController characterController;
    //アニメーターを格納
    private Animator animator;

    protected void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>(); //アニメーターを取得
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //キャラクターの移動を管理するメソッド
    protected void Move(Vector3 vector)
    {
        characterController.Move(vector * 5.5f * Time.deltaTime); //移動入力を更新
        //キャラクターの向きを更新
        if (vector != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(vector);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1000 * Time.deltaTime);
        }
        //アニメーターに移動スピードを反映
        animator.SetFloat("MoveSpeed" , vector.magnitude);
    }
}
