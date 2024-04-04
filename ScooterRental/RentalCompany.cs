using ScooterRental;
using ScooterRental.Interfaces;

public class RentalCompany : IRentalCompany
{
    private readonly IScooterService _scooterService;
    private readonly IRentedScooterArchive _archive;
    private readonly IRentalCalculatorServiss _calculatorService;
    public RentalCompany(string name,
        IScooterService scooterService, 
        IRentedScooterArchive archive, 
        IRentalCalculatorServiss calcualtorService)
    {
        Name = name;
        _scooterService = scooterService;
        _archive = archive;
        _calculatorService = calcualtorService;
    }

    public string Name { get; }

    public void StartRent(string id)
    {
        var scooter = _scooterService.GetScooterById(id);
        _archive.AddRentedScooter(new RentedScooter(scooter.Id,DateTime.Now, scooter.PricePerMinute));
        scooter.IsRented = true;
    }

    public decimal EndRent(string id)
    {
        var scooter = _scooterService.GetScooterById(id);
        var rentalRecord = _archive.EndRental(scooter.Id, DateTime.Now);

        scooter.IsRented = false;
        return _calculatorService.CalculateRent(rentalRecord);
    }

    public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
    {
        return _calculatorService.CalculateIncome(year, includeNotCompletedRentals);
    }
}