using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple_Hand_IK : MonoBehaviour
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 handPosOffset;
    public Vector3 handRotOffset;
    // Update is called once per frame
    void LateUpdate()
    {
        rigTarget.position = vrTarget.TransformPoint(handPosOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(handRotOffset);
    }
}