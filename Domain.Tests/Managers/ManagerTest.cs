

using Domain.Tests.Users;

namespace Domain.Tests.Managers;

public class ManagerTest
{
    private readonly CreateUserFactory _createUserFactory;

    public ManagerTest(CreateUserFactory createUserFactory)
    {
        _createUserFactory = createUserFactory;
    }
}
