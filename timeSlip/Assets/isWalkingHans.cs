using UnityEngine;

public class isWalkingHans : MonoBehaviour
{
    public Transform hans;
    public Animator hansAnimator;
    public float moveSpeed = 1.5f;
    public float targetX = 0.127f;

    public AudioSource voiceAudioSource;  // 🎤 Voice Clip 연결용

    private bool hasArrived = false;
    private bool voicePlayed = false;
    private bool turnTriggered = false;
    private float waitStartTime;

    void Update()
    {
        // 1. 이동 처리
        if (hansAnimator.GetBool("isWalk") && !hasArrived)
        {
            Vector3 currentPosition = hans.position;
            Vector3 targetPosition = new Vector3(targetX, currentPosition.y, currentPosition.z);
            float step = moveSpeed * Time.deltaTime;

            hans.position = Vector3.MoveTowards(currentPosition, targetPosition, step);

            if (Mathf.Abs(hans.position.x - targetX) < 0.01f)
            {
                hasArrived = true;

                // ✅ 걷기 종료, 대기 시작
                hansAnimator.SetBool("isWalk", false);
                hansAnimator.SetBool("isWait", true);
                waitStartTime = Time.time;

                Debug.Log("✅ Hans가 X=0.127 위치에 도착했습니다. 상태 전환 중...");
            }
        }

        // 2. 음성 재생 (1회만)
        if (hasArrived && !voicePlayed &&(hansAnimator.GetBool("isWait") ))
        {
            if (voiceAudioSource != null && !voiceAudioSource.isPlaying)
            {
                voiceAudioSource.Play();
                voicePlayed = true;
                Debug.Log("🎤 Voice 재생 시작");
                hansAnimator.SetBool("isWait", false);
           // hansAnimator.SetBool("isTurn", true);
            }
        }
    }
}
