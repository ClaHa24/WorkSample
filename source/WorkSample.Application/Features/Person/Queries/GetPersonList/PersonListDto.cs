namespace WorkSample.Application.Features.Person.Queries.GetPersonList
{
    /// <summary>
    ///     Data transfer object for list of persons.
    /// </summary>
    public class PersonListDto
    {
        /// <summary>
        ///     ID of the person.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Name of the person.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        ///     Surname of the person.
        /// </summary>
        public string Surname { get; set; } = string.Empty;
    }
}
