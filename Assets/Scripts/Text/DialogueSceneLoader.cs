using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueSceneLoader : MonoBehaviour
{
    // === 1. Inspector에서 연결할 컴포넌트 ===
    [Header("Manager References")]
    public DialogueManager dialogueManager;
    public Fader fader; // (옵션) 씬 전환에 사용

    // === 2. Inspector에서 연결할 데이터 ===
    [Header("Dialogue Data")]
    // 씬에서 시작할 대화 데이터 에셋 (DialogueData.cs 기반)
    public DialogueData dialogueData;

    // === 3. 대화 종료 후 로직 설정 ===
    [Header("End of Dialogue Settings")]
    public bool loadNextSceneOnFinish = true; // 대화 종료 후 다음 씬으로 전환할지 여부
    public string nextSceneName = "MainGameScene"; // 전환할 다음 씬 이름

    // 이 변수는 이름 입력 UI가 끝났는지 여부를 외부에서 알려줄 때 사용 가능
    private bool canStartDialogue = false;

    void Start()
    {
        // 씬 시작 시 이름 입력 로직을 먼저 거쳐야 한다면, 
        // 여기서는 바로 시작하지 않고 이름 입력 로직에서 이 함수를 호출해야 합니다.
        // 현재는 이름 입력 로직이 완료되었다고 가정하고 바로 대화를 시작합니다.

        // Fader가 있다면 페이드 인을 먼저 시작합니다.
        if (fader != null)
        {
            StartCoroutine(fader.FadeIn());
        }

        if (dialogueData != null && dialogueManager != null)
        {
            // 데이터 매니저에게 대화 데이터 전달 및 시작
            dialogueManager.StartDialogue(dialogueData);
            canStartDialogue = true;
        }
    }

    void Update()
    {
        // 대화가 시작된 상태에서 사용자 입력(클릭) 감지
        if (canStartDialogue && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            if (dialogueManager != null)
            {
                // DialogueManager의 다음 라인 출력 함수 호출
                dialogueManager.DisplayNextLine();
            }
        }
    }

    // ***************************************************************
    // DialogueManager에서 대화가 모두 끝났을 때 호출할 함수 (범용 사용)
    // ***************************************************************
    public void OnDialogueFinished()
    {
        canStartDialogue = false; // 입력 무시

        if (loadNextSceneOnFinish)
        {
            Debug.Log($"모든 대화 종료. 다음 씬 ({nextSceneName})으로 전환합니다.");

            if (fader != null)
            {
                // 페이드 아웃 후 씬 전환
                StartCoroutine(fader.FadeOutAndLoadScene(nextSceneName));
            }
            else
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
        else
        {
            Debug.Log("대화 종료. 씬 전환 없이 현재 씬에 머뭅니다.");
            // 선택지 출력 등의 다음 로직을 여기에 구현합니다.
        }
    }
}