using UnityEngine;
using System;

// Unity 에디터의 Asset 메뉴에 추가 (Assets -> Create -> Dialogue -> New Dialogue)
[CreateAssetMenu(fileName = "NewDialogueData", menuName = "Dialogue/Dialogue Group", order = 1)]
public class DialogueData : ScriptableObject
{
    // === 에디터에서 직접 편집할 텍스트 목록 ===
    public DialogueLine[] lines;

    // TODO: 선택지 데이터 구조를 여기에 추가할 수 있습니다.
    // public DialogueChoiceData[] choices; 
}

// 하나의 대화 라인 구조 (DialogueData.lines 배열의 요소)
[System.Serializable]
public class DialogueLine
{
    public string speaker = "서술";    // 대화 주체 (예: "난바다", "서술")
    [TextArea(3, 10)]
    public string text;       // 실제 출력할 텍스트 내용
    public float delay;       // 이 텍스트 출력 후 다음 텍스트까지의 지연 시간 (선택 사항)
}