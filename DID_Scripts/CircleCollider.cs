using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CircleCollider : MonoBehaviour
{
	public bool enterCollider;

    public GameObject targetAvatar;
    private AvatarActionWithPlayer targetScript;

    CircleAnimation circleAnim;
    

    // Start is called before the first frame update
    void Start()
    {
        circleAnim = gameObject.GetComponent<CircleAnimation>();
        targetScript = targetAvatar.GetComponent<AvatarActionWithPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
		if(other.CompareTag("Player"))
        {
            Debug.Log("서클 콜라이더 진입");
            enterCollider = true;
            targetScript._enterCollider = true;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            StartCoroutine(FadeColor());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("서클 콜라이더 진입");
            enterCollider = true;
            targetScript._enterCollider = true;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            StartCoroutine(FadeColor());
        }
    }

    IEnumerator FadeColor()
    {
        float alpha = 0.18f;
        while (alpha > 0)
        {
            alpha -= 0.005f;
            yield return new WaitForSeconds(0.05f);
            circleAnim.animObjects[0].GetComponent<Renderer>().material.SetColor("_TintColor", new Color(1, 0, 0, alpha));
            circleAnim.animObjects[1].GetComponent<Renderer>().material.SetColor("_TintColor", new Color(0, 0.2549f, 1, alpha));
            circleAnim.animObjects[2].GetComponent<Renderer>().material.SetColor("_TintColor", new Color(0.9568627f, 0.7058824f, 0, alpha));
            circleAnim.animObjects[3].GetComponent<Renderer>().material.SetColor("_TintColor", new Color(0.05882353f, 0.6156863f, 0.345098f, alpha));
            circleAnim.animObjects[4].GetComponent<Renderer>().material.SetColor("_Color", new Color(0.07924521f, 1, 0.139871f, alpha));
        }
    }
}
