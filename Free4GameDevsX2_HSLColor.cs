using System;
using System.Collections.Generic;
using UnityEngine;

namespace Free4GameDevsX2
{

    [System.Serializable]
    public class HSLColor
    {

        //This is your 'ColorsList', which is what the Texture2D Color[] array data gets loaded into. 
            //To get a pixel x,y we need use ColorsListGetAt(). 
                //ColorsListGetAt() uses the texWidth as well. 
        
        //The reason for this is that it's a 1 dimensional array, so to find an x,y value in a 1 dimensional array 
            //you need to do: x + (y * texWidth). 

            //The reason I mean ColorsListGetAt() as a method is that you don't want the program to die when you plug in an 
                //x and y value that's off grid accidentally.
        public static List<HSLColor> ColorsList = new List<HSLColor>();

        #region VALUES

        public bool Marked; // Marked for not changeable filling in 'precise' mode.

        public static double MtoL;
        public static double DtoM;

        public double H;
        public double S;
        public double L;

        public static double H_max = 6.0d;
        public static double SorL_max = 1.0d;

        //If you want a different 

        // Lower percentage means that they will be closer together.
            //See the method 'isVryClsTo(checkingAgainstColor)' below.
        public double H_Closeness_Threshhold = 0.055d; //Hue needs to be very close.
        public double S_Closeness_Threshhold = 0.14d;  //Saturation a little farther away
        public double L_Closeness_Threshhold = 0.065d; //Lightness also needs to be very close. 

        #endregion

        #region METHODS

        public void MarkClr()
        {
            Marked = true;
        }

        public static void ClrsMARK(params HSLColor[] Cs)
        {
            foreach (HSLColor c in Cs)
            {
                c.MarkClr();
            }
        }

        public static bool ClrsVryCls(params HSLColor[] Cs)
        {
            //Uses the method 'isVryClsTo' or 'Is Very Close To' to compare color values to determine if they're 
                // very close to one another.

            //Need more than 1 to compare
            if (Cs.Length < 2)
                return false;

            for (int x = 0; x < Cs.Length; x++)
            {
                if (x == Cs.Length - 1) //We must not go over with that [x + 1] check below.
                {
                    break;
                }
                if (Cs[x].isVryClsTo(Cs[x + 1]) == false)
                {
                    return false;
                }
            }

            if (Cs[Cs.Length - 1].isVryClsTo(Cs[0]) == false)
                return false;

            return true;
        }

        public static float distanceBetweenL(HSLColor a, HSLColor b)
        {
            return Mathf.Lerp((float)a.L, (float)b.L, 0.5f);
        }


        public void DebugDistance(HSLColor checkingAgainstColor)
        {
            DebugColor();
            checkingAgainstColor.DebugColor();
            float distance = Mathf.Lerp((float)this.H, (float)checkingAgainstColor.H, 0.5f);

            Debug.Log("Distance = " + distance);
        }

        //Because H's max is 6.0d:
        public static double getPercentageDifference_H(double H_a, double H_b)
        {
            return (H_b - H_a) / H_max * 1.0d;
        }

        //Because their max is 1.0d:
        public static double getPercentageDifference_SorL(double SorL_a, double SorL_b)
        {
            return (SorL_a - SorL_b) / SorL_max * 1.0d;
        }

        public bool isVryClsTo(HSLColor checkingAgainstColor)
        {
            // I'm not sure if this could be optimized (I'm (astrah cat) still only an intermediate-beginner programmer)

            // So it gets a percentage different between the two colors Hues. 
                // the 'getPercentageDifference_H has to be a different method for Hue, because 
                    // Hue is not from 0.0 to 1.0 like Saturation or Lightness, but from 
                        // 0.0 to 6.0. The getPercentageDifference_ will always return though a value of 0.0 to 1.0.

            double percD_H = getPercentageDifference_H(this.H, checkingAgainstColor.H);
            double percD_S = getPercentageDifference_SorL(this.S, checkingAgainstColor.S);
            double percD_L = getPercentageDifference_SorL(this.L, checkingAgainstColor.L);
            if (percD_H < 0)
                percD_H *= -1;
            if (percD_S < 0)
                percD_S *= -1;
            if (percD_L < 0)
                percD_L *= -1;

            // We check Hue closeness first, then Saturation closeness, then Lightness closeness.
                // Only if all 3 are close compared to their own individual threshholds (whose values
                    // are declared above in this class), are these two colors close to each other. 

            if (percD_H > H_Closeness_Threshhold)
            {
                return false;
            }
            else
            {
                if (percD_S > S_Closeness_Threshhold)
                {
                    return false;
                } else
                {
                    if (percD_L > L_Closeness_Threshhold)
                    {
                        return false;
                    }
                    else
                    {
                        return true;

                    }
                }
            }
        }

        public bool isVryClsL(HSLColor checkingAgainstColor)
        {
            double percD_L = getPercentageDifference_SorL(this.L, checkingAgainstColor.L);
            if (percD_L < 0)
                percD_L *= -1;

            if (percD_L > L_Closeness_Threshhold)
            {
                return false;
            } else
            {
                return true;
            }
        }

        public static void set_MtoL_and_DtoM_FromDarkestColor()
        {
            double darkestL = 1.00d;

            for (int index = 0; index < ColorsList.Count; index++)
            {
                if (darkestL > ColorsList[index].L)
                    darkestL = ColorsList[index].L;
            }

            double diff = 1.00d - darkestL;

            MtoL = diff * 0.66d;
            DtoM = diff * 0.33d;
        }

        public double averagePictureL = 0;

        
        public static List<bool> Filled             = new List<bool>();
       
        public HSLColor(double _H, double _S, double _L)
        {
            H = _H;
            S = _S;
            L = _L;
        }

        public void FillWithColor(HSLColor hslColorToFillWith, int x, int y, int texWidth, bool SoftenIt = false)
        {
            if (Marked) //Don't fill marked colors
                return;

            if (SoftenIt == false)
            {
                SetToColor(hslColorToFillWith);
            } else
            {
                SetToColor_Soften(hslColorToFillWith);
            }
            SetToFilled(x, y, texWidth);
        }
        private void SetToColor(HSLColor hslColorToSetTo)
        {
            if (FillWithDebugColor)
            {
                H = 3;
                S = 1;
                L = 0.5;
            }
            else
            {
                H = hslColorToSetTo.H;
                S = hslColorToSetTo.S;
                L = hslColorToSetTo.L;
            }
        }
        private void SetToColor_Soften(HSLColor hslColorToSetCloserTo)
        {
            H = (double)Mathf.Lerp((float)H, (float)hslColorToSetCloserTo.H, 0.95f);
            S = (double)Mathf.Lerp((float)S, (float)hslColorToSetCloserTo.S, 0.75f);
            L = (double)Mathf.Lerp((float)L, (float)hslColorToSetCloserTo.L, 0.50f);
        }

        public bool isOffGrid = false;

        public bool isMorD()
        {
            if (L <= MtoL)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public bool isMorL()
        {
            if (L >= DtoM)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public bool isL()
        {
            if (L > MtoL)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public bool isM()
        {
            if (L <= MtoL && L >= DtoM)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isD()
        {
            if (L < DtoM)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public double c()
        {
            return H * S * L;
        }

        public HSLColor Clone()
        {
            return new HSLColor(this.H, this.S, this.L);
        }

        public static void SetBetween(ref HSLColor input, HSLColor target, float multiplier = 1.0f)
        {
            input.H = (double)Mathf.Lerp((float)input.H, (float)target.H, multiplier * 0.97f);
            input.S = (double)Mathf.Lerp((float)input.S, (float)target.S, multiplier);
            input.L = (double)Mathf.Lerp((float)input.L, (float)target.L, multiplier);
        }
        public static HSLColor GetBetween(HSLColor input, HSLColor target, float multiplier = 1.0f)
        {
            input.H = (double)Mathf.Lerp((float)input.H, (float)target.H, multiplier * 0.97f);
            input.S = (double)Mathf.Lerp((float)input.S, (float)target.S, multiplier);
            input.L = (double)Mathf.Lerp((float)input.L, (float)target.L, multiplier);
            return input;
        }

        public static void SetBetweenL(ref HSLColor input, HSLColor target, float multiplier = 1.0f)
        {
            input.L = (double)Mathf.Lerp((float)input.L, (float)target.L, multiplier);
        }

        public static bool PointIsOnTexture(int x, int y, int texWidth)
        {
            int index = GetIndex(x, y, texWidth);

            if (index < ColorsList.Count && index >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool thisIndexIsOffGrid(int index)
        {
            if (index < 0 || index > ColorsList.Count - 1)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public static void SetAtPoint(HSLColor HSLColorToSetTo, int x, int y, int texWidth)
        {
            int index = GetIndex(x, y, texWidth);
            if (thisIndexIsOffGrid(index))
                return; //Don't do anything to off grid stuff

            ColorsList[index] = HSLColorToSetTo;
        }

        private void SetToFilled(int x, int y, int texWidth)
        {
            int index = GetIndex(x, y, texWidth);
            if (thisIndexIsOffGrid(index))
                return; //Don't do anything to off grid stuff

            Filled[index] = true;
        }

        public static HSLColor ColorsListGetAt(int x, int y, int texWidth)
        {
            int index = GetIndex(x, y, texWidth);
            if (thisIndexIsOffGrid(index))
                return new HSLColor(0, 0, 0);

            return ColorsList[index];
        }

        private static int GetIndex(int x, int y, int texWidth)
        {
            return x + (texWidth * y);
        }

        public static Texture2D ApplyToTexture2D(Texture2D texture2D)
        {
            //THIS IS VERY TRICKY! To get the xMax you need to do count / HEIGHT not width! And Vice Versa!
            int xMax = ColorsList.Count / texture2D.height;
            int yMax = ColorsList.Count / texture2D.width;

            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    HSLColor curHSLColor = ColorsListGetAt(x, y, xMax);

                    Color ColorToSetItTo = (Color)HSLColor.GetRGBColorsFromHSLColorScheme(curHSLColor.H, curHSLColor.S, curHSLColor.L);
                    ColorToSetItTo.a = 1; //NO ALPHA ALLOWED

                    texture2D.SetPixel(x, y, ColorToSetItTo);

                }
            }

            return texture2D;
        }

        public static void InitializeColorsList(Texture2D texture2D)
        {
            List<HSLColor> hslColors = new List<HSLColor>();
            for (int y = 0; y < texture2D.height; y++)
            {
                for (int x = 0; x < texture2D.width; x++)
                {
                    Color32 c32 = (Color32)texture2D.GetPixel(x, y);
                    hslColors.Add(GetHSLColorValues_FromRGBColorValues(c32.r, c32.g, c32.b));
                    //Filled list also expands:
                    Filled.Add(false);
                }
            }

            ColorsList = hslColors;
        }

        public static Color32 GetRGBColorsFromHSLColorScheme(double Hue, double Saturation, double Luminosity)
        {
            byte red, green, blue;
            if (Saturation == 0)
            {
                red = (byte)Math.Round(Luminosity * 255d);
                green = (byte)Math.Round(Luminosity * 255d);
                blue = (byte)Math.Round(Luminosity * 255d);
            }
            else
            {
                double t1, t2;
                double th = Hue / 6.0d;

                if (Luminosity < 0.5d)
                {
                    t2 = Luminosity * (1d + Saturation);
                }
                else
                {
                    t2 = (Luminosity + Saturation) - (Luminosity * Saturation);
                }
                t1 = 2d * Luminosity - t2;

                double tr, tg, tb;
                tr = th + (1.0d / 3.0d);
                tg = th;
                tb = th - (1.0d / 3.0d);

                tr = ColorCalc(tr, t1, t2);
                tg = ColorCalc(tg, t1, t2);
                tb = ColorCalc(tb, t1, t2);
                red     = (byte)Math.Round(tr * 255d);
                green   = (byte)Math.Round(tg * 255d);
                blue    = (byte)Math.Round(tb * 255d);
            }
            return new Color32(red, green, blue, 0); //NO ALPHA!
        }
        private static double ColorCalc(double ColorD, double tangA, double tangB)
        {

            if (ColorD < 0) ColorD += 1d;
            if (ColorD > 1) ColorD -= 1d;
            if (6.0d * ColorD < 1.0d) return tangA + (tangB - tangA) * 6.0d * ColorD;
            if (2.0d * ColorD < 1.0d) return tangB;
            if (3.0d * ColorD < 2.0d) return tangA + (tangB - tangA) * (2.0d / 3.0d - ColorD) * 6.0d;
            return tangA;
        }

        public static HSLColor GetHSLColorValues_FromRGBColorValues(byte redByte, byte greenByte, byte blueByte)
        {
            double _red         = (redByte / 255d);
            double _green       = (greenByte / 255d);
            double _blue        = (blueByte / 255d);

            double _minimum     = Math.Min(Math.Min(_red, _green), _blue);
            double _maximum         = Math.Max(Math.Max(_red, _green), _blue);
            double _deltaValue       = _maximum - _minimum;

            double hue = 0;
            double saturation = 0;
            double lightness = (double)((_maximum + _minimum) / 2.0d);

            if (_deltaValue != 0)            {
                if (lightness < 0.5f)
                {
                    saturation = (double)(_deltaValue / (_maximum + _minimum));
                }
                else
                {
                    saturation = (double)(_deltaValue / (2.0d - _maximum - _minimum));
                }


                if (_red == _maximum)
                {
                    hue = (_green - _blue) / _deltaValue;
                }
                else if (_green == _maximum)
                {
                    hue = 2f + (_blue - _red) / _deltaValue;
                }
                else if (_blue == _maximum)
                {
                    hue = 4f + (_red - _green) / _deltaValue;
                }
            }

            return new HSLColor(hue, saturation, lightness);
        }

        #endregion

        #region DEBUG

        public static bool FillWithDebugColor = false;
        public static void DebugShowColor(int index)
        {
            ColorsList[index].DebugColor();
        }
        public void DebugColor()
        {
            UnityEngine.Debug.Log("Color = ( H: " + H + " S: " + S + " L: " + L);
        }
        public static void DebugAllColorsInSnp(List<HSLColor> Snp)
        {
            for (int index = 0; index < Snp.Count; index++)
            {
                Debug.Log("[" + index + "]: H = " + Snp[index].H + " S = " + Snp[index].S + " L = " + Snp[index].L + " and Snp.Count = " + Snp.Count);
            }
        }

        #endregion

    }

}