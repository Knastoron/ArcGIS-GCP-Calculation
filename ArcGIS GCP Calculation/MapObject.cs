using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcGIS_GCP_Calculation
{
    public class MapObject
    {
        public double TopLeftX = 0;
        public double TopRightX = 0;
        public double MiddleX = 0;
        public double BottomLeftX = 0;
        public double BottomRightX = 0;

        public double TopLeftY = 0;
        public double TopRightY = 0;
        public double MiddleY = 0;
        public double BottomLeftY = 0;
        public double BottomRightY = 0;




        public double PxTopLeftX = 0;
        public double PxTopRightX = 0;
        public double PxMiddleX = 0;
        public double PxBottomLeftX = 0;
        public double PxBottomRightX = 0;
        
        public double PxTopLeftY = 0;
        public double PxTopRightY = 0;
        public double PxMiddleY = 0;
        public double PxBottomLeftY = 0;
        public double PxBottomRightY = 0;
        public MapObject ReturnCopyOfMe()
        {
            var copy = new MapObject();

            copy.TopLeftX = TopLeftX;
            copy.TopRightX = TopRightX;
            copy.MiddleX = MiddleX;
            copy.BottomLeftX = BottomLeftX;
            copy.BottomRightX = BottomRightX;

            copy.TopLeftY = TopLeftY;
            copy.TopRightY = TopRightY;
            copy.MiddleY = MiddleY;
            copy.BottomLeftY = BottomLeftY;
            copy.BottomRightY = BottomRightY;

           // copy.PxTopLeftX = PxTopLeftX;
            //copy.PxTopRightX = PxTopRightX;
            //copy.PxMiddleX = PxMiddleX;
            //copy.PxBottomLeftX = PxBottomLeftX;
            //copy.PxBottomRightX = PxBottomRightX;

            //copy.PxTopLeftY = PxTopLeftY;
            //copy.PxTopRightY = PxTopRightY;
            //copy.PxMiddleY = PxMiddleY;
            //copy.PxBottomLeftY = PxBottomLeftY;
            //copy.PxBottomRightY = PxBottomRightY;


            return copy;
        }
        public void ApplyXModifier(int additive)
        {
            TopLeftX += additive;
            TopRightX += additive;
            MiddleX += additive;
            BottomLeftX += additive;
            BottomRightX += additive;
        }
        public void ApplyYModifier(int additive)
        {
            TopLeftY += additive;
            TopRightY += additive;
            MiddleY += additive;
            BottomLeftY += additive;
            BottomRightY += additive;
        }
        
        public string DumpToFile()
        {
            StringBuilder text = new StringBuilder();
            text.AppendLine(PxTopLeftX.ToString("F6", CultureInfo.InvariantCulture) + '\t' + PxTopLeftY.ToString("F6", CultureInfo.InvariantCulture) + '\t' + TopLeftX.ToString("F6", CultureInfo.InvariantCulture) + '\t' + TopLeftY.ToString("F6", CultureInfo.InvariantCulture));
            text.AppendLine(PxBottomLeftX.ToString("F6", CultureInfo.InvariantCulture) + '\t' + PxBottomLeftY.ToString("F6", CultureInfo.InvariantCulture) + '\t' + BottomLeftX.ToString("F6", CultureInfo.InvariantCulture) + '\t' + BottomLeftY.ToString("F6", CultureInfo.InvariantCulture));
            text.AppendLine(PxBottomRightX.ToString("F6", CultureInfo.InvariantCulture) + '\t' + PxBottomRightY.ToString("F6", CultureInfo.InvariantCulture) + '\t' + BottomRightX.ToString("F6", CultureInfo.InvariantCulture) + '\t' + BottomRightY.ToString("F6", CultureInfo.InvariantCulture));
            text.AppendLine(PxTopRightX.ToString("F6", CultureInfo.InvariantCulture) + '\t' + PxTopRightY.ToString("F6", CultureInfo.InvariantCulture) + '\t' + TopRightX.ToString("F6", CultureInfo.InvariantCulture) + '\t' + TopRightY.ToString("F6", CultureInfo.InvariantCulture));
            text.AppendLine(PxMiddleX.ToString("F6", CultureInfo.InvariantCulture) + '\t' + PxMiddleY.ToString("F6", CultureInfo.InvariantCulture) + '\t' + MiddleX.ToString("F6", CultureInfo.InvariantCulture) + '\t' + MiddleY.ToString("F6", CultureInfo.InvariantCulture));
            return text.ToString();
        }

    }
}
