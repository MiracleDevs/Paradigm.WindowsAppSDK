# XAML

The `Paradigm.WindowsAppSDK.Xaml` package provides a collection of XAML-related utilities, converters, and controls to enhance the development of Windows App SDK applications.

## Installation

```powershell
Install-Package Paradigm.WindowsAppSDK.Xaml
```

## Key Components

### Converters

The package includes a comprehensive set of value converters that facilitate data binding in XAML applications:

| Converter | Description |
|-----------|-------------|
| `BoolToColorConverter` | Converts a boolean value to a Color object |
| `BoolToParameterConverter` | Converts a boolean value to a specified parameter value |
| `BoolToScrollModeConverter` | Converts a boolean value to a ScrollMode enumeration value |
| `BoolToVisibilityConverter` | Converts a boolean value to a Visibility enumeration value |
| `BoolToZoomModeConverter` | Converts a boolean value to a ZoomMode enumeration value |
| `ColorToSolidBrushConverter` | Converts a Color to a SolidColorBrush |
| `DateTimeOffsetToStringConverter` | Converts a DateTimeOffset to a formatted string |
| `DateTimeToStringConverter` | Converts a DateTime to a formatted string |
| `EmptyStringConverter` | Checks if a string is empty or null |
| `EnumerableToBoolConverter` | Converts an enumerable collection to a boolean based on whether it has items |
| `EnumerableToVisibilityConverter` | Converts an enumerable collection to Visibility based on whether it has items |
| `NegatedBoolConverter` | Negates a boolean value |
| `ObjectToBoolConverter` | Converts an object to a boolean based on whether it's null |
| `ObjectToVisibilityConverter` | Converts an object to Visibility based on whether it's null |
| `StringToBoolConverter` | Converts a string to a boolean based on whether it's empty |
| `StringToVisibilityConverter` | Converts a string to Visibility based on whether it's empty |
| `UriToImageSourceConverter` | Converts a URI to an ImageSource |
| `UriToMediaSourceConverter` | Converts a URI to a MediaSource |

#### Usage Example

In XAML:
```xml
<Page
    xmlns:converters="using:Paradigm.WindowsAppSDK.Xaml.Converters">
    
    <Page.Resources>
        <converters:BoolToVisibilityConverter x:Key="BoolToVisibilityConverter" />
        <converters:StringToVisibilityConverter x:Key="StringToVisibilityConverter" />
    </Page.Resources>
    
    <StackPanel>
        <TextBlock 
            Text="This is visible when IsEnabled is true" 
            Visibility="{x:Bind ViewModel.IsEnabled, Mode=OneWay, Converter={StaticResource BoolToVisibilityConverter}}" />
        
        <TextBlock 
            Text="This is visible when ErrorMessage is not empty" 
            Visibility="{x:Bind ViewModel.ErrorMessage, Mode=OneWay, Converter={StaticResource StringToVisibilityConverter}}" />
    </StackPanel>
</Page>
```

### User Controls

#### NavigationRootFrame

A specialized frame control designed for navigation in Windows App SDK applications. It extends the standard Frame control with additional functionality:

- Automatic cleanup of navigated pages
- Integration with the navigation service
- Support for navigation transitions

```csharp
// In your MainWindow.xaml
<local:NavigationRootFrame x:Name="ContentFrame" />

// In code-behind
var navigationService = App.Current.Services.GetRequiredService<INavigationService>();
navigationService.Initialize(ContentFrame);
```

#### DisposableFrame

A Frame control that properly disposes of page content when navigating to ensure resources are released.

### Extensions

The package includes extension methods that enhance the functionality of XAML-related types:

#### WindowExtensions

Extension methods for Window management, including:

- Center window on screen
- Set window size and position
- Show dialog windows

```csharp
// Center the window on screen
myWindow.CenterOnScreen();

// Set the window size
myWindow.SetWindowSize(800, 600);
```

## Advanced Usage

### Combining Converters

You can combine converters using the `ConverterChain` approach or by creating custom converters that compose existing ones.

### Custom Converters

You can create custom converters by inheriting from existing converters or implementing the `IValueConverter` interface:

```csharp
public class MyCustomConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        // Conversion logic
        return convertedValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        // Convert back logic
        return originalValue;
    }
}
``` 