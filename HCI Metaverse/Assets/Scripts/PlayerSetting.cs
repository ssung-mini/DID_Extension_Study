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
            var PlayerPrefab = (GameObject)Resources.Load<GameObject>("Models/" + playerName);
            var Playerinstance = PrefabUtility.InstantiatePrefab(PlayerPrefab) as GameObject;

            Animator animator = Playerinstance.GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Player Controller");
            animator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
            animator.applyRootMotion = true;

            Rigidbody rigidbody = Playerinstance.AddComponent<Rigidbody>();
            rigidbody.angularDrag = 30;
            rigidbody.mass = 9.8f;
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            CapsuleCollider capsuleCollider = Playerinstance.gameObject.AddComponent<CapsuleCollider>();
            capsuleCollider.center = new Vector3(0, 1, 0);
            capsuleCollider.material = Resources.Load<PhysicMaterial>("Player Material");
            capsuleCollider.radius = 0.2f;
            capsuleCollider.height = 2;

            Playerinstance.AddComponent<PlayerMovement>();
            Playerinstance.AddComponent<CameraSetUp>();

            PhotonAnimatorView photonAnimatorView = Playerinstance.AddComponent<PhotonAnimatorView>();
            photonAnimatorView.SetLayerSynchronized(0, PhotonAnimatorView.SynchronizeType.Continuous);
            photonAnimatorView.SetParameterSynchronized("inputValue_Walk", PhotonAnimatorView.ParameterType.Float, PhotonAnimatorView.SynchronizeType.Discrete);

            Playerinstance.AddComponent<PhotonTransformView>();

            PhotonView photonView = Playerinstance.AddComponent<PhotonView>();
            photonView.OwnershipTransfer = OwnershipOption.Fixed;
            photonView.Synchronization = ViewSynchronization.Unreliable;
            photonView.observableSearch = PhotonView.ObservableSearch.AutoFindAll;

            var Playervariant = PrefabUtility.SaveAsPrefabAsset(Playerinstance, "Assets/Resources/" + playerName + ".prefab");
        }
    }

}