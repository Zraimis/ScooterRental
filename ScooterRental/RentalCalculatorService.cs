using ScooterRental.Interfaces;

namespace ScooterRental
{
    public class RentalCalculatorService : IRentalCalculatorServiss
    {
        private readonly decimal _maxRentCost = 20;
        private readonly IRentedScooterArchive _archive;

        public RentalCalculatorService(IRentedScooterArchive archive)
        {
            _archive = archive;
        }

        public decimal CalculateRent(RentedScooter rentalRecord)
        {
            if (rentalRecord == null)
            {
                throw new RentedScooterIsNullException("Rental record is null");
            }

            var totalCost = 0m;
            var rentEnd = rentalRecord.RentEnd ?? DateTime.Now;
            var currentDate = rentalRecord.RentStart.Date;

            while (currentDate <= rentEnd.Date)
            {
                totalCost += RentalPricePerDay(rentalRecord, currentDate);
                currentDate = currentDate.AddDays(1);
            }

            return totalCost;
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            var total = 0m;
            var rentedScooters = _archive.GetRentedScooters();

            foreach (var scooter in rentedScooters)
            {
                bool includeRental = (year == null 
                    ||(scooter.RentEnd?.Year == year 
                    ||scooter.RentStart.Year == year));

                if (includeNotCompletedRentals || scooter.RentEnd.HasValue)
                {
                    if (includeRental)
                    {
                        total += CalculateRent(scooter);
                    }
                }
            }
            return total;
        }

        public decimal RentalPricePerDay(RentedScooter rentalRecord, DateTime day)
        {
            if (rentalRecord == null)
            {
                throw new RentedScooterIsNullException("Rental record is null");
            }

            if (day.Date < rentalRecord.RentStart.Date ||
                (rentalRecord.RentEnd.HasValue 
                && day.Date > rentalRecord.RentEnd.Value.Date))
            {
                return 0m;
            }

            DateTime start;
            DateTime end;

            if (day.Date > rentalRecord.RentStart.Date)
            {
                start = day.Date;
            }
            else
            {
                start = rentalRecord.RentStart;
            }

            if (rentalRecord.RentEnd.HasValue && day.Date < rentalRecord.RentEnd.Value.Date)
            {
                end = day.Date.AddDays(1);
            }
            else
            {
                end = rentalRecord.RentEnd ?? DateTime.Now;
            }

            var minutesRented = (end - start).TotalMinutes;
            var costOfDay = Math.Min((decimal)minutesRented * rentalRecord.PricePerMinute, _maxRentCost);

            return costOfDay;
        }
    }
}