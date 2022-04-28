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
            Debug.Log("플레이어 생성");
            var PlayerPrefab = (GameObject)Resources.Load<GameObject>("Models/" + playerName); // 아바타 model fbx파일 가져오기
            var Playerinstance = PrefabUtility.InstantiatePrefab(PlayerPrefab) as GameObject; // prefab 동적으로 생성

            /* Player  설정(애니메이션, 무브먼트, 포톤 설정 */
            Animator animator = Playerinstance.GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Core/Player Controller");
            animator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
            animator.applyRootMotion = true;

            Playerinstance.AddComponent<PlayerMovement>();
            Playerinstance.AddComponent<TrackingCamera>();
            Playerinstance.AddComponent<SelectPlayer>();

            /* 네트워크 설정 */
            PhotonAnimatorView photonAnimatorView = Playerinstance.AddComponent<PhotonAnimatorView>();
            photonAnimatorView.SetLayerSynchronized(0, PhotonAnimatorView.SynchronizeType.Continuous);
            photonAnimatorView.SetParameterSynchronized("inputValue_Walk", PhotonAnimatorView.ParameterType.Float, PhotonAnimatorView.SynchronizeType.Discrete); // 애니메이터 파라미타

            Playerinstance.AddComponent<PhotonTransformView>();

            PhotonView photonView = Playerinstance.AddComponent<PhotonView>();
            photonView.OwnershipTransfer = OwnershipOption.Fixed;
            photonView.Synchronization = ViewSynchronization.Unreliable;
            photonView.observableSearch = PhotonView.ObservableSearch.AutoFindAll;

            var Playervariant = PrefabUtility.SaveAsPrefabAsset(Playerinstance, "Assets/Resources/" + playerName + ".prefab"); // 설정된 아바타 prefab 만들기
        }
    }

}