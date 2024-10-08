using Moq;
using Xunit;
using PrecoDaBanana.API.Controllers;
using PrecoDaBanana.API.Repositories;
using PrecoDaBanana.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

public class BananasControllerTests
{
    private readonly BananasController _controller;
    private readonly Mock<IBananaRepository> _mockRepo;

    public BananasControllerTests()
    {
        _mockRepo = new Mock<IBananaRepository>();
        _controller = new BananasController(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithListOfBananas()
    {
        var bananas = new List<Banana>
        {
            new Banana { Id = "1", Nome = "Banana Prata", Preco = 2.5M, Origem = "Brasil" },
            new Banana { Id = "2", Nome = "Banana Nanica", Preco = 3.0M, Origem = "Equador" }
        };
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(bananas);

        var result = await _controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnBananas = Assert.IsType<List<Banana>>(okResult.Value);
        Assert.Equal(2, returnBananas.Count);
    }

    [Fact]
    public async Task GetById_ExistingId_ReturnsOkResult_WithBanana()
    {
        var banana = new Banana { Id = "1", Nome = "Banana Prata", Preco = 2.5M, Origem = "Brasil" };
        _mockRepo.Setup(repo => repo.GetByIdAsync("1")).ReturnsAsync(banana);

        var result = await _controller.GetById("1");

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnBanana = Assert.IsType<Banana>(okResult.Value);
        Assert.Equal("Banana Prata", returnBanana.Nome);
    }

    [Fact]
    public async Task Create_ValidBanana_ReturnsCreatedResult()
    {
        var banana = new Banana { Id = "3", Nome = "Banana D'água", Preco = 2.8M, Origem = "Brasil" };
        _mockRepo.Setup(repo => repo.CreateAsync(banana)).Returns(Task.CompletedTask);

        var result = await _controller.Create(banana);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal("GetById", createdResult.ActionName);
        Assert.Equal("3", createdResult.RouteValues["id"]);
    }

    [Fact]
public async Task Update_ExistingId_ReturnsNoContent()
{
    var existingBanana = new Banana { Id = "1", Nome = "Banana Prata", Preco = 2.5M, Origem = "Brasil" };
    var updatedBanana = new Banana { Id = "1", Nome = "Banana Prata Atualizada", Preco = 3.0M, Origem = "Brasil" };

    _mockRepo.Setup(repo => repo.GetByIdAsync(existingBanana.Id)).ReturnsAsync(existingBanana);
    _mockRepo.Setup(repo => repo.UpdateAsync(existingBanana.Id, updatedBanana)).Returns(Task.CompletedTask);

    var result = await _controller.Update(existingBanana.Id, updatedBanana);

    var noContentResult = Assert.IsType<NoContentResult>(result);
}

[Fact]
public async Task Delete_ExistingId_ReturnsNoContent()
{
    var existingBanana = new Banana { Id = "1", Nome = "Banana Prata", Preco = 2.5M, Origem = "Brasil" };

    _mockRepo.Setup(repo => repo.GetByIdAsync(existingBanana.Id)).ReturnsAsync(existingBanana);
    _mockRepo.Setup(repo => repo.DeleteAsync(existingBanana.Id)).Returns(Task.CompletedTask);

    var result = await _controller.Delete(existingBanana.Id);

    var noContentResult = Assert.IsType<NoContentResult>(result);
}

    [Fact]
    public async Task Delete_NonExistingId_ReturnsNotFound()
    {
        _mockRepo.Setup(repo => repo.DeleteAsync("999")).ThrowsAsync(new KeyNotFoundException());

        var result = await _controller.Delete("999");

        Assert.IsType<NotFoundResult>(result);
    }
}
