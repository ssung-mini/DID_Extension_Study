using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NPCSetting : MonoBehaviour
{
    public static void doCreateNPCPrefab(string NPCName) {
        if (!Resources.Load<GameObject>(NPCName))
        {
            var PlayerPrefab = (GameObject)Resources.Load<GameObject>("Models/" + NPCName); // 아바타 model fbx파일 가져오기
            var Playerinstance = PrefabUtility.InstantiatePrefab(PlayerPrefab) as GameObject; // prefab 동적으로 생성

            /* Player  설정(애니메이션, 중력, collider, 움직임, 카메라 세팅) */
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

            var Playervariant = PrefabUtility.SaveAsPrefabAsset(Playerinstance, "Assets/Resources/NPC_" + NPCName + ".prefab"); // 설정된 아바타 prefab 만들기
        }
    }
}
