using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using UnityStandardAssets.Characters.ThirdPerson;

public class DialogLauncher : Interractable
{
    public string Key;
    private ModuleManager moduleM;
    DialogManager manager;

    public override void Interact(Player p)
    {
        moduleM = FindObjectOfType<ModuleManager>();
        moduleM.PlayerMoveController.enabled = false;
        HudManager hud = moduleM.HudManager;
        manager = hud.ShowDialogUI();

        Dialog dialog = new Dialog(Key);
        manager.ShowDialog(dialog, InteractionFinished);
    }

    public override void InteractionFinished()
    {
        moduleM.PlayerMoveController.enabled = true;
        manager.gameObject.SetActive(false);
        base.InteractionFinished();
    }
}
