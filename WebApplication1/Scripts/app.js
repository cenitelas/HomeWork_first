$(document).ready(function () {
    $('#mdb-preloader').removeClass('d-lg-none');
    $.post(
       "/Books/SortGenre",
       {
           id: 1
       },
     onAjaxSuccess
    );
    $('#genre').change(function (e) {

        $.post(
        "/Books/SortGenre",
        {
       id: e.target.value
      },
      onAjaxSuccess
     );
    })


})

function onAjaxSuccess(data) {
    // Здесь мы получаем данные, отправленные сервером и выводим их на экран.
    $('#content').html(data);
    $('#mdb-preloader').addClass('d-lg-none');
}

function editAuthor(id) {
    $('#mdb-preloader').removeClass('d-lg-none');
        $.get(
        "/Author/EditAndCreate",
        {
            id: id
        },
      onAjaxSuccessAuthor
     );
}

function onAjaxSuccessAuthor(data) {
    $('#modal-body').html(data);
    $('#mdb-preloader').addClass('d-lg-none');
}
