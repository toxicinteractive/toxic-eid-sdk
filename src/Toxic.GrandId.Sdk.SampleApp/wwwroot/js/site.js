// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.querySelector("#authenticate").addEventListener("click", async () => {
  const deviceMode = document.querySelector("#sameDevice").checked ? "sameDevice" : "otherDevice";

  const response = await fetch("/auth/create", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      "userVisibleData": btoa(document.querySelector("#userVisibleData").value),
      "userNonVisibleData": btoa(document.querySelector("#userNonVisibleData").value),
      "userVisibleDataFormat": document.querySelector("#userVisibleDataFormat").value,
      "useGui": document.querySelector("#useGui").checked,
      "allowQr": document.querySelector("#allowQrCode").checked,
      "allowFingerPrintAuth": document.querySelector("#allowFingerPrintAuth").checked,
      "allowFingerPrintSign": document.querySelector("#allowFingerPrintSign").checked,
      "callbackUrl": document.querySelector("#callbackUrl").value,
      "returnUrl": document.querySelector("#returnUrl").value
    })
  });

  const data = await response.json();

  if (data.redirectUrl) {
    window.location = data.redirectUrl;
  }
});
