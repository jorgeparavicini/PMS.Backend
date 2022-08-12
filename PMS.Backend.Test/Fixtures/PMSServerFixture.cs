using Microsoft.AspNetCore.Mvc.Testing;

namespace PMS.Backend.Test.Fixtures;

public class PMSServerFixture: WebApplicationFactory<Program>
{
    public HttpClient Client { get; }

    public PMSServerFixture()
    {
        Client = CreateClient();
    }
}
