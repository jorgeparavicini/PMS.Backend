using PMS.Backend.Test.Fixtures;

namespace PMS.Backend.Test.Collections;

[CollectionDefinition(Name)]
public class PMSCollection : ICollectionFixture<PMSServerFixture>,
    ICollectionFixture<AuthenticationSettingsFixture>
{
    public const string Name = "PMS Collection";
}
