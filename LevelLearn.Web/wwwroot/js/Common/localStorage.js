function storageLogin(imagem) {
    localStorage.removeItem("Imagem");
    localStorage.setItem("Imagem", imagem);
}

function getImagemStorage() {
    return localStorage.getItem("Imagem");
}