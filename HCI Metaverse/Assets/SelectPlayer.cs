using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        AvatarActionWithPlayer.player = gameObject;
    }
}
