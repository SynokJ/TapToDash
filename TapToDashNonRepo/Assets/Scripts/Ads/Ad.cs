using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ad : MonoBehaviour, IAd
{

    public InterstitialAd inter_ad;
    private AdsInitializer ad_init;

    private void Start()
    {

        if (inter_ad == null)
        {
            ad_init.OnErrored += OnError;
            Debug.Log("GG");
            return;
        }


        Debug.Log("BB " + Time.deltaTime);
        inter_ad.OnCompleted += OnComplete;
        inter_ad.OnErrored += OnError;
    }

    public void OnComplete()
    {
        int coin_num = PlayerPrefs.GetInt("CoinNum", 0);
        coin_num += 10;
        PlayerPrefs.SetInt("CoinNum", coin_num);
    }

    public void OnError()
    {
        Debug.Log("SNTH Wrong with Ad");
    }
}
