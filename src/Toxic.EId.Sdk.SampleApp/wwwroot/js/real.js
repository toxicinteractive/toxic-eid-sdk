/**
 * This is an example of a more realistic authentication flow in line with the official BankID recommendations: 
 * https://developers.bankid.com/ui-resources/ui-desktop
 * 
 * Note that in this example the backend returns all the user's data upon successful authentication, unfiltered.
 * In real production scenarios this information should stay on the backend and it should issue a cookie or token instead.
 */

const switchPane = (num) => {
  for (let i = 1; i <= 5; i++) {
    document.querySelector(`#pane${i}`).style.display = (i == num ? "block" : "none");
  }
};

const request = async (url, body, parseJson) => {
  const res = await fetch(url, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(body)
  });

  return parseJson ? await res.json() : res;
};

const qr = document.querySelector("#qr");
const root = document.querySelector("#root");
let shouldCancel = false;

/**
 * Button that starts the authentication flow with a QR code as the default method.
 */
document.querySelector("#bankid").addEventListener("click", async (e) => {
  e.preventDefault();
  switchPane(2);

  shouldCancel = false;
  delete root.dataset.sessionId;
  delete root.dataset.autoStartToken;
  document.querySelector("#username").innerText = "";

  // create session
  const res = await request("/auth/create", {
    gui: false,
    qr: true,
    mobileBankId: true,
    allowFingerprintAuth: true
  }, true);

  if (res.status && res.status !== 200) {
    return;
  }

  qr.src = `data:image/svg+xml;base64,${res.qrCode}`;
  root.dataset.sessionId = res.sessionId;
  root.dataset.autoStartToken = res.autoStartToken;

  // start polling for status
  poll(res.sessionId);
});

/**
 * Button to start the authentication flow directly in the user's device.
 */
document.querySelector("#auth-same").addEventListener("click", async (e) => {
  e.preventDefault();
  switchPane(3);

  // open the BankID app
  window.location = `https://app.bankid.com/?autostarttoken=${root.dataset.autoStartToken}`;

  // start polling for status
  poll(res.sessionId);
});

/**
 * Logic for all "cancel" buttons, deletes the current session.
 */
document.querySelectorAll(".reset").forEach(x => x.addEventListener("click", async (e) => {
  e.preventDefault();
  shouldCancel = true;

  request("/auth/logout", {
    sessionId: root.dataset.sessionId
  }, false);

  switchPane(1);
}));

/**
 * Polls the backend for the current authentication status.
 */
const poll = async (sessionId) => {
  const res = await request("/auth/poll", {
    sessionId: sessionId
  }, true);

  // check if authentication was successful
  if (res.isCompleted) {
    request("/auth/logout", {
      sessionId: sessionId
    }, false);

    // in this example the backend returns all user data
    // in prod applications you should keep this on the backend and just issue a cookie or jwt token there
    document.querySelector("#username").innerText = res.userAttributes.name;
    switchPane(5);
    return;
  }

  // we clicked cancel, don't do anything
  if (shouldCancel) {
    return;
  }

  // status is pending
  if (res.isPending) {
    qr.src = `data:image/svg+xml;base64,${res.qrCode}`;

    // user has opened the app
    if (res.hintCode == "userSign") {
      switchPane(3);
    }

    // restart polling
    setTimeout(() => poll(sessionId), 2000);
    return;
  }

  // if we got here we have an error
  request("/auth/logout", {
    sessionId: sessionId
  }, false);

  switchPane(4);
};
