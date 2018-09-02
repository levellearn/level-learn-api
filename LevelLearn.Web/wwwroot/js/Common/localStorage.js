function storageLogin(imagem, userName) {
    localStorage.removeItem("Imagem");
    localStorage.removeItem("UserName");
    localStorage.setItem("Imagem", imagem);
    localStorage.setItem("UserName", userName);
}

function getUserNameStorage() {
    return localStorage.getItem("UserName");
}

function getImagemStorage() {
    return localStorage.getItem("Imagem");
}