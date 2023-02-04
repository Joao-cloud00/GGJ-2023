using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Tower nativeTower;
    Manager manager;

    private void Awake()
    {
        manager = FindObjectOfType<Manager>();
    }

    public void BuyTowerNative()
    {
        if(manager.Money >= nativeTower.Price)
        {
            manager.Money -= nativeTower.Price;
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(nativeTower, new Vector2(cursorPos.x, cursorPos.y), Quaternion.identity);
        }
    }
}
