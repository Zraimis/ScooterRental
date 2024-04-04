using FluentAssertions;
using Moq;
using Moq.AutoMock;
using ScooterRental.Interfaces;

namespace ScooterRental.Mock.Tests;

[TestClass]
public class RentalCompanyTest
{ 
    private AutoMocker _mocker;
    private RentalCompany _company;
    private Mock<IScooterService> _scooterServiceMock;
    private Mock<IRentedScooterArchive> _rentedScooterArchiveMock;
    private Mock<IRentalCalculatorServiss> _rentalCalculatorMock;
    private const string defaultCompany = "Test";
    [TestInitialize]
    public void SetUp()
    {
        _mocker = new AutoMocker();
        _scooterServiceMock = _mocker.GetMock<IScooterService>();
        _rentedScooterArchiveMock = _mocker.GetMock<IRentedScooterArchive>();
        _rentalCalculatorMock = _mocker.GetMock<IRentalCalculatorServiss>();
        _company = new RentalCompany(
            defaultCompany, 
            _scooterServiceMock.Object,
            _rentedScooterArchiveMock.Object,
            _rentalCalculatorMock.Object);
    }
    [TestMethod]
    public void StartRent_ExistingScooter_ScooterIsRented()
    {
        //Arrange
        var scooter = new Scooter("1", 0.1m);
        _scooterServiceMock.Setup(s => s.GetScooterById("1")).Returns(scooter);

        //Act
        _company.StartRent("1");

        //Assert
        scooter.IsRented.Should().BeTrue();
    }
    [TestMethod]
    public void EndRent_ExistingScooter_ScooterRenStopped()
    {
        //Arrange
        var now = DateTime.Now;
        var scooter = new Scooter("1", 0.2m) {IsRented = true};
        var rentalrecord = new RentedScooter(scooter.Id, now.AddMinutes(-20), scooter.PricePerMinute) { RentEnd = now};
        _scooterServiceMock.Setup(s => s.GetScooterById("1")).Returns(scooter);
        _rentedScooterArchiveMock.Setup(s => s.EndRental(scooter.Id, It.IsAny<DateTime>())).Returns(rentalrecord);
        _rentalCalculatorMock.Setup(s => s.CalculateRent(rentalrecord)).Returns(4);

        //Act
        var result = _company.EndRent("1");

        //Assert
        scooter.IsRented.Should().BeFalse();
    }
    [TestMethod]
    //Man kaut kas nepatīk šitajā testā!
    public void CalculateIncome_ScooterInputed_ReturnTotalIncome()
    {
        // Arrange
        var now = DateTime.Now;
        var scooter = new Scooter("1", 0.2m) { IsRented = true };
        var rentalrecord = new RentedScooter(scooter.Id, now.AddMinutes(-20), scooter.PricePerMinute);
        _rentedScooterArchiveMock.Setup(a => a.GetRentedScooters()).Returns(new List<RentedScooter> { rentalrecord });
        _rentalCalculatorMock.Setup(c => c.CalculateIncome(null, true)).Returns(4m);

        // Act
        var totalIncome = _company.CalculateIncome(null, true);

        // Assert
        totalIncome.Should().Be(4m);
    }
}