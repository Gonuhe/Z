using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanRoomObjective : Objective
{

    public override void StartObjective(Quest parent)
    {
        base.StartObjective(parent);
        TanguyComesToTalk();
    }

    private void TanguyComesToTalk()
    {
        Player player = FindObjectOfType<Player>();

        player.AllowToMove(false);

        GameObject Tanguy = GameObject.Find("Tanguy"); //TODO optimiser tout ceci et virer tous ces find. Ce n'est pas un problème simple, à voir en fonction de la gestion de scène et de comment évolue la gestion des quêtes et leur initialisation.

        Vector3 tanguyTargetPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 2);

        SimpleTranslationCutScene tanguyMoves = new SimpleTranslationCutScene(Tanguy, tanguyTargetPos, 5, TanguyTalks);
        tanguyMoves.StartCutScene();
    }

    private void TanguyTalks()
    {
        DialogManager manager = FindObjectOfType<HudManager>().ShowDialogUI();
        manager.ShowDialog(new Dialog("MyMotherIsComingToTown", "MyMotherJustCalledMe"), Placeholder);
    }

    private void Placeholder()
    {
        FinishObjective();
    }

}
