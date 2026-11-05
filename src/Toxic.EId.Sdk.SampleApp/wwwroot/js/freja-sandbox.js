/**
 * This is a sandbox sample with all available parameters for the Freja eID API.
 * Check out the "realistic" sample for a more reasonable experience.
 */

document.querySelector("#authenticate").addEventListener("click", async () => {
  document.querySelector("#qrImg").style.display = "none";
  document.querySelector("#result").style.display = "none";

  const response = await fetch("/auth/freja/create", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      "isSigning": document.querySelector("#userVisibleData").value?.length > 0,
      "userInfoType": document.querySelector("#userInfoType").value,
      "minRegistrationLevel": document.querySelector("#minRegistrationLevel").value,
      "personalNumber": document.querySelector("#personalNumber").value,
      "title": document.querySelector("#title").value,
      "userInfo": document.querySelector("#userInfo").value,
      "countryCode": document.querySelector("#countryCode").value,
      "userVisibleData": document.querySelector("#userVisibleData").value,
      "userNonVisibleData": document.querySelector("#userNonVisibleData").value,
      "gui": document.querySelector("#gui").checked,
      "qr": document.querySelector("#qr").checked,
      "thisDevice": document.querySelector("#thisDevice").checked,
      "otherDevice": document.querySelector("#otherDevice").checked,
      "deviceChoice": document.querySelector("#deviceChoice").checked,
      "enableAutoLaunch": document.querySelector("#enableAutoLaunch").checked,
      "callbackUrl": document.querySelector("#callbackUrl").value,
      "customerUrl": document.querySelector("#customerUrl").value
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
  } else if (data.refToken && document.querySelector("#thisDevice").checked) {
    const ref = encodeURI(data.refToken);
    const cb = encodeURI("https://localhost:7271");
    window.location = `https://app.prod.frejaeid.com/freja?action=bindUserToTransaction&transactionReference=${ref}&originAppScheme=${cb}`;
  } else {
    document.querySelector("#result").innerText = "Unknown error, check console.";
    document.querySelector("#result").style.display = "block";
  }
});

const poll = async (sessionId) => {
  const response = await fetch("/auth/freja/poll", {
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
  } else if (data.isPending) {
    document.querySelector("#result").style.display = "block";
    document.querySelector("#result").innerText = `${data.status}: ${data.statusMessage}`;

    // for freja the qr code is only sent in the initial GetSession response so don't clear it
    if (data.qrCode) {
      document.querySelector("#qrImg").src = `data:image/svg+xml;base64,${data.qrCode}`;
      document.querySelector("#qrImg").style.display = "block";
    }

    setTimeout(async () => await poll(sessionId), 2000);
  } else {
    document.querySelector("#qrImg").style.display = "none";
    document.querySelector("#result").innerText = `Error: ${data.status} - ${data.statusMessage}`;
    document.querySelector("#result").style.display = "block";
  }
};
