using CqgWebApi;
using CqgWebApi.DiagnosticSessionToken;
using FluentAssertions;
using Google.Protobuf;
using Xunit;

namespace Example.UnitTests;

public class ServerMsgTests
{
    [Fact]
    public void GivenServerMsgWithDiagnosticSessionToken_WhenSerializedAndDeserialized_ShouldContainToken()
    {
        // Arrange
        var expectedToken = "test_token";
        var serverMsg = new ServerMsg();
        serverMsg.SetExtension(DiagnosticSessionToken2Extensions.DiagnosticSessionToken, expectedToken);

        // Serialize
        byte[] serializedData;
        using (var memoryStream = new MemoryStream())
        {
            serverMsg.WriteTo(memoryStream);
            serializedData = memoryStream.ToArray();
        }

        // Act
        var deserializedServerMsg = ServerMsg.Parser.ParseFrom(serializedData);
        var actualToken = deserializedServerMsg.GetExtension(DiagnosticSessionToken2Extensions.DiagnosticSessionToken);

        // Assert
        actualToken.Should().Be(expectedToken);
    }
}
