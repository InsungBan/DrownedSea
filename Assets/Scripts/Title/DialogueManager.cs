using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    // === 1. Inspector에서 연결할 컴포넌트 ===
    [Header("UI References")]
    public TextMeshProUGUI dialogueText; // 대화가 출력될 TextMeshPro 컴포넌트

    // === 2. 외부 스크립트 연결 (대화 시작/종료 알림) ===
    [Header("Manager References")]
    // DialogueSceneLoader가 이 오브젝트를 참조해야 함 (Inspector에서 연결 필요)
    public DialogueSceneLoader sceneLoader;

    // === 3. 타이핑 및 속도 설정 ===
    [Header("Typing Settings")]
    public float typingSpeed = 0.05f; // 글자 하나당 출력되는 시간 (초)

    // === 4. 런타임 데이터 ===
    private DialogueData currentDialogueData;
    private int currentLineIndex = 0;
    private bool isTyping = false;
    private string playerName = "난바다"; // PlayerPrefs에서 로드될 기본값

    // *******************************************************************
    // 1. 외부에서 데이터를 받아 대화를 시작하는 범용 함수
    // *******************************************************************
    public void StartDialogue(DialogueData data)
    {
        if (data == null || data.lines == null || data.lines.Length == 0)
        {
            Debug.LogError("DialogueManager: 로드된 대화 데이터가 유효하지 않습니다.");
            return;
        }

        currentDialogueData = data;
        currentLineIndex = 0;

        // 이름 로드: PlayerPrefs에 저장된 이름이 없다면 "난바다" 사용
        playerName = PlayerPrefs.GetString("PlayerName", "난바다");

        DisplayNextLine(); // 첫 번째 대화 라인 출력 시작
    }

    // *******************************************************************
    // 2. 다음 대화 라인 출력 로직 (클릭 또는 자동 호출)
    // *******************************************************************
    public void DisplayNextLine()
    {
        // === 변수 선언을 함수 최상단으로 이동하여 중복 선언 오류 방지 ===
        string rawText = "";
        string finalSentence = "";

        // 1. 타이핑 중일 때의 처리 로직
        if (isTyping)
        {
            // 타이핑 중일 때 클릭하면, 타이핑을 즉시 완료
            StopAllCoroutines();

            // 현재 출력 중이던 문장을 완전히 출력 (currentLineIndex - 1 사용)
            if (currentLineIndex > 0 && currentLineIndex <= currentDialogueData.lines.Length)
            {
                // **할당만 하고 선언은 하지 않음**
                DialogueLine previousLine = currentDialogueData.lines[currentLineIndex - 1];
                rawText = previousLine.text ?? "";

                // finalSentence에 값 할당
                finalSentence = rawText.Replace("{}", playerName ?? "난바다");

                dialogueText.text = finalSentence;
            }
            isTyping = false;
            return;
        }

        // 2. 대화 종료 체크 로직
        if (currentDialogueData == null || currentLineIndex >= currentDialogueData.lines.Length)
        {
            EndDialogue();
            return;
        }

        // 3. 다음 문장 출력 로직

        // 현재 라인 데이터 가져오기
        DialogueLine line = currentDialogueData.lines[currentLineIndex];

        // **NULL 안전성 확보 및 값 할당**
        rawText = line.text ?? ""; // line.text가 null이면 빈 문자열로 처리
        string currentName = playerName ?? "난바다"; // playerName이 null이면 기본값 처리

        // 이름 대체 로직 (finalSentence에 값 할당)
        finalSentence = rawText.Replace("{}", currentName);

        // 타이핑 시작
        StartCoroutine(TypeSentence(finalSentence, line.delay));

        // 다음을 위해 인덱스 증가
        currentLineIndex++;
    }

    // *******************************************************************
    // 3. 텍스트를 한 글자씩 출력하는 코루틴
    // *******************************************************************
    IEnumerator TypeSentence(string sentence, float delayAfterTyping)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

        // 타이핑 완료 후 설정된 지연 시간만큼 대기
        if (delayAfterTyping > 0)
        {
            yield return new WaitForSeconds(delayAfterTyping);
        }
    }

    // *******************************************************************
    // 4. 대화 종료 처리
    // *******************************************************************
    void EndDialogue()
    {
        Debug.Log("DialogueManager: 현재 대화 그룹 종료.");

        // SceneLoader에게 대화가 끝났음을 알리고 다음 씬 전환을 위임합니다.
        if (sceneLoader != null)
        {
            sceneLoader.OnDialogueFinished();
        }
    }
}