using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Free4GameDevsX2
{

    [System.Serializable]
    public class Free4GameDevsX2 : MonoBehaviour
    {

        //These are set in SetPaths() below...
        public static string inputFolderPath;
        public static string outputFolderPath;
        public static string rootRelPath;

        #region DECLAREVALUES

        public static int GetIndex(int x, int y)
        {
            return x * (y+1);
        }

        public PixelScanner pixelScanner;

        public Texture2D inputTexture;
        public Texture2D outputTexture;

        public Color currentInputRGB = new Color();
        public Color currentOutputRGB = new Color();
        public enum ScaleAmount { TwoTimes, FourTimes, EightTimes }
        public ScaleAmount scaleAmount;
        public enum WriteToFileType { PNG, JPG, TGA }
        public WriteToFileType writeToFileType;

        private int timesRan = 0;

        #endregion

        public void StartUp()
        {

            if (SetPaths() == false)
                return;


            string[] files = Directory.GetFiles(inputFolderPath, "*.*", SearchOption.AllDirectories);
            List<string> pngFilePaths = new List<string>();

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
                return;


            foreach (string pngRelFilePth in pngFilePaths)
            {
                Scale(pngRelFilePth);
            }


            Debug.Log("Scaling Successful!");
            AssetDatabase.Refresh();

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
                Debug.Log("path root assets folder, regular name");
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
            }


            string relOutputFilePath = GetNewOutputFilePath(pngRelFilePth);
            string windowsOutputFilePath = Application.dataPath + "\\" + outputFolderPath + relOutputFilePath;
            string windowsOutputDirectoryPath = Path.GetDirectoryName(windowsOutputFilePath) + "\\";

            if (Directory.Exists(windowsOutputDirectoryPath) == false)
            {
                Directory.CreateDirectory(windowsOutputDirectoryPath);
            }

            SaveTexture2DToPNG(outputTexture, windowsOutputFilePath, WriteToFileType.PNG);

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

            MainPass(); //< - Where everything happens.

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

        private void MainPass()
        {
            int totalPasses = 1;
            int texW = outputTexture.width;
            int texH = outputTexture.height;
            for (int passNumber = 0; passNumber < totalPasses; passNumber++)
            {
                for (int scnY = 0; scnY < texH; scnY++)
                {
                    for (int scnX = 0; scnX < texW; scnX++)
                    {
                        pixelScanner.DoPass(ref HSLColor.ColorsList, ref scnX, ref scnY, texW, texH, passNumber); //width is also passed to find x,y in a one dimensional array/list with equasion: x + (y * width)
                    }
                }
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

        public void SaveTexture2DToPNG(Texture2D outputTexture, string windowsOutputFilePath, WriteToFileType writeToFileType)
        {

            //FIRSTLY, WE NEED THE WINDOWS PATH HERE!

            if (File.Exists(windowsOutputFilePath))
            {
                Debug.LogError("FILE ALREADY EXISTS AT: " + windowsOutputFilePath + " !");
                return;
            }

            switch (writeToFileType)
            {
                case WriteToFileType.TGA:
                    byte[] bytesTGA = outputTexture.EncodeToTGA();
                    File.WriteAllBytes(windowsOutputFilePath, bytesTGA);
                    break;
                case WriteToFileType.PNG:
                    byte[] bytesPNG = outputTexture.EncodeToPNG();
                    File.WriteAllBytes(windowsOutputFilePath, bytesPNG);
                    break;
                case WriteToFileType.JPG:
                    byte[] bytesJPG = outputTexture.EncodeToJPG();
                    File.WriteAllBytes(windowsOutputFilePath, bytesJPG);
                    break;
            }


        }

        #endregion

    }


}