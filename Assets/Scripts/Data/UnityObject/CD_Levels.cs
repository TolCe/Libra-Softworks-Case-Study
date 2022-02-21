using Data.ValueObject;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config Data/Level List", order = 33)]
public class CD_Levels : SerializedScriptableObject
{
    public List<LevelVO> Data;
}