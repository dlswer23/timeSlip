using UnityEngine;
using UnityEditor;

public class AnimationClipGenerator
{
    [MenuItem("Tools/Create Hans Walk Animation")]
    public static void CreateHansWalkAnim()
    {
        AnimationClip clip = new AnimationClip
        {
            frameRate = 60
        };

        // 걷기 동작: 좌우 다리 앞뒤로 흔들기
        AnimationCurve leftLeg = new AnimationCurve(
            new Keyframe(0f, 0f),
            new Keyframe(0.25f, 25f),
            new Keyframe(0.5f, 0f),
            new Keyframe(0.75f, -25f),
            new Keyframe(1f, 0f)
        );
        AnimationCurve rightLeg = new AnimationCurve(
            new Keyframe(0f, 0f),
            new Keyframe(0.25f, -25f),
            new Keyframe(0.5f, 0f),
            new Keyframe(0.75f, 25f),
            new Keyframe(1f, 0f)
        );

        clip.SetCurve("", typeof(Animator), "Left Upper Leg Front-Back", leftLeg);
        clip.SetCurve("", typeof(Animator), "Right Upper Leg Front-Back", rightLeg);

        // 루프 설정
        AnimationClipSettings settings = AnimationUtility.GetAnimationClipSettings(clip);
        settings.loopTime = true;
        AnimationUtility.SetAnimationClipSettings(clip, settings);

        // 저장
        string path = "Assets/Animations/Hans_Walk.anim";
        AssetDatabase.CreateAsset(clip, path);
        AssetDatabase.SaveAssets();
        Debug.Log("✅ Hans_Walk.anim 생성 완료! 경로: " + path);
    }
}
