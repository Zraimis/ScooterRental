using ScooterRental.Interfaces;

namespace ScooterRental
{
    public class RentedScooterArchive : IRentedScooterArchive
    {
        private readonly List<RentedScooter> _rentedScootersList;

        public RentedScooterArchive(List<RentedScooter> rentedScootersList)
        {
            _rentedScootersList = rentedScootersList;
        }

        public void AddRentedScooter(RentedScooter scooter)
        {
            if (scooter == null)
            {
                throw new ScooterIsNullException("Scooter is null.");
            }

            if (_rentedScootersList.Any(sc => sc.ScooterId == scooter.ScooterId && sc.RentStart == scooter.RentStart))
            {
                throw new ScooterAlreadyExistsException("Scooter already exists.");
            }

            _rentedScootersList.Add(scooter);
        }

        public RentedScooter EndRental(string scooterId, DateTime rentEnd)
        {
            var rentedScooter = _rentedScootersList.LastOrDefault(
                s => s.ScooterId == scooterId && s.RentEnd == null);

            if (rentedScooter == null)
            {
                throw new RentedScooterIsNullException("Rented scooter is null.");
            }

            if (rentEnd < rentedScooter.RentStart)
            {
                throw new InvalidRentEndException("Scooter is Null.");
            }

            rentedScooter.RentEnd = rentEnd;
            return rentedScooter;
        }

        public List<RentedScooter> GetRentedScooters()
        {
            return _rentedScootersList;
        }
    }
}
