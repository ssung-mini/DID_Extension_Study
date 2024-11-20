/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NPCSetting : MonoBehaviour
{
    public static void doCreateNPCPrefab(string NPCName)
    {
        Debug.Log("NPC ����1");
        if (!Resources.Load<GameObject>("NFT_Models/NPC/" + NPCName))
        {
            Debug.Log("NPC ����");
            var PlayerPrefab = (GameObject)Resources.Load<GameObject>("Models/" + NPCName); // �ƹ�Ÿ model fbx���� ��������
            var Playerinstance = PrefabUtility.InstantiatePrefab(PlayerPrefab) as GameObject; // prefab �������� ����

            //Player ����(�ִϸ��̼�, �߷�, collider, ������, ī�޶� ����)
            Animator animator = Playerinstance.GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("BackgroundAnimation/Background_Based_Animator");
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

            var Playervariant = PrefabUtility.SaveAsPrefabAsset(Playerinstance, "Assets/Resources/NFT_Models/NPC/" + NPCName + ".prefab"); // ������ �ƹ�Ÿ prefab �����
        }
    }
}
*/