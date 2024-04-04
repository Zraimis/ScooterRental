using FluentAssertions;
using Moq;
using Moq.AutoMock;
using ScooterRental.Interfaces;
using ScooterRental.Services;

namespace ScooterRental.Tests
{
    [TestClass]
    public class RentalCalculatorServiceTests
    {
        private RentalCalculatorService _calculatorService;
        private AutoMocker _mocker;

        [TestInitialize]
        public void Initialize()
        {
            _mocker = new AutoMocker();
            _calculatorService = _mocker.CreateInstance<RentalCalculatorService>();
        }

        [TestMethod]
        public void Calculate_Rent_Null_Provided_Throws_RentedScooterIsNull_Exception()
        {
            //Act
            Action act = () => _calculatorService.CalculateRent(null);

            //Assert
            act.Should().Throw<RentedScooterIsNullException>();
        }
        [TestMethod]
        public void Calculate_Rent_Valid_Data_Provided_Return_Total_Amount()
        {
            //Arrange
            var start = new DateTime(2024, 1, 1, 2, 1, 0);
            var end = new DateTime(2024, 1, 2, 3, 1, 0);
            var pricePerMinute = 0.5m;
            var rentedScooter = new RentedScooter("1", start, pricePerMinute) { RentEnd = end };

            //Act
            var totalCost = _calculatorService.CalculateRent(rentedScooter);

            //Assert
            totalCost.Should().Be(40m);
        }
        [TestMethod]
        public void Rental_Cost_Per_Day_Null_Provided_Throws_RentedScooterIsNull_Exception()
        {
            //Act
            Action act = () => _calculatorService.RentalPricePerDay(null, DateTime.Now);

            //Assert
            act.Should().Throw<RentedScooterIsNullException>();
        }
        //Man taču nav jātaisa extra tests lai parbuaditu CalculateIncome
        //ja to jau parbaudijam RentalCompany Testos?
    }
}