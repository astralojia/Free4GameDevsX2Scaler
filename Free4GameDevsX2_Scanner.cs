using UnityEngine;
using System.Collections.Generic;


namespace Free4GameDevsX2
{

    [System.Serializable]
    public class PixelScanner
    {

        #region DEFINEVALUES

        Texture2D outputTexture;
        //List<HSLColor> Snp = new List<HSLColor>(); //'Snp' stands for 'Snapshot'
        private HSLColor[] Snp = new HSLColor[]
        {
            new HSLColor(0,0,0), //1
            new HSLColor(0,0,0),
            new HSLColor(0,0,0),
            new HSLColor(0,0,0),
            new HSLColor(0,0,0),
            new HSLColor(0,0,0),
            new HSLColor(0,0,0),
            new HSLColor(0,0,0), //8
            new HSLColor(0,0,0),
            new HSLColor(0,0,0),
            new HSLColor(0,0,0),
            new HSLColor(0,0,0),
            new HSLColor(0,0,0),
            new HSLColor(0,0,0),
            new HSLColor(0,0,0),
            new HSLColor(0,0,0) //16
        };
        private int rowAndColSize = 4;

        public static void DebugScanner(int scannerX, int scannerY, int texWidth, int texHeight)
        {
            Debug.Log("SCANNER '" + scannerX + "' by '" + scannerY + "'  texW: " + texWidth + " texH: " + texHeight);
        }


        #endregion

        #region PASS

        public void DoPass(ref List<HSLColor> HSLColors, ref int scannerX, ref int scannerY, int texWidth, int texHeight, int passNumber)
        {
            if (isOnGrid(scannerX, scannerY, texWidth, texHeight, 2) == false) //2 for 16 size here
                return;

            GetSnapshot_Array(scannerX, scannerY, texWidth);
            if (SnapshotIsAPatternThatWeNeverSee())  // < - This is optimization
                return;
            Process.Shapes(scannerX, scannerY, texWidth, passNumber, ref Snp); // < - This is where it checks for patterns
            SetSnapshot_Array(scannerX, scannerY, texWidth);
        }

        private bool isOnGrid(int scannerX, int scannerY, int texWidth, int texHeight, int SizeOfSnapshot_SquareRootedTwice)
        {
            //For 16, 4x4 snapshot, put 2
            //If in the future we have 5x5 (25 count) snapshots, plug in 3
            if (scannerX >= texWidth - SizeOfSnapshot_SquareRootedTwice)
            {
                return false;
            } else 
            if (scannerY >= texHeight - SizeOfSnapshot_SquareRootedTwice)
                return false;
            else {
                return true;
            }
        }

        private void GetSnapshot_Array(int scannerX, int scannerY, int texWidth)
        {
            for (int yRow = 0; yRow < rowAndColSize; yRow++)
            {
                for (int xCol = 0; xCol < rowAndColSize; xCol++)
                {
                    Snp[xCol + (yRow * rowAndColSize)] = HSLColor.ColorsListGetAt(scannerX + xCol, scannerY + yRow, texWidth);
                }
            }
        }

        private void SetSnapshot_Array(int scannerX, int scannerY, int texWidth)
        {
            for (int yRow = 0; yRow < rowAndColSize; yRow++)
            {
                for (int xCol = 0; xCol < rowAndColSize; xCol++)
                {
                    HSLColor.SetAtPoint(Snp[xCol + (yRow * rowAndColSize)], scannerX + xCol, scannerY + yRow, texWidth);
                }
            }
        }

        private bool SnapshotIsAPatternThatWeNeverSee()
        {
            //This is our optimization. This pattern is never delt with. 
                //If it is in the future, it needs to be changed:
            
            /*  +-----------+
           |12  |13 |14  |15|
           +-----------+
           |08x |09 |10x |11x|
           +-----------+
           |04  |05 |06  |07x|
           +-----------+
           |00x |01 |02x |03|
           +-----------+ */

            if (Snp[0].isVryClsTo(Snp[2]) && Snp[2].isVryClsTo(Snp[8])
                 && Snp[8].isVryClsTo(Snp[10]) && Snp[10].isVryClsTo(Snp[11])
                 && Snp[11].isVryClsTo(Snp[0]) && Snp[0].isVryClsTo(Snp[7]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region CONSTRUCTOR

        public PixelScanner(ref Texture2D _outputTexture)
        {
            outputTexture = _outputTexture;
        }

        #endregion


    }

}