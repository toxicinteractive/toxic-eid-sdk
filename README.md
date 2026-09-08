# Toxic.GrandId.Sdk
A GrandID (Svensk e-identitet) API implementation.

## How to use
1. Install the nuget `Toxic.GrandId.Sdk`
2. Specify required options and add the client services:
```csharp
builder.Services.Configure<GrandIdOptions>(opts =>
{
    // get these from somewhere safe
    opts.ApiKey = "abc123";
    opts.ServiceKey = "abc123";
});

builder.Services.AddBankIdClient();
```
3. Inject and use the client wherever you want
4. Refer to the documentation: https://docs.grandid.com

For a more high level experience you can use the `Toxic.BankId.Client.AspNet` nuget.
