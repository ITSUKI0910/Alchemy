using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestinstansObj : MonoBehaviour
{
    /*
     入ってきたストリング型から
     モデルを持ってきて
     生成する

     自動で落下して
     鍋といい感じに当たり判定したら
     消してLogを出力する
     */

    Dictionary<string,string> modeldate;

    //モデルを生成するところ
    bool Generate3dModel(string modelname)
    {
        //まず同じ名前があるか
        if (modeldate[modelname] !=modelname) return false;
        //あったら生成させる
        GameObject obj = (GameObject)Resources.Load(modeldate[modelname]);
        Instantiate(obj, Vector3.zero, new Quaternion());
        return true;
    }
    void Start()
    {
        GameObject obj = (GameObject)Resources.Load("3dModel/curryRoux/curryRoux.obj");
        Instantiate(obj, Vector3.zero, new Quaternion());
    }

}
