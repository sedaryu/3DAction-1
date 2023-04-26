using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobController : MonoBehaviour
{
    //キャラクターの移動はCharacterControllerで行う
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

    //キャラクターの移動を管理するメソッド
    protected void Move(Vector3 moving)
    {
        characterController.Move(moving);
    }
}
