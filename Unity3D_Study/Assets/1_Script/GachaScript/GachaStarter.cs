using UnityEngine;
using UnityEngine.Playables;

public class GachaStarter : MonoBehaviour
{
    public PlayableDirector timeline;

    public void OnGachaButtonClick()
    {
        timeline.Play(); // 타임라인 실행
    }
}
