

function onSearch(e) {
    e = e || window.event;
    if (e.keyCode == 13) {
        document.getElementById('fSearchForm').submit();
        return false;
    }
    return true;
}