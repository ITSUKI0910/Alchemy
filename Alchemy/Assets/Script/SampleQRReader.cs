using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleQRReader : MonoBehaviour
{
    string _result = null;
    WebCamTexture _webCam;

    // 読み取り開始していいかのチェック
    private bool readCheck = true;

    [SerializeField, Tooltip("時間で指定")]
    private float intervalSecond = 10;
    // 秒数をFixedUpdateで回した時のフレーム数に変換
    private float intervalFPSConversion;
    private float defaultInterval;

    [SerializeField]
    Renderer planeObj;

    [SerializeField]
    InstanceIngredients instanceIngredients;

    IEnumerator Start()
    {
        intervalFPSConversion = intervalSecond / Time.fixedDeltaTime;
        defaultInterval = intervalFPSConversion;

        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam) == false)
        {
            Debug.LogFormat("no camera.");
            yield break;
        }
        Debug.LogFormat("camera ok.");
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices == null || devices.Length == 0)
            yield break;
        _webCam = new WebCamTexture(devices[0].name, Screen.width, Screen.height, 12);
        _webCam.Play();
        planeObj.material.mainTexture = _webCam;
    }

    private void Update()
    {
        if (_webCam != null && readCheck)
        {
            _result = QRCodeHelper.Read(_webCam);
            Debug.LogFormat("result : " + _result);

            if (_result != "error")
            {
                InstanceIngredients.IngredientsName name;
                Enum.TryParse(_result, out name);
                instanceIngredients.InstanceIngredient(name);
                readCheck = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if(!readCheck)
        {
            // FPSを引く
            intervalFPSConversion--;

            // インターバル終了したら読み取れるようにする
            if (intervalFPSConversion <= 0.0f)
            {
                StartCoroutine(CheckResultName());
                readCheck = true;
                intervalFPSConversion = defaultInterval;
            }
        }
    }

    IEnumerator CheckResultName()
    {
        while (true)
        {

            yield return new WaitForFixedUpdate();
        }
    }
}