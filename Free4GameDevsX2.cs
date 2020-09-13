using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;
using UnityEditor.PackageManager.Requests;

namespace Free4GameDevsX2
{

    [System.Serializable]
    public class Free4GameDevsX2 : MonoBehaviour
    {

        public static int numberOfInstances = 0;

        //These are set in SetPaths() below...
        public static string inputFolderPath;
        public static string outputFolderPath;
        public static string rootRelPath;

        public int NumToProcess;

        #region DECLAREVALUES


        public enum Approach { Traditional, Precise }
        public Approach approach;

        public string outputNameAddon = "";
        public bool ThickerLines;
        public bool SoftenIt;
        public static int GetIndex(int x, int y)
        {
            return x * (y+1);
        }

        public PixelScanner pixelScanner;

        public Texture2D inputTexture;
        public Texture2D outputTexture;

        public Color currentInputRGB = new Color();
        public Color currentOutputRGB = new Color();
        public enum ScaleAmount { TwoTimes, FourTimes, EightTimes, SixteenTimes }
        public ScaleAmount scaleAmount;
        public enum WriteToFileType { PNG, JPG, TGA }
        public WriteToFileType writeToFileType;

        private int timesRan = 0;

        #endregion

        public enum State { Idle, Init, RunOne, NextOne, Finish  }
        public State state;

        private int TotalNumberOfProcess = 0;
        private int CurProcess = 0;

        private List<string> pngFilePaths = new List<string>();

        float a;

        string curFileProcName;

        private void CountUpA()
        {
            if (a < 1000)
            {
                a++;

            }
            else
            {
                a = 0;
            }

        }

        public void AddToEditorCallback()
        {
            EditorApplication.update += UpdateF4GDX2;
        }
        public void RemoveFromEditorCallback()
        {
            EditorApplication.update -= UpdateF4GDX2;
        }


        public void UpdateF4GDX2()
        {
            switch (state)
            {
                case State.Idle:

                    //Do Nothing...
                    break;
                case State.Init:

                    TotalNumberOfProcess = 0;
                    CurProcess = 0;

                    if (SetPaths() == false)
                    {
                        state = State.Finish;
                        return;
                    }

                    string[] files = Directory.GetFiles(inputFolderPath, "*.*", SearchOption.AllDirectories);
                    pngFilePaths = new List<string>();
                    pngFilePaths.Clear();

                    foreach (string file in files)
                    {
                        if (Path.GetExtension(file).ToLower() == ".png" || Path.GetExtension(file).ToLower() == ".gif"
                            || Path.GetExtension(file).ToLower() == "tga" || Path.GetExtension(file).ToLower() == ".jpg"
                            || Path.GetExtension(file).ToLower() == ".jpeg")
                        {
                            string relativeFileP = Path.GetDirectoryName(file) + "\\" + Path.GetFileName(file);
                            pngFilePaths.Add(relativeFileP);
                        }
                    }

                    if (HitCancelInWarningDialogs(pngFilePaths))
                    {
                        state = State.Finish;
                        return;
                    }

                    TotalNumberOfProcess = pngFilePaths.Count;
                    CurProcess = 0;
                    curFileProcName = Path.GetFileName(pngFilePaths[CurProcess]);

                    state = State.RunOne;

                    break;
                case State.RunOne:

                        Scale(pngFilePaths[CurProcess]);
                        if (SaveTexture(pngFilePaths[CurProcess]) == false)
                        {
                            Debug.LogError("Couldn't save texture! Cancelling batch process!");
                            state = State.Finish;
                            return;
                        }
                    if (CurProcess < TotalNumberOfProcess)
                    {
                        a = 0;
                        state = State.NextOne;
                        return;
                    }

                    break;
                case State.NextOne:
                    if (a > 50)
                    {
                        CurProcess++;
                        if (CurProcess >= TotalNumberOfProcess)
                        {
                            Debug.Log("Finished Processing Textures...");
                            state = State.Finish;
                            return;
                        }
                        else
                        {
                            state = State.RunOne;
                            return;
                        }
                    }
                    CountUpA();
                    break;
                case State.Finish:
                    TotalNumberOfProcess = 0;
                    CurProcess = 0;
                    AssetDatabase.Refresh();
                    EditorUtility.ClearProgressBar();
                    pngFilePaths.Clear();
                    state = State.Idle;
                    return;
            }
        }

        private bool SaveTexture(string pngRelFilePth)
        {
            string relOutputFilePath = GetNewOutputFilePath(pngRelFilePth);
            string windowsOutputFolderPath = Application.dataPath + "\\" + rootRelPath.Substring(7, rootRelPath.Length - 7) + "\\" + "OutputFolder" + "\\";
            string windowsOutputFile = windowsOutputFolderPath + relOutputFilePath;
            string windowsOutputDirectoryPath = Path.GetDirectoryName(windowsOutputFile);

            if (Directory.Exists(windowsOutputDirectoryPath) == false)
            {
                Directory.CreateDirectory(windowsOutputDirectoryPath);
            }

            if (SaveTexture2DToPNG(outputTexture, windowsOutputFile, WriteToFileType.PNG))
            {
                return true;
            } else
            {
                return false;
            }
        }


        #region METHODS


        private bool SetPaths()
        {
            if (Directory.Exists(Application.dataPath + "/Free4GameDevsX2Scaler-master"))
            {
                Debug.Log("master path root assets folder");
                rootRelPath = "Assets\\Free4GameDevsX2Scaler-master\\";
                inputFolderPath = "Assets\\Free4GameDevsX2Scaler-master\\InputFolder\\";
                outputFolderPath = "Free4GameDevsX2Scaler-master\\OutputFolder\\";
                return true;
            }
            else if (Directory.Exists(Application.dataPath + "/Free4GameDevsX2Scaler"))
            {
                rootRelPath = "Assets\\Free4GameDevsX2Scaler\\";
                inputFolderPath = "Assets\\Free4GameDevsX2Scaler\\InputFolder\\";
                outputFolderPath = "Free4GameDevsX2Scaler\\OutputFolder\\";
                return true;
            }
            else if (Directory.Exists(Application.dataPath + "/Standard Assets/Free4GameDevsX2Scaler-master"))
            {
                Debug.Log("standard assets folder, master name");
                rootRelPath = "Assets\\Standard Assets\\Free4GameDevsX2Scaler-master\\";
                inputFolderPath = "Assets\\Standard Assets\\Free4GameDevsX2Scaler-master\\InputFolder\\";
                outputFolderPath = "Standard Assets\\Free4GameDevsX2Scaler-master\\OutputFolder\\";
                return true;
            }
            else if (Directory.Exists(Application.dataPath + "/Standard Assets/Free4GameDevsX2Scaler"))
            {
                Debug.Log("standard assets folder, regular name");
                rootRelPath = "Assets\\Standard Assets\\Free4GameDevsX2Scaler\\";
                inputFolderPath = "Assets\\Standard Assets\\Free4GameDevsX2Scaler\\InputFolder\\";
                outputFolderPath = "Standard Assets\\Free4GameDevsX2Scaler\\OutputFolder\\";
                return true;
            }
            else
            {
                Debug.Log("Directory must be in Assets/ or Assets/Standard Assets/ folder and be named 'Free4GameDevsX2Scaler' or 'Free4GameDevsX2Scaler-master'!");
                Debug.Log("Cannot set inputFolderPath and outputFolderPath properly!");
                return false;
            }
        }


        private string GetNewOutputFilePath(string pngRelFilePth)
        {
            return pngRelFilePth.Substring(inputFolderPath.Length, pngRelFilePth.Length - inputFolderPath.Length);
        }


        private bool HitCancelInWarningDialogs(List<string> pngFilePaths)
        {
            if (pngFilePaths.Count > 4)
            {
                if (!EditorUtility.DisplayDialog("WARNING", "You are attempting to batch convert a lot of files. Optimization is still being worked on, so there will be a delay. Are you sure you want to continue?", "Yes", "Cancel"))
                {
                    return true;
                }
            }

            if (scaleAmount > ScaleAmount.TwoTimes)
            {
                if (!EditorUtility.DisplayDialog("WARNING", "You are attempting to batch convert above 2x. Optimization is still being worked on, so there will be a delay. Are you sure you want to continue?", "Yes", "Cancel"))
                {
                    return true;
                }
            }

            if (scaleAmount > ScaleAmount.SixteenTimes)
            {
                if (!EditorUtility.DisplayDialog("WARNING!", "16x is SLOW and can overload the memory if sizing too many or too large of pictures!! Are you sure!?", "I'm sure", "Cancel"))
                {
                    return true;
                }
            }
            return false;
        }

        private void Scale(string pngRelFilePth)
        {
            timesRan = 0;

            switch (scaleAmount)
            {
                case ScaleAmount.TwoTimes:
                    Debug.Log("Starting scaling x2 for ''" + Path.GetFileName(pngRelFilePth) + "''");
                    for (int x = 0; x < 1; x++)
                    {
                        RunIt(pngRelFilePth);
                    }
                    break;
                case ScaleAmount.FourTimes:
                    Debug.Log("Starting scaling x4 for ''" + Path.GetFileName(pngRelFilePth) + "''");
                    for (int x = 0; x < 2; x++)
                    {
                        RunIt(pngRelFilePth);
                    }
                    break;
                case ScaleAmount.EightTimes:
                    Debug.Log("Starting scaling x8 for ''" + Path.GetFileName(pngRelFilePth) + "''");
                    for (int x = 0; x < 3; x++)
                    {
                        RunIt(pngRelFilePth);
                    }
                    break;
                case ScaleAmount.SixteenTimes:
                    Debug.Log("Starting scaling x16 for ''" + Path.GetFileName(pngRelFilePth) + "''");
                    for (int x = 0; x < 4; x++)
                    {
                        RunIt(pngRelFilePth);
                    }
                    break;
            }

        }

        private void RunIt(string textureToLoadPath)
        {
            if (timesRan == 0)
            {
                inputTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(textureToLoadPath);
                inputTexture.filterMode = FilterMode.Point;
            }
            else if (timesRan > 0)
            {
                inputTexture = outputTexture;
            }

            outputTexture = new Texture2D(inputTexture.width * 2, inputTexture.height * 2);
            outputTexture.filterMode = FilterMode.Point;

            pixelScanner = new PixelScanner(ref outputTexture);

            ScalePass(2);

            HSLColor.InitializeColorsList(outputTexture);

            MainPass(SoftenIt, approach); //< - Where everything happens.

            outputTexture = HSLColor.ApplyToTexture2D(outputTexture);
            outputTexture.Apply();

            timesRan++;

        }


        private void UpscaleNearestNeighbor(ref Texture2D outputTexture, int ScaleUpBy)
        {
                for (int y = 0; y < outputTexture.height; y++)
                {
                    for (int x = 0; x < outputTexture.width; x++)
                    {
                        //Input RGB
                        currentInputRGB = inputTexture.GetPixel(x / ScaleUpBy, y / ScaleUpBy);
                        //To Output RGB
                        outputTexture.SetPixel(x, y, currentInputRGB);
                    }
                }
                outputTexture.Apply();
        }

        private void MainPass(bool SoftenIt, Approach approach)
        {
            int totalPasses = 1;
            int texW = outputTexture.width;
            int texH = outputTexture.height;
            switch (approach)
            {
                case Approach.Traditional:
                    for (int passNumber = 0; passNumber < totalPasses; passNumber++)
                    {
                        for (int scnY = 0; scnY < texH; scnY++)
                        {
                            for (int scnX = 0; scnX < texW; scnX++)
                            {
                                pixelScanner.DoTraditionalPass(ref HSLColor.ColorsList, ref scnX, ref scnY, texW, texH, passNumber, SoftenIt, ThickerLines); //width is also passed to find x,y in a one dimensional array/list with equasion: x + (y * width)
                            }
                        }
                    }
                    break;
                case Approach.Precise:
                    totalPasses = 6;
                    for (int passNumber = 0; passNumber < totalPasses; passNumber++)
                    {
                        for (int scnY = 0; scnY < texH; scnY++)
                        {
                            for (int scnX = 0; scnX < texW; scnX++)
                            {
                                pixelScanner.DoPrecisePasses(ref HSLColor.ColorsList, ref scnX, ref scnY, texW, texH, passNumber); //width is also passed to find x,y in a one dimensional array/list with equasion: x + (y * width)
                            }
                        }
                    }
                    break;
            }





        }

        private void ScalePass(int ScaleUpBy)
        {
            UpscaleNearestNeighbor(ref outputTexture, ScaleUpBy);
        }

        public float Soften_Ls;
        public float Soften_ScrapesA;
        public float Soften_ScrapesB;
        public float Soften_SideScrapesA;
        public float Soften_SideScrapesB;

        public bool SaveTexture2DToPNG(Texture2D outputTexture, string windowsOutputFilePath, WriteToFileType writeToFileType)
        {

            //FIRSTLY, WE NEED THE WINDOWS PATH HERE!

            if (File.Exists(windowsOutputFilePath))
            {
                Debug.LogError("FILE ALREADY EXISTS AT: " + windowsOutputFilePath + " !");
                return false;
            }

            switch (writeToFileType)
            {
                case WriteToFileType.TGA:
                    byte[] bytesTGA = outputTexture.EncodeToTGA();
                    File.WriteAllBytes(windowsOutputFilePath, bytesTGA);
                    return true;
                case WriteToFileType.PNG:
                    byte[] bytesPNG = outputTexture.EncodeToPNG();
                    File.WriteAllBytes(windowsOutputFilePath, bytesPNG);
                    return true;
                case WriteToFileType.JPG:
                    byte[] bytesJPG = outputTexture.EncodeToJPG();
                    File.WriteAllBytes(windowsOutputFilePath, bytesJPG);
                    return true;
            }

            return false;


        }

        #endregion

    }


}