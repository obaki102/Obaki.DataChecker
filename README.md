# Obaki.DataChecker
[![NuGet](https://img.shields.io/nuget/v/Obaki.DataChecker.svg)](https://www.nuget.org/packages/Obaki.DataChecker)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Obaki.DataChecker?logo=nuget)](https://www.nuget.org/packages/Obaki.DataChecker)

Obaki.DataChecker is a library that provides flexible validation of JSON and XML inputs through customizable rules, while allowing for custom validations using [FluentValidation](https://github.com/FluentValidation/FluentValidation).

## Installing

To install the package add the following line inside your csproj file with the latest version.

```
<PackageReference Include="Obaki.DataChecker" Version="x.x.x" />
```

An alternative is to install via the .NET CLI with the following command:

```
dotnet add package Obaki.DataChecker
```

For more information you can check the [nuget package](https://www.nuget.org/packages/Obaki.LocalStorageCache).

## Setup
Register the needed services in your Program.cs file as **Scoped**

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddDataCheckerAsScoped();
}
``` 

Or as **Singleton**

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddDataCheckerAsSingleton();
}
```
## Usage 
**Asynchronous(Non-Blocking)**
```c#
using FluentValidation.Results;
using FluentValidation;
using Obaki.DataChecker;


public class TestAsync {
    private readonly IXmlDataChecker<TestObject> _xmlDataChecker;

    public TestAsync(IXmlDataChecker<TestObject> xmlDataChecker) 
    {
          _xmlDataChecker = xmlDataChecker;
    }

   public async Task<ValidationResult> ValidateTestObject(string xmlInput)
   {

          return await _xmlDataChecker.ValidateXmlDataFromStringAsync(xmlInput);
   }
}
```
**Synchronous(Blocking)**
```c#
using FluentValidation.Results;
using FluentValidation;
using Obaki.DataChecker;


public class TestSync {
   private readonly IXmlDataChecker<TestObject> _xmlDataChecker;

    public TestAsync(IXmlDataChecker<TestObject> xmlDataChecker) 
    {
          _xmlDataChecker = xmlDataChecker;
    }

   public ValidationResult ValidateTestObject(string xmlInput)
   {

          return  _xmlDataChecker.ValidateXmlDataFromString(xmlInput);
   }
}
```
