const addItemToTable = (bodyId, item) => {
    let tableBody = document.getElementById(bodyId)
    tableBody.innerHTML += item
}

const removeFromTable = (itemId) => {
    let tr = document.getElementById(itemId)
    if (tr != null) {
        tr.remove()
    }
}