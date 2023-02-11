const showToast = (message) => {
    let x = document.getElementById("snackbar");
    x.innerHTML = message;
    x.classList.add("snackbar-show")
    setTimeout(() => { x.classList.remove("snackbar-show") }, 3000);
}

const showLoading = () => {
    let ulLoaderul = document.getElementById('ulLoaderul');
    ulLoaderul.style.display = 'flex';
}

const hideLoading = () => {
    let ulLoaderul = document.getElementById('ulLoaderul');
    ulLoaderul.style.display = 'none';
}


// Api Call Configs

const getHtmlApiCallOptions = {
    method: 'GET',
    headers: {
        'content-type': 'text/html'
    }
}

const postJsonApiCallOptions = (body) => ({
    method: 'POST',
    headers: {
        'content-type': 'application/json'
    },
    body: JSON.stringify(body)
})

const deleteJsonApiCallOptions = {
    method: 'DELETE',
    headers: {
        'content-type': 'application/json'
    },
}

