﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadHighestScore : MonoBehaviour
{
    public int highScore = 0;

    //Save the score info out to a binary file
    public void Save(int _highScore)
    {
        highScore = _highScore;

        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Open(Application.persistentDataPath + "/HighScore.cas", FileMode.OpenOrCreate);
        HighScoreInfo myInfo = new HighScoreInfo();

        //put what ever you're saving as myInfo.whatever
        myInfo.highScore = highScore;
        bf.Serialize(file, myInfo);
        file.Close();
    }
    public int Load()
    {
        if (File.Exists(Application.persistentDataPath + "/HighScore.cas"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/HighScore.cas", FileMode.Open);
            HighScoreInfo myLoadedInfo = (HighScoreInfo)bf.Deserialize(file);
            highScore = myLoadedInfo.highScore;
        }

        return highScore;
    }
}
[System.Serializable]
public class HighScoreInfo
{
    public int highScore;
}
