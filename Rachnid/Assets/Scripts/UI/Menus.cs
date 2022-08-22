using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour
{
    public static Menus Instance;
    public bool paused = false;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject noteMenu;
    [SerializeField] private GameObject blur;
    public enum MenuOpen { None, Pause, Inventory, ToDo, Note }
    public MenuOpen menuOpen;
    private void Awake()
    {
        Instance = this;
        inventory.SetActive(paused);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause(MenuOpen.Pause, pauseMenu);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePause(MenuOpen.Inventory, inventory);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            TogglePause(MenuOpen.Note, noteMenu);
        }
    }
    void TogglePause(MenuOpen menu, GameObject obj)
    {
        if (menuOpen != MenuOpen.None && menu != menuOpen) return;

        paused = !paused;
        menuOpen = paused ? menu : MenuOpen.None;

        obj.SetActive(paused);
        blur.SetActive(paused);
        Player.Instance.canMove = !paused;

        Time.timeScale = paused ? 0 : 1;

        Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = paused;
    }
}