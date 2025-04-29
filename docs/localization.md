# Localization Service

The `Paradigm.WindowsAppSDK.Services.Localization` package provides an advanced automated localization system for Windows App SDK applications. Unlike standard localization approaches, this service offers automatic extraction and application of localizable content from and to your models.

## Installation

```powershell
Install-Package Paradigm.WindowsAppSDK.Services.Localization
```

## Key Components

### ILocalizationService

The core interface that defines methods for automatic localization extraction and application.

```csharp
public interface ILocalizationService : IService
{
    /// <summary>
    /// Extracts the translation strings.
    /// </summary>
    /// <typeparam name="TModel">The type of the layout.</typeparam>
    /// <param name="model">The layout.</param>
    /// <param name="prefix">The prefix.</param>
    /// <returns>A dictionary of key values.</returns>
    Dictionary<string, string?> ExtractLocalizableStrings<TModel>(TModel model, string prefix);

    /// <summary>
    /// Applies the translations.
    /// </summary>
    /// <typeparam name="TModel">The type of the layout.</typeparam>
    /// <param name="model">The layout.</param>
    /// <param name="strings">The strings.</param>
    /// <param name="prefix">The prefix.</param>
    /// <returns>The model with applied translations.</returns>
    TModel ApplyLocalizableStrings<TModel>(TModel model, Dictionary<string, string> strings, string prefix);
}
```

### LocalizationService

The implementation of `ILocalizationService` that handles the extraction and application of localizable strings from and to models.

### LocalizableAttribute

An attribute that marks properties as localizable, allowing the service to identify which properties should be processed.

```csharp
[AttributeUsage(AttributeTargets.Property)]
public class LocalizableAttribute : Attribute
{
}
```

## How It Works

The Paradigm.WindowsAppSDK localization service offers a unique approach to localization:

1. **Automatic Extraction**: It can traverse your object models (including nested properties, lists, and dictionaries) to extract all properties marked with the `[Localizable]` attribute
2. **Dictionary-based Storage**: The extracted strings are stored in a dictionary with hierarchical keys based on the property paths
3. **Automatic Application**: It can apply translations back to your models by matching the hierarchical keys

This approach makes it easy to:
- Extract all localizable content from your application in one go
- Store translations in any format (JSON, database, etc.)
- Apply translations to your models without manually writing property-by-property code

## Usage

### Service Registration

Register the localization service in your application's startup:

```csharp
services.AddSingleton<ILocalizationService, LocalizationService>();
```

### Marking Localizable Properties

Use the `LocalizableAttribute` to mark properties that should be included in localization:

```csharp
public class ProductViewModel
{
    public int Id { get; set; } // Not localizable
    
    [Localizable]
    public string Name { get; set; } = string.Empty; // Will be included in localization
    
    [Localizable]
    public string Description { get; set; } = string.Empty; // Will be included in localization
    
    public decimal Price { get; set; } // Not localizable
    
    public ProductCategory Category { get; set; } = new(); // Will be traversed to find localizable properties
}

public class ProductCategory
{
    [Localizable]
    public string CategoryName { get; set; } = string.Empty; // Will be included in localization
}
```

### Extracting Localizable Content

Extract all localizable strings from your models:

```csharp
// Get the localization service
var locService = ServiceLocator.Instance.GetRequiredService<ILocalizationService>();

// Model with localizable content
var product = new ProductViewModel
{
    Id = 1,
    Name = "Original Product Name",
    Description = "Original product description",
    Price = 99.99m,
    Category = new ProductCategory
    {
        CategoryName = "Original Category"
    }
};

// Extract localizable strings
var strings = locService.ExtractLocalizableStrings(product, "Product");

// Result:
// {
//   "Product.Name": "Original Product Name",
//   "Product.Description": "Original product description",
//   "Product.Category.CategoryName": "Original Category"
// }
```

### Applying Translations

Apply translated strings back to your models:

```csharp
// Translation dictionary
var translations = new Dictionary<string, string>
{
    ["Product.Name"] = "Nombre del Producto",
    ["Product.Description"] = "Descripción del producto",
    ["Product.Category.CategoryName"] = "Categoría"
};

// Apply translations to the model
var localizedProduct = locService.ApplyLocalizableStrings(product, translations, "Product");

// Result:
// localizedProduct.Name == "Nombre del Producto"
// localizedProduct.Description == "Descripción del producto"
// localizedProduct.Category.CategoryName == "Categoría"
```

## Advanced Usage

### Working with Lists

The service automatically handles lists and applies translations to each item:

```csharp
public class CatalogViewModel
{
    [Localizable]
    public string Title { get; set; } = string.Empty;
    
    public List<ProductViewModel> Products { get; set; } = new();
}

// Create a catalog with products
var catalog = new CatalogViewModel
{
    Title = "Original Catalog Title",
    Products = new List<ProductViewModel>
    {
        new() { 
            Id = 1, 
            Name = "Original Product 1", 
            Description = "Description 1" 
        },
        new() { 
            Id = 2, 
            Name = "Original Product 2", 
            Description = "Description 2" 
        }
    }
};

// Extract strings (keys will include array indices)
var strings = locService.ExtractLocalizableStrings(catalog, "Catalog");

// Result includes:
// "Catalog.Title": "Original Catalog Title"
// "Catalog.Products.0.Name": "Original Product 1"
// "Catalog.Products.0.Description": "Description 1"
// "Catalog.Products.1.Name": "Original Product 2"
// "Catalog.Products.1.Description": "Description 2"
```

### Working with Dictionaries

The service also supports dictionaries:

```csharp
public class LocalizationMap
{
    public Dictionary<string, LocalizedItem> Items { get; set; } = new();
}

public class LocalizedItem
{
    [Localizable]
    public string Value { get; set; } = string.Empty;
}

// Create a dictionary with localizable values
var map = new LocalizationMap
{
    Items = new Dictionary<string, LocalizedItem>
    {
        ["greeting"] = new() { Value = "Hello" },
        ["farewell"] = new() { Value = "Goodbye" }
    }
};

// Extract strings (keys will include dictionary keys)
var strings = locService.ExtractLocalizableStrings(map, "Map");

// Result:
// "Map.Items.greeting.Value": "Hello"
// "Map.Items.farewell.Value": "Goodbye"
```

## Integration with Translation Systems

### Exporting to JSON

Export localizable strings to JSON for translation:

```csharp
var strings = locService.ExtractLocalizableStrings(viewModel, "App");
var json = JsonSerializer.Serialize(strings, new JsonSerializerOptions { WriteIndented = true });
await File.WriteAllTextAsync("localizable.json", json);
```

### Importing from JSON

Import translated strings from JSON:

```csharp
var json = await File.ReadAllTextAsync("translations-es.json");
var translations = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
var localizedModel = locService.ApplyLocalizableStrings(viewModel, translations, "App");
```

### Runtime Language Switching

Implement runtime language switching by storing translations for multiple languages:

```csharp
// Store translations for each language
private Dictionary<string, Dictionary<string, string>> _translations = new();

// Load all translations
public async Task LoadTranslationsAsync()
{
    foreach (var language in new[] { "en", "es", "fr" })
    {
        var json = await File.ReadAllTextAsync($"translations-{language}.json");
        _translations[language] = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
    }
}

// Switch language
public void SwitchLanguage(string language)
{
    if (_translations.TryGetValue(language, out var translations))
    {
        var locService = ServiceLocator.Instance.GetRequiredService<ILocalizationService>();
        var localizedModel = locService.ApplyLocalizableStrings(_viewModel, translations, "App");
        
        // Update UI with localized model
        _viewModel = localizedModel;
        NotifyPropertyChanged(nameof(ViewModel));
    }
}
```

## Best Practices

### Attribute Usage

1. **Be selective**: Only mark properties that need localization with the `[Localizable]` attribute
2. **Include parent models**: The service will traverse object hierarchies, so you don't need to mark every container object

### Prefix Management

1. **Use consistent prefixes**: Choose a meaningful prefix that represents the context of your model
2. **Hierarchical prefixes**: Consider using hierarchical prefixes for complex applications (e.g., "App.MainPage", "App.SettingsPage")

### Performance Considerations

1. **Extract once**: Extract strings once and cache them rather than extracting repeatedly
2. **Apply strategically**: Apply translations at key points (application startup, language change) rather than continuously

### Integration with Other Services

The localization service works well with other services in the Paradigm.WindowsAppSDK framework:

1. **Configuration Service**: Store the selected language in configuration
2. **Local Settings Service**: Save the user's language preference
3. **File Storage Service**: Store translation files
4. **Telemetry Service**: Track language usage 