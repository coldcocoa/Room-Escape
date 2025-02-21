using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    // Inspector에서 타임라인에 연결된 PlayableDirector를 할당하세요.
    public PlayableDirector timelineDirector;

    // 이 메소드를 버튼 OnClick 이벤트에 연결합니다.
    public void StartTimeline()
    {
        if (timelineDirector != null)
        {
            timelineDirector.Play();
        }
        else
        {
            Debug.LogError("PlayableDirector가 할당되지 않았습니다!");
        }
    }
}
