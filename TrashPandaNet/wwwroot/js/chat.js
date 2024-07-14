const timer = setInterval(() => Refresh(), 5000 );

function Refresh()
{
    const xhr = new XMLHttpRequest();
    const chatId = window.location.pathname.split("/")[2];
    const container = document.getElementsByClassName("chat-messages")[0];

    xhr.onload = function ()
    {
        container.innerHTML = xhr.response;
    }

    xhr.open("get", `/Chat/GetMessages/${chatId}`);
    xhr.send();
}