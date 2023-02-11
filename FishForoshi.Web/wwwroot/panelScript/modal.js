const createUniqId = () => {
    const myString = "1234567890qwertyuioplkjhgfdsazxcvbnm";
    let uniqueId = [];
    for (let i = 0; i < 30; i++) {
        uniqueId.push(myString[Math.floor(Math.random() * myString.length)]);
    }
    return uniqueId.join("");
}

const createModal = (title,body) => {
    let modal = document.getElementById("id01")

    document.getElementById('modal-title').innerHTML = title
    document.getElementById('modal-body').innerHTML = body

    modal.style.display = "block"
}

const closeModal = () => {
    let modal = document.getElementById("id01")

    document.getElementById('modal-title').innerHTML = ''
    document.getElementById('modal-body').innerHTML = ''

    modal.style.display = "none"
}