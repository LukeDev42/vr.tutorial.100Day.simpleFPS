using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour {

    public Text ammoText;

    private void Start()
    {
        {
            PlayerShootingController shootingController =
                Camera.main.GetComponentInChildren<PlayerShootingController>();
            UpdateAmmoText(shootingController.MaxAmmo, shootingController.MaxAmmo);
        }
    }

    public void UpdateAmmoText(float currentAmmo, float maxAmmo)
    {
        ammoText.text = currentAmmo + "/" + maxAmmo;
    }
}
