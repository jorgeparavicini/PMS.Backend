using PMS.Backend.Test.Fixtures;

namespace PMS.Backend.Test.Collections;

[CollectionDefinition(Name)]
public class PMSCollection : ICollectionFixture<PMSServerFixture>
{
    public const string Name = "PMS Collection";
}
