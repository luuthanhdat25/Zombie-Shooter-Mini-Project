using RepeatUtil;
using UI;
using UnityEngine;

public class InPlayingUI : RepeatMonoBehaviour
{
    [SerializeField]
    private PlayerGunStatusUI playerGunStatusUI;
    public PlayerGunStatusUI PlayerGunStatusUI => playerGunStatusUI;

    [SerializeField]
    private PlayerHealthStatusUI playerHealthStatusUI;
    private PlayerHealthStatusUI PlayerHealthStatusUI => playerHealthStatusUI;

    [SerializeField]
    private MessageUI messageUI;
    public MessageUI MessageUI => messageUI;

    [SerializeField]
    private KeyCollectedUI keyCollectedUI;
    public KeyCollectedUI KeyCollectedUI => keyCollectedUI;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadComponentInChild(ref playerGunStatusUI, gameObject);
        LoadComponentInChild(ref playerHealthStatusUI, gameObject);
        LoadComponentInChild(ref messageUI, gameObject);
        LoadComponentInChild(ref keyCollectedUI, gameObject);
    }

    public void ShowUI(bool isShow)
    {
        playerGunStatusUI.gameObject.SetActive(isShow);
        playerHealthStatusUI.gameObject.SetActive(isShow);
        messageUI.gameObject.SetActive(isShow);
        keyCollectedUI.gameObject.SetActive(isShow);
    }
}
