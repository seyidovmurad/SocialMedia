
document.getElementById("post").addEventListener("submit", function (e) {
    e.preventDefault();
    const data = new FormData(e.target);
    var entites = { ...Object.fromEntries(data.entries()) }
    var btn = document.getElementById("postBtn")
    btn.disabled = true;
    console.log("post submited");
    axios({
        url: `/Post/CreatePost`,
        method: 'POST',
        data: entites,
        headers: {
            Accept: 'application/json',
            'Content-Type': 'multipart/form-data'
        }
        }).then(res => {
            btn.disabled = false;
            document.getElementById("postContent").value = "";
        }).catch(r => {
            btn.disabled = false;
            document.getElementById("postTitle").innerText = "Something went wrong try again";
        }).finally(() => {
            console.log("reload")
            location.reload();
        })
    location.reload();
})


function chooseFile(isVideo = false) {
    var id = isVideo ? "form-video" : "form-img";
    document.getElementById(id).click();
}