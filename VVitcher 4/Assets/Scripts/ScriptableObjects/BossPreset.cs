using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBossPreset", menuName = "ScriptableObjects/BossPreset")]
public class BossPreset : ScriptableObject {
    public string bossName;
    public int bloodyToSummon;
    public int creackyToSummon;
    public int linthyToSummon;
}
