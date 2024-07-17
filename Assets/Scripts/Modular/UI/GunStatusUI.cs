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

    private void Awake()
    {
        playerGunSelector.OnSwitchGun += PlayerGunSelector_OnSwitchGun;
    }

    private void PlayerGunSelector_OnSwitchGun(object sender, GunSelector.OnSwitchGunEventArgs e)
    {
        image.sprite = e.GunSO.ImageSprite;
    }
}
