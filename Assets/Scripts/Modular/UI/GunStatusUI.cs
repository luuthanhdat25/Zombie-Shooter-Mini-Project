using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunStatusUI : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private TMP_Text bulletCountText;

    [SerializeField]
    private GunSelector playerGunSelector;

    [SerializeField] 
    private GameObject reloadBarGameObject;
    
    [SerializeField] 
    private Image reloadBarImage;

    private void Awake()
    {
        playerGunSelector.OnUpdatedReloadTimer += PlayerGunSelector_OnUpdatedReloadTimer;
        playerGunSelector.OnSwitchGun += PlayerGunSelector_OnSwitchGun;
        playerGunSelector.OnUpdatedBullet += PlayerGunSelector_OnUpdatedBullet;
        reloadBarGameObject.SetActive(false);
    }

    private void PlayerGunSelector_OnUpdatedBullet(object sender, GunSelector.OnUpdatedBulletEventArgs e)
    {
        bulletCountText.text = e.CurrentBullet + "/" + e.TotalBullet;
    }

    private void PlayerGunSelector_OnUpdatedReloadTimer(object sender, GunSelector.OnReloadEventArgs e)
    {
        reloadBarImage.fillAmount = e.ReloadtimerNormalize;

        if (e.ReloadtimerNormalize == 0 || e.ReloadtimerNormalize == 1)
        {
            reloadBarGameObject.SetActive(false);
        }
        else
        {
            reloadBarGameObject.SetActive(true);
        }
    }

    private void PlayerGunSelector_OnSwitchGun(object sender, GunSelector.OnSwitchGunEventArgs e)
    {
        image.sprite = e.GunSO.ImageSprite;
    }
}
