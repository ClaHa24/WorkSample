using CommunityToolkit.Mvvm.Input;
using System.Configuration;
using System.Windows;
using WorkSample.Client.Helpers;
using WorkSample.Client.Models;
using WorkSample.Client.ViewModels.Common;

namespace WorkSample.Client.ViewModels;

/// <summary>
///     View model for window <see cref="EditPerson"/>.
/// </summary>
public class EditPersonViewModel : BaseViewModel
{
    private readonly string path;

    /// <summary>
    /// Gets the <see cref="RelayCommand"/> responsible for canceling the operation.
    /// </summary>
    public RelayCommand<Window> CancelCommand { get; }

    /// <summary>
    /// Gets the <see cref="AsyncRelayCommand"/> responsible for creating/updating the person.
    /// </summary>
    public AsyncRelayCommand<Window> OkCommand { get; }

    /// <summary>
    ///     The person to be edited.
    /// </summary>
    public Person Person { get; set; } = new Person();

    /// <summary>
    ///     Initiates a new instance of <see cref="EditPersonViewModel"/>.
    /// </summary>
    /// <param name="id">ID of the person to edit.</param>
    public EditPersonViewModel(int? id)
    {
        path = $"{ConfigurationManager.AppSettings["Webservice"]}/Persons";

        CancelCommand = new RelayCommand<Window>(CancelOperation);
        OkCommand = new AsyncRelayCommand<Window>(SavePerson);

        if (!id.HasValue) return;
        Task.Run(async () =>
        {
            var url = $"{path}/{id.Value}";
            var person = await HttpHelper.GetAsync<Person>(url);
            if (person == null)
                ErrorMessage = "Cannot load person!";
            else
                Person = person;
        }).Wait();
    }

    /// <summary>
    ///     Cancels the operation and closes the window.
    /// </summary>
    /// <param name="window">The <see cref="Window"/> to be closed.</param>
    public void CancelOperation(Window? window)
    {
        window?.Close();
    }

    /// <summary>
    ///     Saves the currently displayed person.
    /// </summary>
    /// <param name="window">The <see cref="Window"/> to be closed after the operation succeeds.</param>
    public async Task SavePerson(Window? window)
    {
        try
        {
            if (Person.Id.HasValue)
            {
                await HttpHelper.PutAsync(path, Person);
                window?.Close();
            }
            else
            {
                await HttpHelper.PostAsync(path, Person);
                window?.Close();
            }
        }
        catch (Exception ex)
        {
            SetErrorMessage(ex);
        }
    }
}
