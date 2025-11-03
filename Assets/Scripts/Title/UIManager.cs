using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환 기능을 위해 반드시 필요

public class UIManager : MonoBehaviour
{
    // 1. 새 게임 (New Game)
    public void StartNewGame()
    {
        // 'GameScene'이라는 이름의 씬으로 이동합니다.
        // 씬 이름은 Build Settings에 등록된 이름과 일치해야 합니다.
        SceneManager.LoadScene("NewGame");
    }

    // 2. 이어하기 (Load Game)
    public void LoadGame()
    {
        // 'LoadScene'이라는 이름의 씬 또는 저장된 위치의 씬으로 이동합니다.
        // 실제 게임에서는 세이브 파일 로드 로직이 필요합니다.
        SceneManager.LoadScene("LoadGame");
    }

    // 3. 세팅 (Settings)
    public void OpenSettings()
    {
        // 'SettingScene'이라는 이름의 씬으로 이동합니다.
        // 또는 캔버스 내의 세팅 패널을 활성화(SetActive(true))할 수도 있습니다.
        SceneManager.LoadScene("Setting");
    }

    // 4. 나가기 (Exit Game)
    public void QuitGame()
    {
        // 에디터에서 실행 중일 경우와 빌드된 게임일 경우 다르게 작동합니다.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터 중지
#else
            Application.Quit(); // 게임 종료
#endif
    }
}