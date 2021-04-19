using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /*for (int i = 0; i < 6; i++)
        {
            int count = i;
            PlayerPrefs.SetInt("Profile_Count", PlayerPrefs.GetInt("Profile_Count") + 1);
            string name;
            switch (i)
            {
                case 0:
                    name = "Wizard";
                    break;
                case 1:
                    name = "Afro";
                    break;
                case 2:
                    name = "Knight";
                    break;
                case 3:
                    name = "TopHat";
                    break;
                case 4:
                    name = "Builder";
                    break;
                case 5:
                    name = "Viking";
                    break;
                case 6:
                    name = "Clown";
                    break;
                default:
                    name = ".";
                    break;
            }
            PlayerPrefs.SetString("Profile_" + count.ToString() + "_Name", name);
            PlayerPrefs.SetInt("Profile_" + count.ToString() + "_EXP", 0);
            PlayerPrefs.SetInt("Profile_" + count.ToString() + "_HAT", count);
        }
        Debug.Log(PlayerPrefs.GetString("Profile_0_Name"));
        if (!PlayerPrefs.HasKey("Test"))
        {
            PlayerPrefs.SetInt("Test", 1);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateNewProfile(string name)
    {
        /*int count = PlayerPrefs.GetInt("Profile_Count");
        PlayerPrefs.SetInt("Profile_Count", PlayerPrefs.GetInt("Profile_Count") + 1);
        PlayerPrefs.SetString("Profile_" + count.ToString() + "_Name", name);
        PlayerPrefs.SetInt("Profile_" + count.ToString() + "_EXP", 0);
        PlayerPrefs.SetInt("Profile_" + count.ToString() + "_HAT", count);*/
    }

    public void WARNING_GODsRESET()
    {
        PlayerPrefs.DeleteAll();
    }
}
