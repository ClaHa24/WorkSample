using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows;
using WorkSample.Client.Helpers;
using WorkSample.Client.Models;
using WorkSample.Client.ViewModels.Common;

namespace WorkSample.Client.ViewModels;

public class PersonViewModel : BaseViewModel
{
    private readonly string path;
    private List<Person> _persons = [];
    private Person? _selectedPerson;

    /// <summary>
    /// Gets the <see cref="AsyncRelayCommand"/> responsible for reloading the persons.
    /// </summary>
    public AsyncRelayCommand ReloadCommand { get; }

    /// <summary>
    /// Gets the <see cref="AsyncRelayCommand"/> responsible for creating a person.
    /// </summary>
    public AsyncRelayCommand CreateCommand { get; }

    /// <summary>
    /// Gets the <see cref="AsyncRelayCommand"/> responsible for updating a person.
    /// </summary>
    public AsyncRelayCommand UpdateCommand { get; }

    /// <summary>
    /// Gets the <see cref="AsyncRelayCommand"/> responsible for deleting a person.
    /// </summary>
    public AsyncRelayCommand DeleteCommand { get; }

    /// <summary>
    ///     The persons to display.
    /// </summary>
    public ObservableCollection<Person> Persons { get; set; } = [];

    /// <summary>
    ///     The currently selected person.
    /// </summary>
    public Person? SelectedPerson
    {
        get => _selectedPerson;
        set
        {
            if (_selectedPerson != value)
            {
                _selectedPerson = value;
                OnPropertyChanged();
                ClearErrorMessage();
                DeleteCommand.NotifyCanExecuteChanged();
                UpdateCommand.NotifyCanExecuteChanged();
            }
        }
    }

    /// <summary>
    ///     Initiates a new instance of <see cref="PersonViewModel"/>.
    /// </summary>
    public PersonViewModel()
    {
        path = $"{ConfigurationManager.AppSettings["Webservice"]}/Persons";

        ReloadCommand = new AsyncRelayCommand(ReloadPersonsAsync);
        CreateCommand = new AsyncRelayCommand(CreatePersonAsync);
        UpdateCommand = new AsyncRelayCommand(UpdatePersonAsync, UpdateDeleteCanExecute);
        DeleteCommand = new AsyncRelayCommand(DeleteSelectedPersonAsync, UpdateDeleteCanExecute);

        Task.Run(async () =>
        {
            await ReloadPersonsAsync();
        }).Wait();
    }

    /// <summary>
    ///     Reloads the persons to display.
    /// </summary>
    public async Task ReloadPersonsAsync()
    {
        ClearErrorMessage();

        try
        {
            var loadedPersons = await HttpHelper.GetAsync<List<Person>>(path);

            if (loadedPersons == null)
            {
                ErrorMessage = "Could not load persons";
                return;
            }

            Persons.Clear();
            foreach (var person in loadedPersons)
            {
                Persons.Add(person);
            }
        }
        catch (Exception ex)
        {
            SetErrorMessage(ex);
        }
    }

    /// <summary>
    ///     Checks if update or delete can be executed.
    /// </summary>
    /// <returns>True if execution is possible, false otherwise.</returns>
    public bool UpdateDeleteCanExecute()
    {
        return SelectedPerson != null;
    }

    /// <summary>
    ///     Deletes the selected person.
    /// </summary>
    /// <returns></returns>
    public async Task DeleteSelectedPersonAsync()
    {
        ClearErrorMessage();

        if (SelectedPerson == null || !SelectedPerson.Id.HasValue)
        {
            ErrorMessage = "You need to select an entry";
            return;
        }

        var result = MessageBox.Show(
            "Do you really want to delete the selected entry?",
            "Do you really want to delete?",
            MessageBoxButton.YesNoCancel,
            MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            try
            {
                await HttpHelper.DeleteAsync($"{path}/{SelectedPerson.Id.Value}");
                await ReloadPersonsAsync();
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex);
            }
        }
    }

    /// <summary>
    ///     Shows dialog to create a person and reloads all persons afterwards.
    /// </summary>
    public async Task CreatePersonAsync()
    {
        ClearErrorMessage();

        var editPersonWindow = new EditPerson(null);
        editPersonWindow.ShowDialog();
        await ReloadPersonsAsync();
    }

    /// <summary>
    ///     Shows dialog to update a person and reloads all persons afterwards.
    /// </summary>
    public async Task UpdatePersonAsync()
    {
        ClearErrorMessage();

        if (SelectedPerson == null || !SelectedPerson.Id.HasValue)
        {
            ErrorMessage = "You need to select an entry";
            return;
        }

        var editPersonWindow = new EditPerson(SelectedPerson.Id);
        editPersonWindow.ShowDialog();
        await ReloadPersonsAsync();
    }
}