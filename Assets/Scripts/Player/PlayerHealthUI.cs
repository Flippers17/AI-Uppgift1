using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField]
    private Slider _healthBar;

    
    public void SetHealth(int value)
    {
        _healthBar.value = value;
    }
}
