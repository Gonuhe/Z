using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class ModuleManager:MonoBehaviour
{

    public HudManager HudManager;
    public ThirdPersonUserControl PlayerMoveController;

    void Awake()
    {
        HudManager = FindObjectOfType<HudManager>();
        PlayerMoveController = FindObjectOfType<ThirdPersonUserControl>();
    }
}
