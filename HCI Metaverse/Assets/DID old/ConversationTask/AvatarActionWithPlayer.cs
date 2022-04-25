using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarActionWithPlayer : MonoBehaviour
{
    //public VRController player;                         // Player object
    public static GameObject player;                           // Player object
    public GameObject linesCanvas;                      // 대사 나타날 canvas
    public GameObject smartPhone;                       // 전화 애니메이션시 필요한 스마트폰
    public GameObject idCard;                           // 신원 나타나있는 canvas
    public GameObject programManager;                // 실험프로그램 관리
    public GameObject virtualMirror;                    // 가상 거울

    public AudioSource conversationSound;               // 대사 음성
    public AudioClip conversationBlock1_Generic;        // Trial 1의 대사 남성
    public AudioClip conversationBlock1_NFTbased;       // Trial 1의 대사 여성
    public AudioClip conversationBlock2_Generic;         // Trial 2의 대사 남성
    public AudioClip conversationBlock2_NFTbased;        // Trial 2의 대사 여성

    private float gazeDistance = 7.5f;                  // Avatar가 Player를 응시하기 시작하는 거리, 이 거리 안으로 돌아오면 Avatar가 Player를 쳐다보게 된다.
    private float conversationDistance = 6f;          // Avatar가 Player와 대화를 시작하게 되는 거리, 이 거리 안으로 돌아오면 Avatar가 Player에게 말을 건다.

    private Vector3 initialPosition;                    // Avater가 갖고 있는 위치. 정해진 위치에서 벗어나지 않도록 Update를 통해 보정.
    private Animator animator;                          // Avatar animation obejct
    private RuntimeAnimatorController animController;   // Animation play length controller

    private int avatarType;                             // 아바타 종류
    private int nowTrial;                               // 현재 몇번째 trial인가 (1~4)
    private int distanceType;                           // Player와 Avatar의 거리
    private int prevDistanceType;                       // 현재 진행중인 animation의 타입이 뭐냐
    private bool isPlaying;                             // 애니메이션이 재생 중인가?
    private bool isTalking;                             // 인사를 하고 대화를 시작하였는가?
    private bool endTalking;                            //  대사가 처음부터 끝까지 다 진행이 되었는가?
    
    [SerializeField]
    private bool isConversation;                        // 대사를 말하고 있는가?

    private IEnumerator conversationCoroutine;

    private void Start()
    {
        conversationSound = gameObject.GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        animController = animator.runtimeAnimatorController;

        /*Debug.Log("Number of the Animation: " + animController.animationClips.Length);
        for (int i = 0; i < animController.animationClips.Length; i++)
        {
            //Debug.Log(animController.animationClips[i].name);
            
        }*/

        initialPosition = GetComponent<Transform>().position;
        conversationCoroutine = ConversationPlay();

        isConversation = false;
        isPlaying = false;
        endTalking = false;
        distanceType = 0;
        smartPhone.SetActive(false); // 기존 값 : false
        linesCanvas.SetActive(false);
        virtualMirror.SetActive(false);
        avatarType = (int)programManager.GetComponent<ProgramManager>()._avatarType; //experimentManager.GetComponent<ExperimentManager>().avatarType;
        nowTrial = programManager.GetComponent<ProgramManager>()._nowTrial; ; //experimentManager.GetComponent<ExperimentManager>().nowTrial;

        switch (avatarType)
        {
            case 0:
                if (nowTrial == 1) conversationSound.clip = conversationBlock1_Generic;
                else if (nowTrial == 2) conversationSound.clip = conversationBlock2_Generic;
                //else if (nowTrial == 3) conversationSound.clip = conversationBlock3_Generic;
                //else if (nowTrial == 4) conversationSound.clip = conversationBlock4_Generic;
                break;
            case 1:
                if (nowTrial == 1) conversationSound.clip = conversationBlock1_NFTbased;
                else if (nowTrial == 2) conversationSound.clip = conversationBlock2_NFTbased;
                //else if (nowTrial == 3) conversationSound.clip = conversationBlock3_NFTbased;
                //else if (nowTrial == 4) conversationSound.clip = conversationBlock4_NFTbased;
                break;
        }

        // conversationSound.clip = 
        if (avatarType == 0) idCard.SetActive(false);
    }

    private void Update()
    {
        PositionCorrection();
        DistanceCalculation();
    }

    private void PositionCorrection()
    {
        transform.position = new Vector3(transform.position.x, 0.25f, transform.position.z);            // Height positining.
        transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime * 0.5f);  // moving speed ...
    }

    private void AvatarRotation()
    {
        Vector3 distance = (player.transform.position - transform.position).normalized;
        distance = new Vector3(distance.x, 0.0f, distance.z);
        Quaternion lookDirection = Quaternion.LookRotation(distance);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, Time.deltaTime * 1.5f);
    }

    private void DistanceCalculation()
    {
        float distance = Vector3.Distance(gameObject.GetComponent<Transform>().position, player.GetComponent<Transform>().position);
        //Debug.Log("Distance: " + distance);

        if (distance < conversationDistance)
        {
            distanceType = 2;
            Debug.Log("distancetype : 2");
        }
        else if (distance < gazeDistance)
        {
            //StopCoroutine(conversationCoroutine);
            distanceType = 1;
            Debug.Log("distancetype : 1");
            //conversationSound.Stop();
            //isConversation = false;
        }
        else
        {
            //StopCoroutine(conversationCoroutine);
            distanceType = 0;
            //conversationSound.Stop();
            //isConversation = false;
        }

        //Debug.Log("distanceType: " + distanceType);
        //Debug.Log("Previous distanceType: " + prevDistanceType);

        switch (distanceType)
        {
            case 2:
                linesCanvas.SetActive(true);
                virtualMirror.SetActive(true);
                //if (avatarType == 1) idCard.SetActive(false);
                AvatarRotation();
                if (prevDistanceType != distanceType)
                {
                    prevDistanceType = distanceType;
                    isTalking = false;
                    AnimationStop();
                    break;
                }
                if (!isPlaying && !isTalking)
                {
                    prevDistanceType = 2;
                    StartCoroutine(AnimationPlay("HelloGestureAnimation"));     // 인사 애니메이션
                    isTalking = true;
                }
                else if (!isPlaying && isTalking)
                {
                    prevDistanceType = 2;

                    int animationType = Random.Range(1, 15);

                    if (animationType == 1) StartCoroutine(AnimationPlay("Acknowledging"));
                    else if (animationType == 2) StartCoroutine(AnimationPlay("DismissingGesture"));
                    else if (animationType == 3) StartCoroutine(AnimationPlay("Thanksful"));
                    else if (animationType == 4) StartCoroutine(AnimationPlay("Excited"));
                    else if (animationType == 5) StartCoroutine(AnimationPlay("HappyIdle"));
                    else if (animationType == 6) StartCoroutine(AnimationPlay("LookOverShoulder"));
                    else if (animationType == 7) StartCoroutine(AnimationPlay("PointingForward"));
                    else if (animationType == 8) StartCoroutine(AnimationPlay("ThroughfulHeadShake"));
                    else StartCoroutine(AnimationPlay("TalkingAnimation"));          // 대사 애니메이션

                    if (!isConversation && !gameObject.GetComponent<AvatarActionWithPlayer>().endTalking)
                    {
                        conversationCoroutine = ConversationPlay();
                        StartCoroutine(conversationCoroutine);
                    }

                }
                break;

            case 1:

                linesCanvas.SetActive(false);
                //virtualMirror.SetActive(false);
                //if (avatarType == 1) idCard.SetActive(true);
                AvatarRotation();
                if (prevDistanceType != distanceType)
                {
                    prevDistanceType = distanceType;
                    AnimationStop();
                    break;
                }
                if (!isPlaying)
                {
                    prevDistanceType = 1;
                    StartCoroutine(AnimationPlay("StandingIdleAnimation"));     // Idle Posture 애니메이션
                }
                break;

            case 0:
                linesCanvas.SetActive(false);
                //if (avatarType == 1) idCard.SetActive(true);
                if (prevDistanceType != distanceType)
                {
                    prevDistanceType = distanceType;
                    AnimationStop();
                    break;
                }
                if (!isPlaying)
                {
                    prevDistanceType = 0;
                    //int idleAnimation = Random.Range(1, 10);
                    int idleAnimation = Random.Range(1, 8);
                    if (idleAnimation == 1) StartCoroutine(AnimationPlay("ArmStretchingAnimation"));
                    else if (idleAnimation == 2)
                    {
                        smartPhone.SetActive(true);
                        StartCoroutine(AnimationPlay("TalkingCellPhoneAnimation"));
                    }
                    else if (idleAnimation == 3) StartCoroutine(AnimationPlay("IdleSadAnimation"));
                    else if (idleAnimation == 4) StartCoroutine(AnimationPlay("IdleHappyAnimation"));
                    else if (idleAnimation == 5) StartCoroutine(AnimationPlay("IdleBreathingAnimation"));
                    else if (idleAnimation == 6) StartCoroutine(AnimationPlay("LeftTurn"));
                    else if (idleAnimation == 7) StartCoroutine(AnimationPlay("NervouslyLookAround"));
                    else if (idleAnimation == 8) StartCoroutine(AnimationPlay("RightTurn"));


                    //else if (idleAnimation == 3) StartCoroutine(AnimationPlay("StandingSwingAnimation"));
                    //else if (idleAnimation == 17) StartCoroutine(AnimationPlay("TextingWhileStanding"));
                    //else StartCoroutine(AnimationPlay("StrollAnimation"));
                }
                break;
        }
    }

    private IEnumerator ConversationPlay()
    {
        isConversation = true;
        conversationSound.Play();
        Debug.Log("Conversation coroutine start");
        yield return new WaitForSeconds(conversationSound.clip.length);
        isConversation = false;
        gameObject.GetComponent<AvatarActionWithPlayer>().endTalking = true;
        conversationSound.Stop();
        Debug.Log("Conversation coroutine stop");
    }

    public float GetAnimationTime(string animName)
    {
        float animTime = 0.0f;
        for (int i = 0; i < animController.animationClips.Length; i++)
        {
            if (animController.animationClips[i].name.Contains(animName))
            {
                //Debug.Log(animName + " : " + animController.animationClips[i].name);
                animTime = animController.animationClips[i].length;
                break;
            }
        }
        //Debug.Log(animName + ": " + animTime);
        return animTime;
    }

    private IEnumerator AnimationPlay(string animName)
    {
        isPlaying = true;
        animator.GetComponent<Animator>().SetBool(animName, true);
        yield return new WaitForSecondsRealtime(GetAnimationTime(animName));
        isPlaying = false;
        animator.GetComponent<Animator>().SetBool(animName, false);
    }

    private void AnimationStop()
    {
        isPlaying = false;
        isConversation = false;
        conversationSound.Stop();
        smartPhone.SetActive(false);
        StopAllCoroutines();
        for (int i = 0; i < animController.animationClips.Length; i++)
        {
            animator.GetComponent<Animator>().SetBool(animController.animationClips[i].name, false);
        }
    }
}
