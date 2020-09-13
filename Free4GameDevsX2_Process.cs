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



        public static void TraditionalShapes(int scannerX, int scannerY, int texWidth, int passNumber, ref HSLColor[] Snp, bool SoftenIt, bool ThickerLines)
        {

            // --- Checkers
            //If B == C
            if (HSLColor.ClrsVryCls(Snp[2], Snp[3], Snp[6], Snp[7], Snp[9], Snp[8], Snp[13], Snp[12]))
            {
                //check lightness later

                double lightAD = Snp[4].L + Snp[14].L + Snp[1].L + Snp[11].L + (2 * Snp[0].L + Snp[15].L) + (2 * Snp[5].L + Snp[10].L);
                double lightBC = Snp[2].L + Snp[8].L + Snp[7].L + Snp[13].L + (2 * Snp[12].L + Snp[3].L) + (2 * Snp[6].L + Snp[9].L);

                if (ThickerLines)
                {
                    //Darker is less
                    if (lightBC < lightAD)
                    {
                        //Take over 9 with 5 and take over 6 with 10
                        Snp[5].FillWithColor(Snp[6], scannerX, scannerY, texWidth, SoftenIt);
                        Snp[10].FillWithColor(Snp[9], scannerX, scannerY, texWidth, SoftenIt);
                    }
                } else if (ThickerLines == false)
                {
                    if (lightBC > lightAD)
                    {
                        //Take over 9 with 5 and take over 6 with 10
                        Snp[5].FillWithColor(Snp[6], scannerX, scannerY, texWidth, SoftenIt);
                        Snp[10].FillWithColor(Snp[9], scannerX, scannerY, texWidth, SoftenIt);
                    }
                }
            }
            //if A == D
            if (HSLColor.ClrsVryCls(Snp[0], Snp[4], Snp[1], Snp[5], Snp[10], Snp[14], Snp[11], Snp[15]))
            {
                //check lightness later

                double lightAD = Snp[4].L + Snp[14].L + Snp[1].L + Snp[11].L + (2 * Snp[0].L + Snp[15].L) + (2 * Snp[5].L + Snp[10].L);
                double lightBC = Snp[2].L + Snp[8].L + Snp[7].L + Snp[13].L + (2 * Snp[12].L + Snp[3].L) + (2 * Snp[6].L + Snp[9].L);

                if (ThickerLines)
                {
                    //Darker is less
                    if (lightAD < lightBC)
                    {

                        //Take over 9 with 5 and take over 6 with 10
                        Snp[9].FillWithColor(Snp[5], scannerX, scannerY, texWidth, SoftenIt);
                        Snp[6].FillWithColor(Snp[10], scannerX, scannerY, texWidth, SoftenIt);
                    }
                } else if (ThickerLines == false)
                {
                    if (lightAD > lightBC)
                    {

                        //Take over 9 with 5 and take over 6 with 10
                        Snp[9].FillWithColor(Snp[5], scannerX, scannerY, texWidth, SoftenIt);
                        Snp[6].FillWithColor(Snp[10], scannerX, scannerY, texWidth, SoftenIt);
                    }
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
                            Snp[10].FillWithColor(Snp[6], scannerX, scannerY, texWidth, SoftenIt);
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
                            Snp[9].FillWithColor(Snp[5], scannerX, scannerY, texWidth, SoftenIt);
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
                            Snp[5].FillWithColor(Snp[6], scannerX, scannerY, texWidth, SoftenIt);
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
                            Snp[6].FillWithColor(Snp[5], scannerX, scannerY, texWidth, SoftenIt);
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
                            Snp[10].FillWithColor(Snp[9], scannerX, scannerY, texWidth, SoftenIt);
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
                            Snp[5].FillWithColor(Snp[9], scannerX, scannerY, texWidth, SoftenIt);
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
                            Snp[6].FillWithColor(Snp[5], scannerX, scannerY, texWidth, SoftenIt);
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
                            Snp[9].FillWithColor(Snp[10], scannerX, scannerY, texWidth, SoftenIt);
                    }
                }
            }


            // --- ISOLATED EDGES
            //A optimization check:
            if (HSLColor.ClrsVryCls(Snp[0], Snp[10]))
            {
                //A
                if (HSLColor.ClrsVryCls(Snp[2], Snp[5], Snp[6], Snp[8], Snp[9], Snp[1]))
                {
                    //B with one A compared to them
                    if (HSLColor.ClrsVryCls(Snp[13], Snp[14], Snp[15], Snp[11], Snp[7]))
                    {
                        Snp[10].FillWithColor(Snp[11], scannerX, scannerY, texWidth, SoftenIt);
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
                        Snp[5].FillWithColor(Snp[4], scannerX, scannerY, texWidth, SoftenIt);
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
                        Snp[9].FillWithColor(Snp[8], scannerX, scannerY, texWidth, SoftenIt);
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
                        Snp[6].FillWithColor(Snp[7], scannerX, scannerY, texWidth, SoftenIt);
                    }
                }
            }

        }


        public static void PreciseShapes(int scannerX, int scannerY, int texWidth, int passNumber, ref HSLColor[] Snp)
        {
            switch (passNumber)
            {
                case 0:

                    // --- Mark Pixels To Not Be Changed


                    // -----Isolated Towers
                    if (HSLColor.ClrsVryCls(Snp[5], Snp[1]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[5], Snp[6], Snp[9], Snp[10], Snp[13], Snp[14]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[0], Snp[1], Snp[2], Snp[3], Snp[7], Snp[11], Snp[15], Snp[4], Snp[8], Snp[12]))
                            {
                                HSLColor.ClrsMARK(Snp[5], Snp[6], Snp[9], Snp[10], Snp[13], Snp[14]);
                            }
                        }
                    }
                    if (HSLColor.ClrsVryCls(Snp[9], Snp[13]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[1], Snp[2], Snp[5], Snp[6], Snp[9], Snp[10]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[0], Snp[4], Snp[8], Snp[12], Snp[13], Snp[14], Snp[15], Snp[11], Snp[7], Snp[3]))
                            {
                                HSLColor.ClrsMARK(Snp[1], Snp[2], Snp[5], Snp[6], Snp[9], Snp[10]);
                            }
                        }
                    }
                    if (HSLColor.ClrsVryCls(Snp[10], Snp[11]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[8], Snp[9], Snp[10], Snp[4], Snp[5], Snp[6]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[0], Snp[1], Snp[2], Snp[3], Snp[7], Snp[11], Snp[15], Snp[14], Snp[13], Snp[12]))
                            {
                                HSLColor.ClrsMARK(Snp[8], Snp[9], Snp[10], Snp[4], Snp[5], Snp[6]);
                            }
                        }
                    }

                    if (HSLColor.ClrsVryCls(Snp[5], Snp[4]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[9], Snp[10], Snp[11], Snp[5], Snp[6], Snp[7]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[3], Snp[2], Snp[1], Snp[0], Snp[4], Snp[8], Snp[12], Snp[13], Snp[14], Snp[15]))
                            {
                                HSLColor.ClrsMARK(Snp[9], Snp[10], Snp[11], Snp[5], Snp[6], Snp[7]);
                            }
                        }
                    }


                    // -----Isolated Penisulas Shape A
                    if (HSLColor.ClrsVryCls(Snp[6], Snp[7]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[8], Snp[9], Snp[10], Snp[5], Snp[6]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[4], Snp[0], Snp[1], Snp[2], Snp[3], Snp[7], Snp[11], Snp[15], Snp[14], Snp[13], Snp[12]))
                            {
                                HSLColor.ClrsMARK(Snp[8], Snp[9], Snp[10], Snp[5], Snp[6]);
                            }
                        }
                    }
                    if (HSLColor.ClrsVryCls(Snp[5], Snp[4]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[14], Snp[9], Snp[10], Snp[5], Snp[6]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[12], Snp[8], Snp[4], Snp[0], Snp[1], Snp[2], Snp[3], Snp[7], Snp[11], Snp[15], Snp[13]))
                            {
                                HSLColor.ClrsMARK(Snp[14], Snp[9], Snp[10], Snp[5], Snp[6]);
                            }
                        }
                    }
                    if (HSLColor.ClrsVryCls(Snp[5], Snp[8]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[9], Snp[5], Snp[6], Snp[10], Snp[7]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[15], Snp[14], Snp[13], Snp[12], Snp[8], Snp[4], Snp[0], Snp[1], Snp[2], Snp[3], Snp[11]))
                            {
                                HSLColor.ClrsMARK(Snp[9], Snp[5], Snp[6], Snp[10], Snp[7]);
                            }
                        }
                    }
                    if (HSLColor.ClrsVryCls(Snp[9], Snp[8]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[1], Snp[5], Snp[6], Snp[9], Snp[10]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[0], Snp[4], Snp[8], Snp[12], Snp[13], Snp[14], Snp[15], Snp[11], Snp[7], Snp[3], Snp[2]))
                            {
                                HSLColor.ClrsMARK(Snp[1], Snp[5], Snp[6], Snp[9], Snp[10]);
                            }
                        }
                    }


                    // -----Isolated Penisulas Shape B
                    if (HSLColor.ClrsVryCls(Snp[9], Snp[8]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[2], Snp[5], Snp[6], Snp[9], Snp[10]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[0], Snp[1], Snp[3], Snp[4], Snp[8], Snp[12], Snp[13], Snp[14], Snp[15], Snp[11], Snp[7]))
                            {
                                HSLColor.ClrsMARK(Snp[2], Snp[5], Snp[6], Snp[9], Snp[10]);
                            }
                        }
                    }
                    if (HSLColor.ClrsVryCls(Snp[6], Snp[7]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[4], Snp[5], Snp[6], Snp[9], Snp[10]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[0], Snp[1], Snp[2], Snp[3], Snp[7], Snp[8], Snp[11], Snp[12], Snp[13], Snp[14], Snp[15]))
                            {
                                HSLColor.ClrsMARK(Snp[2], Snp[5], Snp[6], Snp[9], Snp[10]);
                            }
                        }
                    }
                    if (HSLColor.ClrsVryCls(Snp[6], Snp[8]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[5], Snp[6], Snp[9], Snp[10], Snp[13]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[0], Snp[1], Snp[2], Snp[3], Snp[4], Snp[7], Snp[8], Snp[11], Snp[12], Snp[14], Snp[15]))
                            {
                                HSLColor.ClrsMARK(Snp[5], Snp[6], Snp[9], Snp[10], Snp[13]);
                            }
                        }
                    }
                    if (HSLColor.ClrsVryCls(Snp[5], Snp[4]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[9], Snp[10], Snp[11], Snp[5], Snp[6]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[0], Snp[1], Snp[2], Snp[3], Snp[4], Snp[7], Snp[8], Snp[12], Snp[13], Snp[14], Snp[15]))
                            {
                                HSLColor.ClrsMARK(Snp[5], Snp[6], Snp[9], Snp[10], Snp[13]);
                            }
                        }
                    }

                    // -----Isolated Islands
                    if (HSLColor.ClrsVryCls(Snp[9], Snp[8]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[9], Snp[10], Snp[5], Snp[6]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[0], Snp[1], Snp[2], Snp[3], Snp[4], Snp[8], Snp[12], Snp[13], Snp[14], Snp[15], Snp[11], Snp[7]))
                            {
                                HSLColor.ClrsMARK(Snp[9], Snp[10], Snp[5], Snp[6]);
                            }
                        }
                    }

                    // ----- Isolated Dots
                    if (HSLColor.ClrsVryCls(Snp[10], Snp[6]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[1], Snp[2], Snp[3], Snp[5], Snp[9], Snp[10], Snp[11], Snp[7]))
                        {

                            HSLColor.ClrsMARK(Snp[6]);
                        }
                    }
                    if (HSLColor.ClrsVryCls(Snp[5], Snp[9]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[4], Snp[5], Snp[6], Snp[8], Snp[12], Snp[13], Snp[14], Snp[10]))
                        {

                            HSLColor.ClrsMARK(Snp[9]);
                        }
                    }

                    break;

                case 1:

                    //    // --- Shaving and Filling Pass

                    // ----- Shave Boxes
                    if (HSLColor.ClrsVryCls(Snp[6], Snp[5]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[6], Snp[7], Snp[2], Snp[3]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[1], Snp[5], Snp[9], Snp[10], Snp[11]))
                            {
                                Snp[6].FillWithColor(Snp[9], scannerX, scannerY, texWidth);
                            }
                        }
                    }
                    if (HSLColor.ClrsVryCls(Snp[5], Snp[10]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[4], Snp[5], Snp[0], Snp[1]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[8], Snp[9], Snp[10], Snp[6], Snp[2]))
                            {
                                Snp[5].FillWithColor(Snp[10], scannerX, scannerY, texWidth);
                            }
                        }
                    }
                    if (HSLColor.ClrsVryCls(Snp[9], Snp[6]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[12], Snp[13], Snp[8], Snp[9]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[4], Snp[5], Snp[6], Snp[10], Snp[14]))
                            {
                                Snp[9].FillWithColor(Snp[6], scannerX, scannerY, texWidth);
                            }
                        }
                    }
                    if (HSLColor.ClrsVryCls(Snp[10], Snp[5]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[14], Snp[15], Snp[10], Snp[11]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[13], Snp[9], Snp[5], Snp[6], Snp[7]))
                            {
                                Snp[10].FillWithColor(Snp[5], scannerX, scannerY, texWidth);
                            }
                        }
                    }

                    break;

                case 2:

                    // ----- Fill Lines A
                    if (HSLColor.ClrsVryCls(Snp[6], Snp[5]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[12], Snp[8], Snp[9], Snp[6], Snp[2], Snp[3]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[4], Snp[5], Snp[1], Snp[13], Snp[10], Snp[7]))
                            {
                                Snp[5].FillWithColor(Snp[6], scannerX, scannerY, texWidth);
                            }
                        }
                    }
                    if (HSLColor.ClrsVryCls(Snp[5], Snp[9]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[0], Snp[4], Snp[5], Snp[10], Snp[14], Snp[15]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[8], Snp[9], Snp[13], Snp[1], Snp[6], Snp[11]))
                            {
                                Snp[9].FillWithColor(Snp[5], scannerX, scannerY, texWidth);
                            }
                        }
                    }
                    if (HSLColor.ClrsVryCls(Snp[9], Snp[8]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[12], Snp[13], Snp[9], Snp[6], Snp[7], Snp[3]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[8], Snp[5], Snp[2], Snp[14], Snp[10], Snp[11]))
                            {
                                Snp[10].FillWithColor(Snp[9], scannerX, scannerY, texWidth);
                            }
                        }
                    }
                    if (HSLColor.ClrsVryCls(Snp[10], Snp[9]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[0], Snp[1], Snp[5], Snp[10], Snp[11], Snp[15]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[4], Snp[9], Snp[14], Snp[2], Snp[6], Snp[7]))
                            {
                                Snp[6].FillWithColor(Snp[10], scannerX, scannerY, texWidth);
                            }
                        }
                    }

                    break;

                case 3:
                    // ----- Fill Lines B
                    if (HSLColor.ClrsVryCls(Snp[9], Snp[5]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[12], Snp[13], Snp[8], Snp[9], Snp[6], Snp[2], Snp[3]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[4], Snp[5], Snp[1], Snp[14], Snp[10], Snp[7]))
                            {
                                Snp[5].FillWithColor(Snp[9], scannerX, scannerY, texWidth);
                            }
                        }
                    }
                    if (HSLColor.ClrsVryCls(Snp[10], Snp[6]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[0], Snp[1], Snp[5], Snp[10], Snp[11], Snp[14], Snp[15]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[13], Snp[9], Snp[4], Snp[6], Snp[7], Snp[2]))
                            {
                                Snp[6].FillWithColor(Snp[10], scannerX, scannerY, texWidth);
                            }
                        }
                    }
                    if (HSLColor.ClrsVryCls(Snp[6], Snp[10]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[12], Snp[13], Snp[9], Snp[6], Snp[7], Snp[2], Snp[8]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[8], Snp[5], Snp[1], Snp[14], Snp[10], Snp[11]))
                            {
                                Snp[10].FillWithColor(Snp[6], scannerX, scannerY, texWidth);
                            }
                        }
                    }
                    if (HSLColor.ClrsVryCls(Snp[5], Snp[9]) == false)
                    {
                        if (HSLColor.ClrsVryCls(Snp[0], Snp[1], Snp[4], Snp[5], Snp[10], Snp[14], Snp[15]))
                        {
                            if (HSLColor.ClrsVryCls(Snp[8], Snp[9], Snp[13], Snp[2], Snp[5], Snp[11]))
                            {
                                Snp[9].FillWithColor(Snp[5], scannerX, scannerY, texWidth);
                            }
                        }
                    }

                    break;

                case 4:

                    // ----- Fill Lines A
                    if (HSLColor.ClrsVryCls(Snp[6], Snp[5]) == false)
                    {
                        if (Snp[6].isD() && Snp[5].isMorL())
                        {
                            if (HSLColor.ClrsVryCls(Snp[12], Snp[8], Snp[9], Snp[6], Snp[2], Snp[3]))
                            {
                                if (HSLColor.ClrsVryCls(Snp[4], Snp[5], Snp[1], Snp[13], Snp[10], Snp[7]))
                                {
                                    Snp[5].FillWithColor(Snp[6], scannerX, scannerY, texWidth);
                                }
                            }
                        }
                    }
                    if (HSLColor.ClrsVryCls(Snp[5], Snp[9]) == false)
                    {
                        if (Snp[5].isD() && Snp[9].isMorL())
                        {
                            if (HSLColor.ClrsVryCls(Snp[0], Snp[4], Snp[5], Snp[10], Snp[14], Snp[15]))
                            {
                                if (HSLColor.ClrsVryCls(Snp[8], Snp[9], Snp[13], Snp[1], Snp[6], Snp[11]))
                                {
                                    Snp[9].FillWithColor(Snp[5], scannerX, scannerY, texWidth);
                                }
                            }
                        }
                    }
                    if (HSLColor.ClrsVryCls(Snp[9], Snp[8]) == false)
                    {
                        if (Snp[9].isD() && Snp[8].isMorL())
                        {
                            if (HSLColor.ClrsVryCls(Snp[12], Snp[13], Snp[9], Snp[6], Snp[7], Snp[3]))
                            {
                                if (HSLColor.ClrsVryCls(Snp[8], Snp[5], Snp[2], Snp[14], Snp[10], Snp[11]))
                                {
                                    Snp[10].FillWithColor(Snp[9], scannerX, scannerY, texWidth);
                                }
                            }
                        }
                    }
                    if (HSLColor.ClrsVryCls(Snp[10], Snp[9]) == false)
                    {
                        if (Snp[10].isD() && Snp[9].isMorL())
                        {
                            if (HSLColor.ClrsVryCls(Snp[0], Snp[1], Snp[5], Snp[10], Snp[11], Snp[15]))
                            {
                                if (HSLColor.ClrsVryCls(Snp[4], Snp[9], Snp[14], Snp[2], Snp[6], Snp[7]))
                                {
                                    Snp[6].FillWithColor(Snp[10], scannerX, scannerY, texWidth);
                                }
                            }
                        }
                    }

                    break;

                case 5:
                    // ----- Fill Lines B
                    if (HSLColor.ClrsVryCls(Snp[9], Snp[5]) == false)
                    {
                        if (Snp[9].isD() && Snp[5].isMorL())
                        {
                            if (HSLColor.ClrsVryCls(Snp[12], Snp[13], Snp[8], Snp[9], Snp[6], Snp[2], Snp[3]))
                            {
                                if (HSLColor.ClrsVryCls(Snp[4], Snp[5], Snp[1], Snp[14], Snp[10], Snp[7]))
                                {
                                    Snp[5].FillWithColor(Snp[9], scannerX, scannerY, texWidth);
                                }
                            }
                        }
                    }
                    if (HSLColor.ClrsVryCls(Snp[10], Snp[6]) == false)
                    {
                        if (Snp[10].isD() && Snp[6].isMorL())
                        {
                            if (HSLColor.ClrsVryCls(Snp[0], Snp[1], Snp[5], Snp[10], Snp[11], Snp[14], Snp[15]))
                            {
                                if (HSLColor.ClrsVryCls(Snp[13], Snp[9], Snp[4], Snp[6], Snp[7], Snp[2]))
                                {
                                    Snp[6].FillWithColor(Snp[10], scannerX, scannerY, texWidth);
                                }
                            }
                        }
                    }
                    if (HSLColor.ClrsVryCls(Snp[6], Snp[10]) == false)
                    {
                        if (Snp[6].isD() && Snp[10].isMorL())
                        {
                            if (HSLColor.ClrsVryCls(Snp[12], Snp[13], Snp[9], Snp[6], Snp[7], Snp[2], Snp[8]))
                            {
                                if (HSLColor.ClrsVryCls(Snp[8], Snp[5], Snp[1], Snp[14], Snp[10], Snp[11]))
                                {
                                    Snp[10].FillWithColor(Snp[6], scannerX, scannerY, texWidth);
                                }
                            }
                        }
                    }
                    if (HSLColor.ClrsVryCls(Snp[5], Snp[9]) == false)
                    {
                        if (Snp[5].isD() && Snp[9].isMorL())
                        {
                            if (HSLColor.ClrsVryCls(Snp[0], Snp[1], Snp[4], Snp[5], Snp[10], Snp[14], Snp[15]))
                            {
                                if (HSLColor.ClrsVryCls(Snp[8], Snp[9], Snp[13], Snp[2], Snp[5], Snp[11]))
                                {
                                    Snp[9].FillWithColor(Snp[5], scannerX, scannerY, texWidth);
                                }
                            }
                        }
                    }

                    break;
            }
        }


    }

}
