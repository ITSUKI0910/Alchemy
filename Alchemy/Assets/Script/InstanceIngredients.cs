using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceIngredients : MonoBehaviour
{
    [SerializeField]
    GameObject[] ingredients;
    [SerializeField, Tooltip("生成する場所")]
    Transform instancePoint;

    List<IngredientsName> removeNames = new List<IngredientsName>();

    public enum IngredientsName
    {
        curryRoux,
        carrot,
        potato
    }

    // 具材を生成
    public void InstanceIngredient(IngredientsName ingredientsType)
    {
        // 同じのがあったら返す
        if (removeNames.Contains(ingredientsType)) return;

        // 追加
        removeNames.Add(ingredientsType);
        Instantiate(ingredients[(int)ingredientsType], instancePoint);
    }
}
