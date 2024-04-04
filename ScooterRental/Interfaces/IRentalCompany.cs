using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Interfaces
{
    public interface IRentalCompany
    {
        string Name { get; }
        void StartRent(string id);
        decimal EndRent(string id);
        decimal CalculateIncome(int? year, bool includeNotCompletedRentals);
    }
}