using UnityEngine;

// 싱글톤 패턴을 적용하여 씬 전환 시에도 파괴되지 않게 합니다.
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    // Inspector에서 생성한 PlayerStatsData 에셋 파일을 연결합니다.
    public PlayerInfo statsData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // 씬이 전환되어도 이 오브젝트가 파괴되지 않도록 설정합니다.
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // 초기 데이터 로드 (실제 게임에서는 세이브 파일 로드 로직이 필요)
        InitializeStats();
    }

    private void InitializeStats()
    {
        if (statsData == null)
        {
            Debug.LogError("PlayerStatsData 에셋이 PlayerManager에 연결되지 않았습니다!");
            return;
        }

        // 초기화 시 현재 체력 등을 최대치로 설정 (또는 세이브 파일에서 로드)
        statsData.currentHealth = statsData.maxHealth;
        // ... 다른 스탯들도 초기화 로직 수행
    }

    // 예시: 생존 수치를 변경하는 범용 함수
    public void ChangeStat(string statName, int amount)
    {
        switch (statName.ToLower())
        {
            case "health":
                statsData.currentHealth = Mathf.Clamp(statsData.currentHealth + amount, 0, statsData.maxHealth);
                break;
            case "hunger":
                statsData.currentHunger = Mathf.Clamp(statsData.currentHunger + amount, 0, statsData.maxHunger);
                break;
            case "thirst":
                statsData.currentHunger = Mathf.Clamp(statsData.currentThirst + amount, 0, statsData.maxThirst);
                break;
            case "sanity":
                statsData.currentSanity = Mathf.Clamp(statsData.currentSanity + amount, 0, statsData.maxSanity);
                break;
                // ... 다른 스탯 로직 추가
        }
        Debug.Log($"{statName} 변경: 현재 {statName} = {GetStatValue(statName)}");

        // TODO: UI 업데이트 이벤트 호출 로직 필요
    }

    // 예시: 현재 스탯 값을 가져오는 함수
    public int GetStatValue(string statName)
    {
        switch (statName.ToLower())
        {
            case "health": return statsData.currentHealth;
            case "hunger": return statsData.currentHunger;
            case "thirst": return statsData.currentThirst;
            case "sanity": return statsData.currentSanity;
            default: return -1;
        }
    }
}