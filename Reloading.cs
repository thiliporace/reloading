using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reloading : MonoBehaviour
{
    public int maxAmmo = 100000;
    [HideInInspector] public int currentMaxAmmo;
    public int ammoCapacity = 50;
    [HideInInspector] public int currentAmmo;

    public float reloadSpeed = 1f;
    public float refillSpeed = 1.5f;

    public Text ammoText;

    [HideInInspector] public bool needReload;
    [HideInInspector] public bool refilling;

    public Animator anim;

    void Start()
    {
        currentAmmo = ammoCapacity;
        currentMaxAmmo = maxAmmo;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (currentAmmo <= 0)
            needReload = true;

        if (currentMaxAmmo > 0 && currentAmmo < ammoCapacity)
        {
            if (Input.GetKeyDown(KeyCode.R))
                StartCoroutine(Reload());
        }

        ammoText.text = "ammo : " + currentAmmo;
    }

    IEnumerator Reload()
    {
        Debug.Log("Reload");
        anim.SetBool("isReloading", true);
        yield return new WaitForSeconds(reloadSpeed);
        needReload = false;

        if ((ammoCapacity - currentAmmo) <= currentMaxAmmo)
        {
            currentMaxAmmo -= (ammoCapacity - currentAmmo);
            currentAmmo += (ammoCapacity - currentAmmo);
        }
        else
        {
            currentAmmo += currentMaxAmmo;
            currentMaxAmmo = 0;
        }

        anim.SetBool("isReloading", false);
    }

    public void RefillAmmo()
    {
        Debug.Log("Refilling ammo");

        refilling = true;
        currentMaxAmmo = maxAmmo;
    }

    public void DecreaseAmmo()
    {
        currentAmmo--;
    }
}
