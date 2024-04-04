using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Test
{
    [TestClass]
    public class ScooterServiceTests
    {
        private ScooterService _scooterService;
        List<Scooter> _scooters;

        [TestInitialize]
        public void SetUp()
        {
            _scooters = new List<Scooter>();
            _scooterService = new ScooterService(_scooters);
        }
        [TestMethod]
        public void AddScooter_Valid_Data_Provided_ScooterAdded()
        {
            //Act
            _scooterService.AddScooter("1", 0.1m);

            //Assert
            _scooters.Count().Should().Be(1);
        }
        [TestMethod]
        public void AddScooter_Invalid_Price_Provided_ThrowException()
        {
            //Act
            Action action = () => _scooterService.AddScooter("1", 0m);

            //Assert
            action.Should().Throw<InvalidPriceException>();
        }
        [TestMethod]
        public void AddScooter_Empty_Id_Provided_ThrowException()
        {
            //Act
            Action action = () => _scooterService.AddScooter("", 1.0m);

            //Assert
            action.Should().Throw<InvalidScooterIdException>();
        }
        [TestMethod]
        public void AddScooter_DuplicateScooter_Provided_ThrowDuplicationException()
        {
            //Arrange
            _scooters.Add(new Scooter("1", 0.1m));
            //Act
            Action action = () => _scooterService.AddScooter("1", 1.0m);

            //Assert
            action.Should().Throw<InvalidDuplicationException>();
        }
        /// <summary>
        /// END OF ADDSCOOTER() TESTS
        /// </summary>
        [TestMethod]

        public void GetScooterById_Valid_Data_Provided_ScooterFound()
        {
            //Arrange
            _scooterService.AddScooter("1", 0.1m);
            //Act
            var result = _scooterService.GetScooterById("1");
            //Assert
            result.Should().NotBeNull();
            result.Id.Should().Be("1");
            result.PricePerMinute.Should().Be(0.1m);
        }
        [TestMethod]
        public void GetScooterById_NonExistingId_ThrowsInvalidScooterIdException()
        {
            // Arrange
            Action action = () => _scooterService.GetScooterById("NonExistingId");
            // Act & Assert
            action.Should().Throw<InvalidScooterIdException>();
        }
        /// <summary>
        /// END OF GETSCOOTERBYID() TESTS
        /// </summary>
        [TestMethod]
        public void GetScooters_Valid_Data_Provided_AllScooterFound()
        {
            //Arrange
            _scooterService.AddScooter("1", 0.1m);
            _scooterService.AddScooter("2", 0.2m);
            _scooterService.AddScooter("3", 0.3m);
            //Act
            var result = _scooterService.GetScooters();
            //Assert
            result.Should().HaveCount(3);
            result.Should().Contain(_scooters);
        }
        /// <summary>
        /// END OF GETSCOOTERS() TESTS
        /// </summary>
        [TestMethod]
        public void RemoveScooter_Valid_Data_Provided_Scooter_Removed()
        {

            // Arrange
            var scooter = new Scooter("1", 0.1m);
            _scooters.Add(scooter);

            // Act
            _scooterService.RemoveScooter("1");

            // Assert
            _scooters.Should().NotContain(scooter);
            _scooters.Should().HaveCount(0);
        }
    }
}
