$(document).ready(function () {
    $(".btn-modulo-home-default").click(function () {
        window.location.href = `/${localStorage.getItem("paginaDefecto")}`;
    })
})