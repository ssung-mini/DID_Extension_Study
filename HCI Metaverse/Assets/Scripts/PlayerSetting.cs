using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerSetting : MonoBehaviour
{
    public static void doCreatePrefab(string playerName)
    {

        if (!Resources.Load<GameObject>(playerName))
        {
            Debug.Log("�÷��̾� ����");
            var PlayerPrefab = (GameObject)Resources.Load<GameObject>("Models/" + playerName); // �ƹ�Ÿ model fbx���� ��������
            var Playerinstance = PrefabUtility.InstantiatePrefab(PlayerPrefab) as GameObject; // prefab �������� ����

            /* Player  ����(�ִϸ��̼�, �����Ʈ, ���� ���� */
            Animator animator = Playerinstance.GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Core/Player Controller");
            animator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
            animator.applyRootMotion = true;

            Playerinstance.AddComponent<PlayerMovement>();
            Playerinstance.AddComponent<TrackingCamera>();
            Playerinstance.AddComponent<SelectPlayer>();

            /* ��Ʈ��ũ ���� */
            PhotonAnimatorView photonAnimatorView = Playerinstance.AddComponent<PhotonAnimatorView>();
            photonAnimatorView.SetLayerSynchronized(0, PhotonAnimatorView.SynchronizeType.Continuous);
            photonAnimatorView.SetParameterSynchronized("inputValue_Walk", PhotonAnimatorView.ParameterType.Float, PhotonAnimatorView.SynchronizeType.Discrete); // �ִϸ����� �Ķ��Ÿ

            Playerinstance.AddComponent<PhotonTransformView>();

            PhotonView photonView = Playerinstance.AddComponent<PhotonView>();
            photonView.OwnershipTransfer = OwnershipOption.Fixed;
            photonView.Synchronization = ViewSynchronization.Unreliable;
            photonView.observableSearch = PhotonView.ObservableSearch.AutoFindAll;

            var Playervariant = PrefabUtility.SaveAsPrefabAsset(Playerinstance, "Assets/Resources/" + playerName + ".prefab"); // ������ �ƹ�Ÿ prefab �����
        }
    }

}