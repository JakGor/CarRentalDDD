using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using DDD.Base.DomainModelLayer.Models;

namespace DDD.CarRentalLib.DomainModelLayer.Models
{
    public class Distance : ValueObject
    {
        public static readonly string DefaultUnit = "km";
        public static readonly Distance Zero = new Distance(0);
        public double Value { get; protected set; }
        public string Unit { get; protected set; }
        public Distance(double value, string unit)
        {
            Value = value;
            Unit = unit;
        }
        public Distance(double value)
        {
            Value = value;
            Unit = DefaultUnit;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Math.Round(Value,1);
            yield return Unit.ToUpper();
        }

        public Distance ChangeKmIntoMiles(Distance d)
        {
            if (d.Unit.ToUpper() != "KM")
                throw new Exception("Give distance in kilometers");
            else
            {
                d.Value = Math.Round(d.Value* 0.621,2);
                
                d.Unit = "mi";
            }
            return d;
        }
        public Distance ChangeMilesIntoKm(Distance d)
        {
            if (d.Unit.ToUpper() != "MI")
                throw new Exception("Give distance in miles");
            else
            {
                d.Value = Math.Round(d.Value* 1.609,2);
                d.Unit = "km";
            }
            return d;
        }
        public static Distance operator +(Distance d, Distance d2)
        {
            if (!AreCompatibleUnits(d, d2))
            {
                throw new ArgumentException("Unit mismatch");
            }
            return new Distance(d.Value + d2.Value, d.Unit);
        }

        public static Distance operator -(Distance d, Distance d2)
        {
            if (!AreCompatibleUnits(d, d2))
            {
                throw new ArgumentException("Unit mismatch");
            }
            return new Distance(d.Value - d2.Value, d.Unit);
        }
        private static bool AreCompatibleUnits(Distance d, Distance d2)
        {
            return IsZero(d.Value) || IsZero(d2.Value) || d.Unit.Equals(d2.Unit);
        }
        private static bool IsZero(double testedValue)
        {
            return decimal.Zero.CompareTo((decimal)testedValue) == 0;
            
        }

        public static bool operator <(Distance d, Distance d2)
        {
            return d.Value.CompareTo(d2.Value) < 0;
        }

        public static bool operator >(Distance d, Distance d2)
        {
            return d.Value.CompareTo(d2.Value) > 0;
        }

        public static bool operator >=(Distance d, Distance d2)
        {
            return d.Value.CompareTo(d2.Value) >= 0;
        }

        public static bool operator <=(Distance d, Distance d2)
        {
            return d.Value.CompareTo(d2.Value) <= 0;
        }
    }
}
