using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorkSample.Client.ViewModels.Common;

/// <summary>
///     Base view model.
/// </summary>
public class BaseViewModel : INotifyPropertyChanged
{
    /// <summary>
    ///     Holds an error message, if there needs to be one displayed.
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;

    /// <summary>
    ///     A property has changed.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    ///     A property changed.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    ///     Sets the error message using the message from the exception.
    /// </summary>
    /// <param name="ex">The <see cref="Exception"/> to get the message from.</param>
    public void SetErrorMessage(Exception ex)
    {
        ErrorMessage = $"Something went wrong: {ex.Message}";
    }

    /// <summary>
    ///     Clears the error message.
    /// </summary>
    public void ClearErrorMessage()
    {
        ErrorMessage = string.Empty;
    }
}
