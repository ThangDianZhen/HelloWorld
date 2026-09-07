namespace Ignite.Application.Exceptions;

public class RepositoryException : Exception
{
    public RepositoryException(string message) : base(message)
    {
    }

    public RepositoryException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

public class AuthenticationException : RepositoryException
{
    public AuthenticationException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

public class AuthorizationException : RepositoryException
{
    public AuthorizationException(string message)
        : base(message)
    {
    }

    public AuthorizationException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
