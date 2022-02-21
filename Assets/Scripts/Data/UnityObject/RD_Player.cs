using Data.ValueObject;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(menuName = "Runtime Data/Player Data", order = 1)]
    public class RD_Player : SerializedScriptableObject
    {
        public PlayerVO Data;

        [Button(ButtonSizes.Gigantic)]
        public void Save()
        {
            ES3.Save("PlayerData", Data);
        }

        [Button(ButtonSizes.Gigantic)]
        public void Load()
        {
            Data = ES3.Load("PlayerData", Data);
        }

        [Button(ButtonSizes.Medium)]
        public void ResetData()
        {
            Data.TotalStarsCollected = 0;
            Save();
        }
    }
}