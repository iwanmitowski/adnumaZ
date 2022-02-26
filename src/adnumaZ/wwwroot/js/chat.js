"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;
function formatAMPM(date) {
  var hours = date.getHours();
  var minutes = date.getMinutes();
  var ampm = hours >= 12 ? "pm" : "am";
  hours = hours % 12;
  hours = hours ? hours : 12; // the hour '0' should be '12'
  minutes = minutes < 10 ? "0" + minutes : minutes;
  var strTime = hours + ":" + minutes + " " + ampm;
  return strTime;
}
connection.on("ReceiveMessage", function (user, message, avatarUrl) {
  var template = `
<div class="chat-message-${
    user === window.userData.username ? "right" : "left"
  } pb-4">
  <div>
    <img src="${avatarUrl}" class="rounded-circle mr-1" width="40" height="40">
    <div class="text-muted small text-nowrap mt-2">${formatAMPM(
      new Date()
    )}</div>
  </div>
  <div class="flex-shrink-1 bg-light rounded py-2 px-3 ml-3">
    <div class="font-weight-bold mb-1">${user}</div>
    ${message}
  </div>
</div>`;

  document.getElementById("messagesList").innerHTML += template;
});

connection
  .start()
  .then(function () {
    document.getElementById("sendButton").disabled = false;
  })
  .catch(function (err) {
    return console.error(err.toString());
  });

document
  .getElementById("sendButton")
  .addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value;
    connection
      .invoke(
        "SendMessage",
        window.userData.username,
        message,
        window.userData.avatarUrl
      )
      .catch(function (err) {
        return console.error(err.toString());
      });
    document.getElementById("messageInput").value = "";
    event.preventDefault();
  });
