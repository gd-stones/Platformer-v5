using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeLevelOnClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string _levelToLoad;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("level dddddd");
    }
}
