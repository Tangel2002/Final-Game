using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadLevel(string name)
    {
        if(name == "LayoutOne")
        {
            Cursor.visible = false;
        }
        SceneManager.LoadScene(name);

    }
    public void Quit()
    {
        Application.Quit();
    }
}
