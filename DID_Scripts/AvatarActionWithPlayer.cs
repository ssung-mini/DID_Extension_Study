using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Emotion
{
    Fear,
    Anger,
    Sadness,
    Joy,
    Disgust,
    Surprise
}

public class AvatarActionWithPlayer : MonoBehaviour
{
    //public VRController player;                         // Player object
    public static GameObject player;                           // Player object
    public GameObject linesCanvas;                      // 대사 나타날 canvas
    public GameObject smartPhone;                       // 전화 애니메이션시 필요한 스마트폰
    public GameObject idCard;                           // 신원 나타나있는 canvas
    public GameObject programManager;                   // 실험프로그램 관리
    //public GameObject virtualMirror;                  // 가상 거울 (Deprecated)

    public AudioSource conversationSound;               // 대사 SoundSource
    public AudioClip conversationHello;                 // 대사 Clip 1 (첫 인사)
    public AudioClip conversationLine;                  // 대사 Clip 2 (주 대화)
    public AudioClip conversationBye;                   // 대사 Clip 3 (마무리 인사)
    /*
    public AudioClip conversationNFT_Trial1;            // NFT trial 1 대사
    public AudioClip conversationNFT_Trial2;            // NFT trial 2 대사
    public AudioClip conversationGeneric_Trial1;        // Generic trial 1 대사
    public AudioClip conversationGeneric_Trial2;        // Generic trial 2 대사
    */

    private float gazeDistance = 5.3f;                  // Avatar가 Player를 응시하기 시작하는 거리, 이 거리 안으로 돌아오면 Avatar가 Player를 쳐다보게 된다.
    //private float conversationDistance = 3.3f;          // Avatar가 Player와 대화를 시작하게 되는 거리, 이 거리 안으로 돌아오면 Avatar가 Player에게 말을 건다.

    private Vector3 initialPosition;                    // Avater가 갖고 있는 위치. 정해진 위치에서 벗어나지 않도록 Update를 통해 보정.
    private Animator animator;                          // Avatar animation obejct
    private RuntimeAnimatorController animController;   // Animation play length controller

    private int avatarType;                             // 아바타 종류
    private int nowTrial;                               // 현재 몇번째 trial인가 (1~4)
    private int distanceType;                           // Player와 Avatar의 거리
    private int prevDistanceType;                       // 현재 진행중인 animation의 타입이 뭐냐
    private bool startTalking;
    private bool isPlaying;                             // 애니메이션이 재생 중인가?
    private bool isTalking;                             // 인사를 하고 대화를 시작하였는가?
    private bool check;
    private static bool check2;
    private static bool check3;
    private bool endTalking;                            //  대사가 처음부터 끝까지 다 진행이 되었는가?
    public int positionNum;
    private GazeManager gazeManager;
    public bool _enterCollider = false;

    public Emotion emotion;                              // 아바타의 감정 상태 (Trial 시)
    
    [SerializeField]
    private bool isConversation;                        // 대사를 말하고 있는가?

    private void Start()
    {
        conversationSound = gameObject.GetComponent<AudioSource>();
        gazeManager = gameObject.GetComponent<GazeManager>();
        animator = GetComponent<Animator>();
        animController = animator.runtimeAnimatorController;

        initialPosition = GetComponent<Transform>().position;

        check = false;
        check2 = false;
        check3 = false;
        isConversation = false;
        isPlaying = false;
        endTalking = false;
        distanceType = 0;
        smartPhone.SetActive(false); // 기존 값 : false
        linesCanvas.SetActive(false);
        //virtualMirror.SetActive(false);
        avatarType = (int)programManager.GetComponent<ProgramManager>()._avatarType; //experimentManager.GetComponent<ExperimentManager>().avatarType;
        nowTrial = programManager.GetComponent<ProgramManager>()._nowTrial; //experimentManager.GetComponent<ExperimentManager>().nowTrial;



        /*
        switch (avatarType)
        {
            case 0:
                if (nowTrial == 1) conversationSound.clip = conversationGeneric_Trial1;
                else if (nowTrial == 2) conversationSound.clip = conversationGeneric_Trial2;
                //else if (nowTrial == 3) conversationSound.clip = conversationBlock3_Generic;
                //else if (nowTrial == 4) conversationSound.clip = conversationBlock4_Generic;
                break;
            case 1:
                if (nowTrial == 1) conversationSound.clip = conversationNFT_Trial1;
                else if (nowTrial == 2) conversationSound.clip = conversationNFT_Trial2;
                //else if (nowTrial == 3) conversationSound.clip = conversationBlock3_NFTbased;
                //else if (nowTrial == 4) conversationSound.clip = conversationBlock4_NFTbased;
                break;
        } */
        IdleAnimStart();
        if (avatarType == 0) idCard.SetActive(false);
    }

    private void Update()
    {
        //PositionCorrection();
        DistanceCalculation();
    }

    /*private void PositionCorrection()
    {
        transform.position = new Vector3(transform.position.x, 0.25f, transform.position.z);            // Height positining.
        transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime * 0.5f);  // moving speed ...
    }*/

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

        if (_enterCollider && !check)
        {
            distanceType = 2;
            //Debug.Log("distancetype : 2");
        }
        else if (distance < gazeDistance)
        {
            //StopCoroutine(conversationCoroutine);
            distanceType = 1;
            //Debug.Log("distancetype : 1");
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
                //linesCanvas.SetActive(true);
                //virtualMirror.SetActive(true);
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
                    if (check2 == false)
                    {

                        StartCoroutine(Greeting());


                    }

                    isTalking = true;
                }
                else if (!isPlaying && isTalking)
                {
                    prevDistanceType = 2;


                    if (!check && !isConversation && !gameObject.GetComponent<AvatarActionWithPlayer>().startTalking && !gameObject.GetComponent<AvatarActionWithPlayer>().endTalking)
                    {

                        if (check3 == false)
                        {
                            StartCoroutine(ConversationStart());
                            check3 = true;
                        }

                    }

                }
                break;

            case 1:
                
                //linesCanvas.SetActive(false);
                //virtualMirror.SetActive(false);
                //if (avatarType == 1) idCard.SetActive(true);
                AvatarRotation();
                if (prevDistanceType != distanceType)
                {
                    prevDistanceType = distanceType;
                    check2 = false;
                    check3 = false;
                    AnimationStop();
                    break;
                }
                if (!isPlaying)
                {
                    prevDistanceType = 1;
                    //StartCoroutine(AnimationPlay("StandingIdleAnimation"));     // Idle Posture 애니메이션
                }
                break;

            case 0:
                //linesCanvas.SetActive(false);
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
                    //int idleAnimation = Random.Range(1, 8);
                    /*if (idleAnimation == 1) StartCoroutine(AnimationPlay("ArmStretchingAnimation"));
                    else if (idleAnimation == 2)
                    {
                        
                        
                    }*/
                    


                    //else if (idleAnimation == 3) StartCoroutine(AnimationPlay("StandingSwingAnimation"));
                    //else if (idleAnimation == 17) StartCoroutine(AnimationPlay("TextingWhileStanding"));
                    //else StartCoroutine(AnimationPlay("StrollAnimation"));
                }
                break;
        }
    }

    private IEnumerator Greeting()
    {
        
        animator.SetBool("Greeting", true);     // 인사 애니메이션
        animator.SetBool("isMeeted", false);
        yield return new WaitForSeconds(2f);
        check2 = true;
        animator.SetBool("isMeeted", true);

    }

    private IEnumerator ConversationStart()
    {
        CsvWritingManager.SetPositionNumber(positionNum);
        CsvWritingManager.Start_Timer();
        CsvWritingManager.nowConversation = true;
        gazeManager.enableCollider();
        animator.SetBool("isMeeted", true);
        animator.SetBool("StartConversation", true);
        Debug.Log("ConversationStart");
        gameObject.GetComponent<AvatarActionWithPlayer>().startTalking = true;
        yield return new WaitForSeconds(1.5f);
        conversationSound.clip = conversationHello;
        conversationSound.Play();   // 인삿말 Sound Play
        yield return new WaitForSeconds(conversationSound.clip.length); // NPC의 인삿말 길이동안 Wait
        conversationSound.Stop();
        

        StartCoroutine(StartRecord());
    }

    private IEnumerator StartRecord()
    {
        conversationSound.Stop();
        CsvWritingManager.nowConversation = false;
        CanvasOnOff.EnablePressCanvas();
        yield return new WaitUntil(() => RecordingOnOff.isRecording == true);
        CanvasOnOff.DisablePressCanvas();
        StartCoroutine(Recording());
    }
    private IEnumerator Recording()
    {
        CanvasOnOff.EnableRecordingCanvas();
        yield return new WaitUntil(() => RecordingOnOff.isRecording == false);
        CanvasOnOff.DisableRecordingCanvas();
        CsvWritingManager.nowConversation = true;
        StartCoroutine(ConversationPlay());
    }

    private IEnumerator ConversationPlay()
    {
        
        isConversation = true;
        yield return new WaitForSeconds(1f);

        EmotionAnimation();

        conversationSound.clip = conversationLine;
        conversationSound.Play();   // 주 대화 내용 Sound Play
        Debug.Log("Conversation coroutine start");
        yield return new WaitForSeconds(conversationSound.clip.length); // NPC의 주 대화 내용 길이동안 Wait
        conversationSound.Stop();
        StartCoroutine(StartRecord_end());
    }

    private IEnumerator StartRecord_end()
    {
        animator.SetBool("LineEnd", true);
        CsvWritingManager.nowConversation = false;
        
        conversationSound.Stop();
        CanvasOnOff.EnablePressCanvas();
        CsvWritingManager.nowRecording = true;
        yield return new WaitUntil(() => RecordingOnOff.isRecording == true);
        CanvasOnOff.DisablePressCanvas();
        StartCoroutine(Recording_end());
    }
    private IEnumerator Recording_end()
    {
        CanvasOnOff.EnableRecordingCanvas();
        recordingWav.StartRecordMicrophone();
        yield return new WaitUntil(() => RecordingOnOff.isRecording == false);
        CanvasOnOff.DisableRecordingCanvas();
        CsvWritingManager.nowConversation = true;
        CsvWritingManager.nowRecording = false;
        recordingWav.StopRecordMicrophone(CsvWritingManager.GetRecordingPath());
        StartCoroutine(ConversationEnd());
    }

    private IEnumerator ConversationEnd()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("RecordingEnd", true);
        conversationSound.clip = conversationBye;
        conversationSound.Play();   // 마지막 말 Sound Play
        yield return new WaitForSeconds(conversationSound.clip.length); // NPC의 마지막 말 길이동안 Wait
        gameObject.GetComponent<AvatarActionWithPlayer>().endTalking = true;
        
        conversationSound.Stop();
        isConversation = false;
        CsvWritingManager.nowConversation = false;
        
        gazeManager.disableCollider();

        CsvWritingManager.SetValue(emotion.ToString(), positionNum, gazeManager.getHeadGaze(), gazeManager.getBodyGaze(), 
            gazeManager.getVerifiedGaze(), gazeManager.getNameGaze(), gazeManager.getSexGaze(), gazeManager.getBirthGaze(), gazeManager.getCrimeGaze(), gazeManager.getCountryGaze(), gazeManager.getWorkGaze(), 
            gazeManager.RemainderTime(CsvWritingManager.GetConversationTime()));

        CsvWritingManager.nowEvaluation = true;
        CanvasOnOff.EnableSelectCanvas();
        animator.SetBool("ConversationEnd", true);
        check = true;
        
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
        gameObject.GetComponent<AvatarActionWithPlayer>().startTalking = false;
        animator.GetComponent<Animator>().Play("Idle 4 (Breath)", 0);
        gameObject.GetComponent<AvatarActionWithPlayer>().check = false;
    }

    private void EmotionAnimation()
    {
        if (emotion.ToString().Equals("Fear"))
            animator.SetBool("Fear", true);
        else if (emotion.ToString().Equals("Disgust"))
            animator.SetBool("Disgust", true);
        else if (emotion.ToString().Equals("Sadness"))
            animator.SetBool("Sadness", true);
        else if (emotion.ToString().Equals("Anger"))
            animator.SetBool("Angry", true);
        else if (emotion.ToString().Equals("Joy"))
            animator.SetBool("Happy", true);
        else animator.SetBool("Surprise", true);
    }

    private void IdleAnimStart()
    {
        int avatarIdleNum = Random.Range(1, 7);

        Debug.Log(avatarIdleNum);

        if (avatarIdleNum == 1)
            gameObject.GetComponent<AvatarActionWithPlayer>().animator.SetBool("Idle 1", true);

        else if (avatarIdleNum == 2)
            gameObject.GetComponent<AvatarActionWithPlayer>().animator.SetBool("Idle 2", true);

        else if (avatarIdleNum == 3)
            gameObject.GetComponent<AvatarActionWithPlayer>().animator.SetBool("Idle 3", true);

        else if (avatarIdleNum == 4)
            gameObject.GetComponent<AvatarActionWithPlayer>().animator.SetBool("Idle 4", true);

        else if (avatarIdleNum == 5)
            gameObject.GetComponent<AvatarActionWithPlayer>().animator.SetBool("Idle 5", true);

        else
            gameObject.GetComponent<AvatarActionWithPlayer>().animator.SetBool("Idle 6", true);
    }
}
