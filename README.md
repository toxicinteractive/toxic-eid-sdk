# Toxic.EId.Sdk
An eID (e-identitet.se) API implementation.

## How to use
1. Install the nuget `Toxic.EId.Sdk`
2. Specify required options and add the client services:
```csharp
builder.Services.Configure<EIdOptions>(opts =>
{
    // get these from somewhere safe
    opts.ApiKey = "abc123";
    opts.ServiceKey = "abc123";
});

builder.Services.AddBankIdClient();
```
3. Inject and use the client wherever you want
4. Refer to the documentation: https://docs.grandid.com

## Examples
Check out the "Sandbox" and "Real example" examples in the sample app provided here for implementation details.
