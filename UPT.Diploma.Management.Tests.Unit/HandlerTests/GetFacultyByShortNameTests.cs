using System.Linq.Expressions;
using AutoMapper;
using Moq;
using UPT.Diploma.Management.Application.Exceptions;
using UPT.Diploma.Management.Application.Queries.GetFacultyByShortNameQuery;
using UPT.Diploma.Management.Application.ViewModels;
using UPT.Diploma.Management.Domain.Models;
using UPT.Diploma.Management.Persistence.Contracts;

namespace UPT.Diploma.Management.Tests.Unit.HandlerTests;

public class GetFacultyByShortNameTests
{
    private Mock<IBaseRepository<Faculty>> _repositoryMock;
    private Mock<IMapper> _mapperMock;

    public GetFacultyByShortNameTests()
    {
        _repositoryMock = new Mock<IBaseRepository<Faculty>>();
        _mapperMock = new Mock<IMapper>();
    }
    
    [Fact]
    public async Task GetFails_WhenShortNameIsNull()
    {
        var cmd = new GetFacultyByShortNameQuery() { ShortName = null };

        var handler = GetHandler();

        var exception = await Assert.ThrowsAsync<ValidationException>(async () => await handler.Handle(cmd, CancellationToken.None));

        Assert.Equal("Numele facultății nu poate fi null!", exception.ValidationErrors[0]);
    }
    
    [Fact]
    public async Task GetFails_WhenShortNameIsEmpty()
    {
        var cmd = new GetFacultyByShortNameQuery() { ShortName = "" };

        var handler = GetHandler();

        var exception = await Assert.ThrowsAsync<ValidationException>(async () => await handler.Handle(cmd, CancellationToken.None));

        Assert.Equal("Numele facultății nu poate fi gol!", exception.ValidationErrors[0]);
    }
    
    [Fact]
    public async Task GetFails_WhenShortNameIsLonger()
    {
        var cmd = new GetFacultyByShortNameQuery() { ShortName = "long12" };

        var handler = GetHandler();

        var exception = await Assert.ThrowsAsync<ValidationException>(async () => await handler.Handle(cmd, CancellationToken.None));

        Assert.Equal("Numele facultății nu poate depăși 5 litere!", exception.ValidationErrors[0]);
    }
    
    [Fact]
    public async Task GetFails_WhenFacultyIsNull()
    {
        var cmd = new GetFacultyByShortNameQuery() { ShortName = "test" };

        _repositoryMock.Setup(p => p.GetAsync(It.IsAny<Expression<Func<Faculty, bool>>>()))
            .ReturnsAsync(new List<Faculty>());
        
        var handler = GetHandler();

        var exception = await Assert.ThrowsAsync<ValidationException>(async () => await handler.Handle(cmd, CancellationToken.None));

        Assert.Equal("Prescurtarea facultății nu este validă!", exception.ValidationErrors[0]);
        
        _repositoryMock.Verify(p => p.GetAsync(It.IsAny<Expression<Func<Faculty, bool>>>()), Times.Once);
    }
    
    [Fact]
    public async Task GetFails_WhenFacultyWorks()
    {
        var cmd = new GetFacultyByShortNameQuery() { ShortName = "test" };
        var faculty = new Faculty()
            { Id = 1, Name = "Random faculty name", ShortName = "test", CreatedTimeUtc = DateTime.UtcNow };
        var facultyVm = new FacultyViewModel() { Id = 1, Name = "Random faculty name", ShortName = "test" };

        _repositoryMock.Setup(p => p.GetAsync(It.IsAny<Expression<Func<Faculty, bool>>>()))
            .ReturnsAsync(new List<Faculty>(){faculty});

        _mapperMock.Setup(p => p.Map<FacultyViewModel>(It.IsAny<Faculty>())).Returns(facultyVm);
        
        var handler = GetHandler();

        var result = await handler.Handle(cmd, CancellationToken.None);

        _repositoryMock.Verify(p => p.GetAsync(It.IsAny<Expression<Func<Faculty, bool>>>()), Times.Once);
        _mapperMock.Verify(p => p.Map<FacultyViewModel>(faculty), Times.Once);
        Assert.Equal(facultyVm, result.QueryPayload);
    }
    
    
    private GetFacultyByShortNameHandler GetHandler()
    {
        return new GetFacultyByShortNameHandler(
            _repositoryMock.Object,
            _mapperMock.Object
        );
    }
    
    
}