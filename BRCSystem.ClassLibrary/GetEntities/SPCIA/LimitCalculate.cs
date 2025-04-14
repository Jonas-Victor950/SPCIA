using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BRCSystem.ClassLibrary.SPCIA.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BRCSystem.ClassLibrary.GetEntities.SPCIA
{
    public static class LimitCalculate
    {     
        public static double calculate2XBar(List<Samples> lists)
        {
            double doubleXBar = 0;
            
            if (lists.Count() > 0) {
                foreach (Samples sample in lists)
                {
                    doubleXBar = doubleXBar + sample.Xbar;
                }           

                doubleXBar = doubleXBar / lists.Count;
            }

            return doubleXBar;
        }

        public static double calculateRBar(List<Samples> lists)
        {
            double rBar = 0;
            if (lists.Count() > 0) {
                foreach (Samples sample in lists)
                {
                    rBar = rBar + sample.R;
                }

                rBar = rBar / lists.Count;
            }

            return Math.Round(rBar, 4);
        }

        public static double calculateStandardDeviation(List<Samples> lists)
        {
            double stdDeviation = 0;
            List<double> values = new List<double>();

            foreach (Samples sample in lists)
            {
                foreach (Input input in sample.Inputs ?? new List<Input>())
                {
                    values.Add(input.Value);                    
                }
            }

            if(values.Count > 0) {
                // Compute the average.     
                double avg = values.Average();

                // Perform the Sum of (value-avg)_2_2.      
                double sum = values.Sum(d => Math.Pow(d - avg, 2));

                // Put it all together.      
                stdDeviation = Math.Sqrt((sum) / (values.Count()));

                return stdDeviation;
            } else
            {
                return 1;
            }            
        }

        public static double? calculateCpk(double usl, double lsl,
            double doubleXBar, double rBar, double d2)
        {
            double cpk = 0;

            double ohat = rBar / d2;

            if(ohat > 0){
                cpk = Math.Min((usl - doubleXBar), (doubleXBar - lsl)) / (3 * ohat);
                return cpk;
            }else {
                return null;
            }            
        }

        public static double? calculatePpk(double usl, double lsl,
            double doubleXBar, double stdDeviation)
        {
            if(stdDeviation > 0){
                double ppk = 0;

                ppk = Math.Min((usl - doubleXBar), (doubleXBar - lsl)) / (3 * stdDeviation);

                 return ppk;
            }   
            else {
                return null;
            }         
        }

        public static List<String>? calculateXBarAlarms(double clx, double rBar, List<Samples> lists, double a2, double lclx, double uclx)
        {
            List<String> pointColors = new List<String>();
            int clu = 0;
            int cll = 0;
            int trendUp = 0;
            int trendDown = 0;
            double LCLx = lclx;//doubleXBar - (a2 * rBar);
            double UCLx = uclx;//doubleXBar + (a2 * rBar);

          
            for (int x = 0; x < lists.Count; x++)
            {
                if (LCLx > UCLx || lists.ElementAt(x).Xbar < LCLx || UCLx < lists.ElementAt(x).Xbar) {
                    pointColors.Add("Red");
                    clu = 0;
                    cll = 0;
                    trendUp = 0;
                    trendDown = 0;
                    lists.ElementAt(x).XbarAlarm = true;
                }
                if (LCLx <= lists.ElementAt(x).Xbar && lists.ElementAt(x).Xbar <= UCLx)
                {
                    Boolean redFlag = false;
                    if (0 < x)
                    {
                        if (lists.ElementAt(x - 1).Xbar < lists.ElementAt(x).Xbar)
                        {
                            trendDown = 0;
                            trendUp++;
                        }

                        if (lists.ElementAt(x).Xbar < lists.ElementAt(x - 1).Xbar)
                        {
                            trendUp = 0;
                            trendDown++;
                        }

                        if (lists.ElementAt(x - 1).Xbar == lists.ElementAt(x).Xbar)
                        {
                            trendUp++;
                            trendDown++;
                        }
                    }
                    if (5 < trendUp || 5 < trendDown)
                    {
                        redFlag = true;
                    }
                    if (lists.ElementAt(x).Xbar < clx)
                    {
                        clu = 0;
                        cll++;
                    }
                    if (clx < lists.ElementAt(x).Xbar)
                    {
                        cll = 0;
                        clu++;
                    }
                    if (clx == lists.ElementAt(x).Xbar)
                    {
                        cll++;
                        clu++;
                    }
                    if (6 < cll || 6 < clu)
                    {
                        redFlag = true;
                    }
                    if (redFlag)
                    {
                        pointColors.Add("Red");
                        lists.ElementAt(x).XbarAlarm = true;
                    }
                    else
                    {
                        pointColors.Add("Blue");
                    }
                }
                
            }
            return pointColors;
        }

        public static List<String>? calculateRAlarms(double clr, List<Samples> lists, double d4, double uclr, double lclr)
        {
            List<String> pointColors = new List<String>();
            int clu = 0;
            int cll = 0;
            int trendUp = 0;
            int trendDown = 0;
            double UCLr = uclr;//d4 * rBar;
            double LCLr = lclr;

            for (int x = 0; x < lists.Count; x++)
            {
                if (LCLr > UCLr || lists.ElementAt(x).R < LCLr || UCLr < lists.ElementAt(x).R) {
                    pointColors.Add("Red");
                    clu = 0;
                    cll = 0;
                    trendUp = 0;
                    trendDown = 0;
                    lists.ElementAt(x).RAlarm = true;
                }
                Boolean redFlag = false;
                if (LCLr <= lists.ElementAt(x).R && lists.ElementAt(x).R <= UCLr)
                {
                    if (0 < x)
                    {
                        if (lists.ElementAt(x - 1).R < lists.ElementAt(x).R)
                        {
                            trendDown = 0;
                            trendUp++;
                        }

                        if (lists.ElementAt(x).R < lists.ElementAt(x - 1).R)
                        {
                            trendUp = 0;
                            trendDown++;
                        }

                        if (lists.ElementAt(x - 1).R == lists.ElementAt(x).R)
                        {
                            trendUp++;
                            trendDown++;
                        }
                    }
                    if (5 < trendUp || 5 < trendDown)
                    {
                        redFlag = true;
                    }
                    if (lists.ElementAt(x).R < clr)
                    {
                        clu = 0;
                        cll++;
                    }
                    if (clr < lists.ElementAt(x).R)
                    {
                        cll = 0;
                        clu++;
                    }
                    if (clr == lists.ElementAt(x).R)
                    {
                        cll++;
                        clu++;
                    }
                    if (6 < clu || 6 < cll)
                    {
                        redFlag = true;
                    }
                    if (redFlag)
                    {
                        pointColors.Add("Red");
                        lists.ElementAt(x).RAlarm = true;
                    } else
                    {
                        pointColors.Add("Blue");
                    }
                }                         
            }
            return pointColors;
        }

        public static Double calculateUCLx(double doubleXBar, double rBar, double a2)
        {
            return doubleXBar + a2 * rBar;
        }

        public static Double calculateLCLx(double doubleXBar, double rBar, double a2)
        {
            return doubleXBar - a2 * rBar;
        }

        public static Double calculateCLx(double doubleXBar)
        {
            return doubleXBar;
        }

        public static Double calculateUCLr(double rBar, double d4)
        {
            return d4 * rBar;
        }

        public static Double calculateCLr(double rBar)
        {
            return rBar;
        }

        public static Double calculateStandardDeviationByCpk(double usl, double lsl, double doubleXBar, double cpkE)
        {
            if (cpkE == 0) {
                return 0;
            }

            double stdDeviation = 0;

            stdDeviation = Math.Min((usl - doubleXBar), (doubleXBar - lsl)) / (3 * cpkE);

            return stdDeviation;
        }

        public static Double calculateUCLxByCpk(double usl, double cpkE, int inputQtd, double stdDeviation)
        {
            double uclx = 0;
            uclx = usl - ((cpkE * 3) - (3 / Math.Sqrt(inputQtd))) * stdDeviation;

            return uclx;
        }

        public static Double calculateLCLxByCpk(double lsl, double cpkE, int inputQtd, double stdDeviation)
        {
            double lclx = 0;
            lclx = lsl + ((cpkE * 3) - (3 / Math.Sqrt(inputQtd))) * stdDeviation;

            return lclx;
        }

        public static Double calculateCLxByCpk(double uclx, double lclx)
        {
            double lcx = 0;
            lcx = (uclx + lclx) / 2;
            return lcx; 
        }
       
    }
}
