using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    public Image fadePanel; // FadePanel UI Image 연결
    public float fadeDuration = 1.5f; // 페이드에 걸리는 시간 (초)

    // 스크립트 인스턴스가 씬 로드 시 파괴되지 않도록 싱글톤 패턴 권장
    // public static Fader Instance; 

    void Start()
    {
        // **씬 시작 시 페이드 인 효과 (화면이 점점 밝아짐)**
        StartCoroutine(FadeIn());
    }

    // 씬 진입 시 투명해지는 효과 (검은 화면 -> 게임 화면)
    public IEnumerator FadeIn()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = 1f - (timer / fadeDuration); // 1 -> 0으로 감소
            SetAlpha(alpha);
            yield return null; // 한 프레임 대기
        }
        SetAlpha(0f); // 확실히 투명하게
    }

    // 다음 씬으로 넘어가기 전 불투명해지는 효과 (게임 화면 -> 검은 화면)
    public IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = timer / fadeDuration; // 0 -> 1로 증가
            SetAlpha(alpha);
            yield return null; // 한 프레임 대기
        }

        // **페이드 아웃이 완료되면 씬 전환**
        SetAlpha(1f); // 확실히 불투명하게
        SceneManager.LoadScene(sceneName);
    }

    // 알파 값 설정 유틸리티 함수
    private void SetAlpha(float alpha)
    {
        Color color = fadePanel.color;
        color.a = alpha;
        fadePanel.color = color;
    }
}