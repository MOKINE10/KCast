using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{

    public float RSpeedRate;
    public float amp;
    public float frequency;
    public float offset;
    public Animator Avater;

    bool start;
    float count = 0;
    float RSpeed;
    float dyRate = 0.1f;
    float xRate = 0.1f;
    float zRate = 0.1f;
    float ampRate = 0.1f;
    float frequencyRate = 0.01f;
    float x;
    float y;
    float dy;
    float z;
    float da;
    float memA;
    float df;

    // Use this for initialization
    void Start()
    {
        Stop();
    }

    // 毎フレームの制御
    void Update()
    {
        if (start)
        {
            count += Time.deltaTime;
            Cursor.visible = false;
        }

        KeyBoardCommands();
        GamePadCommands();

        y = da * Mathf.Sin(2 * Mathf.PI * df * count) + dy;
        this.transform.position = new Vector3(x, y, z);

        if (Input.GetKey(KeyCode.Escape))
        {
            Quit();
        }

    }


    // アバターを制御するメソッドたち
    void Stop()
    {
        start = false;
        count = 0;
        Avater.SetBool("Home", true);
        RSpeed = 0;
        Avater.SetFloat("RSpeed", RSpeed);
        x = 0;
        z = 0;
        dy = 0;
        df = 0;
        da = 0;
    }

    void InitPosition()
    {
        start = true;
        RSpeed = 1;
        Avater.SetBool("Home", false);
        Avater.SetFloat("RSpeed", RSpeed);
        x = 0;
        z = 0;
        dy = offset;
        df = frequency;
        da = amp;
    }

    void StopRotation()
    {
        if (!start)
        {
            InitPosition();
        }

        else if (Avater.GetFloat("RSpeed") == 0)
        {
            Avater.SetFloat("RSpeed", RSpeed);
        }

        else
        {
            Avater.SetFloat("RSpeed", 0);
        }
    }

    void MoveUp()
    {
        dy = dy + dyRate;
    }

    void MoveDown()
    {
        dy = dy - dyRate;
    }

    void MoveLeft()
    {
        x = x - xRate;
    }

    void MoveRight()
    {
        x = x + xRate;
    }

    void MoveClose()
    {
        z = z - zRate;
    }

    void MoveFar()
    {
        z = z + zRate;
    }

    void LeftRotation()
    {
        if (Avater.GetBool("Reverse") && RSpeed > 1)
        {
            RSpeed = 1;
            Avater.SetFloat("RSpeed", RSpeed);
        }

        else if (Avater.GetBool("Reverse") && RSpeed == 1)
        {
            Avater.SetBool("Reverse", false);
        }

        else
        {
            RSpeed = RSpeed + RSpeedRate;
            Avater.SetFloat("RSpeed", RSpeed);
        }
    }

    void RightRotation()
    {
        if (!Avater.GetBool("Reverse") && RSpeed > 1)
        {
            RSpeed = 1;
            Avater.SetFloat("RSpeed", RSpeed);
        }

        else if (!Avater.GetBool("Reverse") && RSpeed == 1)
        {
            Avater.SetBool("Reverse", true);
        }

        else
        {
            RSpeed = RSpeed + RSpeedRate;
            Avater.SetFloat("RSpeed", RSpeed);
        }
    }

    void UpFrequency()
    {
        df = df + frequencyRate;
    }

    void DownFrequency()
    {
        if (df > 0)
        {
            df = df - frequencyRate;
        }

        else
        {
            df = 0;
        }

    }

    void UpAmp()
    {
        da = da + ampRate;
    }

    void DownAmp()
    {
        if (da > 0)
        {
            da = da - ampRate;
        }

        else

        {
            da = 0;
        }

    }

    void StopFloat()
    {

        if (da == 0)
        {
            da = memA;
        }

        else
        {
            memA = da;
            da = 0;
        }

    }



    // キーボード入力の場合の制御
    void KeyBoardCommands()
    {
        if (Input.GetButton("Option1"))
        {
            if (Input.GetButtonDown("Start"))
            {
                StopFloat();
            }

            if (Input.GetButtonDown("Left"))
            {
                LeftRotation();
            }

            if (Input.GetButtonDown("Right"))
            {
                RightRotation();
            }

            if (Input.GetButton("Up"))
            {
                UpFrequency();
            }

            if (Input.GetButton("Down"))
            {
                DownFrequency();
            }
        }

        else if (Input.GetButton("Option2"))
        {

            if (Input.GetButton("Left"))
            {
                DownFrequency();
                DownAmp();
            }

            if (Input.GetButton("Right"))
            {
                UpFrequency();
                UpAmp();
            }

            if (Input.GetButton("Up"))
            {
                UpAmp();
            }

            if (Input.GetButton("Down"))
            {
                DownAmp();
            }
        }

        else if (Input.GetButton("Option3"))
        {

            if (Input.GetButtonDown("Start"))
            {
                Stop();
            }

            if (Input.GetButtonUp("Start"))
            {
                InitPosition();
            }

            if (Input.GetButton("Left"))
            {
                DownFrequency();
                DownAmp();
            }

            if (Input.GetButton("Right"))
            {
                UpFrequency();
                UpAmp();
            }

            if (Input.GetButton("Up"))
            {
                MoveFar();
            }

            if (Input.GetButton("Down"))
            {
                MoveClose();
            }
        }

        else
        {
            if (Input.GetButtonDown("Start"))
            {
                StopRotation();
            }

            if (Input.GetButton("Left"))
            {
                MoveLeft();
            }

            if (Input.GetButton("Right"))
            {
                MoveRight();
            }

            if (Input.GetButton("Up"))
            {
                MoveUp();
            }

            if (Input.GetButton("Down"))
            {
                MoveDown();
            }
        }
    }

    // ゲームパッド入力の場合の制御
    void GamePadCommands()
    {

        if (Input.GetAxis("Horizontal") < -0.5)
        {
            MoveLeft();
        }

        if (Input.GetAxis("Horizontal") > 0.5)
        {
            MoveRight();
        }

        if (Input.GetAxis("Vertical") < -0.5)
        {
            MoveUp();
        }

        if (Input.GetAxis("Vertical") > 0.5)
        {
            MoveDown();
        }

        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetAxis("Frequency") > 0.5)
            {
                UpFrequency();
            }

            if (Input.GetAxis("Frequency") < -0.5)
            {
                DownFrequency();
            }

            if (Input.GetAxis("FarWin") < -0.5)
            {
                MoveFar();
            }

            if (Input.GetAxis("FarWin") > 0.5)
            {
                MoveClose();
            }
        }

        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
        {
            if (Input.GetAxis("Frequency") < -0.5)
            {
                UpFrequency();
            }

            if (Input.GetAxis("Frequency") > 0.5)
            {
                DownFrequency();
            }

            if (Input.GetAxis("FarMac") < -0.5)
            {
                MoveFar();
            }

            if (Input.GetAxis("FarMac") > 0.5)
            {
                MoveClose();
            }
        }

        if (Input.GetButtonDown("Stop"))
        {
            Stop();
        }

        if (Input.GetButtonDown("StopFloat"))
        {
            StopFloat();
        }

        if (Input.GetButtonUp("Init"))
        {
            InitPosition();
        }

        if (Input.GetButtonDown("LeftRotation"))
        {
            LeftRotation();
        }

        if (Input.GetButtonDown("RightRotation"))
        {
            RightRotation();
        }

        if (Input.GetAxis("Amp") > 0.5)
        {
            UpAmp();
        }

        if (Input.GetAxis("Amp") < -0.5)
        {
            DownAmp();
        }

    }

    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();

#endif
    }

}
