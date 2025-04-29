# Dialog Service

The `Paradigm.WindowsAppSDK.Services.Dialog` package provides a framework for creating and managing dialogs in Windows App SDK applications.

## Installation

```powershell
Install-Package Paradigm.WindowsAppSDK.Services.Dialog
```

## Key Components

### IDialogService

The core interface that defines methods for registering and opening dialogs.

```csharp
public interface IDialogService : IService
{
    /// <summary>
    /// Registers a dialog element and its paired view.
    /// </summary>
    void Register<TDialogView, TDialog>()
        where TDialogView : IDialogView
        where TDialog : IDialog;

    /// <summary>
    /// Creates an instance of the IDialog element and opens its paired IDialogView.
    /// </summary>
    /// <returns>
    /// - true if the dialog prompt is confirmed.
    /// - false if the dialog prompt is denied.
    /// - null if the dialog is cancelled.
    /// </returns>
    Task<bool?> OpenAsync<TDialog>(object? arguments) where TDialog : IDialog;

    /// <summary>
    /// Opens the IDialog element paired IDialogView.
    /// </summary>
    /// <returns>
    /// - true if the dialog prompt is confirmed.
    /// - false if the dialog prompt is denied.
    /// - null if the dialog is cancelled.
    /// </returns>
    Task<bool?> OpenAsync<TDialog>(TDialog dialog) where TDialog : IDialog;
}
```

### DialogService

The implementation of `IDialogService` that handles dialog registration and presentation.

### IDialog

Interface that represents a dialog's view model. It's responsible for the dialog's business logic and data.

```csharp
public interface IDialog
{
    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    string Title { get; set; }

    /// <summary>
    /// Initializes this instance with the provided arguments.
    /// </summary>
    void Initialize(object? arguments);
}
```

### IDialogView

Interface that represents a dialog's view. It's responsible for presenting the dialog to the user.

```csharp
public interface IDialogView
{
    /// <summary>
    /// Gets or sets the dialog.
    /// </summary>
    IDialog? Dialog { get; set; }

    /// <summary>
    /// Opens this instance.
    /// </summary>
    /// <returns>
    /// - true if the dialog prompt is confirmed.
    /// - false if the dialog prompt is denied.
    /// - null if the dialog is cancelled.
    /// </returns>
    Task<bool?> OpenAsync();
    
    /// <summary>
    /// Hides this instance.
    /// </summary>
    void Hide();
}
```

## Usage

### Service Registration

Register the dialog service in your application's startup:

```csharp
services.AddSingleton<IDialogService, DialogService>();
```

### Dialog Registration

Register dialogs and their views in your application's startup:

```csharp
private static void RegisterDialogs()
{
    var dialogService = ServiceLocator.Instance.GetRequiredService<IDialogService>();
    dialogService.Register<ConfirmationDialog, ConfirmationDialogViewModel>();
    dialogService.Register<InputDialog, InputDialogViewModel>();
}
```

### Create a Dialog View Model

Create a view model for your dialog by implementing the `IDialog` interface:

```csharp
public class ConfirmationDialogViewModel : ViewModelBase, IDialog
{
    private string _title;
    private string _message;
    
    public string Title
    {
        get => _title;
        set => SetPropertyField(ref _title, value);
    }
    
    public string Message
    {
        get => _message;
        set => SetPropertyField(ref _message, value);
    }
    
    public ConfirmationDialogViewModel(IServiceProvider serviceProvider) 
        : base(serviceProvider)
    {
    }
    
    public void Initialize(object? arguments)
    {
        if (arguments is string message)
        {
            Message = message;
            Title = "Confirmation";
        }
    }
}
```

### Create a Dialog View

Create a view for your dialog by implementing the `IDialogView` interface:

```xaml
<!-- ConfirmationDialog.xaml -->
<ContentDialog
    x:Class="MyApp.Views.Dialogs.ConfirmationDialog"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    Title="{x:Bind ViewModel.Title, Mode=OneWay}"
    PrimaryButtonText="Yes"
    SecondaryButtonText="No"
    CloseButtonText="Cancel">
    
    <TextBlock Text="{x:Bind ViewModel.Message, Mode=OneWay}" TextWrapping="Wrap" />
</ContentDialog>
```

```csharp
// ConfirmationDialog.xaml.cs
public sealed partial class ConfirmationDialog : ContentDialog, IDialogView
{
    public IDialog? Dialog
    {
        get => DataContext as IDialog;
        set => DataContext = value;
    }
    
    public ConfirmationDialog()
    {
        this.InitializeComponent();
    }
    
    public async Task<bool?> OpenAsync()
    {
        var result = await this.ShowAsync();
        
        return result switch
        {
            ContentDialogResult.Primary => true,
            ContentDialogResult.Secondary => false,
            _ => null
        };
    }
    
    public void Hide()
    {
        this.Hide();
    }
}
```

### Using the Dialog Service

Open a dialog using the dialog service:

```csharp
// Open a dialog with arguments
var dialogService = ServiceLocator.Instance.GetRequiredService<IDialogService>();
var result = await dialogService.OpenAsync<ConfirmationDialogViewModel>("Do you want to save changes?");

if (result == true)
{
    // User clicked "Yes"
    await SaveChangesAsync();
}
else if (result == false)
{
    // User clicked "No"
    DiscardChanges();
}
else
{
    // User cancelled the dialog
}

// Or create and initialize a dialog manually
var inputDialog = new InputDialogViewModel(ServiceProvider);
inputDialog.Title = "Enter Name";
inputDialog.Placeholder = "Your name";

var result = await dialogService.OpenAsync(inputDialog);
if (result == true)
{
    string name = inputDialog.Value;
    // Use the input value
}
``` 