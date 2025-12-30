using VpsControlLayer.Application.Commands;
using Xunit;

namespace VpsControlTests.CliCommandTests;

public class InitCommandHandlerTests
{
    [Fact]
    public void Handle_ValidParameters_ExecutesWithoutException()
    {
        // Arrange
        var handler = new InitCommandHandler();
        
        // Act & Assert
        var exception = Record.Exception(() => 
            handler.Handle("192.168.1.1", "google.com", 443, "root"));
        
        Assert.Null(exception);
    }
    
    [Theory]
    [InlineData("192.168.1.1", "google.com", 443, "root")]
    [InlineData("10.0.0.1", "youtube.com", 8443, "admin")]
    [InlineData("172.16.0.1", "microsoft.com", 443, "user")]
    public void Handle_DifferentValidParameters_ExecutesSuccessfully(
        string server, string domain, int port, string user)
    {
        // Arrange
        var handler = new InitCommandHandler();
        
        // Act & Assert
        var exception = Record.Exception(() => 
            handler.Handle(server, domain, port, user));
        
        Assert.Null(exception);
    }
}