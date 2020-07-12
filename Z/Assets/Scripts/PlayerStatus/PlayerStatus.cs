using UnityEngine;
using System.Collections;

public enum PlayerStatusType
{
    Hunger, Sleep, Shower, Pee, Friends, Hobbies, LifeQuality
}

public class PlayerStatus : MonoBehaviour
{
    public PlayerStatusElement GetPlayerStatusElement(PlayerStatusType type)
    {
        switch(type)
        {
            case PlayerStatusType.Hunger:
                return GetComponentInChildren<Hunger>();
            case PlayerStatusType.Sleep:
                return GetComponentInChildren<Sleep>();
            case PlayerStatusType.Shower:
                return GetComponentInChildren<Shower>();
            case PlayerStatusType.Pee:
                return GetComponentInChildren<Pee>();
            case PlayerStatusType.Friends:
                return GetComponentInChildren<Friends>();
            case PlayerStatusType.Hobbies:
                return GetComponentInChildren<Hobbies>();
            case PlayerStatusType.LifeQuality:
                return GetComponentInChildren<LifeQuality>();
        }
        return null;
    }

}
