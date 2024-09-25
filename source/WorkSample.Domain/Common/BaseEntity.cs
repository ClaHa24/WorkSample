namespace WorkSample.Domain.Common;

/// <summary>
///     Base class for entities.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    ///     ID of the entity. Needs to be unique.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Creation date of the entity.
    /// </summary>
    public DateTime? DateCreated { get; set; }

    /// <summary>
    ///     Last modification date of the entity.
    /// </summary>
    public DateTime? DateModified { get; set; }
}
