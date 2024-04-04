using ScooterRental.Interfaces;

namespace ScooterRental
{
    public class ScooterService : IScooterService
    {
        private readonly List<Scooter> _scooters;
        public ScooterService(List<Scooter> scooter)
        {
            _scooters = scooter;
        }
        public void AddScooter(string id, decimal pricePerMinute)
        {
            if(string.IsNullOrEmpty(id))
            {
                throw new InvalidScooterIdException("Invalid scooterId");
            }
            if (pricePerMinute == null)
            {
                throw new InvalidPriceException("Invalid price");
            }
            if (pricePerMinute <= 0)
            {
                throw new InvalidPriceException("Invalid price");
            }
            if(_scooters.Any(scooter =>  scooter.Id == id)) 
            {
                throw new InvalidDuplicationException("Scooter already exists");
            }
            _scooters.Add(new Scooter(id, pricePerMinute));
        }

        public Scooter GetScooterById(string scooterId)
        {
            var scooter = _scooters.FirstOrDefault(s => s.Id == scooterId);
            if (scooter == null)
            {
                throw new InvalidScooterIdException("Invalid scooterId");
            }
            return scooter;
        }   

        public IList<Scooter> GetScooters()
        {
            if(_scooters.Count == 0)
            {
                throw new NoScootersException("No scooters, Its empty");
            }
            return _scooters;
        }
        public void RemoveScooter(string id)
        {
            _scooters.RemoveAll(scooter => scooter.Id == id);
        }
    }
}
