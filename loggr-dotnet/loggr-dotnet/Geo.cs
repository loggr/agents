using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loggr
{
    public class Geo
    {
        protected double _latitude = 0;
        protected double _longitude = 0;

        public double Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                _latitude = value;
            }
        }

        public double Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                _longitude = value;
            }
        }
    }
}
