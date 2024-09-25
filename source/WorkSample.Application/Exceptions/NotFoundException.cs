namespace WorkSample.Application.Exceptions;

/// <summary>
///     Exception thrown if item was not found.
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    ///     Initiates a new instance of <see cref="NotFoundException"/>.
    /// </summary>
    /// <param name="type">Type of object that was not found.</param>
    /// <param name="key">Key of the object not found.</param>
    public NotFoundException(string type, object key) : base($"{type} ({key}) was not found")
    {

    }
}