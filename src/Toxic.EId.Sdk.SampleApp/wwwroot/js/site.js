// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.querySelector("#authenticate").addEventListener("click", async () => {
  const response = await fetch("/auth/create", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      "userVisibleData": btoa(document.querySelector("#userVisibleData").value),
      "userNonVisibleData": btoa(document.querySelector("#userNonVisibleData").value),
      "userVisibleDataFormat": document.querySelector("#userVisibleDataFormat").value,
      "gui": document.querySelector("#gui").checked,
      "qr": document.querySelector("#qr").checked,
      "mobileBankId": document.querySelector("#mobileBankId").checked,
      "desktopBankId": document.querySelector("#desktopBankId").checked,
      "thisDevice": document.querySelector("#thisDevice").checked,
      "allowFingerPrintAuth": document.querySelector("#allowFingerPrintAuth").checked,
      "allowFingerPrintSign": document.querySelector("#allowFingerPrintSign").checked,
      "callbackUrl": document.querySelector("#callbackUrl").value,
      "returnUrl": document.querySelector("#returnUrl").value
    })
  });

  const data = await response.json();

  if (data.redirectUrl) {
    window.location = data.redirectUrl;
  } else if (data.qrCode) {
    document.querySelector("#qrImg").src = `data:image/svg+xml;base64,${data.qrCode}`;
    poll(data.sessionId);
  }
});

const poll = async (sessionId) => {
  const response = await fetch("/auth/poll", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      "sessionId": sessionId
    })
  });

  const data = await response.json();

  if (data.userAttributes) {
    document.querySelector("#qrImg").style.display = "none";
    document.querySelector("#result").innerText = JSON.stringify(data.userAttributes, null, 2);
    document.querySelector("#result").style.display = "block";
  } else if (data.isPending && data.qrCode) {
    document.querySelector("#result").style.display = "none";
    document.querySelector("#result").innerText = "";
    document.querySelector("#qrImg").src = `data:image/svg+xml;base64,${data.qrCode}`;
    document.querySelector("#qrImg").style.display = "block";
    setTimeout(async () => await poll(sessionId), 2000);
  } else {
    document.querySelector("#qrImg").style.display = "none";
    document.querySelector("#result").style.display = "block";
    document.querySelector("#result").innerText = `Error: ${data.hintCode}`;
  }
};
