using EquipmentHostingService.Controllers;
using EquipmentHostingService.DTOs;
using EquipmentHostingService.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentHostingService.Tests.Controllers
{
    public class ContractsControllerTests
    {
        private readonly Mock<IContractService> _mockContractService;
        private readonly ContractsController _controller;

        public ContractsControllerTests()
        {
            _mockContractService = new Mock<IContractService>();
            _controller = new ContractsController(_mockContractService.Object);
        }

        [Fact]
        public async Task GetAllContracts_ReturnsOkWithContracts()
        {
            // Arrange
            var contractDtos = new List<ContractDto>
            {
                new ContractDto { Id = 1, FacilityName = "North Facility", EquipmentTypeName = "Type A" },
                new ContractDto { Id = 2, FacilityName = "South Facility", EquipmentTypeName = "Type B" }
            };
            _mockContractService
                .Setup(service => service.GetAllContractsAsync())
                .ReturnsAsync(contractDtos);

            // Act
            var result = await _controller.GetAllContracts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedContracts = Assert.IsType<List<ContractDto>>(okResult.Value);
            Assert.Equal(2, returnedContracts.Count);
        }

        [Fact]
        public async Task GetAllContracts_ReturnsOkWithEmptyList()
        {
            // Arrange
            _mockContractService
                .Setup(service => service.GetAllContractsAsync())
                .ReturnsAsync(new List<ContractDto>());

            // Act
            var result = await _controller.GetAllContracts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedContracts = Assert.IsType<List<ContractDto>>(okResult.Value);
            Assert.Empty(returnedContracts);
        }

        [Fact]
        public async Task CreateContract_ValidInput_ReturnsCreated()
        {
            // Arrange
            var createContractDto = new CreateContractDto
            {
                FacilityCode = "FAC001",
                EquipmentTypeCode = "EQP001",
                EquipmentQuantity = 5
            };

            var contractDto = new ContractDto
            {
                Id = 1,
                FacilityName = "North Facility",
                EquipmentTypeName = "Type A"
            };

            _mockContractService
                .Setup(service => service.CreateContractAsync(createContractDto))
                .ReturnsAsync(contractDto);

            // Act
            var result = await _controller.CreateContract(createContractDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedContract = Assert.IsType<ContractDto>(createdResult.Value);
            Assert.Equal(1, returnedContract.Id);
            Assert.Equal("North Facility", returnedContract.FacilityName);
        }

        [Fact]
        public async Task CreateContract_ThrowsException_PassesToMiddleware()
        {
            // Arrange
            var createContractDto = new CreateContractDto
            {
                FacilityCode = "FAC001",
                EquipmentTypeCode = "INVALID_CODE",
                EquipmentQuantity = 5
            };

            _mockContractService
                .Setup(service => service.CreateContractAsync(createContractDto))
                .ThrowsAsync(new KeyNotFoundException("Equipment type not found."));

            // Act
            var ex = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _controller.CreateContract(createContractDto));

            // Assert
            Assert.Equal("Equipment type not found.", ex.Message);
        }

        [Fact]
        public async Task CreateContract_InvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("FacilityCode", "Required");

            var createContractDto = new CreateContractDto
            {
                EquipmentTypeCode = "EQP001",
                FacilityCode = null,
                EquipmentQuantity = 5
            };

            // Act
            var result = await _controller.CreateContract(createContractDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("Invalid input.", badRequestResult.Value.ToString());
        }

        [Fact]
        public async Task CreateContract_DuplicateContract_ReturnsCreated()
        {
            // Arrange
            var createContractDto = new CreateContractDto
            {
                FacilityCode = "FAC001",
                EquipmentTypeCode = "EQP001",
                EquipmentQuantity = 5
            };

            var createdContractDto = new ContractDto
            {
                Id = 1,
                FacilityName = "North Facility",
                EquipmentTypeName = "Excavator",
                EquipmentQuantity = 5
            };

            _mockContractService
                .Setup(service => service.CreateContractAsync(It.IsAny<CreateContractDto>()))
                .ReturnsAsync(createdContractDto);

            // Act
            var result = await _controller.CreateContract(createContractDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedContract = Assert.IsType<ContractDto>(createdResult.Value);

            Assert.Equal("North Facility", returnedContract.FacilityName);
            Assert.Equal("Excavator", returnedContract.EquipmentTypeName);
        }
    }
}