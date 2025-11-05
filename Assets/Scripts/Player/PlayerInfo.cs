using System;
using System.Collections.Generic;
using UnityEngine;

// Unity 에디터의 Asset 메뉴에 추가 (Assets -> Create -> Dialogue -> New Dialogue)
[CreateAssetMenu(fileName = "PlayerInfo", menuName = "PlayerData", order = 0)]
public class PlayerInfo : ScriptableObject
{
    // =======================================================
    // 1. 핵심 생존 수치 (Survival Core Stats)
    // =======================================================
    [Header("Core Survival Stats")]

    // 최대치와 현재치를 분리하여 관리
    public int maxHealth = 100;
    public int currentHealth = 100;

    public int maxHunger = 100;
    public int currentHunger = 100;

    public int maxThirst = 100;
    public int currentThirst = 100;

    public int maxSanity = 100; // 정신력 (Post-apocalypse 생존 게임의 핵심)
    public int currentSanity = 100;

    // =======================================================
    // 2. 캐릭터 능력치 및 기술 (Skills & Attributes)
    // =======================================================
    [Header("Skills and Attributes")]
    [Tooltip("제작(Crafting) 성공률 등에 영향을 줍니다.")]
    public int craftingSkillLevel = 1;

    [Tooltip("탐색 및 위험 감지 성공률에 영향을 줍니다.")]
    public int perceptionLevel = 1;

    [Tooltip("전투 또는 힘을 쓰는 행동의 성공률에 영향을 줍니다.")]
    public int strengthLevel = 1;

    // =======================================================
    // 3. 인벤토리 및 상태 (Inventory & State)
    // =======================================================
    [Header("Inventory and Items")]
    public int maxInventorySlots = 10;

    // 현재 보유 중인 아이템 목록 (실제 게임에서는 별도의 ItemData ScriptableObject를 사용)
    public List<string> inventory = new List<string>();

    [Header("Game State Flags")]
    [Tooltip("현재 진행 중인 스토리나 퀘스트의 플래그")]
    public List<string> storyFlags = new List<string>();
}