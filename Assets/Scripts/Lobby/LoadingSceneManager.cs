using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening; // DOTween 네임스페이스
using System.Collections;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider; // 슬라이더 UI
    [SerializeField] private Text loadingText;     // 진행률 텍스트 (선택 사항)
    [SerializeField] private float tweenDuration; // 트윈 애니메이션 지속 시간
    [SerializeField] private string mainSceneName; // 로드할 메인 씬 이름

    private float targetProgress = 0f; // 목표 진행률
    private float currentProgress = 0f; // 현재 진행률 (트윈용)

    void Start()
    {
        // 슬라이더 초기화
        if (loadingSlider != null)
        {
            loadingSlider.value = 0f;
        }
        if (loadingText != null)
        {
            loadingText.text = "0%";
        }

        // 메인 씬 로딩 시작
        StartCoroutine(LoadMainSceneAsync());
    }

    private IEnumerator LoadMainSceneAsync()
    {
        // 비동기적으로 메인 씬 로드
        AsyncOperation operation = SceneManager.LoadSceneAsync(mainSceneName);
        operation.allowSceneActivation = false; // 로딩 완료 후 자동 전환 방지

        // 로딩 진행률 추적
        while (!operation.isDone)
        {
            // 진행률 계산 (0~1 사이)
            targetProgress = Mathf.Clamp01(operation.progress / 0.9f); // 진행률이 0.9까지로 계산되므로 정규화

            // 트윈 애니메이션으로 슬라이더 부드럽게 채우기
            DOTween.To(() => currentProgress, x => currentProgress = x, targetProgress, tweenDuration)
                .OnUpdate(() =>
                {
                    if (loadingSlider != null)
                    {
                        loadingSlider.value = currentProgress;
                    }
                    if (loadingText != null)
                    {
                        loadingText.text = Mathf.RoundToInt(currentProgress * 100) + "%";
                    }
                });

            // 로딩이 완료되면 (progress가 0.9 이상일 때)
            if (targetProgress >= 1f)
            {
                // 최소 표시 시간 보장 (선택 사항)
                yield return new WaitForSeconds(5f);

                // 씬 전환 허용
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}