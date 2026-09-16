/**
 * This is a non-realistic sandbox sample with all available parameters.
 * Check out the "realistic" sample for a more reasonable experience.
 */

document.querySelector("#authenticate").addEventListener("click", async () => {
  document.querySelector("#qrImg").style.display = "none";
  document.querySelector("#result").style.display = "none";

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

  if (response.status !== 200) {
    const err = await response.json();
    document.querySelector("#result").innerText = `Error: ${err.title}`;
    document.querySelector("#result").style.display = "block";
    return;
  }

  const data = await response.json();

  if (data.redirectUrl) {
    window.location = data.redirectUrl;
  } else if (data.qrCode) {
    document.querySelector("#qrImg").src = `data:image/svg+xml;base64,${data.qrCode}`;
    document.querySelector("#qrImg").style.display = "block";
    poll(data.sessionId);
  } else if (data.autoStartToken) {
    window.location = `https://app.bankid.com/?autostarttoken=${data.autoStartToken}`;
  } else {
    document.querySelector("#result").innerText = "Unknown error, check console.";
    document.querySelector("#result").style.display = "block";
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
    document.querySelector("#result").innerText = `Error: ${data.hintCode}`;
    document.querySelector("#result").style.display = "block";
  }
};
