using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class Pause : MonoBehaviour
{
    bool swap = false;
    public GameObject canv;

    void Update()
    {
        if (Input.GetButtonDown("Cancel")){
            
            mPause();
        }
    }

    public void mPause()
    {
        swap = !swap;
        if (swap == true)
        {
            canv.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            canv.SetActive(false);
            Cursor.visible = false;
        }


    }
    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);

    }
    public void Quit()
    {
        Application.Quit();
    }
}
