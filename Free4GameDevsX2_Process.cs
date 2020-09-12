using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Free4GameDevsX2;


namespace Free4GameDevsX2
{

    public class Process
    {

        public static bool IfABCD_AllBoxes(HSLColor[] Snp)
        {
                   //A all same
            if (   HSLColor.ClrsVryCls(Snp[0], Snp[1], Snp[4], Snp[5]) 
                   //B all the same
                && HSLColor.ClrsVryCls(Snp[2], Snp[3], Snp[6], Snp[7])
                   //C all the same
                && HSLColor.ClrsVryCls(Snp[12], Snp[13], Snp[8], Snp[9])
                   //D all the same
                && HSLColor.ClrsVryCls(Snp[10], Snp[11], Snp[14], Snp[15]))
            {
                return true;
            } else
            {
                return false;
            }
        }

        public static void Shapes(int scannerX, int scannerY, int texWidth, int passNumber, ref HSLColor[] Snp)
        {

            // --- Checkers
            //If B == C
            if (HSLColor.ClrsVryCls(Snp[2], Snp[3], Snp[6], Snp[7], Snp[9], Snp[8], Snp[13], Snp[12]))
            {
                //check lightness later

                double lightAD = Snp[4].L + Snp[14].L + Snp[1].L + Snp[11].L + (2 * Snp[0].L + Snp[15].L) + (2 * Snp[5].L + Snp[10].L);
                double lightBC = Snp[2].L + Snp[8].L + Snp[7].L + Snp[13].L + (2 * Snp[12].L + Snp[3].L) + (2 * Snp[6].L + Snp[9].L);

                //Darker is less
                if (lightBC < lightAD)
                {

                    //Take over 9 with 5 and take over 6 with 10
                    Snp[5].FillWithColor(Snp[6], scannerX, scannerY, texWidth);
                    Snp[10].FillWithColor(Snp[9], scannerX, scannerY, texWidth);
                }
            }
            //if A == D
            if (HSLColor.ClrsVryCls(Snp[0], Snp[4], Snp[1], Snp[5], Snp[10], Snp[14], Snp[11], Snp[15]))
            {
                //check lightness later

                double lightAD = Snp[4].L + Snp[14].L + Snp[1].L + Snp[11].L + (2 * Snp[0].L + Snp[15].L) + (2 * Snp[5].L + Snp[10].L);
                double lightBC = Snp[2].L + Snp[8].L + Snp[7].L + Snp[13].L + (2 * Snp[12].L + Snp[3].L) + (2 * Snp[6].L + Snp[9].L);

                //Darker is less
                if (lightAD < lightBC)
                {

                    //Take over 9 with 5 and take over 6 with 10
                    Snp[9].FillWithColor(Snp[5], scannerX, scannerY, texWidth);
                    Snp[6].FillWithColor(Snp[10], scannerX, scannerY, texWidth);
                }
            }




            // --- LINES A
            if (HSLColor.ClrsVryCls(Snp[3], Snp[12]))
            {
                if (HSLColor.ClrsVryCls(Snp[7], Snp[6], Snp[8], Snp[9], Snp[13]))
                {
                    //9 is compared to:
                    if (!HSLColor.ClrsVryCls(Snp[9], Snp[0], Snp[1], Snp[2], Snp[4], Snp[5], Snp[10], Snp[11], Snp[14], Snp[15]))
                    {
                        if (Snp[10].L > Snp[6].L)
                            Snp[10].FillWithColor(Snp[6], scannerX, scannerY, texWidth);
                    }
                }
            }
            if (HSLColor.ClrsVryCls(Snp[0], Snp[15]))
            {
                if (HSLColor.ClrsVryCls(Snp[4], Snp[5], Snp[10], Snp[11], Snp[14]))
                {
                    //10 is compared to:
                    if (!HSLColor.ClrsVryCls(Snp[10], Snp[1], Snp[2], Snp[3], Snp[6], Snp[7], Snp[8], Snp[9], Snp[12], Snp[13]))
                    {
                        if (Snp[9].L > Snp[5].L)
                            Snp[9].FillWithColor(Snp[5], scannerX, scannerY, texWidth);
                    }
                }
            }
            if (HSLColor.ClrsVryCls(Snp[12], Snp[2]))
            {
                if (HSLColor.ClrsVryCls(Snp[8], Snp[9], Snp[6], Snp[7], Snp[3]))
                {
                    //6 is compared to:
                    if (!HSLColor.ClrsVryCls(Snp[6], Snp[0], Snp[1], Snp[4], Snp[5], Snp[13], Snp[14], Snp[15], Snp[10], Snp[11]))
                    {
                        if (Snp[5].L > Snp[6].L)
                            Snp[5].FillWithColor(Snp[6], scannerX, scannerY, texWidth);
                    }
                }
            }
            if (HSLColor.ClrsVryCls(Snp[0], Snp[15]))
            {
                if (HSLColor.ClrsVryCls(Snp[1], Snp[4], Snp[5], Snp[10], Snp[11]))
                {
                    //5 is compared to:
                    if (!HSLColor.ClrsVryCls(Snp[5], Snp[12], Snp[13], Snp[14], Snp[8], Snp[9], Snp[6], Snp[7], Snp[2], Snp[3]))
                    {
                        if (Snp[6].L > Snp[5].L)
                            Snp[6].FillWithColor(Snp[5], scannerX, scannerY, texWidth);
                    }
                }
            }


            // --- LINES B
            if (HSLColor.ClrsVryCls(Snp[12], Snp[3]))
            {
                if (HSLColor.ClrsVryCls(Snp[13], Snp[9], Snp[6], Snp[7]))
                {
                    //9 is compared to: 
                    if (!HSLColor.ClrsVryCls(Snp[9], Snp[14], Snp[15], Snp[8], Snp[10], Snp[11], Snp[4], Snp[5], Snp[1], Snp[2]))
                    {
                        if (Snp[10].L > Snp[9].L)
                            Snp[10].FillWithColor(Snp[9], scannerX, scannerY, texWidth);
                    }
                }
            }
            if (HSLColor.ClrsVryCls(Snp[12], Snp[2]))
            {
                if (HSLColor.ClrsVryCls(Snp[8], Snp[9], Snp[6], Snp[3]))
                {
                    //9 is compared to:
                    if (!HSLColor.ClrsVryCls(Snp[9], Snp[13], Snp[14], Snp[10], Snp[11], Snp[4], Snp[5], Snp[7], Snp[0], Snp[1]))
                    {
                        if (Snp[5].L > Snp[9].L)
                            Snp[5].FillWithColor(Snp[9], scannerX, scannerY, texWidth);
                    }
                }
            }
            if (HSLColor.ClrsVryCls(Snp[0], Snp[15]))
            {
                if (HSLColor.ClrsVryCls(Snp[1], Snp[5], Snp[10], Snp[11]))
                {
                    //5 is compared to:
                    if (!HSLColor.ClrsVryCls(Snp[5], Snp[13], Snp[8], Snp[9], Snp[4], Snp[14], Snp[6], Snp[7], Snp[2], Snp[3]))
                    {
                        if (Snp[6].L > Snp[5].L)
                            Snp[6].FillWithColor(Snp[5], scannerX, scannerY, texWidth);
                    }
                }
            }
            if (HSLColor.ClrsVryCls(Snp[0], Snp[15]))
            {
                if (HSLColor.ClrsVryCls(Snp[4], Snp[5], Snp[10], Snp[14]))
                {
                    //10 is compared to:
                    if (!HSLColor.ClrsVryCls(Snp[10], Snp[12], Snp[13], Snp[8], Snp[9], Snp[1], Snp[2], Snp[6], Snp[7], Snp[11]))
                    {
                        if (Snp[9].L > Snp[10].L)
                            Snp[9].FillWithColor(Snp[10], scannerX, scannerY, texWidth);
                    }
                }
            }


            // --- Isolated Edges
            //A optimization check:
            if (HSLColor.ClrsVryCls(Snp[0], Snp[10]))
            {
                //A
                if (HSLColor.ClrsVryCls(Snp[2], Snp[5], Snp[6], Snp[8], Snp[9], Snp[1]))
                {
                    //B with one A compared to them
                    if (HSLColor.ClrsVryCls(Snp[13], Snp[14], Snp[15], Snp[11], Snp[7]))
                    {
                        Snp[10].FillWithColor(Snp[11], scannerX, scannerY, texWidth);
                    }
                }
            }
            //B optimization check:
            if (HSLColor.ClrsVryCls(Snp[13], Snp[5]))
            {
                //A
                if (HSLColor.ClrsVryCls(Snp[15], Snp[9], Snp[10], Snp[11], Snp[7], Snp[6], Snp[14]))
                {
                    //B with one A compared to them
                    if (HSLColor.ClrsVryCls(Snp[8], Snp[4], Snp[0], Snp[1], Snp[2]))
                    {
                        Snp[5].FillWithColor(Snp[4], scannerX, scannerY, texWidth);
                    }
                }
            }
            //C optimization check:
            if (HSLColor.ClrsVryCls(Snp[1], Snp[11]))
            {
                //A
                if (HSLColor.ClrsVryCls(Snp[3], Snp[5], Snp[6], Snp[7], Snp[9], Snp[10], Snp[2]))
                {
                    //B with one A compared to them
                    if (HSLColor.ClrsVryCls(Snp[12], Snp[13], Snp[14], Snp[8], Snp[4]))
                    {
                        Snp[9].FillWithColor(Snp[8], scannerX, scannerY, texWidth);
                    }
                }
            }
            //D optimization check:
            if (HSLColor.ClrsVryCls(Snp[12], Snp[4]))
            {
                //A
                if (HSLColor.ClrsVryCls(Snp[14], Snp[8], Snp[9], Snp[10], Snp[13], Snp[5], Snp[6]))
                {
                    //B with one A compared to them
                    if (HSLColor.ClrsVryCls(Snp[1], Snp[2], Snp[3], Snp[7], Snp[11]))
                    {
                        Snp[6].FillWithColor(Snp[7], scannerX, scannerY, texWidth);
                    }
                }
            }

        }



    }

}
