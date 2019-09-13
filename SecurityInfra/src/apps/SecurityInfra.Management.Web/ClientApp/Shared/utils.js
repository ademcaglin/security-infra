import axios from 'axios';

function getCurrentPage() {
    var url = window.location.pathname;
    var page = url.substring(url.lastIndexOf('/') + 1);
    return page;
}

export const HTTP = axios.create({
    baseURL: `/`,
    headers: {
        RequestVerificationToken: document.getElementById("RequestVerificationToken").value,
        CurrentPage: getCurrentPage()
    }
})