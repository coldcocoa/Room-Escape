using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening; // DOTween ���ӽ����̽�
using System.Collections;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider; // �����̴� UI
    [SerializeField] private Text loadingText;     // ����� �ؽ�Ʈ (���� ����)
    [SerializeField] private float tweenDuration; // Ʈ�� �ִϸ��̼� ���� �ð�
    [SerializeField] private string mainSceneName; // �ε��� ���� �� �̸�

    private float targetProgress = 0f; // ��ǥ �����
    private float currentProgress = 0f; // ���� ����� (Ʈ����)

    void Start()
    {
        // �����̴� �ʱ�ȭ
        if (loadingSlider != null)
        {
            loadingSlider.value = 0f;
        }
        if (loadingText != null)
        {
            loadingText.text = "0%";
        }

        // ���� �� �ε� ����
        StartCoroutine(LoadMainSceneAsync());
    }

    private IEnumerator LoadMainSceneAsync()
    {
        // �񵿱������� ���� �� �ε�
        AsyncOperation operation = SceneManager.LoadSceneAsync(mainSceneName);
        operation.allowSceneActivation = false; // �ε� �Ϸ� �� �ڵ� ��ȯ ����

        // �ε� ����� ����
        while (!operation.isDone)
        {
            // ����� ��� (0~1 ����)
            targetProgress = Mathf.Clamp01(operation.progress / 0.9f); // ������� 0.9������ ���ǹǷ� ����ȭ

            // Ʈ�� �ִϸ��̼����� �����̴� �ε巴�� ä���
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

            // �ε��� �Ϸ�Ǹ� (progress�� 0.9 �̻��� ��)
            if (targetProgress >= 1f)
            {
                // �ּ� ǥ�� �ð� ���� (���� ����)
                yield return new WaitForSeconds(5f);

                // �� ��ȯ ���
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}