# Toxic.EId.Sdk
An eID (e-identitet.se) API implementation for BankID and Freja eID.

## How to use
1. Install the nuget `Toxic.EId.Sdk`
2. Specify required options and add the client services:

```csharp
builder.Services.Configure<EIdOptions>(opts =>
{
    // get these from somewhere safe (like a key vault) or via environment variables
    // for more options check the EIdOptions class
    opts.ApiKey = "abc123";
    opts.IsTest = true;
    opts.BankId.AuthServiceKey = "abc123";
    opts.BankId.SignServiceKey = "abc123";
    opts.FrejaEId.AuthServiceKey = "abc123";
    opts.FrejaEId.SignServiceKey = "abc123";
});

builder.Services.AddBankIdClient();
builder.Services.AddFrejaEIdClient();
```

3. Inject and use the `BankIdClient` or `FrejaEIdClient` wherever you want
4. Refer to the documentation: https://docs.grandid.com

## Debug logging
If the option `LogRequestData` is enabled the framework logs the raw responses from the eID API at the debug log level including any sensitive information such as API keys and personal numbers.

## Examples
Check out the "Sandbox" and "Real example" examples in the sample app provided here for implementation details.

## Detailed API information
More information about the API and all parameters can be found at https://docs.grandid.com/.
