using DDD.CarRentalLib.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Base.DomainModelLayer.Models
{
    public class Position : ValueObject
    {
        public static readonly string DefaultUnit = "km";
        public double X { get; protected set; }
        public double Y { get; protected set; }
        public string Unit { get; protected set; }
        public Position(double x, double y, string unit)
        {
            X = x;
            Y = y;
            Unit = unit;

        }
        public Position(double x, double y)
        {
            X = x;
            Y = y;
            Unit = DefaultUnit;

        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Math.Round(X, 2).ToString()+" X "+Unit;
            yield return Math.Round(Y, 2).ToString() + " Y " + Unit;

        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Math.Round(X, 2));
            sb.Append(" X " + Unit+" ");
            sb.Append(Math.Round(Y, 2));
            sb.Append(" Y " + Unit+" ");

            return sb.ToString();
        }
        public Distance CalculateDistance(Position currentPosition,Position givenPosition)
        {
            if (currentPosition.Unit != givenPosition.Unit)
                throw new Exception("Positions must be in the same unit");
            double distanceValue = Math.Sqrt(Math.Pow(givenPosition.X - currentPosition.X, 2) + Math.Pow(givenPosition.Y - currentPosition.Y, 2));
            Distance calculatedDistance = new Distance(distanceValue,currentPosition.Unit);
            return calculatedDistance;
        }
    }
}
