using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : MonoBehaviour
{


    public void ButtonClickStageINF()
    {
        BackendGameUpgradeData.Instance.GameDataUpdate();
        Utils.LoadScene(SceneNames.LevelINF);

    }

    public void ButtonClickStage1()
    {
        BackendGameUpgradeData.Instance.GameDataUpdate();
        Utils.LoadScene(SceneNames.Level01);
    }

    public void ButtonClickStage2()
    {
        BackendGameUpgradeData.Instance.GameDataUpdate();
        Utils.LoadScene(SceneNames.Level02);
    }

    public void ButtonClickStage3()
    {
        BackendGameUpgradeData.Instance.GameDataUpdate();
        Utils.LoadScene(SceneNames.Level03);
    }

    public void ButtonClickStage4()
    {
        BackendGameUpgradeData.Instance.GameDataUpdate();
        Utils.LoadScene(SceneNames.Level04);
    }

    public void ButtonClickStage5()
    {
        BackendGameUpgradeData.Instance.GameDataUpdate();
        Utils.LoadScene(SceneNames.Level05);
    }

    public void ButtonClickStage6()
    {
        BackendGameUpgradeData.Instance.GameDataUpdate();
        Utils.LoadScene(SceneNames.Level06);
    }

    public void ButtonClickStage7()
    {
        BackendGameUpgradeData.Instance.GameDataUpdate();
        Utils.LoadScene(SceneNames.Level07);
    }

    public void ButtonClickStage8()
    {
        BackendGameUpgradeData.Instance.GameDataUpdate();
        Utils.LoadScene(SceneNames.Level08);
    }

    public void ButtonClickStage9()
    {
        BackendGameUpgradeData.Instance.GameDataUpdate();
        Utils.LoadScene(SceneNames.Level09);
    }

    public void ButtonClickStage10()
    {
        BackendGameUpgradeData.Instance.GameDataUpdate();
        Utils.LoadScene(SceneNames.Level10);
    }

    public void ButtonClickStage11()
    {
        BackendGameUpgradeData.Instance.GameDataUpdate();
        Utils.LoadScene(SceneNames.Level11);
    }

    public void ButtonClickStage12()
    {
        BackendGameUpgradeData.Instance.GameDataUpdate();
        Utils.LoadScene(SceneNames.Level12);
    }


}
