# Toxic.EId.Sdk
An eID (e-identitet.se) API implementation.

## How to use
1. Install the nuget `Toxic.EId.Sdk`
2. Specify required options and add the client services:

```csharp
builder.Services.Configure<EIdOptions>(opts =>
{
    // get these from somewhere safe (like a key vault) or via environment variables
    opts.ApiKey = "abc123";
    opts.BankId.AuthServiceKey = "abc123";
    opts.BankId.SignServiceKey = "abc123";
    opts.IsTest = true;
    opts.UseEuEndpoint = true;
});

builder.Services.AddBankIdClient();
```

3. Inject and use the `BankIdClient` wherever you want
4. Refer to the documentation: https://docs.grandid.com

## Debug logging
The framework logs the raw responses from the eID API at the debug log level so override this namespace if you don't want to expose this information in your logs.

## Examples
Check out the "Sandbox" and "Real example" examples in the sample app provided here for implementation details.
