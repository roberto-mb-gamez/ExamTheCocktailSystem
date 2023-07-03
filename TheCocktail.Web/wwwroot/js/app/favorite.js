$(function () {
    $('.btnRemoveFavorite').on('click', function (event) {
        event.preventDefault();

        $('#btnConfirmRemove').attr('data-favoriteid', $(this).attr('data-favoriteid'));
        $('#modalConfirmRemove').modal('show');
    });

    $('#btnConfirmRemove').on('click', function (event) {
        event.preventDefault();

        var favoriteid = $(this).attr('data-favoriteid');

        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/FavoriteCocktails/AddRemoveFavorite",
            data: {
                FavoriteId: favoriteid
            },
            success: function (result) {
                if (result.success) {
                    $(`.btnRemoveFavorite[data-favoriteid='${favoriteid}']`)
                        .parents('.media-favorite')
                        .remove();

                    $('#totalFavorites').text(result.total == "1" ? `${result.total} cóctel` : `${result.total} cocteles`);
                } else {
                    // TODO: Mensaje de error
                }
            },
            error: function () {
                $('#icon-favorite')
                    .removeClass('bi-heart-fill')
                    .addClass('bi-heart');

                $('#notiFavorites .notiFavoritesTitle')
                    .text('No se pudo agregar a favoritos');
                $('#notiFavorites .goFavorites').hide();
            },
            complete: function () {
                $('#modalConfirmRemove').modal('hide');
            }
        });
    });
});