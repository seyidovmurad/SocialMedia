


function chooseFile(id) {
    document.getElementById(id).click();
}

function onFileChooseChangeImage(input, imgId) {
    var [file] = input.files
    if (file)
        document.getElementById(imgId).src = URL.createObjectURL(file);
}