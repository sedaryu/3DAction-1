using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <Summary>
/// PlayerParam�EGunParam�ESmashParam���擾���i�[����
/// Param�̒l�𑝌�������ړI�̃N���X
/// </Summary>
public class PlayerStatus : MonoBehaviour
{
    //���
    //public Dictionary<string, bool> Staus { get; private set; } = new Dictionary<string, bool>() 
    //{ { "IsMovable", true }, { "IsShootable", true }, { "IsDamageable", true } };
    public bool IsMobable { get; private set; } = true;
    public bool IsShootable { get; private set; } = true;
    public bool IsDamageable { get; private set; } = true;

    public IEnumerator WaitForStatusTransition(bool state, float time)
    {
        state = false;
        yield return new WaitForSeconds(time);
        state = true;
    }

    //�v���C���[�p�����[�^�[
    public PlayerParam PlayerParam
    {
        get => _playerParam;
    }
    private PlayerParam _playerParam;

    //�e�p�����[�^�[
    public GunParam GunParam
    {
        get => _gunParam;
    }
    private GunParam _gunParam;

    //�X�}�b�V���Z�p�����[�^�[
    public SmashParam SmashParam
    {
        get => _smashParam;
    }
    [SerializeField] private SmashParam _smashParam;

    //�����ݒ�p�����[�^�[
    [SerializeField] private PlayerParam initialPlayerParam;
    [SerializeField] private GunParam initialGunParam;

    //�e�̃G�t�F�N�g
    public ParticleSystem GunEffect
    {
        get => _gunEffect;
    }
    private ParticleSystem _gunEffect;

    protected void Awake()
    {
        _playerParam = new PlayerParam(initialPlayerParam);
        _gunParam = new GunParam(initialGunParam);

        SettingGunPrefab(); //�e�̃I�u�W�F�N�g�𐶐����A�ʒu�𒲐�����
    }

    /// <summary>
    /// �e�̃I�u�W�F�N�g�𐶐����A�ʒu�𒲐�����ړI�̃��\�b�h
    /// </summary>
    private void SettingGunPrefab()
    {
        GameObject gunPrefab = Instantiate(GunParam.GunPrefab);
        string path = "Armature | Humanoid/Hips/Spine/Spine1/Spine2/RightShoulder/RightArm/RightForeArm/RightHand";
        gunPrefab.transform.parent = GameObject.Find("Player").transform.Find(path);
        gunPrefab.transform.localPosition = new Vector3(0, 0.25f, 0);
        gunPrefab.transform.localRotation = Quaternion.Euler(-90, 180, -90);
        gunPrefab.transform.localScale = new Vector3(3, 3, 3);
        _gunEffect = gunPrefab.transform.Find("ShotEffect").GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// �󂯂��_���[�W�Ԃ�HitPoint������������ړI�̃��\�b�h
    /// </summary>
    /// <param name="damage">�󂯂��_���[�W��</param>
    /// <returns>�_���[�W�̌���HitPoint��0�ȉ��ɂȂ���(���S������)true�A�����łȂ����false��Ԃ�</returns>
    public void Damage(float damage)
    {
        _playerParam.HitPoint -= damage;
        if (PlayerParam.HitPoint <= 0) Destroy(gameObject);
    }

    /// <summary>
    /// �U���E�����[�h�̍ۂ�Bullet�̑������Ǘ�����ړI�̃��\�b�h
    /// </summary>
    /// <param name="bullet">����܂����U�����e��̗�</param>
    public void SetBullet(float bullet)
    { 
        _gunParam.Bullet += (int)bullet;
    }
}
