using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    // Inspector���� Ÿ�Ӷ��ο� ����� PlayableDirector�� �Ҵ��ϼ���.
    public PlayableDirector timelineDirector;

    // �� �޼ҵ带 ��ư OnClick �̺�Ʈ�� �����մϴ�.
    public void StartTimeline()
    {
        if (timelineDirector != null)
        {
            timelineDirector.Play();
        }
        else
        {
            Debug.LogError("PlayableDirector�� �Ҵ���� �ʾҽ��ϴ�!");
        }
    }
}
