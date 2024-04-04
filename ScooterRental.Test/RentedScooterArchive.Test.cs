using FluentAssertions;

namespace ScooterRental.Tests
{
    [TestClass]
    public class RentedScooterArchiveTests
    {
        List<RentedScooter> _rentedScooterList;
        private RentedScooterArchive _rentedScooterArchive;
        [TestInitialize]
        public void SetUp()
        {
            _rentedScooterList = new List<RentedScooter>();
            _rentedScooterArchive = new RentedScooterArchive(_rentedScooterList);
        }
        [TestMethod]
        public void AddRentedScooter_Should_AddScooterToList()
        {
            // Arrange
            var scooter = new RentedScooter("1", DateTime.Now, 0.1m);

            // Act
            _rentedScooterArchive.AddRentedScooter(scooter);

            // Assert
            _rentedScooterList.Should().ContainSingle();
            _rentedScooterList.Should().Contain(scooter);
        }

        [TestMethod]
        public void AddRentedScooter_NullScooter_Should_ThrowException()
        {
            // Act
            Action act = () => _rentedScooterArchive.AddRentedScooter(null);

            //Assert
            act.Should().Throw<ScooterIsNullException>();
        }

        [TestMethod]
        public void AddRentedScooter_DuplicateScooter_Should_ThrowException()
        {
            // Arrange
            var scooter = new RentedScooter("1", DateTime.Now, 6m);
            var rentedScootersList = new List<RentedScooter> { scooter };
            var rentedScooterArchive = new RentedScooterArchive(rentedScootersList);

            // Act
            Action act = () => rentedScooterArchive.AddRentedScooter(scooter);

            //Assert
            act.Should().Throw<ScooterAlreadyExistsException>();
        }

        [TestMethod]
        public void EndRental_ValidInput_Should_EndRentalAndReturnRentedScooter()
        {
            // Arrange
            var rentedScooter = new RentedScooter("1", DateTime.Now, 0.1m);
            _rentedScooterList.Add(rentedScooter);
            var end = DateTime.Now;

            // Act
            var scooterToEnd = _rentedScooterArchive.EndRental("1", end);

            // Assert
            scooterToEnd.Should().NotBeNull();
            scooterToEnd.RentEnd.Should().Be(end);
        }

        [TestMethod]
        public void EndRental_ScooterNotFound_Should_ThrowException()
        {
            // Arrange
            var end = DateTime.Now;

            // Act
            Action act = () => _rentedScooterArchive.EndRental("1", end);

            //Assert
            act.Should().Throw<RentedScooterIsNullException>();
        }

        [TestMethod]
        public void EndRental_InvalidRentEnd_Should_ThrowException()
        {
            // Arrange
            var rentedScooter = new RentedScooter("1", DateTime.Now, 0.1m);
            _rentedScooterList.Add(rentedScooter);

            var end = DateTime.Now.AddMinutes(-1);

            // Act
            Action act = () => _rentedScooterArchive.EndRental("1", end);

            //Assert
            act.Should().Throw<InvalidRentEndException>();
        }

        [TestMethod]
        public void GetRentedScooters_Should_ReturnListOfRentedScooters()
        {
            // Arrange
            var rentedScooter1 = new RentedScooter("1", DateTime.Now, 0.1m);
            var rentedScooter2 = new RentedScooter("2", DateTime.Now, 0.2m);
            _rentedScooterList.Add(rentedScooter1);
            _rentedScooterList.Add(rentedScooter2);

            // Act
            var result = _rentedScooterArchive.GetRentedScooters();

            // Assert
            result.Should().BeEquivalentTo(_rentedScooterList);
        }
    }
}