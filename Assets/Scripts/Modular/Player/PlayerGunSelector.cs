using ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using System;

namespace Player
{
    public class PlayerGunSelector : MonoBehaviour
    {
        [SerializeField]
        private List<GunSO> gunSOList;

        private int indexSelectGun;

        private void Start()
        {
            InputManager.Instance.OnSwitchGun += NewMethod;
        }

        private void NewMethod()
        {
            Debug.Log("Switch Gun");
        }
    }
}
