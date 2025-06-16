using Microsoft.Extensions.Logging;
using Moq;
using Rhyous.UnitTesting;
using Rhyous.WebApiExtensions.Exceptions;
using Rhyous.WebApiExtensions.Interfaces;
using Rhyous.WebApiExtensions.Models;
using Shouldly;

namespace Rhyous.WebApiExtensions.Tests.StartupValidators
{
    [TestClass]
    public class AllStartupValidatorsTests
    {
        private MockRepository _mockRepository;
        private Mock<IStartupValidator> _mockValidator1;
        private Mock<IStartupValidator> _mockValidator2;
        private IEnumerable<IStartupValidator> _validators;
        private Mock<ILogger<AllStartupValidators>> _mockLogger;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _mockValidator1 = _mockRepository.Create<IStartupValidator>();
            _mockValidator2 = _mockRepository.Create<IStartupValidator>();
            _validators = new List<IStartupValidator> { _mockValidator1.Object, _mockValidator2.Object };
            _mockLogger = _mockRepository.Create<ILogger<AllStartupValidators>>(MockBehavior.Loose);
        }

        private AllStartupValidators CreateAllStartupValidators()
        {

            return new AllStartupValidators(
                _validators,
                _mockLogger.Object);
        }

        #region ValidateAsync
        [TestMethod]
        [ListTNullOrEmpty(typeof(IStartupValidator))]
        public async Task AllStartupValidators_ValidateAsync_NoValidatorsExist(List<IStartupValidator> startupValidators)
        {
            // Arrange
            _validators = startupValidators;
            var allStartupValidators = CreateAllStartupValidators();

            // Act
            await allStartupValidators.ValidateAsync();

            // Assert
            _mockRepository.VerifyAll();
        }

        [TestMethod]
        public async Task AllStartupValidators_ValidateAsync_AllValidatorsPass()
        {
            // Arrange
            var allStartupValidators = CreateAllStartupValidators();
            var validationResult1 = new List<StartupValidationResult> { new StartupValidationResult(true, "Validation 1", "Success 1!") };
            _mockValidator1.Setup(m => m.ValidateAsync()).ReturnsAsync(validationResult1);
            var validationResult2 = new List<StartupValidationResult> { new StartupValidationResult(true, "Validation 2", "Success 2!") };
            _mockValidator2.Setup(m => m.ValidateAsync()).ReturnsAsync(validationResult2);

            // Act
            await allStartupValidators.ValidateAsync();

            // Assert
            _mockRepository.VerifyAll();
        }

        [TestMethod]
        public async Task AllStartupValidators_ValidateAsync_OneValidatorsFails_Throws()
        {
            // Arrange
            var allStartupValidators = CreateAllStartupValidators();
            var validationResults1 = new List<StartupValidationResult> { new StartupValidationResult(true, "Validation 1", "Success 1!") };
            _mockValidator1.Setup(m => m.ValidateAsync()).ReturnsAsync(validationResults1);
            var validationResults2 = new List<StartupValidationResult> { new StartupValidationResult(false, "Validation 2", "Failed 2!") };
            _mockValidator2.Setup(m => m.ValidateAsync()).ReturnsAsync(validationResults2);


            // Act & Assert
            var ex = await Should.ThrowAsync<StartupValidationException>(async () =>
            {
                await allStartupValidators.ValidateAsync();
            });
            ex.Message.ShouldBe($"Startup Validation Errors: {Environment.NewLine}Validation 2: Failed 2!");
            _mockRepository.VerifyAll();
        }

        [TestMethod]
        public async Task AllStartupValidators_ValidateAsync_AllValidatorsFail_Throws()
        {
            // Arrange
            var allStartupValidators = CreateAllStartupValidators();
            var validationResults1 = new List<StartupValidationResult> { new StartupValidationResult(false, "Validation 1", "Failed 1!") };
            _mockValidator1.Setup(m => m.ValidateAsync()).ReturnsAsync(validationResults1);
            var validationResults2 = new List<StartupValidationResult> { new StartupValidationResult(false, "Validation 2", "Failed 2!") };
            _mockValidator2.Setup(m => m.ValidateAsync()).ReturnsAsync(validationResults2);

            var expectedMessage = $"Startup Validation Errors: {Environment.NewLine}"
                                + $"Validation 1: Failed 1!{Environment.NewLine}"
                                + $"Validation 2: Failed 2!";

            // Act & Assert
            var ex = await Should.ThrowAsync<StartupValidationException>(allStartupValidators.ValidateAsync);
            ex.Message.ShouldBe(expectedMessage);
            _mockRepository.VerifyAll();
        }
        #endregion
    }
}
