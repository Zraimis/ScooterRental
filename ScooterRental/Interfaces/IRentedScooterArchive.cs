using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Interfaces
{
    public interface IRentedScooterArchive
    {
        void AddRentedScooter(RentedScooter scooter);
        RentedScooter EndRental(string scooterId, DateTime rentEnd);
        List<RentedScooter> GetRentedScooters();
    }
}