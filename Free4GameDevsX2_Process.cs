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

        public static void BoxesNCheckers(int scannerX, int scannerY, int texWidth, int passNumber, ref HSLColor[] Snp)
        {

            switch (passNumber)
            {

                //** This is not the only pattern we look for, below there's also the checkboard included in this method. That's why it's called 
                    // 'BoxesNChecker'.


                //We look for this pattern all 4 ways. 
                //1) if all 4 sides are their own box     (0, 1, 4, 5 all similar && 8, 9, 12, 13 all similar, 
                                                    // && 14, 15, 10, 11 all similar && 6, 7, 2 and 3 all similar)
                //1) if x are all similar color
                //2) if d and o are all similar color
                //3) 
                //4) fill it 10 with 6's color

                /*  +-----------+
                   |12x |13x  |14x |  15x |
                   +-----------+
                   |08x |09x  |10x |  11x |
                   +-----------+
                   |04x |05x  |06f |  07d |
                   +-----------+
                   |00x |01x  |02d |  03d |
                   +-----------+ */

                //  | C | D |
                //  | A | B |

                //** If A, B, C, D are all a box (ClrsVryCls() means 'Colors Very Close')
                    //Most of the magic is in that small method 'ClrsVryCls'.

                case 0:

                    //We plug two values in here for optimization. If we check for the 'all four sides are their own separate box' part
                        //first, it takes longer. We know that if we do the 'if part of (A == C)' check here first we're not gonna be moving 
                            //on to the next slower if statement.
                    if (HSLColor.ClrsVryCls(Snp[0], Snp[12]))
                    {

                        //This is the 'all four sides are their own separate box part'
                            //I think that this is the slow part of the code.
                        //If All Pixels in A == C == D == A
                        if (IfABCD_AllBoxes(Snp))
                        {

                            //If A == C && C == D (Already checked for A == C above so we just do C == D here)
                            if (HSLColor.ClrsVryCls(Snp[12], Snp[15]))
                            { 

                                //If C != B (If the top left box is not equal to the bottom right box)
                                if (HSLColor.ClrsVryCls(Snp[12], Snp[3]) == false)
                                {
                                    Snp[6] = Snp[9];
                                }
                            }
                        }

                    }

                    break;


                case 1:


                    //If A == C && A == D && C == D
                    if (HSLColor.ClrsVryCls(Snp[3], Snp[12], Snp[15]))
                    {

                        //If All Pixels in A == C == D == A
                        if (IfABCD_AllBoxes(Snp))
                        {

                            //If C != B
                            if (HSLColor.ClrsVryCls(Snp[12], Snp[0]) == false)
                            {

                                Snp[5] = Snp[10];
                            }
                        }
                    }
                    break;

                case 2:

                    //If A == C && A == D && C == D
                    if (HSLColor.ClrsVryCls(Snp[0], Snp[3], Snp[15]))
                    {

                        //If All Pixels in A == C == D == A
                        if (IfABCD_AllBoxes(Snp))
                        {

                            //If C != B
                            if (HSLColor.ClrsVryCls(Snp[12], Snp[3]) == false)
                            {

                                Snp[9] = Snp[6];
                            }
                        }
                    }
                    break;

                case 3:

                    //If A == B && A == C && C == A
                    if (HSLColor.ClrsVryCls(Snp[0], Snp[3], Snp[12]))
                    {

                        //If All Pixels in A == C == D == A
                        if (IfABCD_AllBoxes(Snp))
                        {

                            //If D != A
                            if (HSLColor.ClrsVryCls(Snp[15], Snp[0]) == false)
                            {

                                Snp[10] = Snp[5];
                            }
                        }
                    }

                    break;

                case 4:


                    //This is our check for a checker pattern. 

                    //What you're doing is checking that 
                    //side A is very close to side D, and 
                    //side B is very close to side C.

                    //We then check which one is lighter with a very similar equasion to xBR: 
                    //For the bottom left to top right checker it's: 
                    //4.L + 14.L + 1.L + 11.L + (2 * 0.L + 15.L) + (2 * 5.L + 10.L)
                    //And for the bottom right to top left check it's: 
                    //2.L + 8.L + 7.L + 13.L + (2 * 3.L + 12.L) + (2 * 6.L + 9.L)

                    //How this differs from xBR or xBRZ, is that in xBR you're checking for how 
                    //far a color is from one another. This new one only checks for 
                    //the HSL  'Lightness' after it's determined that it's a checkboard. 

                    //This could be slower and less optimized, I'm not sure, but it's what worked for me
                    //after days of test.

                    //If the first possible checker is like this, where 'drk' means darker and 'lght' means lighter:

                    // ( :: POSSIBLE CHECKER A :: )
                    /*  +-----------+
                       |12drk    |13drk   |14lght |15lght|
                       +-----------+
                       |08drk    |09drk   |10lght |11lght|
                       +-----------+
                       |04lght   |05lght  |06drk  |07drk|
                       +-----------+
                       |00lght   |01lght  |02drk  |03drk|
                       +-----------+ */

                    //Then we fill it like this:

                    /*  +-----------+
                       |12drk    |13drk   |14lght |15lght|
                       +-----------+
                       |08drk    |09drk   |10FILL |11lght|
                       +-----------+
                       |04lght   |05FILL  |06drk  |07drk|
                       +-----------+
                       |00lght   |01lght  |02drk  |03drk|
                       +-----------+ */

                    //We ''FILL' it with the dark color adjescent to it, like for example 5 could be filled with 9 and 10 could be filled with 6


                    //We do vise versa, so if the checker alternatively looks like this: 

                    // ( :: POSSIBLE CHECKER B :: )
                    /*  +-----------+
                       |12lght    |13lght   |14drk   |15drk|
                       +-----------+
                       |08lght    |09lght   |10drk   |11drk|
                       +-----------+
                       |04drk     |05drk    |06lght  |07lght|
                       +-----------+
                       |00drk     |01drk    |02lght  |03lght|
                       +-----------+ */

                    //Then we fill it like this:

                    /*  +-----------+
                       |12lght    |13lght   |14drk   |15drk|
                       +-----------+
                       |08lght    |09FILL   |10drk   |11drk|
                       +-----------+
                       |04drk     |05drk    |06FILL  |07lght|
                       +-----------+
                       |00drk     |01drk    |02lght  |03lght|
                       +-----------+ */

                    //We ''FILL' it with the dark color adjescent to it, like for example 6 could be filled with 10 and 5 could be filled with 9

                    //  | C | D |
                    //  | A | B |

                    //If A == D && B == C
                    if (HSLColor.ClrsVryCls(Snp[0], Snp[15]) && HSLColor.ClrsVryCls(Snp[3], Snp[12]))
                    {

                        //If All Pixels in A == C == D == A
                        if (IfABCD_AllBoxes(Snp))
                        {
                            //If A != B and C != D
                            if (HSLColor.ClrsVryCls(Snp[0], Snp[3]) == false
                                && HSLColor.ClrsVryCls(Snp[12], Snp[15]) == false)
                            {
                                double light_BLft_To_TRgt = Snp[4].L + Snp[14].L + Snp[1].L + Snp[11].L + (2 * Snp[0].L + Snp[15].L) + (2 * Snp[5].L + Snp[10].L);
                                double light_BRgt_To_TLft = Snp[2].L + Snp[8].L + Snp[7].L + Snp[13].L + (2 * Snp[12].L + Snp[3].L) + (2 * Snp[6].L + Snp[9].L);

                                //Darker is less
                                if (light_BLft_To_TRgt < light_BRgt_To_TLft)
                                {
                                    //Take over 9 with 5 and take over 6 with 10
                                    Snp[9].FillWithColor(Snp[5], scannerX, scannerY, texWidth);
                                    Snp[6].FillWithColor(Snp[10], scannerX, scannerY, texWidth);
                                }
                                else
                                {
                                    //Take over 5 and 10 and take over 10 with 6
                                    Snp[5].FillWithColor(Snp[9], scannerX, scannerY, texWidth);
                                    Snp[10].FillWithColor(Snp[6], scannerX, scannerY, texWidth);
                                }
                            }
                        }
                    }

                    break;

            }

        }

        public static void UnFilledCurves(int scannerX, int scannerY, int texWidth, int passNumber, ref HSLColor[] Snp) 
        {
            switch (passNumber)
            {

                //We look for this pattern all 4 ways. 
                    //1) if x are all similar color
                    //2) if d and o are all similar color
                    //3) if 10 is greater lightness than 6
                    //4) fill it 10 with 6's color

                //As you can see here, we're basically trying to complete a curved line:
                /*  +-----------+
                   |12x |13x  |14d|  15d |
                   +-----------+
                   |08x |09x  |10f|  11d |
                   +-----------+
                   |04d |05d  |06x|  07x|
                   +-----------+
                   |00d |01d  |02 |  03x|
                   +-----------+ */

                //  | C | D |
                //  | A | B |

                case 5:

                    if (HSLColor.ClrsVryCls(Snp[3], Snp[12]))
                    {
                        if (HSLColor.ClrsVryCls(Snp[7], Snp[6], Snp[8], Snp[9], Snp[13]))
                        {
                                                    //9 is compared to:
                            if (!HSLColor.ClrsVryCls(Snp[9],                Snp[0], Snp[1], Snp[2], Snp[4], Snp[5], Snp[10], Snp[11], Snp[14], Snp[15]))
                            {
                                if (Snp[10].L > Snp[6].L)
                                    Snp[10].FillWithColor(Snp[6], scannerX, scannerY, texWidth);
                            }
                        }
                    }

                    break;

                case 6:

                    if (HSLColor.ClrsVryCls(Snp[0], Snp[15]))
                    {
                        if (HSLColor.ClrsVryCls(Snp[4], Snp[5], Snp[10], Snp[11], Snp[14]))
                        {
                                                    //10 is compared to:
                            if (!HSLColor.ClrsVryCls(Snp[10],               Snp[1], Snp[2], Snp[3], Snp[6], Snp[7], Snp[8], Snp[9], Snp[12], Snp[13]))
                            {
                                if (Snp[9].L > Snp[5].L)
                                    Snp[9].FillWithColor(Snp[5], scannerX, scannerY, texWidth);
                            }
                        }
                    }
                    break;

                case 7:

                    if (HSLColor.ClrsVryCls(Snp[12], Snp[2])) {
                        if (HSLColor.ClrsVryCls(Snp[8], Snp[9], Snp[6], Snp[7], Snp[3]))
                        {
                                                    //6 is compared to:
                            if (!HSLColor.ClrsVryCls(Snp[6],                Snp[0], Snp[1], Snp[4], Snp[5], Snp[13], Snp[14], Snp[15], Snp[10], Snp[11]))
                            {
                                if (Snp[5].L > Snp[6].L)
                                    Snp[5].FillWithColor(Snp[6], scannerX, scannerY, texWidth);
                            }
                        }
                    }

                    break;

                case 8:

                    if (HSLColor.ClrsVryCls(Snp[0], Snp[15]))
                    {
                        if (HSLColor.ClrsVryCls(Snp[1], Snp[4], Snp[5], Snp[10], Snp[11]))
                        {           
                                                    //5 is compared to:
                            if (!HSLColor.ClrsVryCls(Snp[5],                Snp[12], Snp[13], Snp[14], Snp[8], Snp[9], Snp[6], Snp[7], Snp[2], Snp[3]))
                            {
                                if (Snp[6].L > Snp[5].L)
                                    Snp[6].FillWithColor(Snp[5], scannerX, scannerY, texWidth);
                            }
                        }
                    }

                    break;

            }


        }

        public static void UnFilledLines(int scannerX, int scannerY, int texWidth, int passNumber, ref HSLColor[] Snp)
        {
            switch (passNumber)
            {
                //We look for this pattern all 4 ways. 
                //1) if x are all similar color
                //2) if d and f are all similar color
                //3) if 10 is greater lightness than 9
                //4) fill it 10 with 9's color

                   //x          - 'x' marks the spot
                   //d means    - 'different' than x, not 'dark'
                   //f means    - 'different' and we're seeing if we want to 'fill' that color with an 'x' color, changing it

                /*  +-----------+
                   |12x |13x  |14d|  15d |
                   +-----------+
                   |08d  |09x |10f|  11d | 
                   +-----------+
                   |04d |05d  |06x|  07x|
                   +-----------+
                   |00d |01d  |02d | 03x |
                   +-----------+ */

                //  | C | D |
                //  | A | B |

                case 9:

                    if (HSLColor.ClrsVryCls(Snp[12], Snp[3])) {
                        if (HSLColor.ClrsVryCls(Snp[13], Snp[9], Snp[6], Snp[7]))
                        {
                                                    //9 is compared to: 
                            if (!HSLColor.ClrsVryCls(Snp[9],            Snp[14], Snp[15], Snp[8], Snp[10], Snp[11], Snp[4], Snp[5], Snp[1], Snp[2]))
                            {
                                if (Snp[10].L > Snp[9].L)
                                    Snp[10].FillWithColor(Snp[9], scannerX, scannerY, texWidth);
                            }
                        }
                    }

                    break;

                case 10:


                    if (HSLColor.ClrsVryCls(Snp[12], Snp[2]))
                    {
                        if (HSLColor.ClrsVryCls(Snp[8], Snp[9], Snp[6], Snp[3]))
                        {
                                                    //9 is compared to:
                            if (!HSLColor.ClrsVryCls(Snp[9],            Snp[13], Snp[14], Snp[10], Snp[11], Snp[4], Snp[5], Snp[7], Snp[0], Snp[1]))
                            {
                                if (Snp[5].L > Snp[9].L)
                                    Snp[5].FillWithColor(Snp[9], scannerX, scannerY, texWidth);
                            }
                        }
                    }

                    break;

                case 11:

                    if (HSLColor.ClrsVryCls(Snp[0], Snp[15]))
                    {
                        if (HSLColor.ClrsVryCls(Snp[1], Snp[5], Snp[10], Snp[11]))
                        {
                                                    //5 is compared to:
                            if (!HSLColor.ClrsVryCls(Snp[5],            Snp[13], Snp[8], Snp[9], Snp[4], Snp[14], Snp[6], Snp[7], Snp[2], Snp[3]))
                            {
                                if (Snp[6].L > Snp[5].L)
                                    Snp[6].FillWithColor(Snp[5], scannerX, scannerY, texWidth);
                            }
                        }
                    }

                    break;

                case 12:

                    if (HSLColor.ClrsVryCls(Snp[0], Snp[15]))
                    {
                        if (HSLColor.ClrsVryCls(Snp[4], Snp[5], Snp[10], Snp[14]))
                        {
                                                    //10 is compared to:
                            if (!HSLColor.ClrsVryCls(Snp[10],               Snp[12], Snp[13], Snp[8], Snp[9], Snp[1], Snp[2], Snp[6], Snp[7], Snp[11]))
                            {
                                if (Snp[9].L > Snp[10].L)
                                    Snp[9].FillWithColor(Snp[10], scannerX, scannerY, texWidth);
                            }
                        }
                    }

                    break;
            }
        }

        public static void EXAMPLEPROCESS(int scannerX, int scannerY, int texWidth, int passNumber, ref HSLColor[] Snp)
        {
            switch (passNumber)
            {

                //Snp ('Snapshot') looks like this. In the future we could also have 5x5 or 6x6 snapshots, but likely this will 
                    //mean it needs to be further optimized:

                /*  +-----------+
                   |12 |13  |14|  15|
                   +-----------+
                   |08 |09 | 10|  11| 
                   +-----------+
                   |04 |05  |06|  07|
                   +-----------+
                   |00 |01  |02 | 03|
                   +-----------+ */

                //  | C | D |
                //  | A | B |

                //A, B, C and D here just refer to:
                //0,1,4,5       is A
                //2,3,6,7       is B
                //12,13,8,9     is C
                //10,11,14,15   is D

                //A, B, C and D aren't variables here, it's just a way of looking at it.

                //You set these case numbers to match the pass numbers in Free4GameDevsX2_Scanner.cs:

                case 13:

                    //Here's a template to use to check pixels against one another. You can check for whatever shape 
                        //you want with it:

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

                    break;

                case 14:

                    break;

                case 15:

                    break;

                case 16:

                    break;
            }
        }




    }

}
