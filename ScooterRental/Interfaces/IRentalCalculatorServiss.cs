using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Interfaces
{
    public interface IRentalCalculatorServiss
    {
        decimal CalculateRent(RentedScooter rentalRecord);
        decimal CalculateIncome(int? year, bool includeNotCompletedRentals);
        public decimal RentalPricePerDay(RentedScooter rentalRecord, DateTime day);
    }
}